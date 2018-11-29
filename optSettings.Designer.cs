namespace traderTools
{
    partial class OptimizerForm
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
            this.OptSettingsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.optSettingsListView = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Parameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Min = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Max = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sumOrAvg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OptSettingsMenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptSettingsMenuStrip
            // 
            this.OptSettingsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.OptSettingsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.OptSettingsMenuStrip.Name = "OptSettingsMenuStrip";
            this.OptSettingsMenuStrip.Size = new System.Drawing.Size(670, 24);
            this.OptSettingsMenuStrip.TabIndex = 0;
            this.OptSettingsMenuStrip.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.optSettingsListView, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 521);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // optSettingsListView
            // 
            this.optSettingsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader,
            this.Parameter,
            this.Min,
            this.Max,
            this.sumOrAvg});
            this.optSettingsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optSettingsListView.FullRowSelect = true;
            this.optSettingsListView.Location = new System.Drawing.Point(137, 3);
            this.optSettingsListView.Name = "optSettingsListView";
            this.optSettingsListView.Size = new System.Drawing.Size(530, 410);
            this.optSettingsListView.TabIndex = 0;
            this.optSettingsListView.UseCompatibleStateImageBehavior = false;
            this.optSettingsListView.SelectedIndexChanged += new System.EventHandler(this.optSettingsListView_SelectedIndexChanged);
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "Column ";
            // 
            // Parameter
            // 
            this.Parameter.Text = "Parameter";
            // 
            // Min
            // 
            this.Min.Text = "Min";
            // 
            // Max
            // 
            this.Max.Text = "Max";
            // 
            // sumOrAvg
            // 
            this.sumOrAvg.Text = "Sum or Average";
            // 
            // OptimizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 545);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.OptSettingsMenuStrip);
            this.MainMenuStrip = this.OptSettingsMenuStrip;
            this.Name = "OptimizerForm";
            this.Text = "Optimizer Settings";
            this.Load += new System.EventHandler(this.OptimizerForm_Load);
            this.OptSettingsMenuStrip.ResumeLayout(false);
            this.OptSettingsMenuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip OptSettingsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView optSettingsListView;
        private System.Windows.Forms.ColumnHeader Parameter;
        private System.Windows.Forms.ColumnHeader Min;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.ColumnHeader Max;
        private System.Windows.Forms.ColumnHeader sumOrAvg;
    }
}