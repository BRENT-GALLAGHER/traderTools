namespace FI_Analytics
{
   partial class bondModeling
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
         this.tabControlBondModels = new System.Windows.Forms.TabControl();
         this.tabDUS = new System.Windows.Forms.TabPage();
         this.DUSSubmitButton = new System.Windows.Forms.Button();
         this.DUSBenchmarkTextBox = new System.Windows.Forms.TextBox();
         this.labelDUSBenchmark = new System.Windows.Forms.Label();
         this.DUSWACTextBox = new System.Windows.Forms.TextBox();
         this.labelDUSWAC = new System.Windows.Forms.Label();
         this.DUSCouponTextBox = new System.Windows.Forms.TextBox();
         this.DUSSettlementMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
         this.labelDUSCoupon = new System.Windows.Forms.Label();
         this.labelDUSSettlement = new System.Windows.Forms.Label();
         this.DUSIOPeriodTextBox = new System.Windows.Forms.TextBox();
         this.labelDUSIOPeriod = new System.Windows.Forms.Label();
         this.DUSAmoritizationTextBox = new System.Windows.Forms.TextBox();
         this.labelDUSAmoritization = new System.Windows.Forms.Label();
         this.DUSYieldMaintenanceTextBox = new System.Windows.Forms.TextBox();
         this.labelYieldMaintenance = new System.Windows.Forms.Label();
         this.DUSMonthsToMaturityTextBox = new System.Windows.Forms.TextBox();
         this.labelMonthsMaturity = new System.Windows.Forms.Label();
         this.DUSFaceValueTextBox = new System.Windows.Forms.TextBox();
         this.DUSOfferPriceTextBox = new System.Windows.Forms.TextBox();
         this.labelFaceValue = new System.Windows.Forms.Label();
         this.labelOfferPrice = new System.Windows.Forms.Label();
         this.tabPageMBSpass = new System.Windows.Forms.TabPage();
         this.menuStrip1.SuspendLayout();
         this.tabControlBondModels.SuspendLayout();
         this.tabDUS.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(540, 24);
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
         // tabControlBondModels
         // 
         this.tabControlBondModels.Controls.Add(this.tabDUS);
         this.tabControlBondModels.Controls.Add(this.tabPageMBSpass);
         this.tabControlBondModels.Location = new System.Drawing.Point(12, 27);
         this.tabControlBondModels.Name = "tabControlBondModels";
         this.tabControlBondModels.SelectedIndex = 0;
         this.tabControlBondModels.Size = new System.Drawing.Size(516, 364);
         this.tabControlBondModels.TabIndex = 1;
         // 
         // tabDUS
         // 
         this.tabDUS.Controls.Add(this.DUSSubmitButton);
         this.tabDUS.Controls.Add(this.DUSBenchmarkTextBox);
         this.tabDUS.Controls.Add(this.labelDUSBenchmark);
         this.tabDUS.Controls.Add(this.DUSWACTextBox);
         this.tabDUS.Controls.Add(this.labelDUSWAC);
         this.tabDUS.Controls.Add(this.DUSCouponTextBox);
         this.tabDUS.Controls.Add(this.DUSSettlementMaskedTextBox);
         this.tabDUS.Controls.Add(this.labelDUSCoupon);
         this.tabDUS.Controls.Add(this.labelDUSSettlement);
         this.tabDUS.Controls.Add(this.DUSIOPeriodTextBox);
         this.tabDUS.Controls.Add(this.labelDUSIOPeriod);
         this.tabDUS.Controls.Add(this.DUSAmoritizationTextBox);
         this.tabDUS.Controls.Add(this.labelDUSAmoritization);
         this.tabDUS.Controls.Add(this.DUSYieldMaintenanceTextBox);
         this.tabDUS.Controls.Add(this.labelYieldMaintenance);
         this.tabDUS.Controls.Add(this.DUSMonthsToMaturityTextBox);
         this.tabDUS.Controls.Add(this.labelMonthsMaturity);
         this.tabDUS.Controls.Add(this.DUSFaceValueTextBox);
         this.tabDUS.Controls.Add(this.DUSOfferPriceTextBox);
         this.tabDUS.Controls.Add(this.labelFaceValue);
         this.tabDUS.Controls.Add(this.labelOfferPrice);
         this.tabDUS.Location = new System.Drawing.Point(4, 22);
         this.tabDUS.Name = "tabDUS";
         this.tabDUS.Padding = new System.Windows.Forms.Padding(3);
         this.tabDUS.Size = new System.Drawing.Size(508, 338);
         this.tabDUS.TabIndex = 0;
         this.tabDUS.Text = "DUS";
         this.tabDUS.UseVisualStyleBackColor = true;
         // 
         // DUSSubmitButton
         // 
         this.DUSSubmitButton.Location = new System.Drawing.Point(209, 267);
         this.DUSSubmitButton.Name = "DUSSubmitButton";
         this.DUSSubmitButton.Size = new System.Drawing.Size(75, 23);
         this.DUSSubmitButton.TabIndex = 21;
         this.DUSSubmitButton.Text = "Run Model";
         this.DUSSubmitButton.UseVisualStyleBackColor = true;
         this.DUSSubmitButton.Click += new System.EventHandler(this.DUSSubmitButton_Click);
         // 
         // DUSBenchmarkTextBox
         // 
         this.DUSBenchmarkTextBox.Location = new System.Drawing.Point(383, 148);
         this.DUSBenchmarkTextBox.Name = "DUSBenchmarkTextBox";
         this.DUSBenchmarkTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSBenchmarkTextBox.TabIndex = 20;
         this.DUSBenchmarkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelDUSBenchmark
         // 
         this.labelDUSBenchmark.Location = new System.Drawing.Point(253, 151);
         this.labelDUSBenchmark.Name = "labelDUSBenchmark";
         this.labelDUSBenchmark.Size = new System.Drawing.Size(124, 13);
         this.labelDUSBenchmark.TabIndex = 19;
         this.labelDUSBenchmark.Text = "Benchmark:";
         this.labelDUSBenchmark.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSWACTextBox
         // 
         this.DUSWACTextBox.Location = new System.Drawing.Point(383, 122);
         this.DUSWACTextBox.Name = "DUSWACTextBox";
         this.DUSWACTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSWACTextBox.TabIndex = 18;
         this.DUSWACTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelDUSWAC
         // 
         this.labelDUSWAC.Location = new System.Drawing.Point(253, 125);
         this.labelDUSWAC.Name = "labelDUSWAC";
         this.labelDUSWAC.Size = new System.Drawing.Size(124, 13);
         this.labelDUSWAC.TabIndex = 17;
         this.labelDUSWAC.Text = "WAC:";
         this.labelDUSWAC.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSCouponTextBox
         // 
         this.DUSCouponTextBox.Location = new System.Drawing.Point(383, 96);
         this.DUSCouponTextBox.Name = "DUSCouponTextBox";
         this.DUSCouponTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSCouponTextBox.TabIndex = 16;
         this.DUSCouponTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // DUSSettlementMaskedTextBox
         // 
         this.DUSSettlementMaskedTextBox.Location = new System.Drawing.Point(383, 70);
         this.DUSSettlementMaskedTextBox.Name = "DUSSettlementMaskedTextBox";
         this.DUSSettlementMaskedTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSSettlementMaskedTextBox.TabIndex = 15;
         // 
         // labelDUSCoupon
         // 
         this.labelDUSCoupon.Location = new System.Drawing.Point(253, 100);
         this.labelDUSCoupon.Name = "labelDUSCoupon";
         this.labelDUSCoupon.Size = new System.Drawing.Size(124, 13);
         this.labelDUSCoupon.TabIndex = 14;
         this.labelDUSCoupon.Text = "Coupon:";
         this.labelDUSCoupon.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // labelDUSSettlement
         // 
         this.labelDUSSettlement.Location = new System.Drawing.Point(253, 74);
         this.labelDUSSettlement.Name = "labelDUSSettlement";
         this.labelDUSSettlement.Size = new System.Drawing.Size(124, 13);
         this.labelDUSSettlement.TabIndex = 12;
         this.labelDUSSettlement.Text = "Settlement Date:";
         this.labelDUSSettlement.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSIOPeriodTextBox
         // 
         this.DUSIOPeriodTextBox.Location = new System.Drawing.Point(135, 196);
         this.DUSIOPeriodTextBox.Name = "DUSIOPeriodTextBox";
         this.DUSIOPeriodTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSIOPeriodTextBox.TabIndex = 11;
         this.DUSIOPeriodTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelDUSIOPeriod
         // 
         this.labelDUSIOPeriod.Location = new System.Drawing.Point(4, 200);
         this.labelDUSIOPeriod.Name = "labelDUSIOPeriod";
         this.labelDUSIOPeriod.Size = new System.Drawing.Size(125, 13);
         this.labelDUSIOPeriod.TabIndex = 10;
         this.labelDUSIOPeriod.Text = "IO Period:";
         this.labelDUSIOPeriod.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSAmoritizationTextBox
         // 
         this.DUSAmoritizationTextBox.Location = new System.Drawing.Point(135, 170);
         this.DUSAmoritizationTextBox.Name = "DUSAmoritizationTextBox";
         this.DUSAmoritizationTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSAmoritizationTextBox.TabIndex = 9;
         this.DUSAmoritizationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelDUSAmoritization
         // 
         this.labelDUSAmoritization.Location = new System.Drawing.Point(5, 174);
         this.labelDUSAmoritization.Name = "labelDUSAmoritization";
         this.labelDUSAmoritization.Size = new System.Drawing.Size(125, 13);
         this.labelDUSAmoritization.TabIndex = 8;
         this.labelDUSAmoritization.Text = "Amoritization:";
         this.labelDUSAmoritization.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSYieldMaintenanceTextBox
         // 
         this.DUSYieldMaintenanceTextBox.Location = new System.Drawing.Point(136, 144);
         this.DUSYieldMaintenanceTextBox.Name = "DUSYieldMaintenanceTextBox";
         this.DUSYieldMaintenanceTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSYieldMaintenanceTextBox.TabIndex = 7;
         this.DUSYieldMaintenanceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelYieldMaintenance
         // 
         this.labelYieldMaintenance.Location = new System.Drawing.Point(5, 148);
         this.labelYieldMaintenance.Name = "labelYieldMaintenance";
         this.labelYieldMaintenance.Size = new System.Drawing.Size(125, 13);
         this.labelYieldMaintenance.TabIndex = 6;
         this.labelYieldMaintenance.Text = "Yield Maint. Period:";
         this.labelYieldMaintenance.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSMonthsToMaturityTextBox
         // 
         this.DUSMonthsToMaturityTextBox.Location = new System.Drawing.Point(135, 118);
         this.DUSMonthsToMaturityTextBox.Name = "DUSMonthsToMaturityTextBox";
         this.DUSMonthsToMaturityTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSMonthsToMaturityTextBox.TabIndex = 5;
         this.DUSMonthsToMaturityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelMonthsMaturity
         // 
         this.labelMonthsMaturity.Location = new System.Drawing.Point(5, 122);
         this.labelMonthsMaturity.Name = "labelMonthsMaturity";
         this.labelMonthsMaturity.Size = new System.Drawing.Size(124, 13);
         this.labelMonthsMaturity.TabIndex = 4;
         this.labelMonthsMaturity.Text = "Months to Maturity:";
         this.labelMonthsMaturity.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // DUSFaceValueTextBox
         // 
         this.DUSFaceValueTextBox.Location = new System.Drawing.Point(135, 92);
         this.DUSFaceValueTextBox.Name = "DUSFaceValueTextBox";
         this.DUSFaceValueTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSFaceValueTextBox.TabIndex = 3;
         this.DUSFaceValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // DUSOfferPriceTextBox
         // 
         this.DUSOfferPriceTextBox.Location = new System.Drawing.Point(135, 70);
         this.DUSOfferPriceTextBox.Name = "DUSOfferPriceTextBox";
         this.DUSOfferPriceTextBox.Size = new System.Drawing.Size(100, 20);
         this.DUSOfferPriceTextBox.TabIndex = 2;
         this.DUSOfferPriceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.DUSOfferPriceTextBox.TextChanged += new System.EventHandler(this.DUSOfferPriceTextBox_TextChanged);
         // 
         // labelFaceValue
         // 
         this.labelFaceValue.Location = new System.Drawing.Point(5, 100);
         this.labelFaceValue.Name = "labelFaceValue";
         this.labelFaceValue.Size = new System.Drawing.Size(124, 13);
         this.labelFaceValue.TabIndex = 1;
         this.labelFaceValue.Text = "Face Value:";
         this.labelFaceValue.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // labelOfferPrice
         // 
         this.labelOfferPrice.Location = new System.Drawing.Point(5, 74);
         this.labelOfferPrice.Name = "labelOfferPrice";
         this.labelOfferPrice.Size = new System.Drawing.Size(124, 13);
         this.labelOfferPrice.TabIndex = 0;
         this.labelOfferPrice.Text = "Offer Price:";
         this.labelOfferPrice.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // tabPageMBSpass
         // 
         this.tabPageMBSpass.Location = new System.Drawing.Point(4, 22);
         this.tabPageMBSpass.Name = "tabPageMBSpass";
         this.tabPageMBSpass.Padding = new System.Windows.Forms.Padding(3);
         this.tabPageMBSpass.Size = new System.Drawing.Size(508, 338);
         this.tabPageMBSpass.TabIndex = 1;
         this.tabPageMBSpass.Text = "MBS Pass Through";
         this.tabPageMBSpass.UseVisualStyleBackColor = true;
         // 
         // bondModeling
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(540, 403);
         this.Controls.Add(this.tabControlBondModels);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "bondModeling";
         this.Text = "Bond Models";
         this.Deactivate += new System.EventHandler(this.bondModeling_Deactivate);
         this.Load += new System.EventHandler(this.bondModeling_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.tabControlBondModels.ResumeLayout(false);
         this.tabDUS.ResumeLayout(false);
         this.tabDUS.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }


      void bondModeling_Deactivate(object sender, System.EventArgs e)
      {
         this.Hide();
      }
      #endregion

      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.TabControl tabControlBondModels;
      private System.Windows.Forms.TabPage tabDUS;
      private System.Windows.Forms.TabPage tabPageMBSpass;
      private System.Windows.Forms.Label labelFaceValue;
      private System.Windows.Forms.Label labelOfferPrice;
      private System.Windows.Forms.TextBox DUSOfferPriceTextBox;
      private System.Windows.Forms.Label labelMonthsMaturity;
      private System.Windows.Forms.TextBox DUSFaceValueTextBox;
      private System.Windows.Forms.TextBox DUSYieldMaintenanceTextBox;
      private System.Windows.Forms.Label labelYieldMaintenance;
      private System.Windows.Forms.TextBox DUSMonthsToMaturityTextBox;
      private System.Windows.Forms.TextBox DUSIOPeriodTextBox;
      private System.Windows.Forms.Label labelDUSIOPeriod;
      private System.Windows.Forms.TextBox DUSAmoritizationTextBox;
      private System.Windows.Forms.Label labelDUSAmoritization;
      private System.Windows.Forms.MaskedTextBox DUSSettlementMaskedTextBox;
      private System.Windows.Forms.Label labelDUSCoupon;
      private System.Windows.Forms.Label labelDUSSettlement;
      private System.Windows.Forms.TextBox DUSBenchmarkTextBox;
      private System.Windows.Forms.Label labelDUSBenchmark;
      private System.Windows.Forms.TextBox DUSWACTextBox;
      private System.Windows.Forms.Label labelDUSWAC;
      private System.Windows.Forms.TextBox DUSCouponTextBox;
      private System.Windows.Forms.Button DUSSubmitButton;
   }
}