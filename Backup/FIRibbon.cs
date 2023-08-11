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

namespace traderTools
{
    public partial class FixedIncome
    {
       //clientInfoForm cInfo = new clientInfoForm();
       userLogInForm uLogIn = new userLogInForm();
       //userProfileForm uProfile = new userProfileForm();
       //passwordResetForm uPass = new passwordResetForm();
       bondFinder bFinder = new bondFinder();
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
       RibbonDropDownItem rbnItem;

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
           //Workbook bk = null;
           //MessageBox.Show(bks.Count.ToString());
           //Sheets mySheets = null;
           //Worksheet sheet = null; 
           //Range myRange = null;

           bks.Application.SheetSelectionChange += new AppEvents_SheetSelectionChangeEventHandler(Application_SheetSelectionChange);
           bks.Application.WorkbookOpen += new AppEvents_WorkbookOpenEventHandler(Application_WorkbookOpen);
           bks.Application.OnKey("^.", "bondSwapSellSide");
           bks.Application.OnKey("^,", "bondSwapBuySide");
           usingSQLServer = true;
        }
        
       
        void Application_WorkbookOpen(Workbook Wb)
        {
           //MessageBox.Show("HELLO");
           //Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
           //MessageBox.Show(Wb.Name);
           // MessageBox.Show(bk.Name);
           try
           {
              //Wb.Application.Run("RegisterCallback", Wb);
           }
           catch
           {

           }
        }

  
        void Application_SheetSelectionChange(object Sh, Range Target)
        {    
           //MessageBox.Show( Target.Value );  
        }

        private void buttonGetCMO_Click(object sender, RibbonControlEventArgs e)
        {
            if (dropSector.SelectedItem.Label=="CMO")
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

           if ( sheet.Name.Equals("CMO INV"))
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

           if ( sheet.Name.Equals("CMO INV"))
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

        private void loginButton_Click(object sender, RibbonControlEventArgs e)
        {
            uLogIn.verifyUser();  //this verifys without displaying login screen.  No need with SQL Server windows authentication
          
            //uLogIn.Show(); //use this if user must enter userID and password

           //bool vbl;
           //vbl = uLogIn.testRun();
           //MessageBox.Show(vbl.ToString());
        }

        public void fillPortfolioDropDown()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                   "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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
                clientName= PortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                   "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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


        public void fillFI_OptPortfolioDropDown()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                   "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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

                cn.Close();
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
                clientName = FI_OptPortfoliodropDown.SelectedItem.ToString();

                SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                   "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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

                SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                   "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct PW_SNL_ID, PW_ASSET_GROUP_MIN,  PW_ASSET_GROUP_MAX " +
                    " from PW_BROKER WHERE PW_CLIENT='" + clientName + "';";

                Rdr = cmd.ExecuteReader();
                //PortfolioDatedropDown.Items.Clear();
                SNLIDeditBox.Text = "";

                Rdr.Read();
                //while (Rdr.Read())
                //{
                    //RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
                    //rbnItem.Label = Rdr.GetValue(0).ToString();
                    SNLIDeditBox.Text = Rdr.GetValue(0).ToString();
                    AssetGrpMineditBox.Text = Rdr.GetValue(1).ToString();
                    AssetGrpMaxeditBox.Text = Rdr.GetValue(2).ToString();

                    //PortfolioDatedropDown.Items.Add(rbnItem);
                    //FI_OptAsOfdropDown.Items.Add(rbnItem);
                //}

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

               dropSector.Enabled = true;
               buttonPublicHolders.Enabled = true;
               buttonDavidsonMatchers.Enabled = true;
               buttonPublicHolders.Visible=true;
               //buttonGetCMO.Enabled = true;
               buttonPriceRefresh.Enabled = true;
               buttonOpenInventory.Enabled = true;
               buttonBloomYT.Enabled = true;
               bondFinderButton.Enabled = true;
               bondSheetMatchersbutton.Visible = true;

              //===BRING OTHERS ON AS DEVELOPED AS OF 12/2/2013 ONLY CMO INVENTORIES
              //clientInfoButton.Enabled = true;
              //buttonClientEmail.Enabled = true;
              //buttonClientPDF.Enabled = true;
               inventoriesGroup.Visible = true;
               
               if (Environment.UserName.Equals("bgallagher") || Environment.UserName.Equals("spigg"))
               {
                   tabStrategy.Visible = true;
                   AnalyticsGroup.Visible = true;
                   //monthlyPackageButton.Enabled = true;
                   //AnalyticsCommonTasksdropDown.Enabled = true;
                   //StrategyTaskButton.Enabled = true;
                   //BondModelingButton.Enabled = true;
                   //menuBondSwap.Enabled = true;

                   inventoriesGroup.Visible = true;
                   //pullMatchersbutton.Enabled = true;
                   //mappedSectordropDown.Enabled = true;
                   //callblmMappedFieldsbutton.Enabled = true;
                   //bloombergMapperbutton.Enabled = true;
                   //bloomMappedFieldscomboBox.Enabled = true;

                   clientMngmtGroup.Visible = true;

                   PM_group.Visible = true;
                   fillPortfolioDropDown();
                   fillAsOfDropDown();

                   tabOptimization.Visible = true;
                   StrategiesOptgroup.Visible = true;
                   fillFI_OptPortfolioDropDown();
                   fillFI_OptAsOfDropDown();
                   fillFI_OptSNL_ID();
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


               dropDownBloomFunctions.Enabled = true;
               buttonRunBloomCall.Enabled = true;
               groupCommonBloomberg.Visible = true;
           
               //BWIC 
               BWICgroup.Visible = true;
               BWICrowRefreshbutton.Visible = true;



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
              clientInfoButton.Enabled = false;
              buttonClientEmail.Enabled = false;
              buttonClientPDF.Enabled = false;
              monthlyPackageButton.Enabled = false;
              menuBondSwap.Enabled = false;
              dropSector.Enabled = false;
              //buttonGetCMO.Enabled = false;
              buttonPriceRefresh.Enabled = false;
              buttonOpenInventory.Enabled = false;
              buttonBloomYT.Enabled =false;
              bondFinderButton.Enabled = false;
              BondModelingButton.Enabled = false;
              buttonPublicHolders.Enabled =false;
              buttonDavidsonMatchers.Enabled = false;
              bondSheetMatchersbutton.Visible = false;
  
              AdministratorGroupBox.Visible = false;

              monthlyPackageButton.Enabled =false;
              AnalyticsCommonTasksdropDown.Enabled =false;
              StrategyTaskButton.Enabled =false;
 
              //SINGLE SECURITY CONTROLS
              cusipEditBox.Enabled =false;
              priceEditBox.Enabled =false;
              settlementEditBox.Enabled =false;
              updateCusipButton.Enabled =false;
              updateGraphButton.Enabled =false;
              PDFButton.Enabled =false;
              EmailButton.Enabled =false;
              singleSectorDropDown.Enabled =false;
              bloomieDropDown.Enabled = false;
              originationEditBox.Enabled = false;
              singleSecurityGroup.Visible =false;

              //USER TEMPLATES
              userTemplateDropDown.Enabled = false;
              userTemplateNameEditBox.Enabled = false;
              UserTemplateSheetsDropDown.Enabled = false;
              HeaderRowEditBox.Enabled = false;
              IDColumnEditBox.Enabled = false;
              saveUserSheetbutton.Enabled = false;
              saveUserTemplatebutton.Enabled = false;

              //CALLABLE BONDS
              checkBox_FHLB.Enabled =false;
              checkBox_FNMA.Enabled =false;
              checkBox_FHLMC.Enabled = false;
              checkBox_Searched.Enabled = false;
              button_runCalledBonds.Enabled =false;

              dropDownBloomFunctions.Enabled =false;
              buttonRunBloomCall.Enabled =false;

              inventoriesGroup.Visible = false;
              tabStrategy.Visible = false;

           }


        }

        private void cusipEditBox_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void singleSectorDropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
           openSingleOffering( singleSectorDropDown.SelectedItem.Label);
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

              if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^\+Agency - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^\+CMO - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^\+CMO FLOATER - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^\+MBS - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

           }

           if (sector.Equals("Agency") && templateOpen == false)
           {
              originationEditBox.Enabled = false;
              bk = bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+Agency - Single Security Offering1.xlsm");
           }

           if (sector.Equals("CMO") && templateOpen == false)
           {
              originationEditBox.Enabled = true;
              bk= bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+CMO - Single Security Offering1.xltm");  
           }

           if (sector.Equals("CMO Floater") && templateOpen == false)
           {
              originationEditBox.Enabled = true;
              bk = bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+CMO FLOATER - Single Security Offering.xltm");
           }

           if (sector.Equals("MBS") && templateOpen == false)
           {
              originationEditBox.Enabled = true;
              bk = bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+MBS - Single Security Offering1.xltm");
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
              cusipEditBox.Text = sheet.Cells[2, 2].Value.ToString();
              priceEditBox.Text = sheet.Cells[3, 2].Value.ToString();
              originationEditBox.Text = "";
           }

           if (sector.Equals("CMO"))
           {
              sheet = (Worksheet)bk.Worksheets["CMO"];
              cusipEditBox.Text = sheet.Cells[1, 5].Value.ToString();
              priceEditBox.Text = sheet.Cells[2, 5].Value.ToString();
              //settlementEditBox.Text = sheet.Cells[3, 5].Value.ToString();
              originationEditBox.Text = sheet.Cells[4, 5].Value.ToString();
           }

           if (sector.Equals("CMO Floater"))
           {           
              sheet = (Worksheet)bk.Worksheets["CMO Floater"];
              cusipEditBox.Text = sheet.Cells[2, 5].Value.ToString();
              priceEditBox.Text = sheet.Cells[3, 5].Value.ToString();
              originationEditBox.Text = sheet.Cells[5, 5].Value.ToString();
           }

           if (sector.Equals("MBS"))
           {
              sheet = (Worksheet)bk.Worksheets["MBS"];
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
                if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^\+Agency - Single Security Offering\d+").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^\+CMO - Single Security Offering\d+").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^\+CMO FLOATER - Single Security Offering\d+").Success)
                {
                    templateOpen = true;
                    bk = x;
                }

                if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^\+MBS - Single Security Offering\d+").Success)
                {
                    templateOpen = true;
                    bk = x;
                }
            }

            strPrice = priceEditBox.Text.ToString();
            if (Regex.Match(strPrice, @"-").Success)
            {
                strPrice = "=" +  priceEditBox.Text.Replace("-","+") + "/32";
            }

            if (sector.Equals("Agency"))
            {
                if (templateOpen == false)
                {
                    originationEditBox.Enabled = false;
                    bk = bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+Agency - Single Security Offering1.xlsm");
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
                    bk = bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+CMO - Single Security Offering1.xltm");
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
                    bk=bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+CMO FLOATER - Single Security Offering.xltm");
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
                    bk = bks.Open("\\\\denfs\\Groups\\Strategy\\templates\\SingleSecurity\\+MBS - Single Security Offering1.xltm");
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
               if ( Regex.Match(x.Name.ToString(), @"^FI OptTemplate").Success)
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
              if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^\+Agency - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^\+CMO - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^\+CMO FLOATER - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^\+MBS - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }
           }


           //if (singleSectorDropDown.SelectedItem.Label.Equals("Agency"))
              if (templateOpen==true)
                 Globals.ThisAddIn.Application._Run2("exportToPDF", sector,
                 cusipEditBox.Text.ToString(),priceEditBox.Text.ToString());

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
              if (sector.Equals("Agency") && Regex.Match(x.Name.ToString(), @"^\+Agency - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("CMO") && Regex.Match(x.Name.ToString(), @"^\+CMO - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("CMO Floater") && Regex.Match(x.Name.ToString(), @"^\+CMO FLOATER - Single Security Offering\d+").Success)
              {
                 templateOpen = true;
                 bk = x;
              }

              if (sector.Equals("MBS") && Regex.Match(x.Name.ToString(), @"^\+MBS - Single Security Offering\d+").Success)
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
                 cusipEditBox.Text.ToString(),priceEditBox.Text.ToString(),
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

           if ( checkBox_FHLMC.Checked.Equals(true) )
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
            SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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
            fillMappedSector( mappedSectordropDown.SelectedItem.ToString());
        }

        private void fillMappedSector(string sector)
        {
            SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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
                RibbonDropDownItem mp= Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
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

            SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
    "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

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
                Globals.ThisAddIn.Application._Run2("pullPMClientMissing");
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


            if (MuniDetailcheckBox.Checked )
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_MUNIdetail", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }

            if (MuniSummarycheckBox.Checked )
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_MUNI", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
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
                catch
                {
                }

            if (CapitalImpactcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_CAPITAL_IMPACT", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }


            if (PMSectorDetailcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_SectorDetail_Setup", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                    Globals.ThisAddIn.Application._Run2("PM_SectorDetail", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //sectorDetail = true;

            if (PMIRSDetailcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_IRS_Detail", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //IRSDetail = true;

            if (PMIRScheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_IRS", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //IRSSummary = true;

            if (PMSectorSummarycheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_SectorAllocation", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //sectorSummary = true;

            if (cash24checkBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_Cash24", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //cash24 = true;

            if (cashYrlyCheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_Cash_10YR", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //cashYrly = true;

            if (cashSectorCheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_Cash_By_Sector", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //cashSector = true;

            if (PrePaycheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_CashSetup", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //prePaySetup = true;

            if (WatchlistcheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_WATCHLIST", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //watchlist = true;

            if (mtgFactorscheckBox.Checked)
                try
                {
                    Globals.ThisAddIn.Application._Run2("PM_MTGFACTOR_SETUP", PortfoliodropDown.SelectedItem.ToString(),
                        PortfolioDatedropDown.SelectedItem.ToString());
                }
                catch
                {
                }
            //mtgFactors = true;

            try
            {
                Globals.ThisAddIn.Application._Run2("runPMreports", PortfoliodropDown.SelectedItem.ToString(),
                    PortfolioDatedropDown.SelectedItem.ToString(), sectorDetail, sectorSummary,cash24, cashYrly,
                    cashSector,prePaySetup,IRSSummary, IRSDetail, watchlist, mtgFactors);
            }
            catch
            {
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
                    PortfolioDatedropDown.SelectedItem.ToString() );
            }
            catch
            {
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

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                    "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "update PW_BROKER SET PW_SNL_ID='" + SNLID + "', PW_ASSET_GROUP_MIN=" 
                    + SNLMIN + " , PW_ASSET_GROUP_MAX=" + SNLMAX + " where PW_CLIENT='" + CLIENT + "';";
                cmd.ExecuteNonQuery();

                cn.Close();

                //MessageBox.Show("Update xsl.xsl");

            }

        }

        private void FillDefaultSpeedsbutton_Click(object sender, RibbonControlEventArgs e)
        {

            try
            {
                Globals.ThisAddIn.Application._Run2("fillPrepayMatrix", true);
            }
            catch
            {
            }
        }

        private void buildCashMBbutton_Click(object sender, RibbonControlEventArgs e)
        {

            try
            {
                Globals.ThisAddIn.Application._Run2("runCashFromPrepaySetup");
            }
            catch
            {
            }
        }

        private void loadTemplatebutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("blankPW_Load");
            }
            catch
            {
            }
        }

        private void LoadPortfoliobutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("LOAD_PW_PORTFOLIO");
            }
            catch
            {
            }
        }

        private void LoadCleanUpbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("cleanUpPortToProcess");
            }
            catch
            {
            }
        }

        private void pmUserDefinedbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("runPMuserDefined");
            }
            catch
            {
            }

        }

        private void buildCashNonMBbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("calcNonMBSCashFlows");
            }
            catch
            {
            }

        }

        private void OptTemplatebutton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("Opt_openOptTemplate");
        }

        private void FI_OptPortfoliodropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            fillFI_OptAsOfDropDown();
            fillFI_OptSNL_ID();
        }

        private void OptUpdateSNLbutton_Click(object sender, RibbonControlEventArgs e)
        {
            UpdateSNLOptValues();
        }

        private void Opt_FillOptbutton_Click(object sender, RibbonControlEventArgs e)
        {
            fillOptTemplatePortSNL(FI_OptPortfoliodropDown.SelectedItem.Label );
        }

        private void refreshOptPivotbutton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application._Run2("Refresh_Pivot");
            }
            catch
            {
            }
        }

        private void OptSwap_Portbutton_Click(object sender, RibbonControlEventArgs e)
        {
            fillOptSwap_Portfolio();
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
                Globals.ThisAddIn.Application._Run2("opt_getSwap", MaxSwapSize, SwapTolerance, minPL, maxU300);
            }
            catch
            {
            }

        }
    

    }
}
