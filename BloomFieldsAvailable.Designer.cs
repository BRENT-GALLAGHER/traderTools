namespace traderTools
{
    partial class BloomFieldsAvailable
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
            this.blmMappedFieldscomboBox = new System.Windows.Forms.ComboBox();
            this.blmMappedFieldtextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(282, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // blmMappedFieldscomboBox
            // 
            this.blmMappedFieldscomboBox.FormattingEnabled = true;
            this.blmMappedFieldscomboBox.Items.AddRange(new object[] {
            "an\t",
            "example"});
            this.blmMappedFieldscomboBox.Location = new System.Drawing.Point(12, 44);
            this.blmMappedFieldscomboBox.Name = "blmMappedFieldscomboBox";
            this.blmMappedFieldscomboBox.Size = new System.Drawing.Size(161, 24);
            this.blmMappedFieldscomboBox.TabIndex = 1;
            this.blmMappedFieldscomboBox.TextChanged += new System.EventHandler(this.blmMappedFieldscomboBox_TextChanged);
            this.blmMappedFieldscomboBox.DragLeave += new System.EventHandler(this.blmMappedFieldscomboBox_DragLeave);
            // 
            // blmMappedFieldtextBox
            // 
            this.blmMappedFieldtextBox.Location = new System.Drawing.Point(12, 75);
            this.blmMappedFieldtextBox.Name = "blmMappedFieldtextBox";
            this.blmMappedFieldtextBox.Size = new System.Drawing.Size(161, 22);
            this.blmMappedFieldtextBox.TabIndex = 2;
            // 
            // BloomFieldsAvailable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.blmMappedFieldtextBox);
            this.Controls.Add(this.blmMappedFieldscomboBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BloomFieldsAvailable";
            this.Text = "BloomFieldsAvailable";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox blmMappedFieldscomboBox;
        private System.Windows.Forms.TextBox blmMappedFieldtextBox;
    }
}