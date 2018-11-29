namespace FI_Analytics
{
   partial class userAdministration
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
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.userProfileGroupBox = new System.Windows.Forms.GroupBox();
         this.deleteUserButton = new System.Windows.Forms.Button();
         this.updateUserButton = new System.Windows.Forms.Button();
         this.userRoleLabel = new System.Windows.Forms.Label();
         this.userIdLabel = new System.Windows.Forms.Label();
         this.userRoleComboBox = new System.Windows.Forms.ComboBox();
         this.userIDTextBox = new System.Windows.Forms.TextBox();
         this.userListLabel = new System.Windows.Forms.Label();
         this.userListComboBox = new System.Windows.Forms.ComboBox();
         this.clientAssignmentGroupBox = new System.Windows.Forms.GroupBox();
         this.allRadioButton = new System.Windows.Forms.RadioButton();
         this.notesRadioButton = new System.Windows.Forms.RadioButton();
         this.AnalystRadioButton = new System.Windows.Forms.RadioButton();
         this.clientAssignmentButton = new System.Windows.Forms.Button();
         this.clientAssignmentCheckedListBox = new System.Windows.Forms.CheckedListBox();
         this.currentGroupBox = new System.Windows.Forms.GroupBox();
         this.SetGroupBox = new System.Windows.Forms.GroupBox();
         this.setAnalystCheckBox = new System.Windows.Forms.CheckBox();
         this.setNotesCheckBox = new System.Windows.Forms.CheckBox();
         this.menuStrip1.SuspendLayout();
         this.userProfileGroupBox.SuspendLayout();
         this.clientAssignmentGroupBox.SuspendLayout();
         this.currentGroupBox.SuspendLayout();
         this.SetGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(575, 24);
         this.menuStrip1.TabIndex = 0;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // userProfileGroupBox
         // 
         this.userProfileGroupBox.Controls.Add(this.deleteUserButton);
         this.userProfileGroupBox.Controls.Add(this.updateUserButton);
         this.userProfileGroupBox.Controls.Add(this.userRoleLabel);
         this.userProfileGroupBox.Controls.Add(this.userIdLabel);
         this.userProfileGroupBox.Controls.Add(this.userRoleComboBox);
         this.userProfileGroupBox.Controls.Add(this.userIDTextBox);
         this.userProfileGroupBox.Controls.Add(this.userListLabel);
         this.userProfileGroupBox.Controls.Add(this.userListComboBox);
         this.userProfileGroupBox.Location = new System.Drawing.Point(15, 44);
         this.userProfileGroupBox.Name = "userProfileGroupBox";
         this.userProfileGroupBox.Size = new System.Drawing.Size(272, 416);
         this.userProfileGroupBox.TabIndex = 1;
         this.userProfileGroupBox.TabStop = false;
         this.userProfileGroupBox.Text = "Profile";
         this.userProfileGroupBox.Enter += new System.EventHandler(this.userProfileGroupBox_Enter);
         // 
         // deleteUserButton
         // 
         this.deleteUserButton.Location = new System.Drawing.Point(49, 381);
         this.deleteUserButton.Name = "deleteUserButton";
         this.deleteUserButton.Size = new System.Drawing.Size(75, 23);
         this.deleteUserButton.TabIndex = 7;
         this.deleteUserButton.Text = "Delete User";
         this.deleteUserButton.UseVisualStyleBackColor = true;
         this.deleteUserButton.Click += new System.EventHandler(this.deleteUserButton_Click);
         // 
         // updateUserButton
         // 
         this.updateUserButton.Location = new System.Drawing.Point(130, 381);
         this.updateUserButton.Name = "updateUserButton";
         this.updateUserButton.Size = new System.Drawing.Size(75, 23);
         this.updateUserButton.TabIndex = 6;
         this.updateUserButton.Text = "Update User";
         this.updateUserButton.UseVisualStyleBackColor = true;
         this.updateUserButton.Click += new System.EventHandler(this.updateUserButton_Click);
         // 
         // userRoleLabel
         // 
         this.userRoleLabel.AutoSize = true;
         this.userRoleLabel.Location = new System.Drawing.Point(10, 75);
         this.userRoleLabel.Name = "userRoleLabel";
         this.userRoleLabel.Size = new System.Drawing.Size(57, 13);
         this.userRoleLabel.TabIndex = 5;
         this.userRoleLabel.Text = "User Role:";
         this.userRoleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userIdLabel
         // 
         this.userIdLabel.AutoSize = true;
         this.userIdLabel.Location = new System.Drawing.Point(21, 46);
         this.userIdLabel.Name = "userIdLabel";
         this.userIdLabel.Size = new System.Drawing.Size(46, 13);
         this.userIdLabel.TabIndex = 4;
         this.userIdLabel.Text = "User ID:";
         this.userIdLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userRoleComboBox
         // 
         this.userRoleComboBox.FormattingEnabled = true;
         this.userRoleComboBox.Location = new System.Drawing.Point(73, 72);
         this.userRoleComboBox.Name = "userRoleComboBox";
         this.userRoleComboBox.Size = new System.Drawing.Size(158, 21);
         this.userRoleComboBox.TabIndex = 3;
         // 
         // userIDTextBox
         // 
         this.userIDTextBox.Location = new System.Drawing.Point(73, 46);
         this.userIDTextBox.Name = "userIDTextBox";
         this.userIDTextBox.Size = new System.Drawing.Size(158, 20);
         this.userIDTextBox.TabIndex = 2;
         // 
         // userListLabel
         // 
         this.userListLabel.AutoSize = true;
         this.userListLabel.Location = new System.Drawing.Point(18, 22);
         this.userListLabel.Name = "userListLabel";
         this.userListLabel.Size = new System.Drawing.Size(49, 13);
         this.userListLabel.TabIndex = 1;
         this.userListLabel.Text = "user List:";
         this.userListLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userListComboBox
         // 
         this.userListComboBox.FormattingEnabled = true;
         this.userListComboBox.Location = new System.Drawing.Point(73, 19);
         this.userListComboBox.Name = "userListComboBox";
         this.userListComboBox.Size = new System.Drawing.Size(158, 21);
         this.userListComboBox.TabIndex = 0;
         this.userListComboBox.SelectedIndexChanged += new System.EventHandler(this.userListComboBox_SelectedIndexChanged);
         // 
         // clientAssignmentGroupBox
         // 
         this.clientAssignmentGroupBox.Controls.Add(this.SetGroupBox);
         this.clientAssignmentGroupBox.Controls.Add(this.currentGroupBox);
         this.clientAssignmentGroupBox.Controls.Add(this.clientAssignmentButton);
         this.clientAssignmentGroupBox.Controls.Add(this.clientAssignmentCheckedListBox);
         this.clientAssignmentGroupBox.Location = new System.Drawing.Point(293, 44);
         this.clientAssignmentGroupBox.Name = "clientAssignmentGroupBox";
         this.clientAssignmentGroupBox.Size = new System.Drawing.Size(272, 416);
         this.clientAssignmentGroupBox.TabIndex = 2;
         this.clientAssignmentGroupBox.TabStop = false;
         this.clientAssignmentGroupBox.Text = "Client Assignment";
         // 
         // allRadioButton
         // 
         this.allRadioButton.AutoSize = true;
         this.allRadioButton.Location = new System.Drawing.Point(133, 19);
         this.allRadioButton.Name = "allRadioButton";
         this.allRadioButton.Size = new System.Drawing.Size(116, 17);
         this.allRadioButton.TabIndex = 9;
         this.allRadioButton.TabStop = true;
         this.allRadioButton.Text = "Analyst AND Notes";
         this.allRadioButton.UseVisualStyleBackColor = true;
         this.allRadioButton.CheckedChanged += new System.EventHandler(this.allRadioButton_CheckedChanged);
         // 
         // notesRadioButton
         // 
         this.notesRadioButton.AutoSize = true;
         this.notesRadioButton.Location = new System.Drawing.Point(74, 19);
         this.notesRadioButton.Name = "notesRadioButton";
         this.notesRadioButton.Size = new System.Drawing.Size(53, 17);
         this.notesRadioButton.TabIndex = 8;
         this.notesRadioButton.TabStop = true;
         this.notesRadioButton.Text = "Notes";
         this.notesRadioButton.UseVisualStyleBackColor = true;
         this.notesRadioButton.CheckedChanged += new System.EventHandler(this.notesRadioButton_CheckedChanged);
         // 
         // AnalystRadioButton
         // 
         this.AnalystRadioButton.AutoSize = true;
         this.AnalystRadioButton.Location = new System.Drawing.Point(9, 19);
         this.AnalystRadioButton.Name = "AnalystRadioButton";
         this.AnalystRadioButton.Size = new System.Drawing.Size(59, 17);
         this.AnalystRadioButton.TabIndex = 7;
         this.AnalystRadioButton.TabStop = true;
         this.AnalystRadioButton.Text = "Analyst";
         this.AnalystRadioButton.UseVisualStyleBackColor = true;
         this.AnalystRadioButton.CheckedChanged += new System.EventHandler(this.AnalystRadioButton_CheckedChanged);
         // 
         // clientAssignmentButton
         // 
         this.clientAssignmentButton.Location = new System.Drawing.Point(94, 381);
         this.clientAssignmentButton.Name = "clientAssignmentButton";
         this.clientAssignmentButton.Size = new System.Drawing.Size(75, 23);
         this.clientAssignmentButton.TabIndex = 4;
         this.clientAssignmentButton.Text = "Update";
         this.clientAssignmentButton.UseVisualStyleBackColor = true;
         this.clientAssignmentButton.Click += new System.EventHandler(this.clientAssignmentButton_Click);
         // 
         // clientAssignmentCheckedListBox
         // 
         this.clientAssignmentCheckedListBox.FormattingEnabled = true;
         this.clientAssignmentCheckedListBox.Location = new System.Drawing.Point(7, 131);
         this.clientAssignmentCheckedListBox.Name = "clientAssignmentCheckedListBox";
         this.clientAssignmentCheckedListBox.Size = new System.Drawing.Size(254, 244);
         this.clientAssignmentCheckedListBox.TabIndex = 3;
         this.clientAssignmentCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clientAssignmentCheckedListBox_ItemCheck);
         this.clientAssignmentCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.clientAssignmentCheckedListBox_SelectedIndexChanged);
         this.clientAssignmentCheckedListBox.SelectedValueChanged += new System.EventHandler(this.clientAssignmentCheckedListBox_SelectedValueChanged);
         // 
         // currentGroupBox
         // 
         this.currentGroupBox.Controls.Add(this.allRadioButton);
         this.currentGroupBox.Controls.Add(this.AnalystRadioButton);
         this.currentGroupBox.Controls.Add(this.notesRadioButton);
         this.currentGroupBox.Location = new System.Drawing.Point(7, 19);
         this.currentGroupBox.Name = "currentGroupBox";
         this.currentGroupBox.Size = new System.Drawing.Size(254, 43);
         this.currentGroupBox.TabIndex = 10;
         this.currentGroupBox.TabStop = false;
         this.currentGroupBox.Text = "Current Permissions";
         // 
         // SetGroupBox
         // 
         this.SetGroupBox.Controls.Add(this.setNotesCheckBox);
         this.SetGroupBox.Controls.Add(this.setAnalystCheckBox);
         this.SetGroupBox.Location = new System.Drawing.Point(7, 68);
         this.SetGroupBox.Name = "SetGroupBox";
         this.SetGroupBox.Size = new System.Drawing.Size(254, 43);
         this.SetGroupBox.TabIndex = 11;
         this.SetGroupBox.TabStop = false;
         this.SetGroupBox.Text = "Permissions To Set";
         // 
         // setAnalystCheckBox
         // 
         this.setAnalystCheckBox.AutoSize = true;
         this.setAnalystCheckBox.Location = new System.Drawing.Point(9, 19);
         this.setAnalystCheckBox.Name = "setAnalystCheckBox";
         this.setAnalystCheckBox.Size = new System.Drawing.Size(60, 17);
         this.setAnalystCheckBox.TabIndex = 0;
         this.setAnalystCheckBox.Text = "Analyst";
         this.setAnalystCheckBox.UseVisualStyleBackColor = true;
         // 
         // setNotesCheckBox
         // 
         this.setNotesCheckBox.AutoSize = true;
         this.setNotesCheckBox.Location = new System.Drawing.Point(75, 20);
         this.setNotesCheckBox.Name = "setNotesCheckBox";
         this.setNotesCheckBox.Size = new System.Drawing.Size(54, 17);
         this.setNotesCheckBox.TabIndex = 1;
         this.setNotesCheckBox.Text = "Notes";
         this.setNotesCheckBox.UseVisualStyleBackColor = true;
         // 
         // userAdministration
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(575, 525);
         this.Controls.Add(this.clientAssignmentGroupBox);
         this.Controls.Add(this.userProfileGroupBox);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "userAdministration";
         this.Text = "userAdministration";
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.userProfileGroupBox.ResumeLayout(false);
         this.userProfileGroupBox.PerformLayout();
         this.clientAssignmentGroupBox.ResumeLayout(false);
         this.currentGroupBox.ResumeLayout(false);
         this.currentGroupBox.PerformLayout();
         this.SetGroupBox.ResumeLayout(false);
         this.SetGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }



      #endregion

      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.GroupBox userProfileGroupBox;
      private System.Windows.Forms.Button deleteUserButton;
      private System.Windows.Forms.Button updateUserButton;
      private System.Windows.Forms.Label userRoleLabel;
      private System.Windows.Forms.Label userIdLabel;
      private System.Windows.Forms.ComboBox userRoleComboBox;
      private System.Windows.Forms.TextBox userIDTextBox;
      private System.Windows.Forms.Label userListLabel;
      private System.Windows.Forms.ComboBox userListComboBox;
      private System.Windows.Forms.GroupBox clientAssignmentGroupBox;
      private System.Windows.Forms.Button clientAssignmentButton;
      private System.Windows.Forms.CheckedListBox clientAssignmentCheckedListBox;
      private System.Windows.Forms.RadioButton allRadioButton;
      private System.Windows.Forms.RadioButton notesRadioButton;
      private System.Windows.Forms.RadioButton AnalystRadioButton;
      private System.Windows.Forms.GroupBox SetGroupBox;
      private System.Windows.Forms.CheckBox setNotesCheckBox;
      private System.Windows.Forms.CheckBox setAnalystCheckBox;
      private System.Windows.Forms.GroupBox currentGroupBox;
   }
}