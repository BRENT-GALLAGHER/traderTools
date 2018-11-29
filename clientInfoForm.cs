using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;


namespace FI_Analytics
{
   
   public partial class clientInfoForm : Form
   {
      private string uid;
      private string pwd;
      private string fileName;

      MySqlDataAdapter daCLog;
      DataSet dsCLog;

      FIEmail cEmail = new FIEmail();

      public clientInfoForm()
      {
         InitializeComponent();
      }

      private string MyConString
      {
         get
         {
             return "SERVER=10.20.0.141;" + "DATABASE=FIG;" 
            + "UID=" + user + ";PASSWORD=" + password + ";";
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

      public string attachName
      {
         get
         {
            return fileName;
         }
         set
         {
            fileName = value;
         }
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         //clientInfoForm_unload();
         //this.Close();
         clientInfoForm_Deactivate(sender,e);
         
      }

      void clientInfoForm_Deactivate(object sender, System.EventArgs e)
      {
         this.Hide();
         //throw new System.NotImplementedException();
      }

      public void clientInfoForm_Load(object sender, EventArgs e)
      {
         fillClientDropDown();
      }

      private void clientContactGroupBox_Enter(object sender, EventArgs e)
      {

      }

      private void clientNameDropDownBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         try
         {
            refreshRecords(clientNameDropDownBox.SelectedItem.ToString());
            fillContactHistory(clientNameDropDownBox.SelectedItem.ToString());
         }
         catch
         {
         }

      }

      private void clientInfoTabPage_Click(object sender, EventArgs e)
      {

      }

      public void fillClientDropDown()
      {
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.CreateCommand();

         MySqlCommand command = connection.CreateCommand();
         MySqlDataReader Reader;

         command.CommandText = "select client_name from client where client_name in "
            + " ( select client_name from clientassignment where access_type='Analytics' "
            + " and user_id='" + user + "' )";

         connection.Open();
         Reader = command.ExecuteReader();
         clientNameDropDownBox.Items.Clear();
         clientNameDropDownBox.Text = "";

         while (Reader.Read())
         {
            clientNameDropDownBox.Items.Add( Reader.GetValue(0).ToString() );
         }

         connection.Close();
         try
         {
            clientNameDropDownBox.SelectedIndex = 0;
         }
         catch
         {
            clientNameDropDownBox.SelectedIndex = -1;
            refreshRecords("");
            fillContactHistory("");
         }

      }

      private void fillContactHistory(string Client)
      {     
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.CreateCommand();

         string SQL = "select contact_date, contact_text " 
            + " from contactlog where contact_name='" + Client + "' and contact_name in "
            + " (select client_name from clientassignment where access_type='Notes' and user_id='"
            + user + "' )order by contact_date ASC";

         daCLog = new MySqlDataAdapter(SQL, connection);
         MySqlCommandBuilder cb = new MySqlCommandBuilder(daCLog);

         dsCLog = new DataSet();
         daCLog.Fill(dsCLog, "contactlog");
         contactDataGrid.DataSource = dsCLog;
         contactDataGrid.DataMember = "contactlog";
      }

      private void refreshRecords(string Client)
      {
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.CreateCommand();

         MySqlCommand command = connection.CreateCommand();
         MySqlDataReader Reader;
         command.CommandText = "select client_city, client_address, client_state, "
            + " client_zip, client_WebSite, client_primary_position, client_primary_name,"
            + " client_primary_email, client_primary_phone1, client_primary_phone2, "
            + " client_secondary_position, client_secondary_name, client_secondary_email,"
            + " client_secondary_phone1, client_secondary_phone2, client_TRSYS, client_agy, "
            + " client_MUNI, client_MTGE, client_CMO, client_Other, client_CORP "
            + " from client where client_name='" + Client + "'";

         connection.Open();
         Reader = command.ExecuteReader();
         Reader.Read();

         try
         {
            clientCityTextBox.Text = Convert.ToString(Reader.GetValue(0));
         }
         catch
         {
            clientCityTextBox.Text = "";
         }
         try
         {
            clientAddressTextBox.Text = Convert.ToString(Reader.GetValue(1));
         }
         catch
         {
            clientAddressTextBox.Text = "";
         }
         try
         {
            clientStateTextBox.Text = Convert.ToString(Reader.GetValue(2));
         }
         catch
         {
            clientStateTextBox.Text = "";
         }
         try
         {
            clientZipTextBox.Text = Convert.ToString(Reader.GetValue(3));
         }
         catch
         {
            clientZipTextBox.Text = "";
         }
         try
         {
            clientWebTextBox.Text = Convert.ToString(Reader.GetValue(4));
         }
         catch
         {
            clientWebTextBox.Text = "";
         }

         try
         {
            primaryPositionTextBox.Text = Convert.ToString(Reader.GetValue(5));
         }
         catch
         {
            primaryPositionTextBox.Text = "";
         }
         try
         {
            primaryNameTextBox.Text = Convert.ToString(Reader.GetValue(6));
         }
         catch
         {
            primaryNameTextBox.Text = "";
         }
         try
         {
            primaryEmailTextBox.Text = Convert.ToString(Reader.GetValue(7));
         }
         catch
         {
            primaryEmailTextBox.Text = "";
         }
         try
         {
            primaryPhoneTextBox.Text = Convert.ToString(Reader.GetValue(8));
         }
         catch
         {
            primaryPhoneTextBox.Text = "";
         }
         try
         {
            primaryPhone2TextBox.Text = Convert.ToString(Reader.GetValue(9));
         }
         catch
         {
            primaryPhone2TextBox.Text = "";
         }
         try
         {
            secondaryPositionTextBox.Text = Convert.ToString(Reader.GetValue(10));
         }
         catch
         {
            secondaryPositionTextBox.Text = "";
         }
         try
         {
            secondaryNameTextBox.Text = Convert.ToString(Reader.GetValue(11));
         }
         catch
         {
            secondaryNameTextBox.Text = "";
         }
         try
         {
            secondaryEmailTextBox.Text = Convert.ToString(Reader.GetValue(12));
         }
         catch
         {
            secondaryEmailTextBox.Text = "";
         }
         try
         {
            secondaryPhoneTextBox.Text = Convert.ToString(Reader.GetValue(13));
         }
         catch
         {
            secondaryPhoneTextBox.Text = "";
         }
         try
         {
            secondaryPhone2TextBox.Text = Convert.ToString(Reader.GetValue(14));
         }
         catch
         {
            secondaryPhone2TextBox.Text = "";
         }
         
         try {
            treasuriesTextBox.Text = String.Format("{0:C}",Convert.ToDouble(Reader.GetValue(15))/1000000);
         } catch 
         { 
            treasuriesTextBox.Text = String.Format("{0:C}",0); 
         }

         try
         {
            agenciesTextBox.Text = String.Format("{0:C}", Convert.ToDouble(Reader.GetValue(16)) / 1000000);
         }
         catch
         {
            agenciesTextBox.Text = String.Format("{0:C}", 0);
         }

         try
         {
            muniesTextBox.Text = String.Format("{0:C}", Convert.ToDouble(Reader.GetValue(17)) / 1000000);
         }
         catch
         {
            muniesTextBox.Text = string.Format("{0:C}", 0);
         }

         try
         {
            mortgagesTextBox.Text = String.Format("{0:C}", Convert.ToDouble(Reader.GetValue(18)) / 1000000);
         }
         catch
         {
            mortgagesTextBox.Text = String.Format("{0:C}", 0);
         }

         try
         {
            CMOsTextBox.Text = String.Format("{0:C}", Convert.ToDouble(Reader.GetValue(19)) / 1000000);
         }
         catch
         {
            CMOsTextBox.Text = String.Format("{0:C}", 0);
         }

         try
         {
            corpTextBox.Text = String.Format("{0:C}", Convert.ToDouble(Reader.GetValue(21)) / 100000);
         }
         catch
         {
            corpTextBox.Text = String.Format("{0:C}", 0);
         }

         try
         {
            otherTextBox.Text = String.Format("{0:C}", Convert.ToDouble(Reader.GetValue(20)) / 1000000);
         }
         catch
         {
            otherTextBox.Text = String.Format("{0:C}", 0);
         }


         Reader.Close();

         command = connection.CreateCommand();
         
         command.CommandText = "select NIS_portfolio "
            + " from nisportfolioloads where NIS_client='" + Client + "' order by nis_date DESC";

         //connection.Open();
         Reader = command.ExecuteReader();
         portfolioComboBox.Text = "";
         portfolioComboBox.Items.Clear();

         while (Reader.Read())
         {
            portfolioComboBox.Items.Add(Reader.GetValue(0).ToString());
         }

         try
         {
            portfolioComboBox.SelectedIndex = 0;
         }
         catch
         {
            portfolioComboBox.SelectedIndex = -1;
         }

         connection.Close();

        // decimal dVal;
         
         //dVal = Decimal.Parse( Regex.Replace("$123,456.28",@"\$",""));
         //MessageBox.Show( String.Format( "{0:f2}", Convert.ToString( dVal)));
         Decimal TotVal;
         Decimal decVal;

         TotVal = decimal.Parse( Regex.Replace(treasuriesTextBox.Text,@"\$","")) + 
            decimal.Parse( Regex.Replace( agenciesTextBox.Text, @"\$","")) + 
            decimal.Parse( Regex.Replace( muniesTextBox.Text, @"\$","")) +
            decimal.Parse( Regex.Replace( mortgagesTextBox.Text, @"\$","")) +
            decimal.Parse( Regex.Replace( CMOsTextBox.Text, @"\$","")) + 
            decimal.Parse( Regex.Replace( corpTextBox.Text, @"\$","")) +
            decimal.Parse( Regex.Replace(otherTextBox.Text , @"\$",""));

        totalTextBox.Text=  String.Format("{0:C}", TotVal );
        //sectorChart.Series.Add("Sectors");
        //;
        
         sectorChart.Series.Clear();
         sectorChart.Series.Add("Sectors");
         System.Windows.Forms.DataVisualization.Charting.Title myTitle =
            new System.Windows.Forms.DataVisualization.Charting.Title(Client);
         sectorChart.Titles[0] = myTitle;
         
         sectorChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
         System.Windows.Forms.DataVisualization.Charting.Legend myLegend =
            new System.Windows.Forms.DataVisualization.Charting.Legend("Treasuries");
         myLegend.Title = "Sectors";

         sectorChart.Legends[0].Title = "Sectors";// = myLegend;
         int i;
         i = 0;

         if (decimal.Parse(Regex.Replace(treasuriesTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(treasuriesTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "Treasuries";
            i++;
         }

         if (decimal.Parse(Regex.Replace(agenciesTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(agenciesTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "Agencies";
            i++;
         }

         if (decimal.Parse(Regex.Replace(muniesTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(muniesTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "Munies";
            i++;
         }

         if (decimal.Parse(Regex.Replace(mortgagesTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(mortgagesTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "Mortgages";
            i++;
         }

         if (decimal.Parse(Regex.Replace(CMOsTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(CMOsTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "CMO's";
            i++;
         }

         if (decimal.Parse(Regex.Replace(corpTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(corpTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "Corp's";
            i++;
         }

         if (decimal.Parse(Regex.Replace(otherTextBox.Text, @"\$", "")) > 0)
         {
            sectorChart.Series[0].Points.AddY(decimal.Parse(Regex.Replace(otherTextBox.Text, @"\$", "")) / TotVal);
            sectorChart.Series[0].Points[i].LegendText = "Other";
            i++;
         }
         //pieLabel.Text = Client + " By Sector";

         if (TotVal == 0)
            TotVal = 1;

        decVal=decimal.Parse(Regex.Replace(treasuriesTextBox.Text, @"\$", "")) / TotVal;
        treasuriesPctTextBox.Text = decVal.ToString("#0.##%");
        decVal = decimal.Parse(Regex.Replace(agenciesTextBox.Text, @"\$", "")) / TotVal;
        agenciesPctTextBox.Text = decVal.ToString("#0.##%");
        decVal = decimal.Parse(Regex.Replace(muniesTextBox.Text, @"\$", "")) / TotVal;
        muniesPctTextBox.Text = decVal.ToString("#0.##%");
        decVal = decimal.Parse(Regex.Replace(mortgagesTextBox.Text, @"\$", "")) / TotVal;
        mortgagesPctTextBox.Text = decVal.ToString("#0.##%");
        decVal = decimal.Parse(Regex.Replace(CMOsTextBox.Text, @"\$", "")) / TotVal;
        CMOsPctTextBox.Text = decVal.ToString("#0.##%");
        decVal = decimal.Parse(Regex.Replace(corpTextBox.Text, @"\$", "")) / TotVal;
        corpPctTextBox.Text = decVal.ToString("#0.##%");
        decVal = decimal.Parse(Regex.Replace(otherTextBox.Text, @"\$", "")) / TotVal;
        otherPctTextBox.Text = decVal.ToString("#0.##%");

        Globals.ThisAddIn.Application._Run2("setClient", Client);
      }

      private void refreshButton_Click(object sender, EventArgs e)
      {
         refreshRecords(clientNameDropDownBox.SelectedItem.ToString());
      }

      private void updateButton_Click(object sender, EventArgs e)
      {
         updateClientInfo(clientNameDropDownBox.SelectedItem.ToString());
      }

      private void updateClientInfo(string Client)
      {
         //update mySQL database!
         string sql;

         
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.Open();

         sql = "update client set client_city='" + clientCityTextBox.Text.ToString()
            + "', client_address='" + clientAddressTextBox.Text.ToString()
            + "', client_state ='" +clientStateTextBox.Text.ToString() + "' , "
            + " client_zip = '" + clientZipTextBox.Text.ToString() + "', "
            + " client_WebSite = '" + clientWebTextBox.Text.ToString() + "', "
            + " client_primary_position = '" + primaryPositionTextBox.Text.ToString() + "', "
            + " client_primary_name = '" + primaryNameTextBox.Text.ToString() + "', "
            + " client_primary_email = '" + primaryEmailTextBox.Text.ToString() + "', "
            + " client_primary_phone1 = '" + primaryPhoneTextBox.Text.ToString() + "', "
            + " client_primary_phone2 = '" + primaryPhone2TextBox.Text.ToString() + "', "
            + " client_secondary_position = '" + secondaryPositionTextBox.Text.ToString() + "', "
            + " client_secondary_name = '" + secondaryNameTextBox.Text.ToString() + "', "
            + " client_secondary_email ='" + secondaryEmailTextBox.Text.ToString() + "', "
            + " client_secondary_phone1 = '" + secondaryPhoneTextBox.Text.ToString() + "', "
            + " client_secondary_phone2 = '" + secondaryPhone2TextBox.Text.ToString() 
            + "'  where client_name='" + Client + "'";

         MySqlCommand cmd = new MySqlCommand(sql, connection);
         cmd.ExecuteNonQuery();
         connection.Close();
      }

      private void fixedIncomeGroupBox_Enter(object sender, EventArgs e)
      {

      }

      private void noteInsertButton_Click(object sender, EventArgs e)
      {
         insertContactNote(clientNameDropDownBox.SelectedItem.ToString(), 
            contactNoteTextBox.Text.ToString());

         fillContactHistory(clientNameDropDownBox.SelectedItem.ToString());
         contactNoteTextBox.Clear();
      }

      private void insertContactNote(string Client, string con_note)
      {
         string sql;
         
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.Open();

         MySqlCommand cmd = new MySqlCommand("select count(*) from clientassignment where client_name='"
            + Client + "' and access_type='Notes' and user_id='" + user + "';", connection);

         MySqlDataReader rdr;

         rdr = cmd.ExecuteReader();
         rdr.Read();

         //MessageBox.Show(rdr.GetValue(0).ToString());
         if (Convert.ToInt32(rdr.GetValue(0)) > 0)
         {
            rdr.Close();
            sql = "insert into contactLog ( broker_ID, contact_Name, contact_Date, contact_Text ) "
               + " values ('" + user + "','" + Client + "', now(), '" + con_note + "');";

            cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
         }
         else
         {
            rdr.Close();
         }

         connection.Close();
      }

      private void emailToolStripMenuItem_Click(object sender, EventArgs e)
      {
         //cEmail.sendEmail2();
         //cEmail.sendEMailThroughOUTLOOK();
      }

      private void emailPrimaryCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         setEmailTo();
      }

      private void setEmailTo()
      {
         if (emailPrimaryCheckBox.Checked == true)
         {
            if (emailSecondaryCheckBox.Checked == true)
            {
               clientToTextBox.Text = primaryEmailTextBox.Text + " ; " + secondaryEmailTextBox.Text;
            }
            else
            {
               clientToTextBox.Text = primaryEmailTextBox.Text;
            }

         }
         else
         {
            if (emailSecondaryCheckBox.Checked == true)
            {
               clientToTextBox.Text = secondaryEmailTextBox.Text;
            }
            else
            {
               clientToTextBox.Text = "";
            }
         }

      }

      private void emailSecondaryCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         setEmailTo();
      }

      private void emailClientButton_Click(object sender, EventArgs e)
      {
         if (attachName==null)
         {
            cEmail.sendEMailThroughOUTLOOK(clientToTextBox.Text, clientSubjectTextBox.Text,
               clientEmailBodyTextBox.Text);
         }
         else
         {
            cEmail.sendEMailThroughOUTLOOK(clientToTextBox.Text, clientSubjectTextBox.Text,
            clientEmailBodyTextBox.Text, attachName);
         }

      }

      private void attachFileButton_Click(object sender, EventArgs e)
      {
         OpenFileDialog fileChooser = new OpenFileDialog();
         DialogResult result = fileChooser.ShowDialog();
  
         if (result != DialogResult.Cancel)
         {
            attachName = fileChooser.FileName;
            attachmentTextBox.Text = attachName;
         }

         this.Show();
   
      }

      private void tabPage2_Click(object sender, EventArgs e)
      {

      }

      private void portfolioComboBox_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      private void clientReports()
      {
         Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
         Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
         Sheets mySheets = null;
         Worksheet sheet=null;
         string portfolio;
         int retInt;


         //Worksheet sheet = (Worksheet)bk.Worksheets[1];
         //this.Application.Workbooks.Open(@"C:\Test\YourWorkbook.xls");

         //MessageBox.Show(bks.Count.ToString());
         //MessageBox.Show(bk.Name);
         //MessageBox.Show(bk.Worksheets.Count.ToString());
         //MessageBox.Show(sheet.Name);

         //foreach (Workbook x in bks)
         //{
         // MessageBox.Show(x.Name);
         //}

         //MessageBox.Show(hardTemplateRadioButton.Checked.ToString());

         if ( bks.Count > 0)
         {
            //use active
            //life is good :)
         }
         else
         {
            bks.Add();
         }

         bk = Globals.ThisAddIn.Application.ActiveWorkbook;
         mySheets = bk.Worksheets;
         //retInt = 1;
       
         //for (retInt = 1; retInt <= mySheets.Count; retInt++ )
         //{
          //  sheet = (Worksheet)bk.Worksheets[retInt];
          //  MessageBox.Show( sheet.Name );
         //}

         Globals.ThisAddIn.Application.Calculation = XlCalculation.xlCalculationManual;
         portfolio = portfolioComboBox.Text;

         //if (mnthlyDetailCheckBox.Checked == true)
         //   Globals.ThisAddIn.Application._Run2("NisDetail", portfolio);

         if (mnthlySectorCheckBox.Checked == true)
            Globals.ThisAddIn.Application._Run2("bk_SectorDetail", portfolio);

         if (mnthlyCashFlows.Checked == true)
         {
            Globals.ThisAddIn.Application._Run2("bk_Cash24", portfolio);
            Globals.ThisAddIn.Application._Run2("bk_CashYR", portfolio);
            Globals.ThisAddIn.Application._Run2("bk_CashYRSector", portfolio);
         }

         if (mnthlyIRSCheckBox.Checked == true)
         {
            Globals.ThisAddIn.Application._Run2("bk_IRSDetail", portfolio);
            Globals.ThisAddIn.Application._Run2("bk_IRS", portfolio);
         }

         if (mnthlyLikelyCallCheckBox.Checked == true)
            Globals.ThisAddIn.Application._Run2("bk_LikelyCall", portfolio);

         if (mnthlyMBSCheckBox.Checked == true)
            Globals.ThisAddIn.Application._Run2("MBSAmortization", portfolio);

         if (mnthlyMBSDetailCheckBox.Checked == true)
            Globals.ThisAddIn.Application._Run2("MBS", portfolio);

         if (mnthlyMuniCheckBox.Checked == true)
         {
            retInt = Globals.ThisAddIn.Application._Run2("NisMUNI", portfolio);
            Globals.ThisAddIn.Application._Run2("NisMUNIdetail", retInt, portfolio);
         }

         if (mnthlyCorpCheckBox.Checked == true)
         {
            retInt = Globals.ThisAddIn.Application._Run2("NisCorp", portfolio);
            Globals.ThisAddIn.Application._Run2("NisCORPdetail", retInt, portfolio);
         }

         if (mnthlyAgencyCheckBox.Checked == true)
            Globals.ThisAddIn.Application._Run2("bk_Agency", portfolio);

         bk.ForceFullCalculation = true;
         Globals.ThisAddIn.Application.Calculation = XlCalculation.xlCalculationManual;

         if (mnthlySectorAllocationCheckBox.Checked == true)
            Globals.ThisAddIn.Application._Run2("bk_SectorAllocation");

         if (mnthlyAdjRateCheckBox.Checked == true)
         {
            retInt = Globals.ThisAddIn.Application._Run2("NisAdjRate", portfolio);
            Globals.ThisAddIn.Application._Run2("NisAdjRatedetail", retInt);
         }

         Globals.ThisAddIn.Application.Calculation = XlCalculation.xlCalculationAutomatic;
         //Globals.ThisAddIn.Application._Run2("cleanUp");
      }

      private void clientReportsButton_Click(object sender, EventArgs e)
      {
         clientReports();
      }

      private void treasuriesTextBox_TextChanged(object sender, EventArgs e)
      {

      }

    

   }
}
