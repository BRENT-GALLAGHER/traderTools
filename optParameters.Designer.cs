namespace traderTools
{
    partial class optParameters
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
            this.optParamMenuStrip = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optParamFieldlabel = new System.Windows.Forms.Label();
            this.optParamMinMaxcomboBox = new System.Windows.Forms.ComboBox();
            this.OptParamMinlabel = new System.Windows.Forms.Label();
            this.optParamMintextBox = new System.Windows.Forms.TextBox();
            this.optParamMaximumtextBox = new System.Windows.Forms.TextBox();
            this.optParamMaxlabel = new System.Windows.Forms.Label();
            this.optParamSumorAveragecomboBox = new System.Windows.Forms.ComboBox();
            this.optParamSumorAvglabel = new System.Windows.Forms.Label();
            this.optPUpdatebutton = new System.Windows.Forms.Button();
            this.optParamMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // optParamMenuStrip
            // 
            this.optParamMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.optParamMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.optParamMenuStrip.Name = "optParamMenuStrip";
            this.optParamMenuStrip.Size = new System.Drawing.Size(284, 24);
            this.optParamMenuStrip.TabIndex = 0;
            this.optParamMenuStrip.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optParamFieldlabel
            // 
            this.optParamFieldlabel.AutoSize = true;
            this.optParamFieldlabel.Location = new System.Drawing.Point(12, 42);
            this.optParamFieldlabel.Name = "optParamFieldlabel";
            this.optParamFieldlabel.Size = new System.Drawing.Size(83, 13);
            this.optParamFieldlabel.TabIndex = 1;
            this.optParamFieldlabel.Text = "Parameter Field:";
            this.optParamFieldlabel.Click += new System.EventHandler(this.optParamFieldlabel_Click);
            // 
            // optParamMinMaxcomboBox
            // 
            this.optParamMinMaxcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.optParamMinMaxcomboBox.FormattingEnabled = true;
            this.optParamMinMaxcomboBox.Items.AddRange(new object[] {
            "Min",
            "Max",
            "Range",
            "Size or Total"});
            this.optParamMinMaxcomboBox.Location = new System.Drawing.Point(101, 34);
            this.optParamMinMaxcomboBox.Name = "optParamMinMaxcomboBox";
            this.optParamMinMaxcomboBox.Size = new System.Drawing.Size(121, 21);
            this.optParamMinMaxcomboBox.TabIndex = 2;
            // 
            // OptParamMinlabel
            // 
            this.OptParamMinlabel.AutoSize = true;
            this.OptParamMinlabel.Location = new System.Drawing.Point(44, 69);
            this.OptParamMinlabel.Name = "OptParamMinlabel";
            this.OptParamMinlabel.Size = new System.Drawing.Size(51, 13);
            this.OptParamMinlabel.TabIndex = 3;
            this.OptParamMinlabel.Text = "Minimum:";
            // 
            // optParamMintextBox
            // 
            this.optParamMintextBox.Location = new System.Drawing.Point(101, 66);
            this.optParamMintextBox.Name = "optParamMintextBox";
            this.optParamMintextBox.Size = new System.Drawing.Size(121, 20);
            this.optParamMintextBox.TabIndex = 4;
            // 
            // optParamMaximumtextBox
            // 
            this.optParamMaximumtextBox.Location = new System.Drawing.Point(101, 92);
            this.optParamMaximumtextBox.Name = "optParamMaximumtextBox";
            this.optParamMaximumtextBox.Size = new System.Drawing.Size(121, 20);
            this.optParamMaximumtextBox.TabIndex = 6;
            // 
            // optParamMaxlabel
            // 
            this.optParamMaxlabel.AutoSize = true;
            this.optParamMaxlabel.Location = new System.Drawing.Point(44, 95);
            this.optParamMaxlabel.Name = "optParamMaxlabel";
            this.optParamMaxlabel.Size = new System.Drawing.Size(54, 13);
            this.optParamMaxlabel.TabIndex = 5;
            this.optParamMaxlabel.Text = "Maximum:";
            // 
            // optParamSumorAveragecomboBox
            // 
            this.optParamSumorAveragecomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.optParamSumorAveragecomboBox.FormattingEnabled = true;
            this.optParamSumorAveragecomboBox.Items.AddRange(new object[] {
            "Sum",
            "Wtd Avg"});
            this.optParamSumorAveragecomboBox.Location = new System.Drawing.Point(101, 118);
            this.optParamSumorAveragecomboBox.Name = "optParamSumorAveragecomboBox";
            this.optParamSumorAveragecomboBox.Size = new System.Drawing.Size(121, 21);
            this.optParamSumorAveragecomboBox.TabIndex = 8;
            // 
            // optParamSumorAvglabel
            // 
            this.optParamSumorAvglabel.AutoSize = true;
            this.optParamSumorAvglabel.Location = new System.Drawing.Point(12, 126);
            this.optParamSumorAvglabel.Name = "optParamSumorAvglabel";
            this.optParamSumorAvglabel.Size = new System.Drawing.Size(86, 13);
            this.optParamSumorAvglabel.TabIndex = 7;
            this.optParamSumorAvglabel.Text = "Sum or Average:";
            // 
            // optPUpdatebutton
            // 
            this.optPUpdatebutton.Location = new System.Drawing.Point(101, 161);
            this.optPUpdatebutton.Name = "optPUpdatebutton";
            this.optPUpdatebutton.Size = new System.Drawing.Size(75, 23);
            this.optPUpdatebutton.TabIndex = 9;
            this.optPUpdatebutton.Text = "U&pdate";
            this.optPUpdatebutton.UseVisualStyleBackColor = true;
            this.optPUpdatebutton.Click += new System.EventHandler(this.optPUpdatebutton_Click);
            // 
            // optParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.optPUpdatebutton);
            this.Controls.Add(this.optParamSumorAveragecomboBox);
            this.Controls.Add(this.optParamSumorAvglabel);
            this.Controls.Add(this.optParamMaximumtextBox);
            this.Controls.Add(this.optParamMaxlabel);
            this.Controls.Add(this.optParamMintextBox);
            this.Controls.Add(this.OptParamMinlabel);
            this.Controls.Add(this.optParamMinMaxcomboBox);
            this.Controls.Add(this.optParamFieldlabel);
            this.Controls.Add(this.optParamMenuStrip);
            this.MainMenuStrip = this.optParamMenuStrip;
            this.Name = "optParameters";
            this.Text = "Optimization Parameter";
            this.Load += new System.EventHandler(this.optParameters_Load);
            this.optParamMenuStrip.ResumeLayout(false);
            this.optParamMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip optParamMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label optParamFieldlabel;
        private System.Windows.Forms.ComboBox optParamMinMaxcomboBox;
        private System.Windows.Forms.Label OptParamMinlabel;
        private System.Windows.Forms.TextBox optParamMintextBox;
        private System.Windows.Forms.TextBox optParamMaximumtextBox;
        private System.Windows.Forms.Label optParamMaxlabel;
        private System.Windows.Forms.ComboBox optParamSumorAveragecomboBox;
        private System.Windows.Forms.Label optParamSumorAvglabel;
        private System.Windows.Forms.Button optPUpdatebutton;
    }
}