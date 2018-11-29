namespace FI_Analytics
{
   partial class passwordResetForm
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
         this.oldPassTextBox = new System.Windows.Forms.TextBox();
         this.newPassTextBox = new System.Windows.Forms.TextBox();
         this.confirmPassTextBox = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.resetPassButton = new System.Windows.Forms.Button();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(280, 24);
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
         // oldPassTextBox
         // 
         this.oldPassTextBox.Location = new System.Drawing.Point(106, 57);
         this.oldPassTextBox.Name = "oldPassTextBox";
         this.oldPassTextBox.PasswordChar = '*';
         this.oldPassTextBox.Size = new System.Drawing.Size(149, 20);
         this.oldPassTextBox.TabIndex = 1;
         // 
         // newPassTextBox
         // 
         this.newPassTextBox.Location = new System.Drawing.Point(106, 83);
         this.newPassTextBox.Name = "newPassTextBox";
         this.newPassTextBox.PasswordChar = '*';
         this.newPassTextBox.Size = new System.Drawing.Size(149, 20);
         this.newPassTextBox.TabIndex = 2;
         // 
         // confirmPassTextBox
         // 
         this.confirmPassTextBox.Location = new System.Drawing.Point(106, 109);
         this.confirmPassTextBox.Name = "confirmPassTextBox";
         this.confirmPassTextBox.PasswordChar = '*';
         this.confirmPassTextBox.Size = new System.Drawing.Size(149, 20);
         this.confirmPassTextBox.TabIndex = 3;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(25, 60);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(75, 13);
         this.label1.TabIndex = 4;
         this.label1.Text = "Old Password:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(19, 86);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(81, 13);
         this.label2.TabIndex = 5;
         this.label2.Text = "New Password:";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(6, 112);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(94, 13);
         this.label3.TabIndex = 6;
         this.label3.Text = "Confirm Password:";
         // 
         // resetPassButton
         // 
         this.resetPassButton.Location = new System.Drawing.Point(106, 144);
         this.resetPassButton.Name = "resetPassButton";
         this.resetPassButton.Size = new System.Drawing.Size(93, 23);
         this.resetPassButton.TabIndex = 7;
         this.resetPassButton.Text = "Reset Password";
         this.resetPassButton.UseVisualStyleBackColor = true;
         this.resetPassButton.Click += new System.EventHandler(this.resetPassButton_Click);
         // 
         // passwordResetForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(280, 198);
         this.Controls.Add(this.resetPassButton);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.confirmPassTextBox);
         this.Controls.Add(this.newPassTextBox);
         this.Controls.Add(this.oldPassTextBox);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "passwordResetForm";
         this.Text = "Reset Password";
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.TextBox oldPassTextBox;
      private System.Windows.Forms.TextBox newPassTextBox;
      private System.Windows.Forms.TextBox confirmPassTextBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button resetPassButton;
   }
}