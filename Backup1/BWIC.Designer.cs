namespace traderTools
{
    partial class BWIC
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
            this.BWICtabControl = new System.Windows.Forms.TabControl();
            this.BWICSearchtabPage = new System.Windows.Forms.TabPage();
            this.BWICdategroupBox = new System.Windows.Forms.GroupBox();
            this.BWICdatecheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.BWICSearchbutton = new System.Windows.Forms.Button();
            this.BWIC_IDgroupBox = new System.Windows.Forms.GroupBox();
            this.BWICListcheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BWICarchivedCheckBox = new System.Windows.Forms.CheckBox();
            this.BWICtabControl.SuspendLayout();
            this.BWICSearchtabPage.SuspendLayout();
            this.BWICdategroupBox.SuspendLayout();
            this.BWIC_IDgroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BWICtabControl
            // 
            this.BWICtabControl.Controls.Add(this.BWICSearchtabPage);
            this.BWICtabControl.Controls.Add(this.tabPage2);
            this.BWICtabControl.Location = new System.Drawing.Point(12, 75);
            this.BWICtabControl.Name = "BWICtabControl";
            this.BWICtabControl.SelectedIndex = 0;
            this.BWICtabControl.Size = new System.Drawing.Size(785, 440);
            this.BWICtabControl.TabIndex = 0;
            // 
            // BWICSearchtabPage
            // 
            this.BWICSearchtabPage.Controls.Add(this.BWICarchivedCheckBox);
            this.BWICSearchtabPage.Controls.Add(this.BWICdategroupBox);
            this.BWICSearchtabPage.Controls.Add(this.BWICSearchbutton);
            this.BWICSearchtabPage.Controls.Add(this.BWIC_IDgroupBox);
            this.BWICSearchtabPage.Location = new System.Drawing.Point(4, 25);
            this.BWICSearchtabPage.Name = "BWICSearchtabPage";
            this.BWICSearchtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BWICSearchtabPage.Size = new System.Drawing.Size(777, 411);
            this.BWICSearchtabPage.TabIndex = 0;
            this.BWICSearchtabPage.Text = "Search";
            this.BWICSearchtabPage.UseVisualStyleBackColor = true;
            // 
            // BWICdategroupBox
            // 
            this.BWICdategroupBox.Controls.Add(this.BWICdatecheckedListBox);
            this.BWICdategroupBox.Location = new System.Drawing.Point(6, 43);
            this.BWICdategroupBox.Name = "BWICdategroupBox";
            this.BWICdategroupBox.Size = new System.Drawing.Size(200, 127);
            this.BWICdategroupBox.TabIndex = 2;
            this.BWICdategroupBox.TabStop = false;
            this.BWICdategroupBox.Text = "BWIC Date";
            // 
            // BWICdatecheckedListBox
            // 
            this.BWICdatecheckedListBox.CheckOnClick = true;
            this.BWICdatecheckedListBox.ColumnWidth = 250;
            this.BWICdatecheckedListBox.Location = new System.Drawing.Point(6, 21);
            this.BWICdatecheckedListBox.Name = "BWICdatecheckedListBox";
            this.BWICdatecheckedListBox.Size = new System.Drawing.Size(178, 89);
            this.BWICdatecheckedListBox.TabIndex = 1;
            this.BWICdatecheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.BWICdatecheckedListBox_ItemCheck);
            // 
            // BWICSearchbutton
            // 
            this.BWICSearchbutton.Location = new System.Drawing.Point(298, 361);
            this.BWICSearchbutton.Margin = new System.Windows.Forms.Padding(4);
            this.BWICSearchbutton.Name = "BWICSearchbutton";
            this.BWICSearchbutton.Size = new System.Drawing.Size(100, 28);
            this.BWICSearchbutton.TabIndex = 20;
            this.BWICSearchbutton.Text = "Pull Data";
            this.BWICSearchbutton.UseVisualStyleBackColor = true;
            this.BWICSearchbutton.Click += new System.EventHandler(this.BWICSearchbutton_Click);
            // 
            // BWIC_IDgroupBox
            // 
            this.BWIC_IDgroupBox.Controls.Add(this.BWICListcheckedListBox);
            this.BWIC_IDgroupBox.Location = new System.Drawing.Point(323, 16);
            this.BWIC_IDgroupBox.Name = "BWIC_IDgroupBox";
            this.BWIC_IDgroupBox.Size = new System.Drawing.Size(436, 324);
            this.BWIC_IDgroupBox.TabIndex = 19;
            this.BWIC_IDgroupBox.TabStop = false;
            this.BWIC_IDgroupBox.Text = "BWIC Lists";
            // 
            // BWICListcheckedListBox
            // 
            this.BWICListcheckedListBox.CheckOnClick = true;
            this.BWICListcheckedListBox.ColumnWidth = 250;
            this.BWICListcheckedListBox.Location = new System.Drawing.Point(6, 21);
            this.BWICListcheckedListBox.Name = "BWICListcheckedListBox";
            this.BWICListcheckedListBox.Size = new System.Drawing.Size(417, 293);
            this.BWICListcheckedListBox.TabIndex = 0;
            this.BWICListcheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.BWICListcheckedListBox_ItemCheck);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(777, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(830, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // BWICarchivedCheckBox
            // 
            this.BWICarchivedCheckBox.AutoSize = true;
            this.BWICarchivedCheckBox.Location = new System.Drawing.Point(6, 16);
            this.BWICarchivedCheckBox.Name = "BWICarchivedCheckBox";
            this.BWICarchivedCheckBox.Size = new System.Drawing.Size(134, 21);
            this.BWICarchivedCheckBox.TabIndex = 21;
            this.BWICarchivedCheckBox.Text = "Include Archived";
            this.BWICarchivedCheckBox.UseVisualStyleBackColor = true;
            this.BWICarchivedCheckBox.CheckedChanged += new System.EventHandler(this.BWICarchivedCheckBox_CheckedChanged);
            // 
            // BWIC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 527);
            this.Controls.Add(this.BWICtabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BWIC";
            this.Text = "BWIC";
            this.Load += new System.EventHandler(this.BWIC_Load);
            this.BWICtabControl.ResumeLayout(false);
            this.BWICSearchtabPage.ResumeLayout(false);
            this.BWICSearchtabPage.PerformLayout();
            this.BWICdategroupBox.ResumeLayout(false);
            this.BWIC_IDgroupBox.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl BWICtabControl;
        private System.Windows.Forms.TabPage BWICSearchtabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox BWIC_IDgroupBox;
        private System.Windows.Forms.CheckedListBox BWICListcheckedListBox;
        private System.Windows.Forms.Button BWICSearchbutton;
        private System.Windows.Forms.GroupBox BWICdategroupBox;
        private System.Windows.Forms.CheckedListBox BWICdatecheckedListBox;
        private System.Windows.Forms.CheckBox BWICarchivedCheckBox;
    }
}