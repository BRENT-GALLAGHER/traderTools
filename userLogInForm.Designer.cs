namespace traderTools
{
   partial class userLogInForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.userIDTextBox = new System.Windows.Forms.TextBox();
            this.userPasswordTextBox = new System.Windows.Forms.TextBox();
            this.logInButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userServertextBox = new System.Windows.Forms.TextBox();
            this.labelUserServer = new System.Windows.Forms.Label();
            this.userDatabaselabel = new System.Windows.Forms.Label();
            this.userDatabasetextBox = new System.Windows.Forms.TextBox();
            this.WindowsAuthenticationcheckBox = new System.Windows.Forms.CheckBox();
            this.userSaveSettingscheckBox = new System.Windows.Forms.CheckBox();
            this.savedSettingslabel = new System.Windows.Forms.Label();
            this.SavedSettingslistBox = new System.Windows.Forms.ListBox();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.databaseSoftwarelistBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.savedNametextBox = new System.Windows.Forms.TextBox();
            this.savedNamelabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 170);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "User Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // userIDTextBox
            // 
            this.userIDTextBox.Location = new System.Drawing.Point(193, 165);
            this.userIDTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userIDTextBox.Name = "userIDTextBox";
            this.userIDTextBox.Size = new System.Drawing.Size(281, 26);
            this.userIDTextBox.TabIndex = 2;
            // 
            // userPasswordTextBox
            // 
            this.userPasswordTextBox.Location = new System.Drawing.Point(193, 207);
            this.userPasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userPasswordTextBox.Name = "userPasswordTextBox";
            this.userPasswordTextBox.PasswordChar = '*';
            this.userPasswordTextBox.Size = new System.Drawing.Size(281, 26);
            this.userPasswordTextBox.TabIndex = 3;
            this.userPasswordTextBox.TextChanged += new System.EventHandler(this.userPasswordTextBox_TextChanged);
            this.userPasswordTextBox.GotFocus += new System.EventHandler(this.userPasswordTextBox_GotFocus);
            this.userPasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userPasswordTextBox_KeyDown);
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(193, 384);
            this.logInButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(112, 35);
            this.logInButton.TabIndex = 4;
            this.logInButton.Text = "Log In";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.logInButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(585, 35);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(55, 29);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // userServertextBox
            // 
            this.userServertextBox.Location = new System.Drawing.Point(193, 244);
            this.userServertextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userServertextBox.Name = "userServertextBox";
            this.userServertextBox.PasswordChar = '*';
            this.userServertextBox.Size = new System.Drawing.Size(281, 26);
            this.userServertextBox.TabIndex = 6;
            // 
            // labelUserServer
            // 
            this.labelUserServer.AutoSize = true;
            this.labelUserServer.Location = new System.Drawing.Point(87, 250);
            this.labelUserServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserServer.Name = "labelUserServer";
            this.labelUserServer.Size = new System.Drawing.Size(96, 20);
            this.labelUserServer.TabIndex = 7;
            this.labelUserServer.Text = "Server or IP:";
            this.labelUserServer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // userDatabaselabel
            // 
            this.userDatabaselabel.AutoSize = true;
            this.userDatabaselabel.Location = new System.Drawing.Point(100, 286);
            this.userDatabaselabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userDatabaselabel.Name = "userDatabaselabel";
            this.userDatabaselabel.Size = new System.Drawing.Size(83, 20);
            this.userDatabaselabel.TabIndex = 9;
            this.userDatabaselabel.Text = "Database:";
            this.userDatabaselabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // userDatabasetextBox
            // 
            this.userDatabasetextBox.Location = new System.Drawing.Point(193, 280);
            this.userDatabasetextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userDatabasetextBox.Name = "userDatabasetextBox";
            this.userDatabasetextBox.PasswordChar = '*';
            this.userDatabasetextBox.Size = new System.Drawing.Size(281, 26);
            this.userDatabasetextBox.TabIndex = 8;
            // 
            // WindowsAuthenticationcheckBox
            // 
            this.WindowsAuthenticationcheckBox.AutoSize = true;
            this.WindowsAuthenticationcheckBox.Location = new System.Drawing.Point(193, 349);
            this.WindowsAuthenticationcheckBox.Name = "WindowsAuthenticationcheckBox";
            this.WindowsAuthenticationcheckBox.Size = new System.Drawing.Size(239, 24);
            this.WindowsAuthenticationcheckBox.TabIndex = 10;
            this.WindowsAuthenticationcheckBox.Text = "Use Windows Authentication";
            this.WindowsAuthenticationcheckBox.UseVisualStyleBackColor = true;
            // 
            // userSaveSettingscheckBox
            // 
            this.userSaveSettingscheckBox.AutoSize = true;
            this.userSaveSettingscheckBox.Location = new System.Drawing.Point(193, 319);
            this.userSaveSettingscheckBox.Name = "userSaveSettingscheckBox";
            this.userSaveSettingscheckBox.Size = new System.Drawing.Size(134, 24);
            this.userSaveSettingscheckBox.TabIndex = 11;
            this.userSaveSettingscheckBox.Text = "Save Settings";
            this.userSaveSettingscheckBox.UseVisualStyleBackColor = true;
            // 
            // savedSettingslabel
            // 
            this.savedSettingslabel.AutoSize = true;
            this.savedSettingslabel.Location = new System.Drawing.Point(62, 67);
            this.savedSettingslabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.savedSettingslabel.Name = "savedSettingslabel";
            this.savedSettingslabel.Size = new System.Drawing.Size(121, 20);
            this.savedSettingslabel.TabIndex = 12;
            this.savedSettingslabel.Text = "Saved Settings:";
            this.savedSettingslabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SavedSettingslistBox
            // 
            this.SavedSettingslistBox.AllowDrop = true;
            this.SavedSettingslistBox.FormattingEnabled = true;
            this.SavedSettingslistBox.ItemHeight = 20;
            this.SavedSettingslistBox.Items.AddRange(new object[] {
            "Hello",
            "Bye"});
            this.SavedSettingslistBox.Location = new System.Drawing.Point(193, 66);
            this.SavedSettingslistBox.Name = "SavedSettingslistBox";
            this.SavedSettingslistBox.Size = new System.Drawing.Size(281, 24);
            this.SavedSettingslistBox.TabIndex = 13;
            this.SavedSettingslistBox.Click += new System.EventHandler(this.SavedSettingslistBox_Click);
            this.SavedSettingslistBox.SelectedValueChanged += new System.EventHandler(this.SavedSettingslistBox_SelectedValueChanged_1);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(313, 384);
            this.cancelbutton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(112, 35);
            this.cancelbutton.TabIndex = 14;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // databaseSoftwarelistBox
            // 
            this.databaseSoftwarelistBox.FormattingEnabled = true;
            this.databaseSoftwarelistBox.ItemHeight = 20;
            this.databaseSoftwarelistBox.Items.AddRange(new object[] {
            "mySQL",
            "SQL Server"});
            this.databaseSoftwarelistBox.Location = new System.Drawing.Point(193, 96);
            this.databaseSoftwarelistBox.Name = "databaseSoftwarelistBox";
            this.databaseSoftwarelistBox.Size = new System.Drawing.Size(281, 24);
            this.databaseSoftwarelistBox.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Database Software:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // savedNametextBox
            // 
            this.savedNametextBox.Location = new System.Drawing.Point(193, 128);
            this.savedNametextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.savedNametextBox.Name = "savedNametextBox";
            this.savedNametextBox.Size = new System.Drawing.Size(281, 26);
            this.savedNametextBox.TabIndex = 18;
            // 
            // savedNamelabel
            // 
            this.savedNamelabel.AutoSize = true;
            this.savedNamelabel.Location = new System.Drawing.Point(81, 134);
            this.savedNamelabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.savedNamelabel.Name = "savedNamelabel";
            this.savedNamelabel.Size = new System.Drawing.Size(104, 20);
            this.savedNamelabel.TabIndex = 17;
            this.savedNamelabel.Text = "Saved Name:";
            this.savedNamelabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // userLogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 449);
            this.Controls.Add(this.savedNametextBox);
            this.Controls.Add(this.savedNamelabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.databaseSoftwarelistBox);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.SavedSettingslistBox);
            this.Controls.Add(this.savedSettingslabel);
            this.Controls.Add(this.userSaveSettingscheckBox);
            this.Controls.Add(this.WindowsAuthenticationcheckBox);
            this.Controls.Add(this.userDatabaselabel);
            this.Controls.Add(this.userDatabasetextBox);
            this.Controls.Add(this.labelUserServer);
            this.Controls.Add(this.userServertextBox);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.userPasswordTextBox);
            this.Controls.Add(this.userIDTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "userLogInForm";
            this.Text = "User LogIn";
            this.Deactivate += new System.EventHandler(this.userLogInForm_Deactivate);
            this.Load += new System.EventHandler(this.userLogInForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox userIDTextBox;
      private System.Windows.Forms.TextBox userPasswordTextBox;
      private System.Windows.Forms.Button logInButton;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox userServertextBox;
        private System.Windows.Forms.Label labelUserServer;
        private System.Windows.Forms.Label userDatabaselabel;
        private System.Windows.Forms.TextBox userDatabasetextBox;
        private System.Windows.Forms.CheckBox WindowsAuthenticationcheckBox;
        private System.Windows.Forms.CheckBox userSaveSettingscheckBox;
        private System.Windows.Forms.Label savedSettingslabel;
        private System.Windows.Forms.ListBox SavedSettingslistBox;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.ListBox databaseSoftwarelistBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox savedNametextBox;
        private System.Windows.Forms.Label savedNamelabel;
    }
}