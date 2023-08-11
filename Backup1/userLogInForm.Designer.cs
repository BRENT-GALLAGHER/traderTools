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
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(51, 37);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(46, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "User ID:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(16, 63);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(81, 13);
         this.label2.TabIndex = 1;
         this.label2.Text = "User Password:";
         this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // userIDTextBox
         // 
         this.userIDTextBox.Location = new System.Drawing.Point(103, 34);
         this.userIDTextBox.Name = "userIDTextBox";
         this.userIDTextBox.Size = new System.Drawing.Size(100, 20);
         this.userIDTextBox.TabIndex = 2;
         // 
         // userPasswordTextBox
         // 
         this.userPasswordTextBox.Location = new System.Drawing.Point(103, 60);
         this.userPasswordTextBox.Name = "userPasswordTextBox";
         this.userPasswordTextBox.PasswordChar = '*';
         this.userPasswordTextBox.Size = new System.Drawing.Size(100, 20);
         this.userPasswordTextBox.TabIndex = 3;
         this.userPasswordTextBox.TextChanged += new System.EventHandler(this.userPasswordTextBox_TextChanged);
         this.userPasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userPasswordTextBox_KeyDown);
         this.userPasswordTextBox.GotFocus += new System.EventHandler(userPasswordTextBox_GotFocus);
         // 
         // logInButton
         // 
         this.logInButton.Location = new System.Drawing.Point(115, 89);
         this.logInButton.Name = "logInButton";
         this.logInButton.Size = new System.Drawing.Size(75, 23);
         this.logInButton.TabIndex = 4;
         this.logInButton.Text = "Log In";
         this.logInButton.UseVisualStyleBackColor = true;
         this.logInButton.Click += new System.EventHandler(this.logInButton_Click);
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(284, 24);
         this.menuStrip1.TabIndex = 5;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // userLogInForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 122);
         this.Controls.Add(this.logInButton);
         this.Controls.Add(this.userPasswordTextBox);
         this.Controls.Add(this.userIDTextBox);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "userLogInForm";
         this.Text = "User LogIn";
         this.Load += new System.EventHandler(this.userLogInForm_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();
         this.Deactivate += new System.EventHandler(userLogInForm_Deactivate);
         
      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox userIDTextBox;
      private System.Windows.Forms.TextBox userPasswordTextBox;
      private System.Windows.Forms.Button logInButton;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
   }
}