namespace FI_Analytics
{
   partial class userProfileForm
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
         this.userNameTextBox = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.userPrimaryPhoneTextBox = new System.Windows.Forms.MaskedTextBox();
         this.userSecondaryPhoneTextBox = new System.Windows.Forms.MaskedTextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.userAddressTextBox = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.userCityTextBox = new System.Windows.Forms.TextBox();
         this.label5 = new System.Windows.Forms.Label();
         this.userStateTextBox = new System.Windows.Forms.TextBox();
         this.label6 = new System.Windows.Forms.Label();
         this.userZipTextBox = new System.Windows.Forms.MaskedTextBox();
         this.label7 = new System.Windows.Forms.Label();
         this.userCancelButton = new System.Windows.Forms.Button();
         this.userUpdateButton = new System.Windows.Forms.Button();
         this.userEmailTextBox = new System.Windows.Forms.TextBox();
         this.label8 = new System.Windows.Forms.Label();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // userNameTextBox
         // 
         this.userNameTextBox.Location = new System.Drawing.Point(91, 50);
         this.userNameTextBox.MaxLength = 55;
         this.userNameTextBox.Name = "userNameTextBox";
         this.userNameTextBox.Size = new System.Drawing.Size(196, 20);
         this.userNameTextBox.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(47, 53);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(38, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "Name:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(558, 24);
         this.menuStrip1.TabIndex = 2;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // userPrimaryPhoneTextBox
         // 
         this.userPrimaryPhoneTextBox.Location = new System.Drawing.Point(91, 102);
         this.userPrimaryPhoneTextBox.Mask = "(999) 000-0000";
         this.userPrimaryPhoneTextBox.Name = "userPrimaryPhoneTextBox";
         this.userPrimaryPhoneTextBox.Size = new System.Drawing.Size(98, 20);
         this.userPrimaryPhoneTextBox.TabIndex = 3;
         // 
         // userSecondaryPhoneTextBox
         // 
         this.userSecondaryPhoneTextBox.Location = new System.Drawing.Point(301, 105);
         this.userSecondaryPhoneTextBox.Mask = "(999) 000-0000";
         this.userSecondaryPhoneTextBox.Name = "userSecondaryPhoneTextBox";
         this.userSecondaryPhoneTextBox.Size = new System.Drawing.Size(98, 20);
         this.userSecondaryPhoneTextBox.TabIndex = 4;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(7, 105);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(78, 13);
         this.label2.TabIndex = 5;
         this.label2.Text = "Primary Phone:";
         this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(225, 108);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(70, 13);
         this.label3.TabIndex = 6;
         this.label3.Text = "Other Phone:";
         this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userAddressTextBox
         // 
         this.userAddressTextBox.Location = new System.Drawing.Point(91, 128);
         this.userAddressTextBox.MaxLength = 55;
         this.userAddressTextBox.Name = "userAddressTextBox";
         this.userAddressTextBox.Size = new System.Drawing.Size(196, 20);
         this.userAddressTextBox.TabIndex = 7;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(37, 131);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(48, 13);
         this.label4.TabIndex = 8;
         this.label4.Text = "Address:";
         this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userCityTextBox
         // 
         this.userCityTextBox.Location = new System.Drawing.Point(91, 154);
         this.userCityTextBox.MaxLength = 55;
         this.userCityTextBox.Name = "userCityTextBox";
         this.userCityTextBox.Size = new System.Drawing.Size(166, 20);
         this.userCityTextBox.TabIndex = 9;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(58, 157);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(27, 13);
         this.label5.TabIndex = 10;
         this.label5.Text = "City:";
         this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userStateTextBox
         // 
         this.userStateTextBox.Location = new System.Drawing.Point(316, 154);
         this.userStateTextBox.MaxLength = 2;
         this.userStateTextBox.Name = "userStateTextBox";
         this.userStateTextBox.Size = new System.Drawing.Size(57, 20);
         this.userStateTextBox.TabIndex = 11;
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(275, 157);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(35, 13);
         this.label6.TabIndex = 12;
         this.label6.Text = "State:";
         this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userZipTextBox
         // 
         this.userZipTextBox.Location = new System.Drawing.Point(424, 154);
         this.userZipTextBox.Mask = "00000-9999";
         this.userZipTextBox.Name = "userZipTextBox";
         this.userZipTextBox.Size = new System.Drawing.Size(81, 20);
         this.userZipTextBox.TabIndex = 13;
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(393, 157);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(25, 13);
         this.label7.TabIndex = 14;
         this.label7.Text = "Zip:";
         this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userCancelButton
         // 
         this.userCancelButton.Location = new System.Drawing.Point(301, 202);
         this.userCancelButton.Name = "userCancelButton";
         this.userCancelButton.Size = new System.Drawing.Size(75, 23);
         this.userCancelButton.TabIndex = 15;
         this.userCancelButton.Text = "Cancel";
         this.userCancelButton.UseVisualStyleBackColor = true;
         this.userCancelButton.Click += new System.EventHandler(this.userCancelButton_Click);
         // 
         // userUpdateButton
         // 
         this.userUpdateButton.Location = new System.Drawing.Point(220, 202);
         this.userUpdateButton.Name = "userUpdateButton";
         this.userUpdateButton.Size = new System.Drawing.Size(75, 23);
         this.userUpdateButton.TabIndex = 16;
         this.userUpdateButton.Text = "Update";
         this.userUpdateButton.UseVisualStyleBackColor = true;
         this.userUpdateButton.Click += new System.EventHandler(this.userUpdateButton_Click);
         // 
         // userEmailTextBox
         // 
         this.userEmailTextBox.Location = new System.Drawing.Point(91, 76);
         this.userEmailTextBox.Name = "userEmailTextBox";
         this.userEmailTextBox.Size = new System.Drawing.Size(196, 20);
         this.userEmailTextBox.TabIndex = 17;
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Location = new System.Drawing.Point(47, 79);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(38, 13);
         this.label8.TabIndex = 18;
         this.label8.Text = "E-mail:";
         this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userProfileForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(558, 243);
         this.Controls.Add(this.label8);
         this.Controls.Add(this.userEmailTextBox);
         this.Controls.Add(this.userUpdateButton);
         this.Controls.Add(this.userCancelButton);
         this.Controls.Add(this.label7);
         this.Controls.Add(this.userZipTextBox);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.userStateTextBox);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.userCityTextBox);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.userAddressTextBox);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.userSecondaryPhoneTextBox);
         this.Controls.Add(this.userPrimaryPhoneTextBox);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.userNameTextBox);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "userProfileForm";
         this.Text = "User Profile Management";
         this.Load += new System.EventHandler(this.userProfileForm_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox userNameTextBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.MaskedTextBox userPrimaryPhoneTextBox;
      private System.Windows.Forms.MaskedTextBox userSecondaryPhoneTextBox;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox userAddressTextBox;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox userCityTextBox;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.TextBox userStateTextBox;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.MaskedTextBox userZipTextBox;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Button userCancelButton;
      private System.Windows.Forms.Button userUpdateButton;
      private System.Windows.Forms.TextBox userEmailTextBox;
      private System.Windows.Forms.Label label8;
   }
}