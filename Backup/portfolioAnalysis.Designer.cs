namespace traderTools
{
    partial class PortfolioAnalysis
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonImportPortDetail = new System.Windows.Forms.Button();
            this.menuStripPortfolioAnalysis = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonImportPortfolioCash = new System.Windows.Forms.Button();
            this.menuStripPortfolioAnalysis.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonImportPortDetail
            // 
            this.buttonImportPortDetail.Location = new System.Drawing.Point(12, 63);
            this.buttonImportPortDetail.Name = "buttonImportPortDetail";
            this.buttonImportPortDetail.Size = new System.Drawing.Size(182, 23);
            this.buttonImportPortDetail.TabIndex = 0;
            this.buttonImportPortDetail.Text = "Import Portfolio Detail";
            this.buttonImportPortDetail.UseVisualStyleBackColor = true;
            this.buttonImportPortDetail.Click += new System.EventHandler(this.buttonImportPortDetail_Click);
            // 
            // menuStripPortfolioAnalysis
            // 
            this.menuStripPortfolioAnalysis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuStripPortfolioAnalysis.Location = new System.Drawing.Point(0, 0);
            this.menuStripPortfolioAnalysis.Name = "menuStripPortfolioAnalysis";
            this.menuStripPortfolioAnalysis.Size = new System.Drawing.Size(638, 28);
            this.menuStripPortfolioAnalysis.TabIndex = 1;
            this.menuStripPortfolioAnalysis.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buttonImportPortfolioCash
            // 
            this.buttonImportPortfolioCash.Location = new System.Drawing.Point(12, 92);
            this.buttonImportPortfolioCash.Name = "buttonImportPortfolioCash";
            this.buttonImportPortfolioCash.Size = new System.Drawing.Size(182, 23);
            this.buttonImportPortfolioCash.TabIndex = 2;
            this.buttonImportPortfolioCash.Text = "Import Portfolio Cash";
            this.buttonImportPortfolioCash.UseVisualStyleBackColor = true;
            this.buttonImportPortfolioCash.Click += new System.EventHandler(this.buttonImportPortfolioCash_Click);
            // 
            // PortfolioAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 457);
            this.Controls.Add(this.buttonImportPortfolioCash);
            this.Controls.Add(this.buttonImportPortDetail);
            this.Controls.Add(this.menuStripPortfolioAnalysis);
            this.MainMenuStrip = this.menuStripPortfolioAnalysis;
            this.Name = "PortfolioAnalysis";
            this.Text = "Portfolio Analysis";
            this.menuStripPortfolioAnalysis.ResumeLayout(false);
            this.menuStripPortfolioAnalysis.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonImportPortDetail;
        private System.Windows.Forms.MenuStrip menuStripPortfolioAnalysis;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button buttonImportPortfolioCash;
    }
}