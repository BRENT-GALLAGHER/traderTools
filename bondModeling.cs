using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FI_Analytics
{
   public partial class bondModeling : Form
   {
      private string uid;
      private string pwd;
      private double bndPrice;
      private double bndFace;
      private long bndMnthsMaturity;
      private long bndYMPmonths;
      private long bndAmoritization;
      private long bndIO;
      private DateTime bndSettlement;
      private double bndCoupon;
      private double bndWAC;
      private double bndBench;

      public double benchmark
      {
         get
         {
            return bndBench;
         }
         set
         {
            bndBench = value;
         }
      }

      public double WAC
      {
         get
         {
            return bndWAC;
         }
         set
         {
            bndWAC = value;
         }
      }

      public double coupon
      {
         get
         {
            return bndCoupon;
         }
         set
         {
            bndCoupon = value;
         }
      }

      public DateTime settlementDate
      {
         get
         {
            return bndSettlement;
         }
         set
         {
            bndSettlement = value;
         }
      }

      public long interestOnlyPeriod
      {
         get
         {
            return bndIO;
         }
         set
         {
            bndIO = value;
         }
      }

      public long amoritization
      {
         get
         {
            return bndAmoritization;
         }
         set
         {
            bndAmoritization = value;
         }
      }

      public long yieldMaintenancePeriod
      {
         get
         {
            return bndYMPmonths;
         }
         set
         {
            bndYMPmonths = value;
         }
      }

      public long monthsToMaturity
      {
         get
         {
            return bndMnthsMaturity;
         }
         set
         {
            bndMnthsMaturity = value;
         }
      }

      public double faceValue
      {
         get
         {
            return bndFace;
         }
         set
         {
            bndFace = value;
         }
      }

      public string user
      {
         get
         {
            return uid;
         }
         set
         {
            uid = value;
         }
      }

      public string password
      {
         get
         {
            return pwd;
         }
         set
         {
            pwd = value;
         }
      }

      public double offerPrice
      {
         get
         {
            return bndPrice;
         }
         set
         {
            bndPrice = value;
         }
      }

      public bondModeling()
      {
         InitializeComponent();
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         bondModeling_Deactivate(sender, e);
      }

      private void bondModeling_Load(object sender, EventArgs e)
      {

      }

      private void DUSOfferPriceTextBox_TextChanged(object sender, EventArgs e)
      {

      }

      private void DUSSubmitButton_Click(object sender, EventArgs e)
      {

         try
         {
            DUSOfferPriceTextBox.Text = String.Format("{0:#0.000}", Convert.ToDouble(DUSOfferPriceTextBox.Text));
         }
         catch
         {
            DUSOfferPriceTextBox.Text = String.Format("{0:#0.000}", 0);
         }
         offerPrice = Double.Parse(DUSOfferPriceTextBox.Text);

         try
         {
            DUSFaceValueTextBox.Text = String.Format("{0:##,0}", Convert.ToDouble(DUSFaceValueTextBox.Text));
         }
         catch
         {
            DUSFaceValueTextBox.Text = String.Format("{0:##,0}", 0);
         }
         faceValue = Double.Parse(DUSFaceValueTextBox.Text );

         try
         {
            DUSMonthsToMaturityTextBox.Text = String.Format("{0:##,0}", Convert.ToInt16(DUSMonthsToMaturityTextBox.Text));
         }
         catch
         {
            DUSMonthsToMaturityTextBox.Text = String.Format("{0:##,0}", 0);
         }
         monthsToMaturity = int.Parse(DUSMonthsToMaturityTextBox.Text);

         try
         {
            DUSYieldMaintenanceTextBox.Text = String.Format("{0:##,0}", Convert.ToInt16(DUSYieldMaintenanceTextBox.Text));
         }
         catch
         {
            DUSYieldMaintenanceTextBox.Text = String.Format("{0:##,0}", 0);
         }
         yieldMaintenancePeriod = int.Parse(DUSYieldMaintenanceTextBox.Text);

         try
         {
            DUSAmoritizationTextBox.Text = String.Format("{0:##,0}", Convert.ToInt16(DUSAmoritizationTextBox.Text));
         }
         catch
         {
            DUSAmoritizationTextBox.Text = String.Format("{0:##,0}", 0);
         }
         amoritization = int.Parse(DUSAmoritizationTextBox.Text);

         try
         {
            DUSIOPeriodTextBox.Text = String.Format("{0:##,0}", Convert.ToInt16(DUSIOPeriodTextBox.Text));
         }
         catch
         {
            DUSIOPeriodTextBox.Text = String.Format("{0:##,0}", 0);
         }
         interestOnlyPeriod = int.Parse(DUSIOPeriodTextBox.Text);

         try
         {
            DUSSettlementMaskedTextBox.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(DUSSettlementMaskedTextBox.Text));
         }
         catch
         {
            DUSSettlementMaskedTextBox.Text=String.Format("{0:MM/dd/yyyy}", DateTime.Today);
         }
         settlementDate =DateTime.Parse(DUSSettlementMaskedTextBox.Text);

         try
         {
            DUSCouponTextBox.Text = String.Format("{0:#0.000}", Convert.ToDecimal(DUSCouponTextBox.Text));
         }
         catch
         {
            DUSCouponTextBox.Text = String.Format("{0:#0.000}", 0);
         }
         coupon = Double.Parse(DUSCouponTextBox.Text);

         try
         {
            DUSWACTextBox.Text = String.Format("{0:#0.000}", Convert.ToDecimal(DUSWACTextBox.Text));
         }
         catch
         {
            DUSWACTextBox.Text = String.Format("{0:#0.000}", 0);
         }
         WAC = Double.Parse(DUSWACTextBox.Text);

         try
         {
            DUSBenchmarkTextBox.Text = String.Format("{0:#0.000}", Convert.ToDecimal(DUSBenchmarkTextBox.Text));
         }
         catch
         {
            DUSBenchmarkTextBox.Text = String.Format("{0:#0.000}", 0);
         }
         benchmark = Double.Parse(DUSBenchmarkTextBox.Text);
         runDUS();

      }

      public void runDUS()
      {
         Globals.ThisAddIn.Application._Run2("DUSbonds", monthsToMaturity, yieldMaintenancePeriod,interestOnlyPeriod, faceValue, coupon, WAC, offerPrice, benchmark,settlementDate,amoritization);
      }

 
   }
}
