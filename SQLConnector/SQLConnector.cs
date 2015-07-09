using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Data.SqlClient;
using System.Data.Sql;

namespace SQLConnector
{
    [ProvideToolboxControl("SQLConnector", false)]
    public partial class SQLConnector : UserControl
    {

        //////////////////////////////////////
        // Private vars to handle first load.
        // First Load handling is specific to wheather it needs to run the first load.
        // Not wheather or not its been completed.  I dont know maybe change this
        //////////////////////////////////////
        #region StorageVars
        private bool ServerFirstLoad = true;
        private bool DatabaseFirstLoad = true;
        private bool ServerAutoLoadStored = false;
        private string SeverExcludeMatchType = "";
        private string DatabaseExcludeMatchType = "";
        private string ServerStored = "";
        private string DatabaseStored = "";
        private bool UseWAStored = true;
        private string UsernameStored = "";
        private string PasswordStored = "";
        #endregion StorageVars

        //////////////////////////////////////
        // Properties and their Category
        //////////////////////////////////////
        #region Properties
        [
        Category("SQL Connector Settings"),
        Description("Default Server")
        ]
        public string Server
        {
            get { return ServerStored; }
            set { comboBoxServerList.Text = value; ServerStored = value; }
        }
        [
        Category("SQL Connector Settings"),
        Description("Default DB")
        ]
        public string Database
        {
            get { return DatabaseStored; }
            set { comboBoxDatabaseList.Text = value; DatabaseStored = value; }
        }
        [
        Category("SQL Connector Settings"),
        Description("Default for Windows Auth"),
        DefaultValueAttribute(true)
        ]
        public bool WindowsAuthChecked
        {
            get { return UseWAStored; }
            set { checkBoxUseWA.Checked = value; UseWAStored = value; }
        }
        [
        Category("SQL Connector Settings"),
        Description("Default Username")
        ]
        public string UserName
        {
            get { return UsernameStored; }
            set { textBoxUserName.Text = value; UsernameStored = value; }
        }
        [
        Category("SQL Connector Settings"),
        Description("Default Password")
        ]
        public string Password
        {
            get { return PasswordStored; }
            set { textBoxPassword.Text = value; PasswordStored = value; }
        }
        [
        Category("SQL Connector Settings"),
        Description("ConnectionString")
        ]
        public string ConnectionString
        {
            get { return GetConnectionString(Server, Database, WindowsAuthChecked, UserName, Password); }
        }
        [
        Category("SQL Connector Settings"),
        Description("Load Servers at Launch"),
        DefaultValueAttribute(false)
        ]
        public bool ServerAutoLoad
        {
            get { return ServerAutoLoadStored;  }
            set { if (value) { ServerFirstLoad = false; DatabaseFirstLoad = false; ServerAutoLoadStored = value; } }
        }
        [
        Category("SQL Connector Settings"),
        Description("Exclude Servers from populating")
        ]
        public string[] ServersToExclude
        {
            get;
            set;
        }
        [
        Category("SQL Connector Settings"),
        Description("Exclude DBs from populating")
        ]
        public string[] DatabasesToExclude
        {
            get;
            set;
        }
        [
        Category("SQL Connector Settings"),
        Description("How to Match Excluded Servers"),
        TypeConverter(typeof(StringListConverter))
        ]
        public string ServerMatchType
        {
            get
            {
                string DV = "";
                if (this.SeverExcludeMatchType != null)
                {
                    DV = this.SeverExcludeMatchType;
                }
                else
                {
                    DV = "Exact Match Case Sensative";
                }
                return DV;
            }
            set { SeverExcludeMatchType = value; }
        }
        [
        Category("SQL Connector Settings"),
        Description("How to Match Excluded Databases"),
        TypeConverter(typeof(StringListConverter)),
        ]
        public string DatabaseMatchType
        {
            get
            {
                string DV = "";
                if (this.DatabaseExcludeMatchType != null)
                {
                    DV = this.DatabaseExcludeMatchType;
                }
                else
                {
                    DV = "Exact Match Case Sensative";
                }
                return DV;
            }
            set { DatabaseExcludeMatchType = value; }
        }
        #endregion Properties

        //////////////////////////////////////
        // AutoLoad stuff
        // Note: this is for loading using in VS, like when you first put it in there
        //////// Yeah I never knew this
        //////////////////////////////////////
        public SQLConnector()
        {
            InitializeComponent();
        }

        //////////////////////////////////////
        // Got the below off the internet
        // Im strill trying to fully understand this part.
        // http://bytes.com/topic/c-sharp/answers/577480-propertygrid-how-display-string-array-dropdown-property
        //////////////////////////////////////
        public class StringListConverter : TypeConverter
        {
            //////////////////////////////////////
            // Display Drop Down
            //////////////////////////////////////
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            //////////////////////////////////////
            // DD vs Combo
            //////////////////////////////////////
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
            //////////////////////////////////////
            // Here is the list of values to return
            //////////////////////////////////////
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[] { "Exact Match Case Sensative", "Exact Match Case Insensative", "Contains Case Sensative",
"Contains Case Insensative" });
            }
        }

        //////////////////////////////////////
        // Populate SQL Server List
        //////////////////////////////////////
        private class SQLServerList
        {
            //////////////////////////////////////
            // Simple Cobbler of the Server Setup
            //////////////////////////////////////
            private string ConnCobler(string Server, string Instance = null)
            {
                string Output = Server;
                if (!string.IsNullOrWhiteSpace(Instance))
                {
                    Output += @"\" + Instance;
                }
                return Output;
            }

            //////////////////////////////////////
            // Get the SQL ServerList
            //////////////////////////////////////
            public List<string> ServerList(string ExcludeMethod, string[] ExcludedServers = null)
            {
                List<string> Output = new List<string>();
                DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
                bool AddIt = true;
                foreach (DataRow server in dt.Rows)
                {
                    string si = ConnCobler(server[dt.Columns["ServerName"]].ToString(), server[dt.Columns["InstanceName"]].ToString());
                    if (ExcludedServers != null)
                    {
                        if (ExcludeMethod == "Exact Match Case Sensative")
                        {
                            foreach (string ExServ in ExcludedServers)
                            {
                                if (si == ExServ)
                                {
                                    AddIt = false;
                                }
                            }
                            if (AddIt)
                            {
                                Output.Add(si);
                            }
                        }
                        else if (ExcludeMethod == "Exact Match Case Insensative")
                        {
                            foreach (string ExServ in ExcludedServers)
                            {
                                if (si.ToLowerInvariant() == ExServ.ToLowerInvariant())
                                {
                                    AddIt = false;
                                }
                            }
                            if (AddIt)
                            {
                                Output.Add(si);
                            }
                        }
                        else if (ExcludeMethod == "Contains Case Insensative")
                        {
                            ExcludedServers = ExcludedServers.Select(s => s.ToLowerInvariant()).ToArray();
                            if (!ExcludedServers.Contains(si))
                            {
                                Output.Add(si);
                            }
                        }
                        else if (ExcludeMethod == "Contains Case Sensative")
                        {
                            if (!ExcludedServers.Contains(si))
                            {
                                Output.Add(si);
                            }
                        }
                    }
                    else
                    {
                        Output.Add(si);
                    }
                }
                return Output;
            }
        }

        //////////////////////////////////////
        // Used to setup an event
        //////////////////////////////////////
        public List<string> ServerList(string ServerMatchType, string[] ExcludedServers = null)
        {
            List<string> Output = new SQLServerList().ServerList(ServerMatchType, ExcludedServers);
            Output.Sort();
            OnSVRefresh(EventArgs.Empty);
            return Output;
        }

        //////////////////////////////////////
        // Populate SQL Database List
        // Some assumptions are made regarding login stuff
        // this can cause issues sometimes, their might be a better way
        // YOU FIGURE IT OUT
        // This is not pretty but whatever this pig has lipstick
        //////////////////////////////////////
        private class SQLDatabaseList
        {
            public List<string> DatabaseList(string Server, bool IntergratedSecurity, string ExcludeMethod, string Username = null, string Password = null, string[] ExcludedDBs = null)
            {
                List<string> Output = new List<string>();
                SqlConnectionStringBuilder ConnBld = new SqlConnectionStringBuilder();
                DataTable dt = new DataTable();
                ConnBld.DataSource = Server;

                if (IntergratedSecurity)
                {
                    ConnBld.IntegratedSecurity = true;
                }
                else
                {
                    ConnBld.UserID = Username;
                    ConnBld.Password = Password;
                }

                using (SqlConnection Conn = new SqlConnection(ConnBld.ToString()))
                {
                    try
                    {
                        Conn.Open();
                        dt = Conn.GetSchema("Databases");
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception(string.Format("Cannot Connect to Server: {0}\r\n{1}", Server, Ex.Message));
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }

                foreach (DataRow database in dt.Rows)
                {
                    bool AddIt = true;
                    string dbname = database["database_name"].ToString();
                    if (ExcludedDBs != null)
                    {
                        if (ExcludeMethod == "Exact Match Case Sensative")
                        {
                            foreach (string ExDB in ExcludedDBs)
                            {
                                if (dbname == ExDB)
                                {
                                    AddIt = false;
                                }
                            }
                            if (AddIt)
                            {
                                Output.Add(dbname);
                            }
                        }
                        else if (ExcludeMethod == "Exact Match Case Insensative")
                        {
                            foreach (string ExDB in ExcludedDBs)
                            {
                                if (dbname.ToLowerInvariant() == ExDB.ToLowerInvariant())
                                {
                                    AddIt = false;
                                }
                            }
                            if (AddIt)
                            {
                                Output.Add(dbname);
                            }
                        }
                        else if (ExcludeMethod == "Contains Case Insensative")
                        {
                            ExcludedDBs = ExcludedDBs.Select(s => s.ToLowerInvariant()).ToArray();
                            if (!ExcludedDBs.Contains(dbname))
                            {
                                Output.Add(dbname);
                            }
                        }
                        else if (ExcludeMethod == "Contains Case Sensative")
                        {
                            if (!ExcludedDBs.Contains(dbname))
                            {
                                Output.Add(dbname);
                            }
                        }
                    }
                    else
                    {
                        Output.Add(dbname);
                    }
                }
                return Output;
            }
        }

        //////////////////////////////////////
        // Used to setup an event
        //////////////////////////////////////
        public List<string> DatabaseList(string Server, bool IntergratedSecurity, string DatabaseMatchType, string Username = null, string Password = null, string[] ExcludedDBs = null)
        {
            List<string> Output = new List<string>();
            if (!IntergratedSecurity && (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)))
            {
                Output = new SQLDatabaseList().DatabaseList(Server, true, DatabaseMatchType, null, null, ExcludedDBs);
            }
            else
            {
                Output = new SQLDatabaseList().DatabaseList(Server, IntergratedSecurity, DatabaseMatchType, Username, Password, ExcludedDBs);
            }
            Output.Sort();
            OnDBRefresh(EventArgs.Empty);
            return Output;
        }

        //////////////////////////////////////
        // Check box toggle
        //////////////////////////////////////
        private void checkBoxUseWA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseWA.Checked)
            {
                groupBoxWA.Enabled = false;
            }
            else
            {
                groupBoxWA.Enabled = true;
            }
            UseWAStored = checkBoxUseWA.Checked;
            OnWAChanged(EventArgs.Empty);
        }

        //////////////////////////////////////
        // On Enter if its the first load load it up
        // else DO IT MANUALLY LAZY BONES
        //////////////////////////////////////
        private void comboBoxServerList_Enter(object sender, EventArgs e)
        {
            if (ServerFirstLoad)
            {
                Cursor.Current = Cursors.WaitCursor;
                comboBoxServerList.DataSource = ServerList(ServerMatchType, ServersToExclude);
                ServerFirstLoad = false;
                Cursor.Current = Cursors.Default;
            }
        }

        //////////////////////////////////////
        // Same as above, only will error if it cant get into the server
        //////////////////////////////////////
        private void comboBoxDatabaseList_Enter(object sender, EventArgs e)
        {
            if (DatabaseFirstLoad)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if(string.IsNullOrWhiteSpace(ServerStored))
                    {
                        ServerStored = Environment.MachineName;
                        comboBoxServerList.Text = Environment.MachineName;
                    }
                    comboBoxDatabaseList.DataSource = DatabaseList(Server, WindowsAuthChecked, DatabaseExcludeMatchType, UserName, Password, DatabasesToExclude);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                DatabaseFirstLoad = false;
                Cursor.Current = Cursors.Default;
            }
        }

        //////////////////////////////////////
        // RELOAD!
        //////////////////////////////////////
        private void buttonServerRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ServerStored = "";
            comboBoxServerList.Text = "";
            comboBoxServerList.DataSource = ServerList(ServerMatchType, ServersToExclude);
            ServerFirstLoad = false;
            Cursor.Current = Cursors.Default;
            OnSVRClick(EventArgs.Empty);
        }

        private void buttonDatabaseRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DatabaseStored = "";
            comboBoxDatabaseList.Text = "";
            try
            {
                if (string.IsNullOrWhiteSpace(ServerStored))
                {
                    ServerStored = Environment.MachineName;
                    comboBoxServerList.Text = Environment.MachineName;
                }
                comboBoxDatabaseList.DataSource = DatabaseList(Server, WindowsAuthChecked, DatabaseExcludeMatchType, UserName, Password, DatabasesToExclude);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DatabaseFirstLoad = false;
            Cursor.Current = Cursors.Default;
            OnDBRClick(EventArgs.Empty);
        }

        //////////////////////////////////////
        // Clear the Database List when changing servers
        //////////////////////////////////////
        private void comboBoxServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseFirstLoad = true;
            DatabaseStored = "";
            comboBoxDatabaseList.Text = "";
        }

        //////////////////////////////////////
        // Incase you want to call it as well
        // SECRET REASONS
        //////////////////////////////////////
        public bool TestConnection()
        {
            bool b = true;
            try
            {
                using (SqlConnection Conn = new SqlConnection(GetConnectionString(Server, Database, WindowsAuthChecked, UserName, Password)))
                using (SqlCommand Cmd = new SqlCommand("SELECT 1", Conn))
                {
                    try
                    {
                        Conn.Open();
                        try
                        {
                            Cmd.ExecuteScalar();
                        }
                        catch
                        {
                            b = false;
                        }
                    }
                    catch
                    {
                        b = false; ;
                    }
                }
            }
            catch
            {
                b = false;
            }
            return b;
        }

        //////////////////////////////////////
        // Test Connection
        //////////////////////////////////////
        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                using (SqlConnection Conn = new SqlConnection(GetConnectionString(Server, Database, WindowsAuthChecked, UserName, Password)))
                using (SqlCommand Cmd = new SqlCommand("SELECT 1", Conn))
                {
                    try
                    {
                        Conn.Open();
                        try
                        {
                            Cmd.ExecuteScalar();
                        }
                        catch (Exception Ex)
                        {
                            throw new Exception(string.Format("SQL Execution Failed on DB: {0}\r\n{1}", Database, Ex.Message));
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception(string.Format("SQL Connection Failed on Server: {0}\r\n{1}", Server, Ex.Message));
                    }
                }
                MessageBox.Show("Connection Successful.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
            OnTCClick(EventArgs.Empty);
        }

        //////////////////////////////////////
        // GET DAT STRING JERKBUTT
        //////////////////////////////////////
        public string GetConnectionString(string Server, string Database, bool IntergratedSecurity, string Username = null, string Password = null)
        {
            SqlConnectionStringBuilder ConnBld = new SqlConnectionStringBuilder();
            ConnBld.DataSource = Server;
            ConnBld.InitialCatalog = Database;
            if (IntergratedSecurity)
            {
                ConnBld.IntegratedSecurity = true;
            }
            else
            {
                ConnBld.IntegratedSecurity = false;
                ConnBld.UserID = Username;
                ConnBld.Password = Password;
            }
            OnCSChanged(EventArgs.Empty);
            return ConnBld.ToString();
        }

        //////////////////////////////////////
        // Populate SQL Server List at first sight
        //////////////////////////////////////
        private void SQLConnector_Load(object sender, EventArgs e)
        {
            if (ServerAutoLoad)
            {
                Cursor.Current = Cursors.WaitCursor;
                comboBoxServerList.DataSource = ServerList(ServerMatchType, ServersToExclude);
                ServerFirstLoad = false;
                Cursor.Current = Cursors.Default;
            }
            else
            {
                ServerFirstLoad = false;
                DatabaseFirstLoad = false;
            }
        }

        //////////////////////////////////////
        // Events and their Category
        // There has to be a better way to set
        // a bunch to the same category at once
        //////////////////////////////////////
        #region Events
        [
        Category("SQL Connector"),
        Description("Server List Changing")
        ]
        public event EventHandler ServerListChange;
        protected virtual void OnSVRefresh(EventArgs e)
        {
            if (ServerListChange != null)
            {
                ServerListChange(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Database List Changing")
        ]
        public event EventHandler DatabaseListChange;
        protected virtual void OnDBRefresh(EventArgs e)
        {
            if (DatabaseListChange != null)
            {
                DatabaseListChange(this, e);
            }
        }

        [
        Category("SQL Connector"),
        Description("Server Refresh Clicked")
        ]
        public event EventHandler ServerRefreshClick;
        protected virtual void OnSVRClick(EventArgs e)
        {
            if (ServerRefreshClick != null)
            {
                ServerRefreshClick(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Database Refresh Clicked")
        ]
        public event EventHandler DatabaseRefreshClick;
        protected virtual void OnDBRClick(EventArgs e)
        {
            if (DatabaseRefreshClick != null)
            {
                DatabaseRefreshClick(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Test Connection Clicked")
        ]
        public event EventHandler TestConnectionClick;
        protected virtual void OnTCClick(EventArgs e)
        {
            if (TestConnectionClick != null)
            {
                TestConnectionClick(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Windows Auth Checkbox Changed")
        ]
        public event EventHandler WinAuthChanged;
        protected virtual void OnWAChanged(EventArgs e)
        {
            if (WinAuthChanged != null)
            {
                WinAuthChanged(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Username Changed")
        ]
        public event EventHandler UserNameChanged;
        protected virtual void OnUNChanged(EventArgs e)
        {
            if (UserNameChanged != null)
            {
                UserNameChanged(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Password Changed")
        ]
        public event EventHandler PasswordChanged;
        protected virtual void OnPSChanged(EventArgs e)
        {
            if (PasswordChanged != null)
            {
                PasswordChanged(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Server Text Changed")
        ]
        public event EventHandler ServerChanged;
        protected virtual void OnSVChanged(EventArgs e)
        {
            if (ServerChanged != null)
            {
                ServerChanged(this, e);
            }
        }
        [
        Category("SQL Connector"),
        Description("Datbase Text Changed")
        ]
        public event EventHandler DatabaseChanged;
        protected virtual void OnDBChanged(EventArgs e)
        {
            if (DatabaseChanged != null)
            {
                DatabaseChanged(this, e);
            }
        }

        [
        Category("SQL Connector"),
        Description("Connection String Changed")
        ]
        public event EventHandler ConnectionStringChanged;
        protected virtual void OnCSChanged(EventArgs e)
        {
            if (ConnectionStringChanged != null)
            {
                ConnectionStringChanged(this, e);
            }
        }

        #endregion Events

        //////////////////////////////////////
        // Added for Events
        //////////////////////////////////////
        #region ItemsForEventsOnly
        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            UsernameStored = textBoxUserName.Text;
            OnUNChanged(EventArgs.Empty);
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            PasswordStored = textBoxPassword.Text;
            OnPSChanged(EventArgs.Empty);
        }

        private void comboBoxServerList_TextChanged(object sender, EventArgs e)
        {
            ServerStored = comboBoxServerList.Text;
            OnSVChanged(EventArgs.Empty);
        }

        private void comboBoxDatabaseList_TextChanged(object sender, EventArgs e)
        {
            DatabaseStored = comboBoxDatabaseList.Text;
            OnDBChanged(EventArgs.Empty);
        }

        #endregion ItemsForEventsOnly


    }
}
