namespace SQLConnector
{
    partial class SQLConnector
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxServerList = new System.Windows.Forms.ComboBox();
            this.comboBoxDatabaseList = new System.Windows.Forms.ComboBox();
            this.checkBoxUseWA = new System.Windows.Forms.CheckBox();
            this.groupBoxWA = new System.Windows.Forms.GroupBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.buttonServerRefresh = new System.Windows.Forms.Button();
            this.buttonDatabaseRefresh = new System.Windows.Forms.Button();
            this.groupBoxWA.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxServerList
            // 
            this.comboBoxServerList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxServerList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxServerList.FormattingEnabled = true;
            this.comboBoxServerList.Location = new System.Drawing.Point(96, 4);
            this.comboBoxServerList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxServerList.Name = "comboBoxServerList";
            this.comboBoxServerList.Size = new System.Drawing.Size(217, 24);
            this.comboBoxServerList.TabIndex = 1;
            this.comboBoxServerList.SelectedIndexChanged += new System.EventHandler(this.comboBoxServerList_SelectedIndexChanged);
            this.comboBoxServerList.TextChanged += new System.EventHandler(this.comboBoxServerList_TextChanged);
            this.comboBoxServerList.Enter += new System.EventHandler(this.comboBoxServerList_Enter);
            // 
            // comboBoxDatabaseList
            // 
            this.comboBoxDatabaseList.FormattingEnabled = true;
            this.comboBoxDatabaseList.Location = new System.Drawing.Point(96, 37);
            this.comboBoxDatabaseList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxDatabaseList.Name = "comboBoxDatabaseList";
            this.comboBoxDatabaseList.Size = new System.Drawing.Size(217, 24);
            this.comboBoxDatabaseList.TabIndex = 4;
            this.comboBoxDatabaseList.TextChanged += new System.EventHandler(this.comboBoxDatabaseList_TextChanged);
            this.comboBoxDatabaseList.Enter += new System.EventHandler(this.comboBoxDatabaseList_Enter);
            // 
            // checkBoxUseWA
            // 
            this.checkBoxUseWA.AutoSize = true;
            this.checkBoxUseWA.Checked = true;
            this.checkBoxUseWA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseWA.Location = new System.Drawing.Point(8, 75);
            this.checkBoxUseWA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxUseWA.Name = "checkBoxUseWA";
            this.checkBoxUseWA.Size = new System.Drawing.Size(196, 20);
            this.checkBoxUseWA.TabIndex = 7;
            this.checkBoxUseWA.Text = "Use Windows Authentication";
            this.checkBoxUseWA.UseVisualStyleBackColor = true;
            this.checkBoxUseWA.CheckedChanged += new System.EventHandler(this.checkBoxUseWA_CheckedChanged);
            // 
            // groupBoxWA
            // 
            this.groupBoxWA.Controls.Add(this.labelPassword);
            this.groupBoxWA.Controls.Add(this.labelUserName);
            this.groupBoxWA.Controls.Add(this.textBoxUserName);
            this.groupBoxWA.Controls.Add(this.textBoxPassword);
            this.groupBoxWA.Enabled = false;
            this.groupBoxWA.Location = new System.Drawing.Point(8, 103);
            this.groupBoxWA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxWA.Name = "groupBoxWA";
            this.groupBoxWA.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxWA.Size = new System.Drawing.Size(400, 91);
            this.groupBoxWA.TabIndex = 8;
            this.groupBoxWA.TabStop = false;
            this.groupBoxWA.Text = "SQL Server Authentication";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(8, 59);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(71, 16);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password:";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(8, 27);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(80, 16);
            this.labelUserName.TabIndex = 0;
            this.labelUserName.Text = "User Name:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(113, 23);
            this.textBoxUserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(277, 22);
            this.textBoxUserName.TabIndex = 1;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(113, 55);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(277, 22);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(4, 7);
            this.labelServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(51, 16);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server:";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(4, 41);
            this.labelDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(71, 16);
            this.labelDatabase.TabIndex = 3;
            this.labelDatabase.Text = "Database:";
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Location = new System.Drawing.Point(261, 70);
            this.buttonTestConnection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(147, 28);
            this.buttonTestConnection.TabIndex = 6;
            this.buttonTestConnection.Text = "Test Connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // buttonServerRefresh
            // 
            this.buttonServerRefresh.Location = new System.Drawing.Point(323, 1);
            this.buttonServerRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonServerRefresh.Name = "buttonServerRefresh";
            this.buttonServerRefresh.Size = new System.Drawing.Size(85, 28);
            this.buttonServerRefresh.TabIndex = 2;
            this.buttonServerRefresh.Text = "Refresh";
            this.buttonServerRefresh.UseVisualStyleBackColor = true;
            this.buttonServerRefresh.Click += new System.EventHandler(this.buttonServerRefresh_Click);
            // 
            // buttonDatabaseRefresh
            // 
            this.buttonDatabaseRefresh.Location = new System.Drawing.Point(323, 34);
            this.buttonDatabaseRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDatabaseRefresh.Name = "buttonDatabaseRefresh";
            this.buttonDatabaseRefresh.Size = new System.Drawing.Size(85, 28);
            this.buttonDatabaseRefresh.TabIndex = 5;
            this.buttonDatabaseRefresh.Text = "Refresh";
            this.buttonDatabaseRefresh.UseVisualStyleBackColor = true;
            this.buttonDatabaseRefresh.Click += new System.EventHandler(this.buttonDatabaseRefresh_Click);
            // 
            // SQLConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDatabaseRefresh);
            this.Controls.Add(this.buttonServerRefresh);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.groupBoxWA);
            this.Controls.Add(this.checkBoxUseWA);
            this.Controls.Add(this.comboBoxDatabaseList);
            this.Controls.Add(this.comboBoxServerList);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(416, 203);
            this.MinimumSize = new System.Drawing.Size(416, 203);
            this.Name = "SQLConnector";
            this.Size = new System.Drawing.Size(416, 203);
            this.Load += new System.EventHandler(this.SQLConnector_Load);
            this.groupBoxWA.ResumeLayout(false);
            this.groupBoxWA.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxServerList;
        private System.Windows.Forms.ComboBox comboBoxDatabaseList;
        private System.Windows.Forms.CheckBox checkBoxUseWA;
        private System.Windows.Forms.GroupBox groupBoxWA;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Button buttonServerRefresh;
        private System.Windows.Forms.Button buttonDatabaseRefresh;

    }
}
