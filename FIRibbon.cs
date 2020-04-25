using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace traderTools
{

    [ComVisible(true)]
    public interface IFixedIncome
    {
        string QSuser { get; set; }
        bool usingSQLServer { get; set; }
        void fillQS_swapIDDropDown();

        void QS_swapIDDropDown_clear();

        void getSwapIDcount();

        string optFieldName { get; set; }
        
        void showOptimizerParameters();
        
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class FixedIncome : IFixedIncome
    {
        //clientInfoForm cInfo = new clientInfoForm();
        userLogInForm uLogIn = new userLogInForm();
        //userProfileForm uProfile = new userProfileForm();
        //passwordResetForm uPass = new passwordResetForm();
        bondFinder bFinder = new bondFinder();
        OptimizerForm optSettings = new OptimizerForm();
        optParameters optParameter = new optParameters();
        equity eqty = new equity();

        BWIC uBWIC = new BWIC();
        

        //monthlyAnalyticsForm uStrategy = new monthlyAnalyticsForm();
        PortfolioAnalysis uStrategy = new PortfolioAnalysis();
        BloombergFieldMapper bloomMapper = new BloombergFieldMapper();
        BloomFieldsAvailable bloomFields = new BloomFieldsAvailable();

        //userAdministration uAdmin = new userAdministration();
        //bondModeling uBModel = new bondModeling();
        //userTemplates uTemplate = new userTemplates();

        SqlDataAdapter SQLdaCriteria;
        DataSet dsCriteria;
        bool isSQLServer;
        bool boolEquityExists;
        RibbonDropDownItem rbnItem;

        //private string optPFieldName;

        public string optFieldName
        {
            get { return optParameter.optFieldName ; }
            set { optParameter.optFieldName = value; }
        }

        public bool equityExists
        {
            get
            {
                return boolEquityExists;
            }
            set
            {
                boolEquityExists = value;
            }
        }
            
        
        public string optMin
        {
            get
            {
                return optParameter.optMin;
            }
            set
            {
                optParameter.optMin = value;
            }
        }

        public string optMax
        {
            get
            {
                return optParameter.optMax;
            }
            set
            {
                optParameter.optMax = value;
            }
        }

        public int optMinMaxComboItem
        {
            get
            {
                return optParameter.optMinMaxIndex;
            }
            set
            {
                optParameter.optMinMaxIndex = value;
            }
        }

        public int optSumAvgIndex
        {
            get
            {
                return optParameter.optSumAvgIndex;
            }
            set
            {
                optParameter.optSumAvgIndex= value;
            }
        }

        public int optPCol
        {
            get
            {
                return optParameter.parameterCol;
            }
            set
            {
                optParameter.parameterCol = value;
            }

        }

        private string QSUser;

        public string QSuser
        {
            get
            {
                return QSUser;
            }
            set
            {
                QSUser = value;
            }
        }

        public bool usingSQLServer
        {
            get
            {
                return isSQLServer;
            }
            set
            {
                isSQLServer = true;
            }
        }

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            //Sheets shts = Globals.ThisAddIn.Application.Worksheets;

            //Workbook bk = null;
            //MessageBox.Show(bks.Count.ToString());
            //Sheets mySheets = null;
            //Worksheet sheet = null; 
            //Range myRange = null;

            //shts.Application.SheetBeforeDoubleClick += 
            bks.Application.SheetSelectionChange += new  AppEvents_SheetSelectionChangeEventHandler(Application_SheetSelectionChange);

            //shts.Application.SheetBeforeDoubleClick += new AppEvents_SheetBeforeDoubleClickEventHandler(Application_SheetBeforeDoubleClick);
            bks.Application.SheetBeforeDoubleClick += new AppEvents_SheetBeforeDoubleClickEventHandler(Application_SheetBeforeDoubleClick);
            
            bks.Application.WorkbookOpen += new AppEvents_WorkbookOpenEventHandler(Application_WorkbookOpen);
            bks.Application.OnKey("^.", "bondSwapSellSide");
            bks.Application.OnKey("^,", "bondSwapBuySide");

            usingSQLServer = true;
           
        }

 
        void Application_WorkbookOpen(Workbook Wb)
        {
            //MessageBox.Show("HELLO");
            //Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            // MessageBox.Show(Wb.Name);
            // MessageBox.Show(bk.Name);
            try
            {
                //Wb.Application.Run("RegisterCallback", Wb);
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }


        void Application_SheetSelectionChange(object Sh, Range Target)
        {
            //MessageBox.Show( Target.Value );  
        }

        void Application_SheetBeforeDoubleClick(object Sh,  Range Target,ref Boolean Cancel)
        {
            string myString;
            string myMin;
            string myMax;

            //MessageBox.Show(Target.Worksheet.Name.ToString());
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;
            sheet = (Worksheet)bk.ActiveSheet;
            
            if ((Target.Worksheet.Name.ToString().Equals("Optimization_Model Sell") || Target.Worksheet.Name.ToString().Equals("Optimization_Model Buy")) && Target.Row == 6 && Target.Column>=11)
            {
                optMin = "";
                optMax = "";
                optMinMaxComboItem = 0;

                optPCol = Target.Column;

                myString = sheet.Cells[1, Target.Column].Text;

                if (myString.ToUpper().Equals("MIN"))
                {
                    optMinMaxComboItem = 0;
                    optMin = sheet.Cells[2, Target.Column].Text;
                    optMax = "0";
                }

                if (myString.ToUpper().Equals("MAX"))
                {
                    optMinMaxComboItem = 1;
                    optMax = sheet.Cells[2, Target.Column].Text;
                    optMin = "0";
                }

                if (myString.ToUpper().Contains("RANGE"))
                    optMinMaxComboItem = 2;

                if (myString.ToUpper().Contains("SIZE") || myString.ToUpper().Contains("TOTAL"))
                    optMinMaxComboItem = 3;

                myString = sheet.Cells[2, Target.Column].Text;

                if (myString.Contains("|"))
                {
                    myMin = myString.Substring(0, myString.IndexOf("|"));
                    myMax = myString.Substring(myString.IndexOf("|") + 1);
                    optMin = myMin;
                    optMax = myMax;
                }

                myString = sheet.Cells[3, Target.Column].Text;

                if (myString.ToUpper().Contains("SUM"))
                    optSumAvgIndex = 0;

                if (myString.ToUpper().Contains("AVG"))
                    optSumAvgIndex = 1;

                showOptimizerParameters(Target.Value);
                Cancel=true;
            }


            if (Target.Worksheet.Name.ToString().Equals("CD Inventory")  && Target.Row == 6 && Target.Column >= 5)
            {
                optMin = "";
                optMax = "";
                optMinMaxComboItem = 0;

                optPCol = Target.Column;

                myString = sheet.Cells[1, Target.Column].Text;

                if (myString.ToUpper().Equals("MIN"))
                {
                    optMinMaxComboItem = 0;
                    optMin = sheet.Cells[2, Target.Column].Text;
                    optMax = "0";
                }

                if (myString.ToUpper().Equals("MAX"))
                {
                    optMinMaxComboItem = 1;
                    optMax = sheet.Cells[2, Target.Column].Text;
                    optMin = "0";
                }

                if (myString.ToUpper().Contains("RANGE"))
                    optMinMaxComboItem = 2;

                if (myString.ToUpper().Contains("SIZE") || myString.ToUpper().Contains("TOTAL"))
                    optMinMaxComboItem = 3;

                myString = sheet.Cells[2, Target.Column].Text;

                if (myString.Contains("|"))
                {
                    myMin = myString.Substring(0, myString.IndexOf("|"));
                    myMax = myString.Substring(myString.IndexOf("|") + 1);
                    optMin = myMin;
                    optMax = myMax;
                }

                myString = sheet.Cells[3, Target.Column].Text;

                if (myString.ToUpper().Contains("SUM"))
                    optSumAvgIndex = 0;

                if (myString.ToUpper().Contains("AVG"))
                    optSumAvgIndex = 1;

                showOptimizerParameters(Target.Value);
                Cancel = true;
            }


        }

        private void buttonGetCMO_Click(object sender, RibbonControlEventArgs e)
        {
            if (dropSector.SelectedItem.Label == "CMO")
                Globals.ThisAddIn.Application._Run2("OpenTemplate");
            // Globals.ThisAddIn.Application._Run2("DA_CMO_INV");

            //Globals.ThisAddIn.Application._Run2("CMO_INV");
        }

        private void buttonPriceRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet;


            //if ((Regex.Match(bk.Name.ToString(), @"CMO_inventory").Success ||
            // Regex.Match(bk.Name.ToString(), @"CMO_inventory\d{8}").Success) && sheet.Name.Equals("CMO INV"))
            //    Globals.ThisAddIn.Application._Run2("priceRefreshCMO_INV");

            if (sheet.Name.Equals("CMO INV"))
                Globals.ThisAddIn.Application._Run2("priceRefreshCMO_INV");

            
        }

        private void buttonOpenInventory_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("OpenInventory");
        }

        private void buttonBloomYT_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet;

            //if ( Regex.Match(bk.Name.ToString(), @"CMO_inventory").Success  && sheet.Name.Equals("CMO INV"))
            //    Globals.ThisAddIn.Application._Run2("InvBloomB");

            if (sheet.Name.Equals("CMO INV"))
                Globals.ThisAddIn.Application._Run2("InvBloomB");

            if (sheet.Name.Equals("MBS INV"))
                Globals.ThisAddIn.Application._Run2("InvBloomB");

        }

        private void clientInfoButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                //cInfo.user = uLogIn.userID;
                //cInfo.password = uLogIn.password;
                //cInfo.fillClientDropDown();
                //cInfo.Show();
            }
            else
            {
                uLogIn.Show();
            }

        }

        private void LoginButton_Click(object sender, RibbonControlEventArgs e)
        {
            uLogIn.verifyUser();  //this verifys without displaying login screen.  No need with SQL Server windows authentication

           // after DEMO uLogIn.Show(); //use this if user must enter userID and password

            //bool vbl;
            //vbl = uLogIn.testRun();
            //MessageBox.Show(vbl.ToString());
        }

        public void fillEquityAcctDropDown()
        {
            string user = Environment.UserName.ToString();

            if (usingSQLServer == false)
            { }

            if (usingSQLServer==true)
            {

                equityAcctdropDown.Items.Clear();

                RibbonDropDownItem rbnNew = this.Factory.CreateRibbonDropDownItem();
                rbnNew.Label = "New Account";
                equityAcctdropDown.Items.Add(rbnNew);

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader rdr;

                cmd.CommandText = "SELECT distinct acct_account FROM EQUITY_ACCOUNT where acct_user='" + user.ToString() + "';";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = rdr.GetValue(0).ToString();
                    equityAcctdropDown.Items.Add(rbnItem);
                }

                rdr.Close();
                cn.Close();
            }

        }

        public void fillEquityUserDropDown()
        {
            //EquityAcctOwnerdropDown
            string user = Environment.UserName.ToString();

            if (usingSQLServer==false)
            { }

            if (usingSQLServer==true)
            {
                EquityAcctOwnerdropDown.Items.Clear();

                RibbonDropDownItem rbnNew = this.Factory.CreateRibbonDropDownItem();
                rbnNew.Label = "New User";
                EquityAcctOwnerdropDown.Items.Add(rbnNew);
                
                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader rdr;

                cmd.CommandText = "select usr_id from equity_user;";
                try
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                        rbnItem.Label = rdr.GetValue(0).ToString();
                        EquityAcctOwnerdropDown.Items.Add(rbnItem);
                    }
                    rdr.Close();
                }
                catch(Exception ex)
                {

                }

                
                cn.Close();

            }

        }
        public void fillPortfolioDropDown()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                createPMdetail();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct PW_CLIENT from PW_SECURITYDETAIL "
                   + " ORDER BY PW_CLIENT;";

                Rdr = cmd.ExecuteReader();
                PortfoliodropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    PortfoliodropDown.Items.Add(rbnItem);
                }
                Rdr.Close();
                cn.Close();
            }

        }

        public void fillAsOfDropDown()
        {
            string clientName;
            clientName = "";

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMdetail();
                

                clientName = PortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct PW_ASOF_DATE from PW_SECURITYDETAIL WHERE PW_CLIENT='" + clientName
                   + "' ORDER BY PW_ASOF_DATE;";

                Rdr = cmd.ExecuteReader();
                PortfolioDatedropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    PortfolioDatedropDown.Items.Add(rbnItem);
                }

                cn.Close();
            }

        }
        public void fillReportingPortfolioDropDown()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                createPMdetail();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct InstName from SECURITY_INSTRUMENTS where broker='" + QSuser.ToString()
                    + "' ORDER BY InstName;";

                Rdr = cmd.ExecuteReader();
                ReportingPortfoliosdropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    ReportingPortfoliosdropDown.Items.Add(rbnItem);
                }

                cn.Close();
            }

        }

        public void fillReportingPortfolioDropDown(string Catalog )
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                createPMdetail();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=" + Catalog + "; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct InstName from SECURITY_INSTRUMENTS where broker='" + QSuser.ToString()
                    + "' ORDER BY InstName;";

                Rdr = cmd.ExecuteReader();
                ReportingPortfoliosdropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    ReportingPortfoliosdropDown.Items.Add(rbnItem);
                }

                cn.Close();
            }

        }
        public void fillQS_AsOfDropDown()
        {
            string swapName;
            int i;
            i = 0;
            swapName = "";

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                //createPMdetail();

                //swapName = PortfoliodropDown.SelectedItem.ToString();
                //MessageBox.Show(QS_swapIDdropDown.SelectedItemIndex.ToString());

                swapName = QS_swapIDdropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                //cmd.CommandText = "select distinct PW_ASOF_DATE from PW_SECURITYDETAIL WHERE PW_CLIENT in ('" + swapName
                //   + "_B','" + swapName + "_S') ORDER BY PW_ASOF_DATE;";

                if (QS_swapIDdropDown.SelectedItemIndex == 0)
                {
                    if (QS_asOfdropDown.SelectedItemIndex <= 0 )
                    {
                        cmd.CommandText = "select distinct PW_SWAP_ASOF from PW_SWAPS WHERE PW_BROKER ='" + QSuser.ToString() + "' ORDER BY PW_SWAP_ASOF;";
                    }
                    else
                    {
                        cmd.CommandText = "select distinct PW_SWAP_ASOF from PW_SWAPS WHERE PW_BROKER ='" + QSuser.ToString()
                            + "' and PW_SWAP_ASOF='" + QS_asOfdropDown.SelectedItem.ToString() + "' ORDER BY PW_SWAP_ASOF;";

                    }

                }
                else
                {
                    cmd.CommandText = "select distinct PW_SWAP_ASOF from PW_SWAPS WHERE PW_SWAP_NAME ='" + swapName
                      + "' AND PW_BROKER ='" + QSuser.ToString() + "' ORDER BY PW_SWAP_ASOF;";

                }

                Rdr = cmd.ExecuteReader();
                QS_asOfdropDown.Items.Clear();

                RibbonDropDownItem fstItem = this.Factory.CreateRibbonDropDownItem();
                fstItem.Label = "Swap Date";
                QS_asOfdropDown.Items.Add(fstItem);

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    if (i == 0)
                        try
                        {
                            Globals.ThisAddIn.Application._Run2("QSasOf_SET", Rdr.GetValue(0).ToString());
                        }
                        catch
                        {

                        }

                    QS_asOfdropDown.Items.Add(rbnItem);
                    i++;
                }

                cn.Close();
            }

        }

        public string getSNLid()
        {
            string portName;
            string pricingDate;
            string SNL;

            portName = "";
            pricingDate = "";
            SNL = "";

            if (usingSQLServer == false)
            {
                //return SNL;
            }

            if (usingSQLServer == true)
            {
                //createPMdetail();

                try
                {
                    portName = ReportingPortfoliosdropDown.SelectedItem.ToString();
                }
                catch
                {
                    portName = "";
                }


                try
                {
                    pricingDate = ReportingPricingDatedropDown.SelectedItem.ToString();
                }
                catch
                {
                    pricingDate = "";
                }

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct SNLID from SECURITY_INSTRUMENTS WHERE InstName = '" + portName
                        + "' AND PORTDATE='" + pricingDate + "'  ORDER BY SNLID;";

                Rdr = cmd.ExecuteReader();

                Rdr.Read();
                try
                {
                    SNL = Rdr.GetValue(0).ToString();
                }
                catch
                {
                    SNL = "";
                }

                cn.Close();

                //return SNL;
            }

            return SNL;

        }

        public void fillReporting_PricingDateDropDown()
        {
            string portName;
            portName = "";

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                //createPMdetail();

                portName = ReportingPortfoliosdropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct convert(varchar(10), cast(PORTDATE as date), 101) AS PortDATE "
                    + " from SECURITY_INSTRUMENTS WHERE InstName in ('" + portName + "') ORDER BY PortDATE;";

                Rdr = cmd.ExecuteReader();
                ReportingPricingDatedropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    ReportingPricingDatedropDown.Items.Add(rbnItem);
                }

                cn.Close();
            }

        }

        public void fillFI_OptPortfolioDropDown(string DB)
        {
            DB = DB.ToUpper();

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMdetail();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=" + DB + "; Integrated Security=SSPI;");


                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                if ( DB.Equals("ZM_GALLAGHER"))
                {
                    cmd.CommandText = "select distinct PW_CLIENT from PW_SECURITYDETAIL "
                       + " ORDER BY PW_CLIENT;";

                } else
                {
                    cmd.CommandText = "select distinct SNLinstitution from rptInstrument "
                       + " ORDER BY SNLinstitution;";

                }

                Rdr = cmd.ExecuteReader();
                //PortfoliodropDown.Items.Clear();
                FI_OptPortfoliodropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    //PortfoliodropDown.Items.Add(rbnItem);
                    FI_OptPortfoliodropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }

        }
        public void fillFI_OptPortfolioDropDown()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMdetail();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct PW_CLIENT from PW_SECURITYDETAIL "
                   + " ORDER BY PW_CLIENT;";

                Rdr = cmd.ExecuteReader();
                //PortfoliodropDown.Items.Clear();
                FI_OptPortfoliodropDown.Items.Clear();
                
                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    //PortfoliodropDown.Items.Add(rbnItem);
                    FI_OptPortfoliodropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void fill_TemplatesDropDown()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMdetail();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct PW_TEMPLATE_NAME from PW_TEMPLATES  where pw_owner in ('SHARED','" + Environment.UserName + "') "
                   + " ORDER BY PW_TEMPLATE_NAME;";

                Rdr = cmd.ExecuteReader();
                TemplatesDropDown.Items.Clear();
                
                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    //PortfoliodropDown.Items.Add(rbnItem);
                    TemplatesDropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void fillFI_OptAsOfDropDown(string DB)
        {
            string clientName;
            clientName = "";

            DB = DB.ToUpper();

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMdetail();

                clientName = FI_OptPortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=" + DB + "; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                if (DB.Equals("ZM_GALLAGHER"))
                {
                    cmd.CommandText = "select distinct PW_ASOF_DATE from PW_SECURITYDETAIL WHERE PW_CLIENT='" + clientName
                       + "' ORDER BY PW_ASOF_DATE;";
                } else
                {
                    cmd.CommandText = "select distinct PORTDATE from RPTINSTRUMENT WHERE SNLinstitution='" + clientName
                       + "' ORDER BY PORTDATE;";
                }

                Rdr = cmd.ExecuteReader();
                //PortfolioDatedropDown.Items.Clear();
                FI_OptAsOfdropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    //PortfolioDatedropDown.Items.Add(rbnItem);
                    FI_OptAsOfdropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
                try
                {
                    Globals.ThisAddIn.Application._Run2("port_Date", FI_OptAsOfdropDown.SelectedItem.ToString());
                } catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
                
            }

        }

        public void fillFI_OptAsOfDropDown()
        {
            string clientName;
            clientName = "";

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMdetail();

                clientName = FI_OptPortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct PW_ASOF_DATE from PW_SECURITYDETAIL WHERE PW_CLIENT='" + clientName
                   + "' ORDER BY PW_ASOF_DATE;";

                Rdr = cmd.ExecuteReader();
                //PortfolioDatedropDown.Items.Clear();
                FI_OptAsOfdropDown.Items.Clear();

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();

                    //PortfolioDatedropDown.Items.Add(rbnItem);
                    FI_OptAsOfdropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void getSwapIDcount()
        {
            MessageBox.Show(QS_swapIDdropDown.Items.Count.ToString());
        }
        
        public void QS_swapIDDropDown_clear()
        {
            try
            {
                if (QS_swapIDdropDown.Items.Count > 0)
                {

                    MessageBox.Show(QS_swapIDdropDown.Items[2].ToString());
                }
                else
                {
                    MessageBox.Show("Hello");
                }

                // QS_swapIDdropDown.SelectedItemIndex = -1;
                //while (QS_swapIDdropDown.Items.Count > 0)
                //    QS_swapIDdropDown.Items.RemoveAt(0);

            }
            catch { }
        }
        public void fillQS_swapIDDropDown()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMswaps();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                //Environment.UserName.ToString()
                if (QS_asOfdropDown.SelectedItemIndex <=0)
                {
                    cmd.CommandText = "select distinct PW_SWAP_NAME from PW_SWAPS where PW_BROKER='" + QSuser.ToString() + "' "
                       + " ORDER BY PW_SWAP_NAME;";

                }
                else
                {
                    cmd.CommandText = "select distinct PW_SWAP_NAME from PW_SWAPS where PW_BROKER='" + QSuser.ToString() + "' "
                       + "  and PW_SWAP_ASOF='" + QS_asOfdropDown.SelectedItem.ToString() + "' ORDER BY PW_SWAP_NAME;";

                }

                Rdr = cmd.ExecuteReader();
                QS_swapIDdropDown.Items.Clear();

                while (QS_swapIDdropDown.Items.Count > 0)
                    QS_swapIDdropDown.Items.RemoveAt(0);

                RibbonDropDownItem fstItem = this.Factory.CreateRibbonDropDownItem();
                fstItem.Label = "Select Swap Name";
                QS_swapIDdropDown.Items.Add(fstItem);

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();
                    QS_swapIDdropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();

                // MessageBox.Show(QS_swapIDdropDown.Items[2].ToString());
            }
        }

        public void fill_userIDDropDown()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createPMswaps();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                //cmd.CommandText = "select distinct PW_BROKER from PW_SWAPS ORDER BY PW_BROKER;";
                cmd.CommandText = "select distinct pw_broker from pw_swaps   union all  " +
                    " select distinct broker from SECURITY_INSTRUMENTS where broker not in " +
                    " (select pw_broker from pw_swaps) order by pw_broker;";

                Rdr = cmd.ExecuteReader();
                UserdropDown.Items.Clear();

                RibbonDropDownItem fstItem = this.Factory.CreateRibbonDropDownItem();
                fstItem.Label = "Select User";
                UserdropDown.Items.Add(fstItem);

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();
                    UserdropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }
        }

        public void fillQS_rowTasksdropDown()
        {


            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                createQSrowTasks();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct QS_ROW_TASK from QS_ROW_TASKS order by QS_ROW_TASK;";

                Rdr = cmd.ExecuteReader();
                QS_rowTasksdropDown.Items.Clear();

                RibbonDropDownItem fstItem = this.Factory.CreateRibbonDropDownItem();
                fstItem.Label = "Select Task";
                QS_rowTasksdropDown.Items.Add(fstItem);

                while (Rdr.Read())
                {
                    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    rbnItem.Label = Rdr.GetValue(0).ToString();
                    QS_rowTasksdropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }
        }

        public void createPMswaps()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "IF OBJECT_ID (N'PW_SWAPS', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table PW_SWAPS (id INT IDENTITY(1,1) PRIMARY KEY, " +
                        "PW_BROKER VARCHAR(255), PW_SWAP_NAME VARCHAR(25), PW_SWAP_ASOF CHAR(8));";
                    cmd.ExecuteNonQuery();

                }
                else
                {
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void createPMissues()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "IF OBJECT_ID (N'PW_ISSUE_LOG', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table PW_ISSUE_LOG (id INT IDENTITY(1,1) PRIMARY KEY, PW_USER VARCHAR(55), " +
                        " PW_ISSUE VARCHAR(MAX), PW_ISSUE_DATE CHAR(8), PW_RESOLVED_DATE CHAR(8));";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void createQSrowTasks()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "IF OBJECT_ID (N'QS_ROW_TASKS', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table QS_ROW_TASKS (id INT IDENTITY(1,1) PRIMARY KEY, QS_USER VARCHAR(55), " +
                        " QS_ROW_TASK VARCHAR(255), QS_TASK_CODE VARCHAR(25));";
                    cmd.ExecuteNonQuery();

                }
                else
                {
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void createPMdetail()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "IF OBJECT_ID (N'PW_SECURITYDETAIL', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                //MessageBox.Show(Rdr.GetValue(0).ToString());

                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table PW_SECURITYDETAIL ([id] [int] IDENTITY(1,1) NOT NULL, [PW_CLIENT] [varchar](255) NULL, " +
                        " [PW_ASOF_DATE] [char](8) NULL, [PW_RECEIVED_DATE] [char](8) NULL, [PW_F115] [char](3) NULL, [PW_CUSIP] [char](9) NULL, " +
                        " [PS_ISMTGE] [char](1) NULL, [PW_SECTOR] [varchar](55) NULL, [PW_DESCRIPTION] [varchar](55) NULL, [PW_OFACE] [decimal](11, 2) NULL," +
                        " [PW_CFACE] [decimal](11, 2) NULL, [PW_BOOKPX] [decimal](7, 4) NULL, [PW_BOOKVALUE] [decimal](11, 2) NULL, " +
                        " [PW_MKTPX] [decimal](7, 4) NULL, [PW_MKTVALUE] [decimal](11, 2) NULL, [PW_COUPON] [decimal](6, 4) NULL, [PW_MAT_DATE] [char](8) NULL, " +
                        " [PW_call_date] [char](8) NULL, [PW_call_price] [decimal](7, 4) NULL, [PW_call_type] [varchar](55) NULL, " +
                        " [PW_bk_Yld_worst] [decimal](7, 4) NULL, [PW_bk_Yld_worst_teq] [decimal](7, 4) NULL, [PW_mkt_yld_worst_teq] [decimal](7, 4) NULL, " +
                        " [PW_profit_loss] [decimal](11, 2) NULL, [PW_avg_life] [decimal](7, 4) NULL, [PW_AVG_LIFE_D300] [decimal](7, 4) NULL, " +
                        " [PW_AVG_LIFE_D200] [decimal](7, 4) NULL, [PW_AVG_LIFE_D100] [decimal](7, 4) NULL, [PW_AVG_LIFE_U100] [decimal](7, 4) NULL, " +
                        " [PW_AVG_LIFE_U200] [decimal](7, 4) NULL, [PW_AVG_LIFE_U300] [decimal](7, 4) NULL, [PW_pct_book_gain] [decimal](11, 2) NULL, " +
                        " [PW_MOD_DUR] [decimal](7, 4) NULL, [PW_MOD_DUR_D300] [decimal](7, 4) NULL, [PW_MOD_DUR_D200] [decimal](7, 4) NULL, " +
                        " [PW_MOD_DUR_D100] [decimal](7, 4) NULL, [PW_MOD_DUR_U100] [decimal](7, 4) NULL, [PW_MOD_DUR_U200] [decimal](7, 4) NULL, " +
                        " [PW_MOD_DUR_U300] [decimal](7, 4) NULL, [PW_EFF_DUR] [decimal](7, 4) NULL, [PW_USER_DEFINED] [varchar](255) NULL, " +
                        " [PW_PORTID] [varchar](55) NULL, [PW_MKTPX_D300] [decimal](7, 4) NULL, [PW_MKTPX_D200] [decimal](7, 4) NULL, " +
                        " [PW_MKTPX_D100] [decimal](7, 4) NULL, [PW_MKTPX_U100] [decimal](7, 4) NULL, [PW_MKTPX_U200] [decimal](7, 4) NULL, " +
                        " [PW_MKTPX_U300] [decimal](7, 4) NULL, [PW_MKTPX_BASE] [decimal](7, 4) NULL, [PW_MAT_DATE_D300] [char](8) NULL, " +
                        " [PW_MAT_DATE_D200] [char](8) NULL, [PW_MAT_DATE_D100] [char](8) NULL, [PW_MAT_DATE_U100] [char](8) NULL, " +
                        " [PW_MAT_DATE_U200] [char](8) NULL, [PW_MAT_DATE_U300] [char](8) NULL, [PW_MKTVALUE_D300] [decimal](14, 2) NULL, " +
                        " [PW_MKTVALUE_D200] [decimal](14, 2) NULL, [PW_MKTVALUE_D100] [decimal](14, 2) NULL, [PW_MKTVALUE_U100] [decimal](14, 2) NULL, " +
                        " [PW_MKTVALUE_U200] [decimal](14, 2) NULL, [PW_MKTVALUE_U300] [decimal](14, 2) NULL, [PW_MKTPX_CHNG_D300] [decimal](7, 4) NULL, " +
                        " [PW_MKTPX_CHNG_D200] [decimal](7, 4) NULL, [PW_MKTPX_CHNG_D100] [decimal](7, 4) NULL, [PW_MKTPX_CHNG_U100] [decimal](7, 4) NULL, " +
                        " [PW_MKTPX_CHNG_U200] [decimal](7, 4) NULL, [PW_MKTPX_CHNG_U300] [decimal](7, 4) NULL, " +
                        " [PW_BKPX_D300] [decimal](7, 4) NULL, [PW_BKPX_D200] [decimal](7, 4) NULL, [PW_BKPX_D100] [decimal](7, 4) NULL, [PW_BKPX_U100] [decimal](7, 4) NULL," +
                        " [PW_BKPX_U200] [decimal](7, 4) NULL, [PW_BKPX_U300] [decimal](7, 4) NULL, [PW_MKTYLD_D300] [decimal](9, 5) NULL, " +
                        " [PW_MKTYLD_D200] [decimal](9, 5) NULL, [PW_MKTYLD_D100] [decimal](9, 5) NULL, [PW_MKTYLD_U100][decimal](9, 5) NULL, " +
                        " [PW_MKTYLD_U200] [decimal](9, 5) NULL, [PW_MKTYLD_U300] [decimal](9, 5) NULL, [PW_BKYLD_D300] [decimal](9, 5) NULL, " +
                        " [PW_BKYLD_D200] [decimal](9, 5) NULL, [PW_BKYLD_D100] [decimal](9, 5) NULL, [PW_BKYLD_U100] [decimal](9, 5) NULL, [PW_BKYLD_U200] [decimal](9, 5) NULL, [PW_BKYLD_U300] [decimal](9, 5) NULL, [PW_MTG_POOL] [varchar](25) NULL, " +
                        " [PW_CPN_TYP] [varchar](25) NULL, [PW_SECURITY_TYP] [varchar](25) NULL, [PW_WALA] [varchar](15) NULL, [PW_INT_ACCR] [decimal](14, 2) NULL, " +
                        " [PW_WAM] [decimal](4, 0) NULL, [PW_CPR_3MO] [varchar](10) NULL, [PW_CPR_LIFE] [varchar](10) NULL, [PW_ISSUE_DATE] [char](8) NULL, " +
                        " [PW_EFF_MAT_DATE] [char](8) NULL, [PW_NXT_CPN_DATE] [char](8) NULL, [PW_CPN_RESET_DATE] [char](8) NULL, [PW_CPN_FREQ] [decimal](2, 0) NULL, " +
                        " [PW_REM_TERM2] [decimal](6, 2) NULL, [PW_WAC] [decimal](6, 4) NULL, [PW_MOODY] [varchar](15) NULL, [PW_SP] [varchar](15) NULL, " +
                        " [PW_INDUSTRY] [varchar](25) NULL, [PW_INDEX_VAL] [decimal](7, 4) NULL, [PW_INDEX] [varchar](25) NULL, [PW_INDEX_FORMULA] [varchar](255) NULL, " +
                        " [PW_CPN_FLOOR] [decimal](7, 4) NULL, [PW_CPN_CAP] [decimal](7, 4) NULL, [PW_YLD_MAINT] [int] NULL, [PW_CPN_RESET_FREQ] [decimal](2, 0) NULL, " +
                        " [PW_MTG_TYP] [varchar](10) NULL, [PW_LINKED_ID] [int] NULL, [PW_IS_TAXABLE] [char](1) NULL, [PW_COLLAT_TYPE] [varchar](55) NULL, " +
                        " [PW_BASE_MTKYLD] [decimal](8, 4) NULL, [PW_BASE_BKYLD] [decimal](8, 4) NULL, [PW_EFF_CONV] [decimal](8, 4) NULL, [PW_IS_CALLABLE] [char](1) NULL, [PW_ISMTGE] [char](1) NULL, " +
                        " [PW_INSWAP] [char](1) NULL, [PW_ALT_ID] [VARCHAR](25), [PW_FACTOR] [DECIMAL](8,5), " +
                        " PRIMARY KEY CLUSTERED ( [id] Asc )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, " +
                        " ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];";
                    cmd.ExecuteNonQuery();

                }
                else
                {
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void createPMCash()
        {

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                cmd.CommandText = "IF OBJECT_ID (N'PW_CASHFLOW', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                // MessageBox.Show(Rdr.GetValue(0).ToString());
                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table PW_CASHFLOW (id INT IDENTITY(1,1) PRIMARY KEY, PW_CUSIP CHAR(9), PW_CASHDATE CHAR(8), " +
                        " PW_SCENARIO VARCHAR(55), PW_PRIN_AMT DECIMAL(11,2), PW_INT_AMT DECIMAL(11,2), PW_PRIN_BAL DECIMAL(11,2), PW_ID INT);";
                    cmd.ExecuteNonQuery();

                }
                else
                {
                }


                Rdr.Close();

                cmd.CommandText = "IF OBJECT_ID (N'PW_CASHFLOW_SETUP', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table PW_CASHFLOW_SETUP (id INT IDENTITY(1,1) PRIMARY KEY, PW_CUSIP CHAR(9), PW_SCENARIO VARCHAR(55), " +
                        " PW_PP_TYPE VARCHAR(3), PW_PP_SPEED DECIMAL(7,2), PW_PP_MATRIX VARCHAR(255), PW_ID INT);";
                    cmd.ExecuteNonQuery();

                }
                else
                {
                }


                Rdr.Close();

                cn.Close();
            }

        }

        public void fillFI_OptSNL_ID()
        {
            string clientName;
            clientName = "";

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                clientName = FI_OptPortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "IF OBJECT_ID (N'PW_BROKER', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                //MessageBox.Show(Rdr.GetValue(0).ToString());
                if (Rdr.GetValue(0).ToString() == "0")
                {
                    Rdr.Close();
                    cmd.CommandText = "create table PW_BROKER (id INT IDENTITY(1,1) PRIMARY KEY, " +
                        " PW_BROKER VARCHAR(255), PW_CLIENT VARCHAR(255), PW_CLIENTID VARCHAR(25), PW_ASOF_DATE CHAR(8), PW_SNL_ID CHAR(7), " +
                        " PW_ASSET_GROUP_MIN INT,  PW_ASSET_GROUP_MAX INT);";
                    cmd.ExecuteNonQuery();

                }
                else
                {
                }

                Rdr.Close();
                cmd.CommandText = "select distinct PW_SNL_ID, PW_ASSET_GROUP_MIN,  PW_ASSET_GROUP_MAX " +
                    " from PW_BROKER WHERE PW_CLIENT='" + clientName + "';";

                Rdr = cmd.ExecuteReader();
                //PortfolioDatedropDown.Items.Clear();
                SNLIDeditBox.Text = "";

                //Rdr.Read();
                while (Rdr.Read())
                {
                    //RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    //rbnItem.Label = Rdr.GetValue(0).ToString();
                    SNLIDeditBox.Text = Rdr.GetValue(0).ToString();
                    AssetGrpMineditBox.Text = Rdr.GetValue(1).ToString();
                    AssetGrpMaxeditBox.Text = Rdr.GetValue(2).ToString();

                    //PortfolioDatedropDown.Items.Add(rbnItem);
                    //FI_OptAsOfdropDown.Items.Add(rbnItem);
                }

                Rdr.Close();
                cn.Close();
            }

        }

        public void fillFI_OptSNL_ID(string DB)
        {
            string clientName;
            clientName = "";

            DB = DB.ToUpper();

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                clientName = FI_OptPortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=" + DB + "; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                if (DB.Equals("ZM_GALLAGHER"))
                {
                    cmd.CommandText = "IF OBJECT_ID (N'PW_BROKER', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                    Rdr = cmd.ExecuteReader();
                    Rdr.Read();
                    //MessageBox.Show(Rdr.GetValue(0).ToString());
                    if (Rdr.GetValue(0).ToString() == "0")
                    {
                        Rdr.Close();
                        cmd.CommandText = "create table PW_BROKER (id INT IDENTITY(1,1) PRIMARY KEY, " +
                            " PW_BROKER VARCHAR(255), PW_CLIENT VARCHAR(255), PW_CLIENTID VARCHAR(25), PW_ASOF_DATE CHAR(8), PW_SNL_ID CHAR(7), " +
                            " PW_ASSET_GROUP_MIN INT,  PW_ASSET_GROUP_MAX INT);";
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                    }
                    Rdr.Close();

                    cmd.CommandText = "select distinct PW_SNL_ID, PW_ASSET_GROUP_MIN,  PW_ASSET_GROUP_MAX " +
                    " from PW_BROKER WHERE PW_CLIENT='" + clientName + "';";

                    Rdr = cmd.ExecuteReader();
                    //PortfolioDatedropDown.Items.Clear();
                    SNLIDeditBox.Text = "";

                    //Rdr.Read();
                    while (Rdr.Read())
                    {
                        //RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                        //rbnItem.Label = Rdr.GetValue(0).ToString();
                        SNLIDeditBox.Text = Rdr.GetValue(0).ToString();
                        AssetGrpMineditBox.Text = Rdr.GetValue(1).ToString();
                        AssetGrpMaxeditBox.Text = Rdr.GetValue(2).ToString();

                        try
                        {
                            Globals.ThisAddIn.Application._Run2("SNLID_SET", Rdr.GetValue(0).ToString());
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show(er.ToString());
                        }
                        
                        //PortfolioDatedropDown.Items.Add(rbnItem);
                        //FI_OptAsOfdropDown.Items.Add(rbnItem);
                    }

                    Rdr.Close();
                }
                else
                {
                    // a ZM system database
                    cmd.CommandText = "select distinct SNLID from rptInstrument where SNLinstitution='" + FI_OptPortfoliodropDown.SelectedItem.ToString() + "';";
                    Rdr = cmd.ExecuteReader();
                    SNLIDeditBox.Text = "";

                    Rdr.Read();
                    SNLIDeditBox.Text = Rdr.GetValue(0).ToString();
                    try
                    {
                        Globals.ThisAddIn.Application._Run2("SNLID_SET", Rdr.GetValue(0).ToString());
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.ToString());
                    }

                    Rdr.Close();

                    cmd.CommandText = "select Cost_of_Funds from SNLBankData where SNL_Institution_Key='" + SNLIDeditBox.Text.ToString() + "';";
                    Rdr = cmd.ExecuteReader();

                    Rdr.Read();
                    if (Rdr.HasRows)
                    {
                        optTefraeditBox.Text = Rdr.GetValue(0).ToString();
                        if (OptimizationDataBasesdropDown.SelectedItem.ToString().ToUpper().Equals("ZM_HOURIET"))
                        {
                            try
                            {
                                Globals.ThisAddIn.Application._Run2("tefra", optTefraeditBox.Text.ToString());
                            } catch (Exception er)
                            {
                                MessageBox.Show(er.ToString());
                            }
                            
                        }
                        if (OptimizationDataBasesdropDown.SelectedItem.ToString().ToUpper().Equals("ZM_PIGG"))
                        {
                            try
                            {
                                Globals.ThisAddIn.Application._Run2("COF", optTefraeditBox.Text.ToString());
                            } catch (Exception er)
                            {
                                MessageBox.Show(er.ToString());
                            }
                            
                        }
                    }

                    Rdr.Close();
                }

                cn.Close();
            }

        }


        public bool equity_checkForSetup()
        {
            
            if (usingSQLServer == false)
            {

            }
            if ( usingSQLServer==true)
            {

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" + "Initial Catalog=ZM_GALLAGHER; Integrated security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = cn.CreateCommand();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                SqlDataReader rdr;

                cmd.CommandText = "IF OBJECT_ID (N'EQUITY_TICKET',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";
                rdr = cmd.ExecuteReader();
                rdr.Read();
                if (rdr.GetValue(0).ToString()=="0")
                {
                    equityExists = false;
                }
                else
                {
                    equityExists = true;
                }

            }

            return equityExists;
        }

        
        public void fillFI_OptSNL_ID(string DB, string pDate)
        {
            string clientName;
            clientName = "";

            DB = DB.ToUpper();

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                clientName = FI_OptPortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                   "Initial Catalog=" + DB + "; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                if (DB.Equals("ZM_GALLAGHER"))
                {
                    cmd.CommandText = "IF OBJECT_ID (N'PW_BROKER', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res;";
                    Rdr = cmd.ExecuteReader();
                    Rdr.Read();
                    //MessageBox.Show(Rdr.GetValue(0).ToString());
                    if (Rdr.GetValue(0).ToString() == "0")
                    {
                        Rdr.Close();
                        cmd.CommandText = "create table PW_BROKER (id INT IDENTITY(1,1) PRIMARY KEY, " +
                            " PW_BROKER VARCHAR(255), PW_CLIENT VARCHAR(255), PW_CLIENTID VARCHAR(25), PW_ASOF_DATE CHAR(8), PW_SNL_ID CHAR(7), " +
                            " PW_ASSET_GROUP_MIN INT,  PW_ASSET_GROUP_MAX INT);";
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                    }
                    Rdr.Close();

                    cmd.CommandText = "select distinct PW_SNL_ID, PW_ASSET_GROUP_MIN,  PW_ASSET_GROUP_MAX " +
                    " from PW_BROKER WHERE PW_CLIENT='" + clientName + "';";

                    Rdr = cmd.ExecuteReader();
                    //PortfolioDatedropDown.Items.Clear();
                    SNLIDeditBox.Text = "";
                    
                    //Rdr.Read();
                    while (Rdr.Read())
                    {
                        //RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                        //rbnItem.Label = Rdr.GetValue(0).ToString();
                        SNLIDeditBox.Text = Rdr.GetValue(0).ToString();
                        AssetGrpMineditBox.Text = Rdr.GetValue(1).ToString();
                        AssetGrpMaxeditBox.Text = Rdr.GetValue(2).ToString();

                        //PortfolioDatedropDown.Items.Add(rbnItem);
                        //FI_OptAsOfdropDown.Items.Add(rbnItem);
                    }

                    Rdr.Close();
                } else
                {
                    // a ZM system database
                    cmd.CommandText = "select distinct SNLID from rptInstrument where SNLinstitution='" + FI_OptPortfoliodropDown.SelectedItem.ToString() + "' and portDate ='" + pDate + "';";
                    Rdr = cmd.ExecuteReader();
                    SNLIDeditBox.Text = "";

                    Rdr.Read();
                    SNLIDeditBox.Text = Rdr.GetValue(0).ToString();
                    Rdr.Close();
                }
                
                cn.Close();
            }

        }

        private void profileButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                //uProfile.userId = uLogIn.userID;
                //uProfile.userPassword = uLogIn.password;

                //uProfile.Show();
            }
            else
            {
                uLogIn.Show();
            }
        }

        private void dropSector_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void passResetButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                //uPass.userID= uLogIn.userID;
                //uPass.userPassword = uLogIn.password;
                //uPass.Text = "Reset Password for " + uPass.userID;
                //uPass.Show();
            }
            else
            {
                uLogIn.Show();
            }
        }

        private void bondFinderButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                bFinder.userID = uLogIn.userID;
                bFinder.userPassword = uLogIn.password;
                
                //bFinder.fillSavedSearchesCombo();  
                //bFinder.fillCMOcriteraiList();

                //bFinder.fillClientcriteraiList();


                ////bFinder.fillMBSsearchesComboBox();
                //bFinder.fillMBSTypeList();//***   MAYBE TEMP  ***
                ////bFinder.fillMBSClientList();
                try
                {
                    bFinder.Show();
                }
                catch
                {
                    bFinder = new bondFinder();
                    bFinder.Show();
                }
            }
            else
            {
                uLogIn.Show();
            }

        }

        private void editUserButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                //uAdmin.userID = uLogIn.userID;
                //uAdmin.userPassword = uLogIn.password;
                //uAdmin.fillUserListDropDown();
                //uAdmin.Show();
            }
            else
            {
                uLogIn.Show();
            }

        }

        private void monthlyPackageButton_Click(object sender, RibbonControlEventArgs e)
        {
            //MessageBox.Show(crtUser.password);
            if (uLogIn.userConnected == true)
            {
                uStrategy.userID = uLogIn.userID;
                //uStrategy.userPassword = uLogIn.password;
                //uStrategy.fillMonthlyClientDropDown();
                //uStrategy.fillMonthlyPortfolioDropDown();
                ////cInfo.fillClientDropDown();
                uStrategy.Show();
            }
            else
            {
                uLogIn.Show();
            }
        }


        private void runMonthly()
        {

        }

        public void enableButtons()
        {

            if (uLogIn.userConnected == true)
            {
                // MessageBox.Show(uLogIn.userID.ToString());
                SingleSecuritycheckBox.Visible = true;
                SingleSecuritycheckBox.Checked = true;
                Issues_onoffcheckBox.Visible = true;
                Issues_onoffcheckBox.Checked = true;
                Issuesgroup.Visible = true;
                QuickSwapgroup.Visible = true;
                QuickSwapcheckBox.Visible = true;
                QuickSwapcheckBox.Checked = true;
                ZMinventorycheckBox.Visible = true;
                ZMinventorycheckBox.Checked = true;
                pullBloomBergcheckBox.Checked = true;
                showSubTotalscheckBox.Checked = true;

                QSuser = Environment.UserName.ToString();
                fillQS_swapIDDropDown();
                fillQS_rowTasksdropDown();
                QS_useAltcheckBox.Visible = true;

                Reportinggroup.Visible = true;
                fillReportingPortfolioDropDown();
                templatesGroupcheckBox.Visible = true;
                //TemplatesGroup.Visible = true;
                fill_TemplatesDropDown();

                //BondModelingButton.Enabled =true;
                // dropSector.Enabled = true;
                // buttonPublicHolders.Enabled = true;
                // buttonDavidsonMatchers.Enabled = true;
                // buttonPublicHolders.Visible=true;
                // //buttonGetCMO.Enabled = true;
                // buttonPriceRefresh.Enabled = true;
                // buttonOpenInventory.Enabled = true;
                // buttonBloomYT.Enabled = true;
                //Inventories.Visible = true;
                bondFinderButton.Enabled = true;
                // bondSheetMatchersbutton.Visible = true;

                ////===BRING OTHERS ON AS DEVELOPED AS OF 12/2/2013 ONLY CMO INVENTORIES
                ////clientInfoButton.Enabled = true;
                ////buttonClientEmail.Enabled = true;
                ////buttonClientPDF.Enabled = true;
                // inventoriesGroup.Visible = true;
                if (Environment.UserName.ToUpper().Equals("BRENT.GALLAGHER"))
                {
                    StreetInventoriescheckBox.Visible = true;
                    tabEquities.Visible = true;
                    if (equity_checkForSetup() == false)
                    {
                        equityCreateTablesbutton.Visible = true;
                        equitySetupgroup.Visible = true;
                    }
                    else
                    {
                        equityCreateTablesbutton.Visible = false;
                        equitySetupgroup.Visible =true;
                    }

                    fillEquityAcctDropDown();
                    fillEquityUserDropDown();
                }

                if (Environment.UserName.ToUpper().Equals("BRENT.GALLAGHER") || 1==1)
                {
                    tabStrategy.Visible = true;
                    AnalyticsGroup.Visible =false;

                    fill_userIDDropDown();

                    if (Environment.UserName.ToUpper().Equals("BRENT.GALLAGHER"))
                        UserdropDown.Visible = true;

                    //monthlyPackageButton.Enabled = true;
                    //AnalyticsCommonTasksdropDown.Enabled = true;
                    //StrategyTaskButton.Enabled = true;
                    //BondModelingButton.Enabled = true;
                    //menuBondSwap.Enabled = true;

                    //inventoriesGroup.Visible = true;
                    //pullMatchersbutton.Enabled = true;
                    //mappedSectordropDown.Enabled = true;
                    //callblmMappedFieldsbutton.Enabled = true;
                    //bloombergMapperbutton.Enabled = true;
                    //bloomMappedFieldscomboBox.Enabled = true;

                    //clientMngmtGroup.Visible = true;

                    PM_group.Visible = true;
                    fillPortfolioDropDown();
                    fillQS_AsOfDropDown();
                    fillAsOfDropDown();
                    
                    tabOptimization.Visible = true;
                    StrategiesOptgroup.Visible = true;
                    fillFI_OptPortfolioDropDown();
                    fillFI_OptAsOfDropDown();
                    fillFI_OptSNL_ID();

                    resolve_Issuebutton.Visible = true;
                }



                //menuBondSwap.Enabled = true;

                //BondModelingButton.Enabled = true;

                ////SINGLE SECURITY CONTROLS

                singleSecurityGroup.Visible = true;
                cusipEditBox.Enabled = true;
                priceEditBox.Enabled = true;
                settlementEditBox.Enabled = true;
                updateCusipButton.Enabled = true;
                updateGraphButton.Enabled = true;
                PDFButton.Enabled = true;
                EmailButton.Enabled = true;
                singleSectorDropDown.Enabled = true;
                bloomieDropDown.Enabled = true;
                originationEditBox.Enabled = true;


                //dropDownBloomFunctions.Enabled = true;
                //buttonRunBloomCall.Enabled = true;
                //groupCommonBloomberg.Visible = true;

                ////BWIC 
                //BWICgroup.Visible = true;
                //BWICrowRefreshbutton.Visible = true;



                ////USER TEMPLATES
                //userTemplateDropDown.Enabled = true;
                ////userTemplateNameEditBox.Enabled = true;
                //UserTemplateSheetsDropDown.Enabled = true;
                //HeaderRowEditBox.Enabled = true;
                //IDColumnEditBox.Enabled = true;
                //uTemplate.fillUserTemplates();
                //saveUserSheetbutton.Enabled = true;
                //saveUserTemplatebutton.Enabled = true;

                //if (uLogIn.userRole.Equals("G"))
                //   AdministratorGroupBox.Visible = true;
                //else
                //   AdministratorGroupBox.Visible = false;

                ////CALLABLE BONDS
                //checkBox_FHLB.Enabled = true;
                //checkBox_FNMA.Enabled = true;
                //checkBox_FHLMC.Enabled = true;
                //checkBox_Searched.Enabled = true;
                //button_runCalledBonds.Enabled = true;

            }
            else
            {
                Issuesgroup.Visible = false;
                Reportinggroup.Visible = false;
                clientInfoButton.Enabled = false;
                buttonClientEmail.Enabled = false;
                buttonClientPDF.Enabled = false;
                monthlyPackageButton.Enabled = false;
                menuBondSwap.Enabled = false;
                dropSector.Enabled = false;
                //buttonGetCMO.Enabled = false;
                buttonPriceRefresh.Enabled = false;
                buttonOpenInventory.Enabled = false;
                buttonBloomYT.Enabled = false;
                bondFinderButton.Enabled = false;
                BondModelingButton.Enabled = false;
                buttonPublicHolders.Enabled = false;
                buttonDavidsonMatchers.Enabled = false;
                bondSheetMatchersbutton.Visible = false;

                AdministratorGroupBox.Visible = false;

                monthlyPackageButton.Enabled = false;
                AnalyticsCommonTasksdropDown.Enabled = false;
                StrategyTaskButton.Enabled = false;

                showSubTotalscheckBox.Checked = false;
                pullBloomBergcheckBox.Checked = false;

                //SINGLE SECURITY CONTROLS
                cusipEditBox.Enabled = false;
                priceEditBox.Enabled = false;
                settlementEditBox.Enabled = false;
                updateCusipButton.Enabled = false;
                updateGraphButton.Enabled = false;
                PDFButton.Enabled = false;
                EmailButton.Enabled = false;
                singleSectorDropDown.Enabled = false;
                bloomieDropDown.Enabled = false;
                originationEditBox.Enabled = false;
                singleSecurityGroup.Visible = false;

                //USER TEMPLATES
                userTemplateDropDown.Enabled = false;
                userTemplateNameEditBox.Enabled = false;
                UserTemplateSheetsDropDown.Enabled = false;
                HeaderRowEditBox.Enabled = false;
                IDColumnEditBox.Enabled = false;
                saveUserSheetbutton.Enabled = false;
                saveUserTemplatebutton.Enabled = false;

                //CALLABLE BONDS
                checkBox_FHLB.Enabled = false;
                checkBox_FNMA.Enabled = false;
                checkBox_FHLMC.Enabled = false;
                checkBox_Searched.Enabled = false;
                button_runCalledBonds.Enabled = false;

                dropDownBloomFunctions.Enabled = false;
                buttonRunBloomCall.Enabled = false;

                inventoriesGroup.Visible = false;
                tabStrategy.Visible = false;

                UserdropDown.Visible =false;
                templatesGroupcheckBox.Visible = false;

                tabEquities.Visible =false;

                //---EQUITY TAB
                equitySetupgroup.Visible = false;

            }


        }

        private void cusipEditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void singleSectorDropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            openSingleOffering(singleSectorDropDown.SelectedItem.Label);
        }

        private void openSingleOffering(string sector)
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;
            //string portfolio;
            //int retInt;
            bool templateOpen;

            templateOpen = false;

            //MessageBox.Show(sector);
            //Worksheet sheet = (Worksheet)bk.Worksheets[1];
            //this.Application.Workbooks.Open(@"C:\Test\YourWorkbook.xls");

            //MessageBox.Show(bks.Count.ToString());
            //MessageBox.Show(bk.Name);
            //MessageBox.Show(bk.Worksheets.Count.ToString());
            //MessageBox.Show(sheet.Name);

            foreach (Workbook x in bks)
            {
                //if (sector.Equals("Agency") && x.Name.Equals("+Agency - Single Security Offering.xltm"))
                //   templateOpen = true;

                if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^Agency - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^CMO - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^CMO FLOATER - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^MBS - Single Security Offering\d+").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

            }

            if (sector.Equals("Agency") && templateOpen == false)
            {
                originationEditBox.Enabled = false;
                bk = bks.Open("I:\\FIG\\tearsheets\\Agency - Single Security Offering.xlsm");
            }

            if (sector.Equals("CMO") && templateOpen == false)
            {
                originationEditBox.Enabled = true;
                bk = bks.Open("I:\\FIG\\tearsheets\\CMO - Single Security Offering.xlsx");
            }

            if (sector.Equals("CMO Floater") && templateOpen == false)
            {
                originationEditBox.Enabled = true;
                bk = bks.Open("I:\\FIG\\tearsheets\\CMO FLOATER - Single Security Offering.xlsx");
            }

            if (sector.Equals("MBS") && templateOpen == false)
            {
                originationEditBox.Enabled = true;
                bk = bks.Open("I:\\FIG\\tearsheets\\MBS - Single Security Offering.xlsm");
            }

            if (sector.Equals("Agency"))
            {
                originationEditBox.Enabled = false;
            }
            else
            {
                originationEditBox.Enabled = true;
            }

            if (sector.Equals("Agency"))
            {
                sheet = (Worksheet)bk.Worksheets["Agency"];
                if (!cusipEditBox.Text.ToString().Equals(""))
                    sheet.Cells[2, 2] = cusipEditBox.Text;

                if (!priceEditBox.Text.ToString().Equals(""))
                    sheet.Cells[3, 2] = priceEditBox.Text;

                cusipEditBox.Text = sheet.Cells[2, 2].Value.ToString();
                priceEditBox.Text = sheet.Cells[3, 2].Value.ToString();
                originationEditBox.Text = "";
            }

            if (sector.Equals("CMO"))
            {
                sheet = (Worksheet)bk.Worksheets["CMO"];
                if (!cusipEditBox.Text.ToString().Equals(""))
                    sheet.Cells[1, 5] = cusipEditBox.Text;

                if (!priceEditBox.Text.ToString().Equals(""))
                    sheet.Cells[2, 5] = priceEditBox.Text;

                if (!originationEditBox.Text.ToString().Equals(""))
                    sheet.Cells[4, 5] = originationEditBox.Text;

                //settlementEditBox.Text = sheet.Cells[3, 5].Value.ToString();
                cusipEditBox.Text = sheet.Cells[1, 5].Value.ToString();
                priceEditBox.Text = sheet.Cells[2, 5].Value.ToString();
                originationEditBox.Text = sheet.Cells[4, 5].Value.ToString();
            }

            if (sector.Equals("CMO Floater"))
            {
                sheet = (Worksheet)bk.Worksheets["CMO Floater"];

                if (!cusipEditBox.Text.ToString().Equals(""))
                    sheet.Cells[2, 5] = cusipEditBox.Text;

                if (!priceEditBox.Text.ToString().Equals(""))
                    sheet.Cells[3, 5] = priceEditBox.Text;

                if (!originationEditBox.Text.ToString().Equals(""))
                    sheet.Cells[5, 5] = originationEditBox.Text;

                cusipEditBox.Text = sheet.Cells[2, 5].Value.ToString();
                priceEditBox.Text = sheet.Cells[3, 5].Value.ToString();
                originationEditBox.Text = sheet.Cells[5, 5].Value.ToString();

            }

            if (sector.Equals("MBS"))
            {
                sheet = (Worksheet)bk.Worksheets["MBS"];
                if (!cusipEditBox.Text.ToString().Equals(""))
                    sheet.Cells[2, 5] = cusipEditBox.Text;

                if (!priceEditBox.Text.ToString().Equals(""))
                    sheet.Cells[3, 5] = priceEditBox.Text;

                if (!originationEditBox.Text.ToString().Equals(""))
                    sheet.Cells[5, 5] = originationEditBox.Text;

                cusipEditBox.Text = sheet.Cells[2, 5].Value.ToString();
                priceEditBox.Text = sheet.Cells[3, 5].Value.ToString();
                originationEditBox.Text = sheet.Cells[5, 5].Value.ToString();
            }

        }

        private void runSingleOffering(string sector)
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;

            bool templateOpen;
            string strPrice;
            templateOpen = false;

            strPrice = "";
            foreach (Workbook x in bks)
            {
                if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^Agency - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                // MessageBox.Show(x.Name.ToString());
                if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^CMO - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^CMO FLOATER - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^MBS - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }
            }

            strPrice = priceEditBox.Text.ToString();
            if (Regex.Match(strPrice, @"-").Success)
            {
                strPrice = "=" + priceEditBox.Text.Replace("-", "+") + "/32";
            }

            if (sector.Equals("Agency"))
            {
                if (templateOpen == false)
                {
                    originationEditBox.Enabled = false;
                    bk = bks.Open("I:\\FIG\\tearsheets\\Agency - Single Security Offering.xlsm");
                }

                //sheet = (Worksheet)bk.Worksheets["Agency"];

                sheet = (Worksheet)bk.Worksheets["Agency"];
                sheet.Cells[2, 2] = cusipEditBox.Text.ToString();
                //sheet.Cells[3, 2] = priceEditBox.Text.ToString();
                sheet.Cells[3, 2] = strPrice;

                if (!settlementEditBox.Text.ToString().Equals("") && settlementEditBox.Text != null)
                {
                    //sheet.Cells[4, 2] = settlementEditBox.Text.ToString();
                }
            }

            if (sector.Equals("CMO"))
            {
                if (templateOpen == false)
                {
                    originationEditBox.Enabled = true;
                    bk = bks.Open("I:\\FIG\\tearsheets\\CMO - Single Security Offering.xlsx");
                }

                sheet = (Worksheet)bk.Worksheets["CMO"];
                sheet.Cells[1, 5] = cusipEditBox.Text.ToString();
                //sheet.Cells[2, 5] = priceEditBox.Text.ToString();
                sheet.Cells[2, 5] = strPrice;

                if (!settlementEditBox.Text.ToString().Equals("") && settlementEditBox.Text != null)
                {
                    //sheet.Cells[3, 5] = settlementEditBox.Text.ToString();
                }
                else
                {
                    settlementEditBox.Text = sheet.Cells[3, 5].Value.ToString();
                }

                if (!originationEditBox.Text.ToString().Equals("") && originationEditBox.Text != null)
                    sheet.Cells[4, 5] = originationEditBox.Text.ToString();

            }

            if (sector.Equals("CMO Floater"))
            {
                if (templateOpen == false)
                {
                    originationEditBox.Enabled = true;
                    bk = bks.Open("I:\\FIG\\tearsheets\\CMO FLOATER - Single Security Offering.xlsx");
                }

                sheet = (Worksheet)bk.Worksheets["CMO Floater"];
                sheet.Cells[2, 5] = cusipEditBox.Text.ToString();
                //sheet.Cells[3, 5] = priceEditBox.Text.ToString();
                sheet.Cells[3, 5] = strPrice;

                if (!settlementEditBox.Text.ToString().Equals("") && settlementEditBox.Text != null)
                {
                    //   sheet.Cells[4, 5] = settlementEditBox.Text.ToString();
                }
                else
                {
                    settlementEditBox.Text = sheet.Cells[4, 5].Value.ToString();
                }

                if (!originationEditBox.Text.ToString().Equals("") && originationEditBox.Text != null)
                    sheet.Cells[5, 5] = originationEditBox.Text.ToString();

            }

            if (sector.Equals("MBS"))
            {
                if (templateOpen == false)
                {
                    originationEditBox.Enabled = true;
                    bk = bks.Open("I:\\FIG\\tearsheets\\MBS - Single Security Offering.xlsm");
                }

                sheet = (Worksheet)bk.Worksheets["MBS"];
                sheet.Cells[2, 5] = cusipEditBox.Text.ToString();
                //sheet.Cells[3, 5] = priceEditBox.Text.ToString();
                sheet.Cells[3, 5] = strPrice;

                if (!settlementEditBox.Text.ToString().Equals("") && settlementEditBox.Text != null)
                {
                    // sheet.Cells[4, 5] = settlementEditBox.Text.ToString();
                }
                else
                {
                    settlementEditBox.Text = sheet.Cells[4, 5].Value.ToString();
                }

                if (!originationEditBox.Text.ToString().Equals("") && originationEditBox.Text != null)
                    sheet.Cells[5, 5] = originationEditBox.Text.ToString();

            }

        }


        private void fillOptTemplatePortSNL(string client)
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;
            bool templateOpen;

            templateOpen = false;

            foreach (Workbook x in bks)
            {
                if (Regex.Match(x.Name.ToString(), @"^FI OptTemplate").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

            }

            //if (client.Equals("Agency"))
            if (templateOpen == false)
            {
                bks.Open("C:\\requests\\optimization\\FI OptTemplate.xlsm");
            }

            sheet = (Worksheet)bk.Worksheets["Input"];
            sheet.Cells[2, 3] = SNLIDeditBox.Text.ToString();
            sheet.Cells[8, 4] = AssetGrpMineditBox.Text.ToString();
            sheet.Cells[8, 5] = AssetGrpMaxeditBox.Text.ToString();

            Globals.ThisAddIn.Application._Run2("FillPeer");
        }

        private void updateButton_Click(object sender, RibbonControlEventArgs e)
        {
            runSingleOffering(singleSectorDropDown.SelectedItem.Label);
        }

        private void updateGraphButton_Click(object sender, RibbonControlEventArgs e)
        {

            if (singleSectorDropDown.SelectedItem.Label.Equals("Agency"))
                Globals.ThisAddIn.Application._Run2("UpdateGraph");

        }

        private void PDFButton_Click(object sender, RibbonControlEventArgs e)
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            //Worksheet sheet = null;
            bool templateOpen;
            string sector;

            sector = singleSectorDropDown.SelectedItem.Label;

            templateOpen = false;

            foreach (Workbook x in bks)
            {
                if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^Agency - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^CMO - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^CMO FLOATER - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^MBS - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }
            }


            //if (singleSectorDropDown.SelectedItem.Label.Equals("Agency"))
            if (templateOpen == true)
                Globals.ThisAddIn.Application._Run2("exportToPDF", sector,
                cusipEditBox.Text.ToString(), priceEditBox.Text.ToString());

            //  String user = Environment.UserName;
            //sheet = bk.ActiveSheet;
            //sheet.ExportAsFixedForma,
            //   "C:\\Documents and Settings\\" + user + "\\Desktop\\" + cusipEditBox.Text)




            //bk.ActiveSheet  (" Type:=xlTypePDF, Filename:= " +
            //   "'C:\\Documents and Settings\\" + user + "\\Desktop\\" + cusipEditBox.Text +
            //   "', Quality:=xlQualityStandard, IncludeDocProperties:=True, IgnorePrintAreas " +
            //   ":=False, OpenAfterPublish:=True");

            //        ActiveSheet.ExportAsFixedFormat Type:=xlTypePDF, Filename:= _
            //        "C:\Documents and Settings\" & user & "\Desktop\" & Cusip _
            //        , Quality:=xlQualityStandard, IncludeDocProperties:=True, IgnorePrintAreas _
            //        :=False, OpenAfterPublish:=True


        }

        private void EmailButton_Click(object sender, RibbonControlEventArgs e)
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            //Worksheet sheet = null;
            bool templateOpen;
            string sector;

            sector = singleSectorDropDown.SelectedItem.Label;

            templateOpen = false;

            foreach (Workbook x in bks)
            {
                if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^Agency - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^CMO - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^CMO FLOATER - Single Security Offering.xlsx").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^MBS - Single Security Offering.xlsm").Success)
                {
                    templateOpen = true;
                    bk = x;
                }
            }


            //if (singleSectorDropDown.SelectedItem.Label.Equals("Agency"))
            if (templateOpen == true)
                Globals.ThisAddIn.Application._Run2("EmailOffering", sector,
                cusipEditBox.Text.ToString(), priceEditBox.Text.ToString());

        }

        private void bloomieDropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            if (bloomieDropDown.SelectedItem.Label.Equals("BB YT"))
                Globals.ThisAddIn.Application._Run2("BloomYTOffering",
                   singleSectorDropDown.SelectedItem.Label,
                   cusipEditBox.Text.ToString(), priceEditBox.Text.ToString(),
                   settlementEditBox.Text.ToString());

            if (bloomieDropDown.SelectedItem.Label.Equals("BB FMED"))
                Globals.ThisAddIn.Application._Run2("BloomFMEDOffering",
                   cusipEditBox.Text.ToString(), priceEditBox.Text.ToString(),
                   settlementEditBox.Text.ToString());

            if (bloomieDropDown.SelectedItem.Label.Equals("BB YTH"))
                Globals.ThisAddIn.Application._Run2("BloomYTHOffering",
                   singleSectorDropDown.SelectedItem.Label,
                   cusipEditBox.Text.ToString(), priceEditBox.Text.ToString(),
                   settlementEditBox.Text.ToString());
        }


        private void buttonRunSwap_Click(object sender, RibbonControlEventArgs e)
        {
            //Globals.ThisAddIn.Application._Run2("displaySell");

            Globals.ThisAddIn.Application._Run2("createBondSwap");
        }

        private void buttonSwapSetup_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("BondSwapSetUp");
        }

        private void buttonClientPDF_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = bk.ActiveSheet;


            Globals.ThisAddIn.Application._Run2("ClientToPDF", sheet.Cells[3, 2], sheet.Name);
        }

        private void buttonClientEmail_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = bk.ActiveSheet;

            Globals.ThisAddIn.Application._Run2("clientEmail", sheet.Cells[3, 2], sheet.Name);
        }

        private void buttonClearSwap_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = bk.ActiveSheet;

            Globals.ThisAddIn.Application._Run2("resetSwapCusips");
            sheet = null;
            bk = null;

        }

        private void dropDown1_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void editBox1_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void BondModelingButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                //uBModel.user = uLogIn.userID;
                //uBModel.password = uLogIn.password;
                //uBModel.Show();
            }
            else
            {
                uLogIn.Show();
            }

        }

        private void userTemplateDropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

            if (userTemplateDropDown.Items[userTemplateDropDown.SelectedItemIndex].Label.ToString().Equals("New Template"))
            {
                userTemplateNameEditBox.Enabled = true;
                userTemplateNameEditBox.Text = "";
            }
            else
            {
                userTemplateNameEditBox.Enabled = false;
                userTemplateNameEditBox.Text = userTemplateDropDown.Items[userTemplateDropDown.SelectedItemIndex].Label.ToString();
            }

        }

        private void UserTemplateSheetsDropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            if (UserTemplateSheetsDropDown.Items[UserTemplateSheetsDropDown.SelectedItemIndex].Label.ToString().Equals("New Sheet"))
            {
                userSheeteditBox.Enabled = true;
                userSheeteditBox.Text = "";
            }
            else
            {
                userSheeteditBox.Enabled = false;
                userSheeteditBox.Text = UserTemplateSheetsDropDown.Items[UserTemplateSheetsDropDown.SelectedItemIndex].Label.ToString();
            }

        }

        private void button_runCalledBonds_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                //uBModel.user = uLogIn.userID;
                //uBModel.password = uLogIn.password;
                //uBModel.Show();
                runSelectedCalledBondReports();
            }
            else
            {
                uLogIn.Show();
            }
        }

        private void runSelectedCalledBondReports()
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = bk.ActiveSheet;

            if (checkBox_FHLB.Checked.Equals(true))
            {
                Globals.ThisAddIn.Application._Run2("pullFHLBcalled");
                sheet = null;
                bk = null;

            }

            if (checkBox_FNMA.Checked.Equals(true))
            {
                Globals.ThisAddIn.Application._Run2("pullFNMAcalled");
                sheet = null;
                bk = null;

            }

            if (checkBox_FHLMC.Checked.Equals(true))
            {
                Globals.ThisAddIn.Application._Run2("pullFHLMCcalled");
                sheet = null;
                bk = null;

            }

            if (checkBox_Searched.Checked.Equals(true))
            {
                Globals.ThisAddIn.Application._Run2("pullSearched");
                sheet = null;
                bk = null;
            }

        }

        private void buttonPublicHolders_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet;

            if (sheet.Name.Equals("CMO INV"))
                Globals.ThisAddIn.Application._Run2("CMO_Holders");

            if (sheet.Name.Equals("MBS INV"))
                Globals.ThisAddIn.Application._Run2("MBS_Holders");
        }

        private void buttonDavidsonMatchers_Click(object sender, RibbonControlEventArgs e)
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet;

            if (sheet.Name.Equals("CMO INV"))
                Globals.ThisAddIn.Application._Run2("CMO_Matchers");

            if (sheet.Name.Equals("MBS INV"))
                Globals.ThisAddIn.Application._Run2("MBS_Matchers");
        }

        private void AnalyticsCommonTasksdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

            //if (AnalyticsCommonTasksdropDown.SelectedItem.Label.Equals("Called Bonds" ))
            //    Globals.ThisAddIn.Application._Run2("runCalled");

        }

        private void StrategyTaskButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (AnalyticsCommonTasksdropDown.SelectedItem.Label.Equals("Called Bonds"))
                Globals.ThisAddIn.Application._Run2("runCalled");

            if (AnalyticsCommonTasksdropDown.SelectedItem.Label.Equals("New Inventory BLP"))
                Globals.ThisAddIn.Application._Run2("pullClientMissing");

            if (AnalyticsCommonTasksdropDown.SelectedItem.Label.Equals("BWIC blp"))
                Globals.ThisAddIn.Application._Run2("pullBWICClientMissing");
        }

        private void buttonRunBloomCall_Click(object sender, RibbonControlEventArgs e)
        {

            if (dropDownBloomFunctions.SelectedItem.Label.Equals("YT"))
                Globals.ThisAddIn.Application._Run2("BloomYT");

            if (dropDownBloomFunctions.SelectedItem.Label.Equals("YTH"))
                Globals.ThisAddIn.Application._Run2("BloomYTH");

            if (dropDownBloomFunctions.SelectedItem.Label.Equals("FMED"))
                Globals.ThisAddIn.Application._Run2("BloomFMED");

            if (dropDownBloomFunctions.SelectedItem.Label.Equals("CFT"))
                Globals.ThisAddIn.Application._Run2("BloomCFT");

            if (dropDownBloomFunctions.SelectedItem.Label.Equals("DES"))
                Globals.ThisAddIn.Application._Run2("BloomDES");

        }

        private void bloombergMapperbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                bloomMapper.Show();
            }
            catch
            {
                bloomMapper = new BloombergFieldMapper();
                bloomMapper.Show();
            }
        }

        private void callblmMappedFieldsbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                bloomFields.Show();
            }
            catch
            {
                bloomFields = new BloomFieldsAvailable();
                bloomFields.Show();
            }

        }

        private void bloomMappedFieldscomboBox_TextChanged(object sender, RibbonControlEventArgs e)
        {
            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd = cn.CreateCommand();
            //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
            cmd.CommandText = "select blp_label from blp_fields where blp_type='" + mappedSectordropDown.SelectedItem.ToString() + "' and blp_field='" +
                    bloomMappedFieldscomboBox.Text + "'";

            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();

            rdr.Read();
            Clipboard.SetText(rdr.GetValue(0).ToString());
            rdr.Close();
            cn.Close();

        }

        private void mappedSectordropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            //load in mapped fields for this sector...selectedItem.toString()
            fillMappedSector(mappedSectordropDown.SelectedItem.ToString());
        }

        private void fillMappedSector(string sector)
        {
            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd = cn.CreateCommand();
            //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
            cmd.CommandText = "select * from blp_fields where blp_type='" + sector + "'";

            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();

            bloomMappedFieldscomboBox.Items.Clear();

            while (rdr.Read())
            {
                // MessageBox.Show('-' + rdr.GetValue(0).ToString() + '-');
                RibbonDropDownItem mp = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                mp.Label = rdr.GetValue(0).ToString();

                bloomMappedFieldscomboBox.Items.Add(mp);
            }
            rdr.Close();

            cn.Close();
        }

        private void fillOptSwap_Portfolio()
        {
            string CLIENT;
            string ASOF;
            string PORTID;

            PORTID = "";
            CLIENT = FI_OptPortfoliodropDown.SelectedItem.Label;
            ASOF = FI_OptAsOfdropDown.SelectedItem.Label;

            createPMdetail();

            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd = cn.CreateCommand();
            //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
            cmd.CommandText = "select PW_PORTID from PW_SECURITYDETAIL where PW_CLIENT='" + CLIENT
                + "' AND PW_ASOF_DATE='" + ASOF + "'";

            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            rdr.Read();
            PORTID = rdr.GetValue(0).ToString();

            rdr.Close();

            cn.Close();

            Globals.ThisAddIn.Application._Run2("opt_InventoryInsert", PORTID);
        }

        private void pullMatchersbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("client_Matchers");
            //findMatchers();
        }

        private void findMatchers()
        {
            ///*** READ MATCHERS POPULATE TAB
            ///
        }

        private void SwapScrubbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("autoScrub");
        }

        private void SelectSellsbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("createBondAnalysis");
        }

        private void FindBuysbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("findBUY");
            Globals.ThisAddIn.Application._Run2(" createIntRateAnalysis");
        }

        private void RefreshBuysbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("pullSwapDetailAPI");
            Globals.ThisAddIn.Application._Run2(" createIntRateAnalysis");
        }

        private void SwapSummarybutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("createSummaryPage");
            Globals.ThisAddIn.Application._Run2("createCashFlowPage");
        }

        private void BWICbutton_Click(object sender, RibbonControlEventArgs e)
        {
            if (uLogIn.userConnected == true)
            {
                bFinder.userID = uLogIn.userID;
                bFinder.userPassword = uLogIn.password;


                //bFinder.fillSavedSearchesCombo();  
                //bFinder.fillCMOcriteraiList();

                //bFinder.fillClientcriteraiList();


                ////bFinder.fillMBSsearchesComboBox();
                //bFinder.fillMBSTypeList();//***   MAYBE TEMP  ***
                ////bFinder.fillMBSClientList();
                try
                {
                    uBWIC.Show();
                }
                catch
                {
                    uBWIC = new BWIC();
                    uBWIC.Show();
                }
            }
            else
            {
                uLogIn.Show();
            }
        }

        private void BWICrowRefreshbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("refreshBWIC");
        }

        private void BWICsheetRefreshbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("refreshSheetBWIC");
        }

        private void bondSheetMatchersbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("MatchSheet");
        }

        private void BWICrowUpdatebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("updateBWICData");
            }
            catch
            {
                MessageBox.Show("Unable to update BWIC data");
            }
        }

        private void BWICsheetUpdatebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("updateSheetBWIC");
            }
            catch
            {
                MessageBox.Show("Unable to update Sheet BWIC data");
            }
        }

        private void BWICMatchersbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("BWICMatchSheet");
        }

        private void BWICHoldersbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("BWIC_Holders");
        }

        private void BWICpreviousbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("BWICPrior");
        }

        private void PWLoadbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("LOAD_PW_PORTFOLIO");
            }
            catch
            {
            }

        }

        private void PWbulkBLMbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                //Globals.ThisAddIn.Application._Run2("pullPMClientMissing");
                Globals.ThisAddIn.Application._Run2("GetBBbulk");
                //
            }
            catch
            {
            }
        }

        private void PMEmptyStagingbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("cleanUpPortToProcess");
            }
            catch
            {
            }

        }

        private void PMRunReportsbutton_Click(object sender, RibbonControlEventArgs e)
        {
            Boolean sectorDetail;
            Boolean IRSDetail;
            Boolean IRSSummary;
            Boolean sectorSummary;
            Boolean cash24;
            Boolean cashYrly;
            Boolean cashSector;
            Boolean prePaySetup;
            Boolean watchlist;
            Boolean mtgFactors;

            sectorDetail = false;
            IRSSummary = false;
            IRSDetail = false;
            sectorSummary = false;
            cash24 = false;
            prePaySetup = false;
            watchlist = false;
            cashYrly = false;
            cashSector = false;
            mtgFactors = false;


            if (MuniDetailcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_MUNIdetail", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }

            if (MuniSummarycheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_MUNI", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }

            if (LikelyCallcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_LIKELY_CALL", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }


            if (AgencySummarycheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_AGENCY", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }

            if (CapitalImpactcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_CAPITAL_IMPACT", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }


            if (PMSectorDetailcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_SectorDetail_Setup", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                    Globals.ThisAddIn.Application._Run2("PM_SectorDetail", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch( Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //sectorDetail = true;

            if (PMIRSDetailcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_IRS_Detail", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //IRSDetail = true;

            if (PMIRScheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_IRS", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch( Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //IRSSummary = true;

            if (PMSectorSummarycheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_SectorAllocation", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //sectorSummary = true;

            if (cash24checkBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_Cash24", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //cash24 = true;

            if (cashYrlyCheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_Cash_10YR", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //cashYrly = true;

            if (cashSectorCheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_Cash_By_Sector", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //cashSector = true;

            if (PrePaycheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_CashSetup", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //prePaySetup = true;

            if (WatchlistcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_WATCHLIST", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            //watchlist = true;

            if (mtgFactorscheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_MTGFACTOR_SETUP", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString()); 
                }
            //mtgFactors = true;

            try
            {
                Globals.ThisAddIn.Application._Run2("runPMreports", PortfoliodropDown.SelectedItem.ToString(),
                    PortfolioDatedropDown.SelectedItem.ToString(), sectorDetail, sectorSummary, cash24, cashYrly,
                    cashSector, prePaySetup, IRSSummary, IRSDetail, watchlist, mtgFactors);
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void PortfoliodropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            fillAsOfDropDown();
        }

        private void RefreshDatabutton_Click(object sender, RibbonControlEventArgs e)
        {

            try
            {
                Globals.ThisAddIn.Application._Run2("refreshPMdata", PortfoliodropDown.SelectedItem.ToString(),
                    PortfolioDatedropDown.SelectedItem.ToString());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
             
        }

        private void UpdateSNLOptValues()
        {
            string CLIENT;
            string SNLID;
            string SNLMIN;
            string SNLMAX;

            CLIENT = FI_OptPortfoliodropDown.SelectedItem.ToString();
            SNLID = SNLIDeditBox.Text.ToString();
            SNLMIN = AssetGrpMineditBox.Text.ToString();
            SNLMAX = AssetGrpMaxeditBox.Text.ToString();
            
            if (usingSQLServer == true && string.IsNullOrEmpty(SNLMAX)==false  && string.IsNullOrEmpty(CLIENT)==false )
            {
                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "update PW_BROKER SET PW_SNL_ID='" + SNLID + "', PW_ASSET_GROUP_MIN="
                    + SNLMIN + " , PW_ASSET_GROUP_MAX=" + SNLMAX + " where PW_CLIENT='" + CLIENT + "';";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                }

                cn.Close();

            }

        }

        private void FillDefaultSpeedsbutton_Click(object sender, RibbonControlEventArgs e)
        {

            try
            {
                Globals.ThisAddIn.Application._Run2("fillPrepayMatrix", true);
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void buildCashMBbutton_Click(object sender, RibbonControlEventArgs e)
        {

            try
            {
                if (PM_cashToTextcheckBox.Checked)
                {
                    Globals.ThisAddIn.Application._Run2("runCashFromPrepaySetup", true);
                }
                else
                {
                    Globals.ThisAddIn.Application._Run2("runCashFromPrepaySetup");
                }

            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void loadTemplatebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("blankPW_Load");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void LoadPortfoliobutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("LOAD_PW_PORTFOLIO");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void LoadCleanUpbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                //Globals.ThisAddIn.Application._Run2("cleanUpPortToProcess");
                Globals.ThisAddIn.Application._Run2("BloombergWash");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void pmUserDefinedbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("runPMuserDefined");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void buildCashNonMBbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (PM_cashToTextcheckBox.Checked)
                {
                    Globals.ThisAddIn.Application._Run2("calcNonMBSCashFlows", true);
                }
                else
                {
                    Globals.ThisAddIn.Application._Run2("calcNonMBSCashFlows");
                }

            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void OptTemplatebutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("Opt_openOptTemplate");
        }

        private void FI_OptPortfoliodropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            if (OptimizationDataBasesdropDown.SelectedItem.ToString().Equals("ZM_GALLAGHER"))
            {
                fillFI_OptAsOfDropDown();
                fillFI_OptSNL_ID();
            } else
            {
                fillFI_OptAsOfDropDown(OptimizationDataBasesdropDown.SelectedItem.ToString());
                fillFI_OptSNL_ID(OptimizationDataBasesdropDown.SelectedItem.ToString());
            }

        }

        private void OptUpdateSNLbutton_Click(object sender, RibbonControlEventArgs e)
        {

            UpdateSNLOptValues();
        }

        private void Opt_FillOptbutton_Click(object sender, RibbonControlEventArgs e)
        {
            fillOptTemplatePortSNL(FI_OptPortfoliodropDown.SelectedItem.Label);
        }

        private void refreshOptPivotbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("Refresh_Pivot");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void OptSwap_Portbutton_Click(object sender, RibbonControlEventArgs e)
        {
            //fillOptSwap_Portfolio();
            try
            {
                //Globals.ThisAddIn.Application._Run2("insert_RPT_Optimization", OptimizationDataBasesdropDown.SelectedItem.ToString(), 
                //    SNLIDeditBox.Text.ToString(), FI_OptAsOfdropDown.SelectedItem.ToString(), FI_OptBuyorSelldropDown.SelectedItem.ToString() );
                Globals.ThisAddIn.Application._Run2("port_Date", FI_OptAsOfdropDown.SelectedItem.ToString());

                Globals.ThisAddIn.Application._Run2("insert_RPT_Optimization", OptimizationDataBasesdropDown.SelectedItem.ToString(),
					SNLIDeditBox.Text.ToString(), FI_OptAsOfdropDown.SelectedItem.ToString(),
					FI_OptBuyorSelldropDown.SelectedItem.ToString(), FI_OptPortfoliodropDown.SelectedItem.ToString());
			}
			catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void optRunOptbutton_Click(object sender, RibbonControlEventArgs e)
        {
            string MaxSwapSize;
            string SwapTolerance;
            string minPL;
            string maxU300;

            MaxSwapSize = optSwapSizeeditBox.Text.ToString();
            SwapTolerance = optSizeToleranceeditBox.Text.ToString();
            minPL = optSwapGainLosseditBox.Text.ToString();
            maxU300 = optMaxU300editBox.Text.ToString();

            try
            {
				if (MaxSwapSize.Equals("") || SwapTolerance.Equals("") || minPL.Equals("") || maxU300.Equals(""))
				{
					Globals.ThisAddIn.Application._Run2("opt_getSwap");
				}
				else
				{
					Globals.ThisAddIn.Application._Run2("opt_getSwap", MaxSwapSize, SwapTolerance, minPL, maxU300);
				}
                
				
			}
            catch (Exception er)
            {
				Console.WriteLine(er.ToString());
				 MessageBox.Show(er.ToString());
			}

		}

        private void buttonSwapSetup_Click_1(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonClearSwap_Click_1(object sender, RibbonControlEventArgs e)
        {

        }

        private void menuBondSwap_ItemsLoading(object sender, RibbonControlEventArgs e)
        {

        }

        private void QS_NewSwapbutton_Click(object sender, RibbonControlEventArgs e)
        {
            // if (QS_asOfdropDown.SelectedItem == null)
            //{
            //     Globals.ThisAddIn.Application._Run2("BDFS_SWAPCandidates_Setup", QS_swapIDdropDown.SelectedItem.ToString());
            // }
            // else
            // {
            //     Globals.ThisAddIn.Application._Run2("BDFS_SWAPCandidates_Setup", QS_swapIDdropDown.SelectedItem.ToString(), QS_asOfdropDown.SelectedItem.ToString());
            // }

            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_SWAPCandidates_Setup", "New");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void Loadbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_DescriptiveGroup");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void QS_CashPullbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_CashGroup");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void Incomebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try {
                Globals.ThisAddIn.Application._Run2("BDFS_SWAPBaseIncome");

            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void QS_DisplaySheetsbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_LOAD_SHEETS");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void QS_updateRowbutton_Click(object sender, RibbonControlEventArgs e)
        {
            if (QS_rowTasksdropDown.SelectedItem == null)
            {
                Globals.ThisAddIn.Application._Run2("BDFS_REFRESH");
            }
            else
            {
                Globals.ThisAddIn.Application._Run2("BDFS_REFRESH", QS_rowTasksdropDown.SelectedItem.ToString());
            }


        }

        private void QS_RELOADbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_RELOAD");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void QS_swapIDdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

            try
            {
                if (QS_swapIDdropDown.SelectedItemIndex == 0)
                {
                    fillQS_swapIDDropDown();
                } else
                {
                    Globals.ThisAddIn.Application._Run2("QSwapID_SET", QS_swapIDdropDown.SelectedItem.ToString());
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

            fillQS_AsOfDropDown();
        }

        private void QS_PrePaybutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_PREPAY_ASSUMPTIONS");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void out_Issuesbutton_Click(object sender, RibbonControlEventArgs e)
        {
            createPMissues();
            try
            {
                Globals.ThisAddIn.Application._Run2("OUTSTANDING_ISSUES");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }
        private void submit_Issuebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("SUBMIT_ISSUES");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void resolve_Issuebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("RESOLVE_ISSUES");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void Resolved_Issuesbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("VIEW_RESOLVED_ISSUES");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void Chk_forUpdatesbutton_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void QS_useAltcheckBox_Click(object sender, RibbonControlEventArgs e)
        {

            if (QS_useAltcheckBox.Checked)
            {
                Globals.ThisAddIn.Application._Run2("setCheckAlt", true);
            }
            else
            {
                Globals.ThisAddIn.Application._Run2("setCheckAlt", false);
            }

        }

        private void PM_cashToTextcheckBox_Click(object sender, RibbonControlEventArgs e)
        {

            if (PM_cashToTextcheckBox.Checked)
            {
                Globals.ThisAddIn.Application._Run2("setToText", true);
            }
            else
            {
                Globals.ThisAddIn.Application._Run2("setToText", false);
            }
        }

        private void altIdSearchbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_ALTid_SEARCH");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void ReportingPortfoliosdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            fillReporting_PricingDateDropDown();
        }

        private void ReportingInstrumentsbutton_Click(object sender, RibbonControlEventArgs e)
        {
            //getSNLid();
            try
            {
                Globals.ThisAddIn.Application._Run2("SNLID_SET", getSNLid());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

            try
            {
                Globals.ThisAddIn.Application._Run2("pricingDate_SET", ReportingPricingDatedropDown.SelectedItem.ToString());
            }
            catch { }

            try
            {
                Globals.ThisAddIn.Application._Run2("PM_SECTORDETAIL_REPORT");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void QS_asOfdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

            if (QS_asOfdropDown.SelectedItem.ToString().Equals("Swap Date"))
                return;

            try
            {
                Globals.ThisAddIn.Application._Run2("QSasOf_SET", QS_asOfdropDown.SelectedItem.ToString());
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

            if (QS_asOfdropDown.SelectedItem == null)
            {
                try
                {
                    Globals.ThisAddIn.Application._Run2("BDFS_SWAPCandidates_Setup", QS_swapIDdropDown.SelectedItem.ToString());
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
            else
            {
                try {
                    if (QS_swapIDdropDown.SelectedItemIndex==0)
                    {
                        //fillQS_AsOfDropDown();
                        fillQS_swapIDDropDown();

                    } else
                    {
                        try
                        {
                            Globals.ThisAddIn.Application._Run2("BDFS_SWAPCandidates_Setup", QS_swapIDdropDown.SelectedItem.ToString(), QS_asOfdropDown.SelectedItem.ToString());
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show(er.ToString());
                        }
                    }

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }

        private void QS_rowTasksdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void UserdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

            
            try
            {
                QSuser = UserdropDown.SelectedItem.ToString();
                try
                {
                    fillQS_swapIDDropDown();
                    //MessageBox.Show(QS_swapIDdropDown.Items.Count.ToString());
                }
                catch { }
                try
                {
                    fillReportingPortfolioDropDown();
                    try
                    {
                        fillReporting_PricingDateDropDown();
                    }
                    catch { }
                }
                catch { }
            }
            catch { }

            try
            {
                Globals.ThisAddIn.Application._Run2("QSuser_SET", UserdropDown.SelectedItem.ToString());
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void QS_DeleteSwapbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_Delete_Swap");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

            try
            {
                fillQS_swapIDDropDown();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }

            try
            {
                fillReportingPortfolioDropDown();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void SingleSecuritycheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (SingleSecuritycheckBox.Checked)
            {
                singleSecurityGroup.Visible = true;
            }
            else
            {
                singleSecurityGroup.Visible = false;
            }
        }

        private void Issues_onoffcheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (Issues_onoffcheckBox.Checked)
            {
                Issuesgroup.Visible = true;
            }
            else
            {
                Issuesgroup.Visible = false;
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            QS_swapIDDropDown_clear();

        }

        private void QS_RunSwapbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("BDFS_RUNALL");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void QuickSwapcheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (QuickSwapcheckBox.Checked)
            {
                QuickSwapgroup.Visible = true;
            }
            else
            {
                QuickSwapgroup.Visible = false;
            }
        }

        private void ZMinventorycheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (ZMinventorycheckBox.Checked)
            {
                Reportinggroup.Visible = true;
            }
            else
            {
                Reportinggroup.Visible = false;
            }
        }

        private void inventoryDBdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            try
            {
                fillReportingPortfolioDropDown(inventoryDBdropDown.SelectedItem.ToString());
            }
            catch( Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void OptimizationDataBasesdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            string myString;

            try
            {
                myString = OptimizationDataBasesdropDown.SelectedItem.ToString();
                if (myString.ToUpper().Equals("ZM_HOURIET") )
                {
                    optTefraeditBox.Label = "   TEFRA:";
                }
                else
                {
                    optTefraeditBox.Label = "COF:";
                }

                fillFI_OptPortfolioDropDown(OptimizationDataBasesdropDown.SelectedItem.ToString());
            }
            catch( Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

		private void optBuildOutputButton_Click(object sender, RibbonControlEventArgs e)
		{
            try
            {
                Globals.ThisAddIn.Application._Run2("opt_BuildOutput");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

		}

        private void optParametersButton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                showOptimizerParameters();
            }
            catch( Exception er)
            {
                MessageBox.Show(er.ToString());
            }

            //try
            //{
            //    optSettings.Show();
            //}
            //catch
            //{
            //    optSettings = new OptimizerForm();
            //    optSettings.Show();

            //}
        }

        public void showOptimizerForm()
        {
            try
            {
                optSettings.Show();
            }
            catch
            {
                optSettings = new OptimizerForm();
                optSettings.Show();
            }
        }

        public void showOptimizerParameters()
        {
         
            try
            {
                optParameter.Show();
            }
            catch
            {
                optParameter = new optParameters();
                optParameter.optFieldName = optFieldName;

                optParameter.Show();
            }

        }
        public void showOptimizerParameters(String fieldName)
        {
            string myMin;
            string myMax;
            int myMinMaxindex;
            int mySumAvgIndex;
            int myPcol;

            optFieldName = fieldName;
            myMin = optMin;
            myMax = optMax;
            myMinMaxindex = optMinMaxComboItem;
            mySumAvgIndex = optSumAvgIndex;
            myPcol = optPCol;

            try
            {
                optParameter.Show();
            }
            catch
            {
                optParameter = new optParameters();

                optFieldName = fieldName;
                optParameter.optFieldName = optFieldName;
                optParameter.optMin = myMin;
                optParameter.optMax = myMax;
                optParameter.optMinMaxIndex = myMinMaxindex;
                optParameter.optSumAvgIndex = mySumAvgIndex;
                optParameter.parameterCol = myPcol;

                optParameter.Show();
            }

        }

        private void FI_OptAsOfdropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            if (OptimizationDataBasesdropDown.SelectedItem.ToString().Equals("ZM_GALLAGHER"))
            {
                fillFI_OptAsOfDropDown();
                fillFI_OptSNL_ID();
            }
            else
            {
                //fillFI_OptAsOfDropDown(OptimizationDataBasesdropDown.SelectedItem.ToString());
                try
                {
                    Globals.ThisAddIn.Application._Run2("port_Date", FI_OptAsOfdropDown.SelectedItem.ToString());
                } catch(Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
                
                fillFI_OptSNL_ID(OptimizationDataBasesdropDown.SelectedItem.ToString(), FI_OptAsOfdropDown.SelectedItem.ToString());
            }
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void optTefraeditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (OptimizationDataBasesdropDown.SelectedItem.ToString().ToUpper().Equals("ZM_HOURIET"))
                {
                    try
                    {
                        Globals.ThisAddIn.Application._Run2("tefra", optTefraeditBox.Text.ToString());
                    } catch(Exception er)
                    {
                        MessageBox.Show( er.ToString());
                    }
                    
                }
                if (OptimizationDataBasesdropDown.SelectedItem.ToString().ToUpper().Equals("ZM_PIGG"))
                {
                    try
                    {
                        Globals.ThisAddIn.Application._Run2("COF", optTefraeditBox.Text.ToString());
                    } catch(Exception er)
                    {
                        MessageBox.Show(er.ToString());
                    }
                    
                }
            }
            catch
            {

            }
        }

        private void optTaxRateeditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("taxrate", optTaxRateeditBox.Text.ToString() );
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void optSwapSizeeditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void optMaxTimeeditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("MaxJobTime", optMaxTimeeditBox.Text.ToString());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void templatesButton_Click(object sender, RibbonControlEventArgs e)
        {
            string tmplName;

            tmplName = TemplatesDropDown.SelectedItem.ToString();

            try
            {
                if (TemplatesDropDown.SelectedItem.ToString().Equals("DISCOUNT CALLABLE ANALYSIS"))
                {
                    try
                    {
                        Globals.ThisAddIn.Application._Run2("OAS_CALL");
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.ToString());
                    }
                } else
                {
                    if (TemplatesDropDown.SelectedItem.ToString().Equals("LOAD TRADES"))
                    {
                        try
                        {
                            Globals.ThisAddIn.Application._Run2("TASKRUNNER", "LOAD_TRADES");
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show(er.ToString());
                        }

                    } else
                    {
                        try
                        {
                            tmplName = tmplName.Replace(" ", "");
                            //MessageBox.Show(tmplName.ToString());

                            Globals.ThisAddIn.Application._Run2("template_run_" + tmplName);
                        } catch
                        {

                        }
                    }

                }



            }
            catch
            {
            }

        }

        private void TemplateOpenButton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {

                if (TemplatesDropDown.SelectedItem.ToString().Equals("DISCOUNT CALLABLE ANALYSIS"))
                {
                    try
                    {
                        Globals.ThisAddIn.Application._Run2("OAS_CALL_SETUP");
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.ToString());
                    }

                } else
                {
                    if (TemplatesDropDown.SelectedItem.ToString().Equals("CD LADDER"))
                    {
                        try
                        {
                            Globals.ThisAddIn.Application._Run2("CD_openCDLadderTemplate");
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show(er.ToString());
                        }
                    }
                    else
                    {
                        if (TemplatesDropDown.SelectedItem.ToString().Equals("FIG Trading Revenue"))
                        {
                            try
                            {
                                Globals.ThisAddIn.Application._Run2("openBreanUserTemplate", TemplatesDropDown.SelectedItem.ToString());
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show(er.ToString());
                            }
                        } else
                        {
                            try
                            {
                                Globals.ThisAddIn.Application._Run2("openBreanUserTemplate", TemplatesDropDown.SelectedItem.ToString());
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show(er.ToString());
                            }
                        }
                            
                    }
                        
                }
                    


                

                

            }
            catch
            {
            }
        }

        private void SNLIDeditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void TemplatesDropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void StreetInventoriescheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (StreetInventoriescheckBox.Checked==true)
            {
                Inventories.Visible = true;
            }
            else
            {
                Inventories.Visible = false;
            }
        }

        private void templatesGroupcheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (templatesGroupcheckBox.Checked == true)
            {
                TemplatesGroup.Visible = true;
            }
            else
            {
                TemplatesGroup.Visible = false;
            }
        }

        private void optPDFbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("PrintPDF_OPT");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void optEmailbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("EmailOptimization", "");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void PMSectorSummarycheckBox_Click(object sender, RibbonControlEventArgs e)
        {

        }



        private void PMIRScheckBox_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void pullBloomBergcheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (pullBloomBergcheckBox.Checked==true )
            {
                Globals.ThisAddIn.Application._Run2("pullBloomberg", true );
            }
            else
            {
                Globals.ThisAddIn.Application._Run2("pullBloomberg", false);
            }
        }

        private void showSubTotalscheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            if (showSubTotalscheckBox.Checked == true)
            {
                Globals.ThisAddIn.Application._Run2("calculateSubTotals", true);
            }
            else
            {
                Globals.ThisAddIn.Application._Run2("calculateSubTotals", false);
            }
        }

        private void equityCreateTablesbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                eqty.creatEquityTicket();
                eqty.createEquityTicketOverview();
                eqty.createEquityTicketOption();
                eqty.createEquityAccount();
                eqty.createEquityUser();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Acctbutton_Click(object sender, RibbonControlEventArgs e)
        {
           
            if (EquityTieToTemplatecheckBox.Checked==true)
            {
                try
                {
                    Globals.ThisAddIn.Application._Run2("EAG_PULL_ACCOUNT", EquityAcctOwnerdropDown.SelectedItem.ToString(), equityAcctdropDown.SelectedItem.ToString());
                }
                catch
                {

                }

            }

        }

        private void AcctSaveChangesbutton_Click(object sender, RibbonControlEventArgs e)
        {
            if (EquityTieToTemplatecheckBox.Checked == true)
            {
                try
                {
                    Globals.ThisAddIn.Application._Run2("EAG_SAVECHANGES", EquityAcctOwnerdropDown.SelectedItem.ToString(), equityAcctdropDown.SelectedItem.ToString());
                    fillEquityUserDropDown();
                    fillEquityAcctDropDown();    
                }
                catch
                {

                }

            }
        }
    }
}
