using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace traderTools
{
    public partial class bondFinder : Form
    {

        private string uid;
        private string pwd;
        //MySqlDataAdapter daCriteria;
        SqlDataAdapter SQLdaCriteria;
        DataSet dsCriteria;
        bool isSQLServer;
        private string curSearch;

        public string currentSearch
        {
            get
            {
                return curSearch;
            }
            set
            {
                curSearch = value;
            }
        }

        public string userID
        {
            get
            {
                if (uid == null)
                {
                    if (usingSQLServer == true)
                    {
                        uid = Environment.UserName;
                    }
                }

                return uid;
            }
            set
            {
                if (usingSQLServer == true)
                {
                    uid = Environment.UserName;
                }
                else
                {
                    uid = value;
                }
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

        public string userPassword
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

        private string MyConString
        {
            get
            {
                return "SERVER=10.20.0.141;" + "DATABASE=FIG;"
               + "UID=" + userID + ";PASSWORD=" + userPassword + ";";
            }
        }

        public bondFinder()
        {
            isSQLServer = true;
            //uid = "temp";
            //curSearch = "NewValue";   
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void agencyCMOButton_Click(object sender, EventArgs e)
        {
            searchCMO();
        }

        private void searchCMO_OLD20140502()
        {
            // BUID SQL STRING !!
            string SQL;
            bool isFound = false;

            //---WORK ON QUERY FOR SQLSERVER...
            SQL = "";

            if (usingSQLServer == false)
            {
                SQL = " create table tmp_CMO_finder as select b.* from cmolookup a, "
                   + " cmospreadsheet b where a.id = b.id and b.sector='CMO' AND ( ";
            }

            if (usingSQLServer == true)
            {
                SQL = " select b.* into tmp_CMO_finder from cmolookup a, "
                   + " cmospreadsheet b where a.id = b.id and b.sector='CMO' AND ( ";
            }

            if (pt_adCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr( a.tranchedes,'AD')>0 ";

                    if (usingSQLServer == true)
                        SQL += "  CHARINDEX(  'AD',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr( a.tranchedes,'AD')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or  CHARINDEX( 'AD',a.tranchedes)>0";

                    isFound = true;
                }

            if (pt_cstrCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'CSTR')>0 ";

                    if (usingSQLServer == true)
                        SQL += "  CHARINDEX('CSTR',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr(a.tranchedes,'CSTR')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or  CHARINDEX('CSTR',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_exchCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'EXCH')>0 ";

                    if (usingSQLServer == true)
                        SQL += "  CHARINDEX('EXCH',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr(a.tranchedes,'EXCH')>0 ";

                    if (usingSQLServer == true)
                        SQL += " OR  CHARINDEX('EXCH',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_fltCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'FLT')>0 ";

                    if (usingSQLServer == true)
                        SQL += "  CHARINDEX('FLT',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr(a.tranchedes,'FLT')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or  CHARINDEX('FLT',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_mrCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'MR')>0 ";

                    if (usingSQLServer == true)
                        SQL += "  CHARINDEX('MR',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr(a.tranchedes,'MR')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or  CHARINDEX('MR',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_pac1CheckBox.CheckState.ToString().Equals("Checked"))
            {
                if (pt_pac2CheckBox.CheckState.ToString().Equals("Checked"))
                {
                    if (isFound == false)
                    {
                        if (usingSQLServer == false)
                            SQL += " instr(a.tranchedes,'PAC')>0 ";

                        if (usingSQLServer == true)
                            SQL += "  CHARINDEX('PAC',a.tranchedes)>0 ";

                        isFound = true;
                    }
                    else
                    {
                        if (usingSQLServer == false)
                            SQL += " OR instr(a.tranchedes,'PAC')>0 ";

                        if (usingSQLServer == true)
                            SQL += " or  CHARINDEX('PAC',a.tranchedes)>0 ";

                        isFound = true;
                    }
                }
                else
                {
                    if (isFound == false)
                    {
                        if (usingSQLServer == false)
                            SQL += " ( instr(a.tranchedes,'PAC')>0 and instr(a.tranchedes,'PAC-2')<=0 ) ";

                        if (usingSQLServer == true)
                            SQL += " ( CHARINDEX('PAC',a.tranchedes)>0 and CHARINDEX('PAC-2',a.tranchedes)<=0 ) ";
                        isFound = true;
                    }
                    else
                    {
                        if (usingSQLServer == false)
                            SQL += " OR ( instr(a.tranchedes,'PAC')>0 and instr(a.tranchedes,'PAC-2')<=0 ) ";

                        if (usingSQLServer == true)
                            SQL += " OR ( CHARINDEX('PAC',a.tranchedes)>0 and CHARINDEX('PAC-2',a.tranchedes)<=0 ) ";

                        isFound = true;
                    }
                }
            }

            if (pt_pac2CheckBox.CheckState.ToString().Equals("Checked"))
            {
                if (usingSQLServer == false)
                    SQL += " OR instr(a.tranchedes,'PAC-2')>0 ";

                if (usingSQLServer == true)
                    SQL += " or CHARINDEX('PAC-2',a.tranchedes)>0 ";
            }
            else
            {
                // SQL += " AND instr(tranchedes,'PAC-2')<=0 ";
            }

            if (pt_rtlCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'RTL')>0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('RTL',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.tranchedes,'RTL')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('RTL',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_scCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'SC')>0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('SC',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.tranchedes,'SC')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('SC',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_supCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'SUP')>0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('SUP',a.tranchedes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.tranchedes,'SUP')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('SUP',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (pt_zCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.tranchedes,'Z')>0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('Z',a.tranchedes)>0 ";
                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.tranchedes,'Z')>0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('Z',a.tranchedes)>0 ";

                    isFound = true;
                }

            if (isFound == false)
                SQL += " 1=1 ";

            SQL += ") and (";

            isFound = false;

            if (fannieCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    SQL += " a.ticker in ('FNR','FNS','FNW','FSPC') ";
                    isFound = true;
                }
                else
                {
                    SQL += " or a.ticker in ('FNR','FNS','FNW','FSPC') ";
                    isFound = true;
                }

            if (freddieCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    SQL += " a.ticker in ('FHR','FHRR','FHS','FSPC') ";
                    isFound = true;
                }
                else
                {
                    SQL += " or a.ticker in ('FHR','FHRR','FHS','FSPC') ";
                    isFound = true;
                }

            if (ginnieCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    SQL += " a.ticker in ('GNR') ";
                    isFound = true;
                }
                else
                {
                    SQL += " or a.ticker in ('GNR') ";
                    isFound = true;
                }

            if (isFound == false)
                SQL += " 1=1 ";

            SQL += ") and (";
            isFound = false;
            /// NOW THE COLLATERAL TYPE
            /// 

            if (tenYrCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'10YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('10YR',a.groupDes) >0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.groupDes,'10YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('10YR',a.groupDes) >0 ";

                    isFound = true;
                }

            if (fifteenYrCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'15YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('15YR',a.groupDes) >0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.groupDes,'15YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('15YR',a.groupDes) >0";

                    isFound = true;
                }

            if (twentyYrCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'20YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('20YR',a.groupDes) >0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += "  OR instr(a.groupDes,'20YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('20YR',a.groupDes) >0 ";

                    isFound = true;
                }

            if (thirtyYrCheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'30YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('30YR',a.groupDes) >0";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr(a.groupDes,'30YR') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('30YR',a.groupDes) > 0 ";

                    isFound = true;
                }

            if (FNMAcheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'FNMA') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('FNMA',a.groupDes) >0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " OR instr(a.groupDes,'FNMA') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('FNMA',a.groupDes) >0 ";

                    isFound = true;
                }

            if (FHGLDcheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'FHGLD') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('FHGLD',a.groupDes) >0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.groupDes,'FHGLD') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('FHGLD',a.groupDes) >0 ";

                    isFound = true;
                }

            if (GNMAcheckBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'GNMA') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('GNMA',a.groupDes) >0 ";


                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.groupDes,'GNMA') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('GNMA',a.groupDes) > 0 ";

                    isFound = true;
                }

            if (GNMA2checkBox.CheckState.ToString().Equals("Checked"))
                if (isFound == false)
                {
                    if (usingSQLServer == false)
                        SQL += " instr(a.groupDes,'GNMA2') >0 ";

                    if (usingSQLServer == true)
                        SQL += " CHARINDEX('GNMA2',a.groupDes)>0 ";

                    isFound = true;
                }
                else
                {
                    if (usingSQLServer == false)
                        SQL += " or instr(a.groupDes,'GNMA2') >0 ";

                    if (usingSQLServer == true)
                        SQL += " or CHARINDEX('GNMA2',a.groupDes) >0 ";

                    isFound = true;
                }

            if (isFound == false)
                SQL += " 1=1 ";

            SQL += " ) ";

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand("drop table if exists tmp_CMO_finder;", cn);
                //cmd.ExecuteNonQuery();

                //cmd = cn.CreateCommand();
                ////*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                //cmd.CommandText = "select criteriaField, minVal, maxVal from tmp_CMO_criteria" + userID;

                //MySqlDataReader rdr;
                //rdr = cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    if (!rdr.GetValue(1).ToString().Equals(""))
                //    {
                //        SQL += " and " + rdr.GetValue(0).ToString() + " >= " + rdr.GetValue(1).ToString();
                //    }
                //    if (!rdr.GetValue(2).ToString().Equals(""))
                //    {
                //        SQL += " and " + rdr.GetValue(0).ToString() + " <= " + rdr.GetValue(2).ToString();
                //    }

                //}
                //rdr.Close();

                //if (cmoCheckForNew.Checked == true)
                //SQL += " and isNew='1' ";

                //cmd = new MySqlCommand(SQL, cn);
                //cmd.ExecuteNonQuery();

                //cn.Close();
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                //SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_CMO_finder" + userID + "', 'U') IS NOT NULL " +
                //    "DROP TABLE tmp_CMO_finder" + userID + ";", cn);

                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_CMO_finder', 'U') IS NOT NULL " +
               "DROP TABLE tmp_CMO_finder;", cn);

                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved" +
                    " where criteria_sector='CMO' and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Range'";

                //        cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                //" where criteria_sector='MBS' and criteria_searchName='" + MBSSearchescomboBox.Text.ToString() +
                //"' and criteria_descriptor='Range'";


                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") >= " + rdr.GetValue(1).ToString();
                    }
                    if (!rdr.GetValue(2).ToString().Equals(""))
                    {
                        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") <= " + rdr.GetValue(2).ToString();
                    }

                }
                rdr.Close();

                if (cmoCheckForNew.Checked == true)
                    SQL += " and isNew='1' ";

                cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_CMO_finder", "CMO");
            //tmp_CMO_finder

        }


        private void searchCMO()
        {
            // BUID SQL STRING !!
            string SQL;
            bool isFound = false;

            //---WORK ON QUERY FOR SQLSERVER...
            SQL = "";

            if (usingSQLServer == false)
            {
                SQL = " create table tmp_CMO_finder" + userID + " as select b.* from cmolookup a, "
                   + " cmospreadsheet b where a.id = b.id and b.sector='CMO'   AND ( ";
            }

            if (usingSQLServer == true)
            {
                SQL = " select b.* into tmp_CMO_finder" + userID + " from cmolookup a, "
                   + " cmospreadsheet b where a.id = b.id and b.sector='CMO' ";
            }

            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_CMO_finder" + userID + "', 'U') IS NOT NULL " +
               "DROP TABLE tmp_CMO_finder" + userID + ";", cn);

                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved" +
                    " where criteria_sector='CMO' and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Range' and criteria_searchowner='" + userID + "'";

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") >= " + rdr.GetValue(1).ToString();
                    }
                    if (!rdr.GetValue(2).ToString().Equals(""))
                    {
                        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") <= " + rdr.GetValue(2).ToString();
                    }

                }
                rdr.Close();

                SQL += " and (";
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved" +
                    " where criteria_sector='CMO' and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Text' and criteria_field='PType' and criteria_searchowner='" + userID + "'";

                isFound = false;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        if (isFound == false)
                        {
                            isFound = true;
                            SQL += " CHARINDEX( '" + rdr.GetValue(1).ToString() + "',a.trancheDes)>0 ";
                        }
                        else
                        {
                            SQL += " or CHARINDEX( '" + rdr.GetValue(1).ToString() + "',a.trancheDes)>0 ";
                        }

                    }

                }
                rdr.Close();

                if (isFound == false)
                    SQL += " 1=1 ";

                SQL += ") ";

                isFound = false;

                SQL += " and (";
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved" +
                    " where criteria_sector='CMO' and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Text' and criteria_field='CType' and criteria_searchowner='" + userID + "'";

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        if (isFound == false)
                        {
                            isFound = true;
                            SQL += " CHARINDEX( '" + rdr.GetValue(1).ToString() + "',a.GroupDes)>0 ";
                        }
                        else
                        {
                            SQL += " or CHARINDEX( '" + rdr.GetValue(1).ToString() + "',a.GroupDes)>0 ";
                        }

                    }

                }
                rdr.Close();

                if (isFound == false)
                    SQL += " 1=1 ";

                SQL += ") ";

                isFound = false;

                SQL += " and (";
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved" +
                    " where criteria_sector='CMO' and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Text' and criteria_field='Ticker' and criteria_searchowner='" + userID + "'";

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        if (isFound == false)
                        {
                            isFound = true;
                            SQL += " a.Ticker='" + rdr.GetValue(1).ToString() + "' ";
                        }
                        else
                        {
                            SQL += " or a.Ticker='" + rdr.GetValue(1).ToString() + "' ";
                        }

                    }

                }
                rdr.Close();

                if (isFound == false)
                    SQL += " 1=1 ";

                SQL += ") ";

                if (cmoCheckForNew.Checked == true)
                    SQL += " and isNew='1' ";

                cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_CMO_finder" + userID, "CMO");
            //tmp_CMO_finder

        }

        private void SearchCORP()
        {
            // BUID SQL STRING !!
            string SQL;
            //bool isFound = false;
            //string[] txtFlds;
            //int i;

            //---WORK ON QUERY FOR SQLSERVER...
            SQL = "";
            //i = 0;

            if (usingSQLServer == false)
            {
                //SQL = " create table tmp_MBS_finder as select b.* from cmolookup a, "
                //   + " cmospreadsheet b where a.id = b.id AND B.SECTOR='MBS' AND ( ";
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("", cn);
                //cmd.ExecuteNonQuery();

                SQL = " select COUNT(*) from PW_INVENTORIES a where a.SECTOR='CORP' ";

                cmd = cn.CreateCommand();
                SqlDataReader rdr;

                if (CORPTickercheckedListBox.CheckedItems.Count > 0)
                {
                    SQL += " and TICKER IN (";

                    for (int x = 0; x < CORPTickercheckedListBox.Items.Count; x++)
                    {
                        //CheckState ST = CORPTickercheckedListBox.GetItemChecked(x);
                        if (CORPTickercheckedListBox.GetItemChecked(x) == true)
                            SQL += "'" + CORPTickercheckedListBox.Items[x].ToString() + "',";


                    }
                    SQL = SQL.Substring(0, SQL.Length - 1) + ") ";
                    //SQL += ") ";

                }


                if (CORPdealercheckedListBox.CheckedItems.Count > 0)
                {
                    SQL += " and DEALER in (";

                    for (int x = 0; x < CORPdealercheckedListBox.Items.Count; x++)
                    {
                        if (CORPdealercheckedListBox.GetItemChecked(x))
                            SQL += "'" + CORPdealercheckedListBox.Items[x].ToString() + "',";
                    }
                    SQL = SQL.Substring(0, SQL.Length - 1) + ") ";
                }

                //MessageBox.Show(SQL);
                //CORPstatusStrip.Text = SQL.ToString();


                ////*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                ////MessageBox.Show(MBSSearchescomboBox.Text.ToString());
                //cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                //    " where criteria_sector='MBS' and criteria_searchName='" + MBSSearchescomboBox.Text.ToString() +
                //    "' and criteria_descriptor='Range' ";

                //if (MBSradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                //    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}

                //rdr = cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    if (!rdr.GetValue(1).ToString().Equals(""))
                //    {
                //        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") >= " + rdr.GetValue(1).ToString();
                //    }
                //    if (!rdr.GetValue(2).ToString().Equals(""))
                //    {
                //        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") <= " + rdr.GetValue(2).ToString();
                //    }
                //}
                //rdr.Close();

                //cmd.CommandText = "select count(distinct criteria_field) as cnt from InventoryCriteriaSaved " +
                //    " where criteria_sector='MBS' and criteria_descriptor='Text' and criteria_searchname='" +
                //    MBSSearchescomboBox.Text.ToString() + "' ";

                //if (MBSradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                //    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}


                //rdr = cmd.ExecuteReader();
                //rdr.Read();
                //txtFlds = new string[Convert.ToInt32(rdr.GetValue(0))];
                //rdr.Close();

                //cmd.CommandText = "select distinct criteria_field from InventoryCriteriaSaved " +
                //    " where criteria_sector='MBS' and criteria_descriptor='Text' and criteria_searchname='" +
                //    MBSSearchescomboBox.Text.ToString() + "' ";

                //if (MBSradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                //    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}

                //rdr = cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    txtFlds[i] = rdr.GetValue(0).ToString();
                //    i++;
                //}
                //rdr.Close();

                //foreach (string searchFld in txtFlds)
                //{
                //    SQL += " and " + searchFld + " in (";

                //    cmd.CommandText = "select criteria_min from InventoryCriteriaSaved " +
                //      " where criteria_sector='MBS' and criteria_searchname='" +
                //      MBSSearchescomboBox.Text.ToString() + "' and criteria_field='" + searchFld + "' ";

                //    if (MBSradioButtonGroup.Checked == true)
                //    {
                //        cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //    }
                //    else
                //    {
                //        cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //    }

                //    rdr = cmd.ExecuteReader();
                //    while (rdr.Read())
                //    {
                //        SQL += " '" + rdr.GetValue(0).ToString() + "',";
                //    }
                //    rdr.Close();
                //    SQL = SQL.Substring(0, SQL.Length - 1);
                //    SQL += ") ";
                //}

                ////MessageBox.Show(mbsCheckForNew.Checked.ToString());

                //if (mbsCheckForNew.Checked == true)
                //    SQL += " and isNew='1' ";

                //SQL += " order by client";
                ////MessageBox.Show(SQL.ToString());


                cmd.CommandText = SQL;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                CorptoolStripStatusLabel.Text = rdr.GetValue(0).ToString() + " Bonds found";
                rdr.Close();

                cn.Close();
            }

            //Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_MBS_finder" + userID, "MBS");
        }

        private void updateFinderField(int colNum )
        {

            String fieldToUpdate;
            int idCol;

            if (Globals.ThisAddIn.Application.Workbooks.Count == 0)
                Globals.ThisAddIn.Application.Workbooks.Add();

            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;

            sheet = (Worksheet)bk.ActiveSheet;

        }

        private void pullCORP()
        {
            // BUID SQL STRING !!
            string SQL;
            //bool isFound = false;
            //string[] txtFlds;
            int i;

            bool useHeaders = false;
            int timesBlank = 0;
            int col = 0;

            string[] colName;
            int[] colID;

            if (Globals.ThisAddIn.Application.Workbooks.Count == 0)
                Globals.ThisAddIn.Application.Workbooks.Add();

            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;

            //MessageBox.Show(Globals.ThisAddIn.Application.Workbooks.Count.ToString());

            sheet = (Worksheet)bk.ActiveSheet;

            


            //---WORK ON QUERY FOR SQLSERVER...
            SQL = "";
            i = 0;

            if (usingSQLServer == false)
            {
                //SQL = " create table tmp_MBS_finder as select b.* from cmolookup a, "
                //   + " cmospreadsheet b where a.id = b.id AND B.SECTOR='MBS' AND ( ";
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("", cn);
                //cmd.ExecuteNonQuery();

                SQL = " select * from PW_INVENTORIES a where a.SECTOR='CORP' ";

                cmd = cn.CreateCommand();
                SqlDataReader rdr;

                if (CORPTickercheckedListBox.CheckedItems.Count > 0)
                {
                    SQL += " and TICKER IN (";

                    for (int x = 0; x < CORPTickercheckedListBox.Items.Count; x++)
                    {
                        //CheckState ST = CORPTickercheckedListBox.GetItemChecked(x);
                        if (CORPTickercheckedListBox.GetItemChecked(x) == true)
                            SQL += "'" + CORPTickercheckedListBox.Items[x].ToString() + "',";


                    }
                    SQL = SQL.Substring(0, SQL.Length - 1) + ") ";
                    //SQL += ") ";

                }

                if (CORPdealercheckedListBox.CheckedItems.Count > 0)
                {
                    SQL += " and DEALER in (";

                    for (int x = 0; x < CORPdealercheckedListBox.Items.Count; x++)
                    {
                        if (CORPdealercheckedListBox.GetItemChecked(x))
                            SQL += "'" + CORPdealercheckedListBox.Items[x].ToString() + "',";
                    }
                    SQL = SQL.Substring(0, SQL.Length - 1) + ") ";
                }

                //MessageBox.Show(SQL);
                //CORPstatusStrip.Text = SQL.ToString();


                ////*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                ////MessageBox.Show(MBSSearchescomboBox.Text.ToString());
                //cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                //    " where criteria_sector='MBS' and criteria_searchName='" + MBSSearchescomboBox.Text.ToString() +
                //    "' and criteria_descriptor='Range' ";

                //if (MBSradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                //    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}

                //rdr = cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    if (!rdr.GetValue(1).ToString().Equals(""))
                //    {
                //        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") >= " + rdr.GetValue(1).ToString();
                //    }
                //    if (!rdr.GetValue(2).ToString().Equals(""))
                //    {
                //        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") <= " + rdr.GetValue(2).ToString();
                //    }
                //}
                //rdr.Close();

                //cmd.CommandText = "select count(distinct criteria_field) as cnt from InventoryCriteriaSaved " +
                //    " where criteria_sector='MBS' and criteria_descriptor='Text' and criteria_searchname='" +
                //    MBSSearchescomboBox.Text.ToString() + "' ";

                //if (MBSradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                //    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}


                //rdr = cmd.ExecuteReader();
                //rdr.Read();
                //txtFlds = new string[Convert.ToInt32(rdr.GetValue(0))];
                //rdr.Close();

                //cmd.CommandText = "select distinct criteria_field from InventoryCriteriaSaved " +
                //    " where criteria_sector='MBS' and criteria_descriptor='Text' and criteria_searchname='" +
                //    MBSSearchescomboBox.Text.ToString() + "' ";

                //if (MBSradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                //    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}

                //rdr = cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    txtFlds[i] = rdr.GetValue(0).ToString();
                //    i++;
                //}
                //rdr.Close();

                //foreach (string searchFld in txtFlds)
                //{
                //    SQL += " and " + searchFld + " in (";

                //    cmd.CommandText = "select criteria_min from InventoryCriteriaSaved " +
                //      " where criteria_sector='MBS' and criteria_searchname='" +
                //      MBSSearchescomboBox.Text.ToString() + "' and criteria_field='" + searchFld + "' ";

                //    if (MBSradioButtonGroup.Checked == true)
                //    {
                //        cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //    }
                //    else
                //    {
                //        cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //    }

                //    rdr = cmd.ExecuteReader();
                //    while (rdr.Read())
                //    {
                //        SQL += " '" + rdr.GetValue(0).ToString() + "',";
                //    }
                //    rdr.Close();
                //    SQL = SQL.Substring(0, SQL.Length - 1);
                //    SQL += ") ";
                //}

                ////MessageBox.Show(mbsCheckForNew.Checked.ToString());

                //if (mbsCheckForNew.Checked == true)
                //    SQL += " and isNew='1' ";

                //SQL += " order by client";
                ////MessageBox.Show(SQL.ToString());

                cmd.CommandText = SQL;

                // --- IS SHEET A TEMPLATE AND ALREADY CONTAINS COLUMN HEADERS IN ROW 5?  IF SO THEN LOOK FOR THOSE HEADERS
                colName = new string[0];
                colID = new int[0];

                i = 0;
                col = 3;
                while (timesBlank <= 10) // && useHeaders==false
                {
                    // MessageBox.Show(sheet.Cells[5, col].ToString());
                    if (sheet.Cells[5, col] == null || sheet.Cells[5, col].Value2 == null || sheet.Cells[5, col].Value2.ToString() == "")
                    {
                        timesBlank++;
                    }
                    else
                    {
                        timesBlank = 0;
                        i++;
                        Array.Resize(ref colName, i);
                        Array.Resize(ref colID, i);

                        colName[i - 1] = sheet.Cells[5, col].text;
                        //MessageBox.Show(colName[i - 1]);
                        colID[i - 1] = col;
                        if (useHeaders == false)
                            useHeaders = true;

                    }
                    col++;
                }

                if (useHeaders==true && colID.Length>0)
                {
                    sheet.Range[sheet.Cells[6, 3], sheet.Cells[15000, colID[i - 1]]].ClearContents();
                }
                else
                {
                    sheet.Range[sheet.Cells[6, 3], sheet.Cells[15000, 20]].ClearContents();
                }

                // sheet.Range["A6:AZ5000"].ClearContents();

                rdr = cmd.ExecuteReader();
                int row = 6;

                while (rdr.Read())
                {
                    if (useHeaders == true)
                    {
                        for (int x = 0; x < colName.Length; x++)
                        {
                            sheet.Cells[row, colID[x]] = rdr.GetValue(rdr.GetOrdinal(colName[x]));
                        }
                    }
                    else
                    {
                        for (int x = 0; x < rdr.FieldCount; x++)
                        {
                            if (row == 6)
                                sheet.Cells[5, x + 3] = rdr.GetName(x).ToString();


                            sheet.Cells[row, x + 3] = rdr.GetValue(x).ToString();
                            //sheet.Cells[row, x + 3] = rdr.GetValue(rdr.GetOrdinal("CUSIP"));

                        }

                    }
                    row++;
                }

                rdr.Close();

                cn.Close();
            }

            //Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_MBS_finder" + userID, "MBS");
        }

        private void SearchMBS()
        {
            // BUID SQL STRING !!
            string SQL;
            //bool isFound = false;
            string[] txtFlds;
            int i;

            //---WORK ON QUERY FOR SQLSERVER...
            SQL = "";
            i = 0;

            if (usingSQLServer == false)
            {
                SQL = " create table tmp_MBS_finder as select b.* from cmolookup a, "
                   + " cmospreadsheet b where a.id = b.id AND B.SECTOR='MBS' AND ( ";
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_MBS_finder" + userID + "', 'U') IS NOT NULL " +
                    "DROP TABLE tmp_MBS_finder" + userID + ";", cn);
                cmd.ExecuteNonQuery();

                SQL = " select b.* into tmp_MBS_finder" + userID + " from cmolookup a, "
                   + " cmospreadsheet b where a.id = b.id  AND B.SECTOR='MBS' ";

                cmd = cn.CreateCommand();

                //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                //MessageBox.Show(MBSSearchescomboBox.Text.ToString());
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_searchName='" + MBSSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Range' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") >= " + rdr.GetValue(1).ToString();
                    }
                    if (!rdr.GetValue(2).ToString().Equals(""))
                    {
                        SQL += " and CONVERT(float," + rdr.GetValue(0).ToString() + ") <= " + rdr.GetValue(2).ToString();
                    }
                }
                rdr.Close();

                cmd.CommandText = "select count(distinct criteria_field) as cnt from InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_descriptor='Text' and criteria_searchname='" +
                    MBSSearchescomboBox.Text.ToString() + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }


                rdr = cmd.ExecuteReader();
                rdr.Read();
                txtFlds = new string[Convert.ToInt32(rdr.GetValue(0))];
                rdr.Close();

                cmd.CommandText = "select distinct criteria_field from InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_descriptor='Text' and criteria_searchname='" +
                    MBSSearchescomboBox.Text.ToString() + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtFlds[i] = rdr.GetValue(0).ToString();
                    i++;
                }
                rdr.Close();

                foreach (string searchFld in txtFlds)
                {
                    SQL += " and " + searchFld + " in (";

                    cmd.CommandText = "select criteria_min from InventoryCriteriaSaved " +
                      " where criteria_sector='MBS' and criteria_searchname='" +
                      MBSSearchescomboBox.Text.ToString() + "' and criteria_field='" + searchFld + "' ";

                    if (MBSradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                    }
                    else
                    {
                        cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                    }

                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SQL += " '" + rdr.GetValue(0).ToString() + "',";
                    }
                    rdr.Close();
                    SQL = SQL.Substring(0, SQL.Length - 1);
                    SQL += ") ";
                }

                //MessageBox.Show(mbsCheckForNew.Checked.ToString());

                if (mbsCheckForNew.Checked == true)
                    SQL += " and isNew='1' ";

                SQL += " order by client";
                //MessageBox.Show(SQL.ToString());

                cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_MBS_finder" + userID, "MBS");
        }

        private void searchMUNI()
        {
            // BUID SQL STRING !!
            string SQL;
            //bool isFound = false;
            string[] txtFlds;
            int i;

            //---WORK ON QUERY FOR SQLSERVER...
            SQL = "";
            i = 0;

            if (usingSQLServer == false)
            {
                SQL = " create table tmp_MUNI_finder as select b.* from munilookup a, "
                   + " muniinventory b where a.id = b.id AND B.SECTOR='MUNI' AND ( ";
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_MUNI_finder" + userID + "', 'U') IS NOT NULL " +
                    "DROP TABLE tmp_MUNI_finder" + userID + ";", cn);
                cmd.ExecuteNonQuery();

                SQL = " select b.* into tmp_MUNI_finder" + userID + " from munilookup a, "
                   + " muniinventory b where a.id = b.id  AND B.SECTOR='MUNI' ";

                cmd = cn.CreateCommand();

                //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                //MessageBox.Show(MBSSearchescomboBox.Text.ToString());
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='MUNI' and criteria_searchName='" + muniSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='BQ' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                try
                {
                    if (rdr.GetValue(1).Equals("Y"))
                    {
                        SQL += " and Bank_Qualified='Y'";
                    }
                    else
                    {
                        SQL += " and Bank_Qualified='N'";
                    }
                }
                catch
                {
                }
                rdr.Close();

                //********* REFUNDED ******************
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='MUNI' and criteria_searchName='" + muniSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Refunded' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                rdr = cmd.ExecuteReader();
                rdr.Read();
                try
                {
                    if (rdr.GetValue(1).Equals("Y"))
                    {
                        SQL += " and Refunded='Y'";
                    }
                    else
                    {
                        SQL += " and Refunded='N'";
                    }
                }
                catch
                {
                }
                rdr.Close();

                //********* FED TAX ******************
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='MUNI' and criteria_searchName='" + muniSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='FedTaxable' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                rdr = cmd.ExecuteReader();
                rdr.Read();
                try
                {
                    if (rdr.GetValue(1).Equals("Y"))
                    {
                        SQL += " and Fed_Tax='Y'";
                    }
                    else
                    {
                        SQL += " and Fed_Tax='N'";
                    }
                }
                catch
                {
                }
                rdr.Close();

                //********* STATE TAX ******************
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='MUNI' and criteria_searchName='" + muniSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='StateTaxable' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                rdr = cmd.ExecuteReader();
                rdr.Read();
                try
                {
                    if (rdr.GetValue(1).Equals("Y"))
                    {
                        SQL += " and State_Tax='Y'";
                    }
                    else
                    {
                        SQL += " and State_Tax='N'";
                    }
                }
                catch
                {
                }
                rdr.Close();


                //********* Text Types ******************
                cmd.CommandText = "select count(distinct criteria_field) as cnt from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_descriptor='Text' and criteria_searchname='" +
                  muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                rdr = cmd.ExecuteReader();
                rdr.Read();
                txtFlds = new string[Convert.ToInt32(rdr.GetValue(0))];
                rdr.Close();

                cmd.CommandText = "select distinct(criteria_Field) from InventoryCriteriaSaved " +
                    " where criteria_sector='MUNI' and criteria_searchName='" + muniSearchescomboBox.Text.ToString() +
                    "' and criteria_descriptor='Text' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                }

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtFlds[i] = rdr.GetValue(0).ToString();
                    i++;
                }
                rdr.Close();

                foreach (string searchFld in txtFlds)
                {
                    SQL += " and " + searchFld + " in (";

                    cmd.CommandText = "select criteria_min from InventoryCriteriaSaved " +
                      " where criteria_sector='MUNI' and criteria_searchname='" +
                      muniSearchescomboBox.Text.ToString() + "' and criteria_field='" + searchFld + "' ";

                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                    }
                    else
                    {
                        cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                    }

                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (!rdr.GetValue(0).ToString().Equals("Missing"))
                        {
                            SQL += " '" + rdr.GetValue(0).ToString() + "',";
                        }
                        else
                        {
                            SQL += " '',";
                        }
                    }
                    rdr.Close();
                    SQL = SQL.Substring(0, SQL.Length - 1);
                    SQL += ") ";
                }


                SQL += " order by client";

                cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_MUNI_finder" + userID, "MUNI");
        }

        private void searchClient()
        {
            // BUID SQL STRING !!
            string SQL;
            bool isFound = false;

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand("drop table if exists tmp_MostRecent;", cn);
                //cmd.ExecuteNonQuery();

                //cmd = new MySqlCommand("create table tmp_MostRecent as select NIS_Client, "
                //   + " max(NIS_asof) as AsOf, '' as PortID "
                //   + " from fig.nisportfolioloads WHERE length(nis_portfolio)=9 "
                //   + " and substring(nis_portfolio,5,1)=' ' "
                //   + " and (substring(nis_portfolio,8,2) = date_format(curdate(),'%y') or "
                //   + " substring(nis_portfolio,8,2) = "
                //   + " date_format(date_sub(curdate(),INTERVAL 1 YEAR),'%y') )group by nis_client;", cn);

                //cmd.ExecuteNonQuery();

                //cmd = new MySqlCommand("alter table tmp_MostRecent change PortID PortID char(55);", cn);
                //cmd.ExecuteNonQuery();

                //cmd = new MySqlCommand("update tmp_MostRecent a, nisportfolioloads b "
                //   + " set PortID = nis_portfolio where a.nis_client=b.nis_client and a.asof=b.nis_asof;", cn);
                //cmd.ExecuteNonQuery();

                //cmd = new MySqlCommand("alter table tmp_MostRecent add index PortID (PortID);", cn);
                //cmd.ExecuteNonQuery();

                //cmd = new MySqlCommand("drop table if exists tmp_Client_finder;", cn);
                //cmd.ExecuteNonQuery();

                //SQL = " create table tmp_Client_finder as select a.* from nisdetail a, "
                //   + " tmp_MostRecent b where portfolio = Portid AND ( ";

                //if (sect_FixCMOcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FIXED CMO' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR type6='FIXED CMO' ";
                //        isFound = true;
                //    }

                //if (sect_FixCorpcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FIXED CORPOR' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR type6='FIXED CORPOR' ";
                //        isFound = true;
                //    }

                //if (sect_FixedAgencycheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FIXED AGENCY' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR type6='FIXED AGENCY' ";
                //        isFound = true;
                //    }

                //if (sect_FixMBScheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FIXED MBS' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR type6='FIXED MBS' ";
                //        isFound = true;
                //    }

                //if (sect_FixOthercheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FIXED OTHER' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR type6='FIXED OTHER' ";
                //        isFound = true;
                //    }

                //if (sect_floatAgencycheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FLOATING AGE' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='FLOATING AGE' ";
                //        isFound = true;
                //    }

                //if (sect_floatCMOcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FLOATING CMO' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='FLOATING CMO' ";
                //        isFound = true;
                //    }

                //if (sect_floatCorpcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FLOATING COR' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='FLOATING COR' ";
                //        isFound = true;
                //    }

                //if (sect_floatMBScheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FLOATING MBS' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='FLOATING MBS' ";
                //        isFound = true;
                //    }

                //if (sect_floatOthercheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='FLOATING OTH' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='FLOATING OTH' ";
                //        isFound = true;
                //    }

                //if (sect_taxFreeMunicheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='Taxfree Muni' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='Taxfree Muni' ";
                //        isFound = true;
                //    }
                //if (sect_taxMunicheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='Taxable Muni' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='Taxable Muni' ";
                //        isFound = true;
                //    }
                //if (sect_treasurycheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " type6='Treasury' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or type6='Treasury' ";
                //        isFound = true;
                //    }

                //if (isFound == false)
                //    SQL += " 1=1 ";

                ////COUPON TYPE BELOW
                //SQL += ") and (";

                //isFound = false;

                //if (coupon_AdjustablecheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " coupon_type='ADJUST' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or coupon_type='ADJUST' ";
                //        isFound = true;
                //    }

                //if (coupon_FixedcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " coupon_type='FIXED' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or coupon_type='FIXED' ";
                //        isFound = true;
                //    }

                //if (coupon_MulticheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " coupon_type='MULTI' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or coupon_type='MULTI' ";
                //        isFound = true;
                //    }

                //if (coupon_SinglecheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " coupon_type='SINGLE' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or coupon_type='SINGLE' ";
                //        isFound = true;
                //    }

                //if (isFound == false)
                //    SQL += " 1=1 ";

                //SQL += ") and (";
                //isFound = false;
                ///// NOW THE MOODYS RATING
                ///// 

                //if (moody_A1checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='A1' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='A1' ";
                //        isFound = true;
                //    }

                //if (moody_A2checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='A2' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='A2' ";
                //        isFound = true;
                //    }

                //if (moody_A3checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='A3' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += "  OR moodys='A3' ";
                //        isFound = true;
                //    }

                //if (moody_AA1checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Aa1' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR moodys='Aa1' ";
                //        isFound = true;
                //    }

                //if (moody_AA2checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Aa2' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR moodys='Aa2' ";
                //        isFound = true;
                //    }

                //if (moody_AA3checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Aa3' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='Aa3' ";
                //        isFound = true;
                //    }

                //if (moody_AAAcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Aaa' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='Aaa' ";
                //        isFound = true;
                //    }

                //if (moody_BAA1checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Baa1' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='Baa1' ";
                //        isFound = true;
                //    }

                //if (moody_BAA2checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Baa2' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='Baa2' ";
                //        isFound = true;
                //    }

                //if (moody_BAA3checkBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys='Baa3' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys='Baa3' ";
                //        isFound = true;
                //    }

                //if (moody_OthercheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " moodys not in ('Baa3','Baa2','Baa1','Aaa','Aa3','Aa2','Aa1','A3','A2','A1') ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or moodys not in ('Baa3','Baa2','Baa1','Aaa','Aa3','Aa2','Aa1','A3','A2','A1') ";
                //        isFound = true;
                //    }

                //if (isFound == false)
                //    SQL += " 1=1 ";

                //SQL += ") and (";
                //isFound = false;
                ///// NOW THE SP RATING
                ///// 

                //if (sp_AAAcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='AAA' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='AAA' ";
                //        isFound = true;
                //    }

                //if (sp_AAcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='AA' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='AA' ";
                //        isFound = true;
                //    }

                //if (sp_AAMcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='AA-' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR s_and_p='AA-' ";
                //        isFound = true;
                //    }

                //if (sp_AAPcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='AA+' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR s_and_p='AA+' ";
                //        isFound = true;
                //    }

                //if (sp_AcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='A' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " OR s_and_p='A' ";
                //        isFound = true;
                //    }

                //if (sp_AMcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='A-' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='A-' ";
                //        isFound = true;
                //    }

                //if (sp_APcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='A+' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='A+' ";
                //        isFound = true;
                //    }

                //if (sp_BBBcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='BBB' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='BBB' ";
                //        isFound = true;
                //    }

                //if (sp_BBBMcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='BBB-' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='BBB-' ";
                //        isFound = true;
                //    }

                //if (sp_BBBPcheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p='BBB+' ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p='BBB+' ";
                //        isFound = true;
                //    }

                //if (sp_OthercheckBox.CheckState.ToString().Equals("Checked"))
                //    if (isFound == false)
                //    {
                //        SQL += " s_and_p not in ('BBB+','BBB-','BBB','AAA','AA+','AA','AA-','A+','A','A-' ) ";
                //        isFound = true;
                //    }
                //    else
                //    {
                //        SQL += " or s_and_p not in ('BBB+','BBB-','BBB','AAA','AA+','AA','AA-','A+','A','A-' ) ";
                //        isFound = true;
                //    }

                //if (isFound == false)
                //    SQL += " 1=1 ";


                //SQL += " ) ";

                //cmd = cn.CreateCommand();
                ////*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                //cmd.CommandText = "select criteriaField, minVal, maxVal from tmp_Client_criteria" + userID;

                //MySqlDataReader rdr;
                //rdr = cmd.ExecuteReader();
                //while (rdr.Read())
                //{
                //    if (!rdr.GetValue(1).ToString().Equals(""))
                //    {
                //        SQL += " and " + rdr.GetValue(0).ToString() + " >= " + rdr.GetValue(1).ToString();
                //    }
                //    if (!rdr.GetValue(2).ToString().Equals(""))
                //    {
                //        SQL += " and " + rdr.GetValue(0).ToString() + " <= " + rdr.GetValue(2).ToString();
                //    }

                //}
                //rdr.Close();

                //cmd = new MySqlCommand(SQL, cn);
                //cmd.ExecuteNonQuery();

                //cn.Close();
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_MostRecent', 'U') IS NOT NULL " +
                   "DROP TABLE tmp_MostRecent;", cn);

                cmd.ExecuteNonQuery();

                //---QUERY NEEDING RE-WRITE
                cmd = new SqlCommand("create table tmp_MostRecent as select NIS_Client, "
                   + " max(NIS_asof) as AsOf, '' as PortID "
                   + " from fig.nisportfolioloads WHERE length(nis_portfolio)=9 "
                   + " and substring(nis_portfolio,5,1)=' ' "
                   + " and (substring(nis_portfolio,8,2) = date_format(curdate(),'%y') or "
                   + " substring(nis_portfolio,8,2) = "
                   + " date_format(date_sub(curdate(),INTERVAL 1 YEAR),'%y') )group by nis_client;", cn);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("alter table tmp_MostRecent change PortID PortID char(55);", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("update tmp_MostRecent a, nisportfolioloads b "
                   + " set PortID = nis_portfolio where a.nis_client=b.nis_client and a.asof=b.nis_asof;", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("alter table tmp_MostRecent add index PortID (PortID);", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("IF OBJECT_ID('tmp_Client_finder', 'U') IS NOT NULL " +
                   " DROP TABLE tmp_Client_finder;", cn);
                cmd.ExecuteNonQuery();

                SQL = " create table tmp_Client_finder as select a.* from nisdetail a, "
                   + " tmp_MostRecent b where portfolio = Portid AND ( ";

                if (sect_FixCMOcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FIXED CMO' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR type6='FIXED CMO' ";
                        isFound = true;
                    }

                if (sect_FixCorpcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FIXED CORPOR' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR type6='FIXED CORPOR' ";
                        isFound = true;
                    }

                if (sect_FixedAgencycheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FIXED AGENCY' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR type6='FIXED AGENCY' ";
                        isFound = true;
                    }

                if (sect_FixMBScheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FIXED MBS' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR type6='FIXED MBS' ";
                        isFound = true;
                    }

                if (sect_FixOthercheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FIXED OTHER' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR type6='FIXED OTHER' ";
                        isFound = true;
                    }

                if (sect_floatAgencycheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FLOATING AGE' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='FLOATING AGE' ";
                        isFound = true;
                    }

                if (sect_floatCMOcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FLOATING CMO' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='FLOATING CMO' ";
                        isFound = true;
                    }

                if (sect_floatCorpcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FLOATING COR' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='FLOATING COR' ";
                        isFound = true;
                    }

                if (sect_floatMBScheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FLOATING MBS' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='FLOATING MBS' ";
                        isFound = true;
                    }

                if (sect_floatOthercheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='FLOATING OTH' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='FLOATING OTH' ";
                        isFound = true;
                    }

                if (sect_taxFreeMunicheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='Taxfree Muni' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='Taxfree Muni' ";
                        isFound = true;
                    }
                if (sect_taxMunicheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='Taxable Muni' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='Taxable Muni' ";
                        isFound = true;
                    }
                if (sect_treasurycheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " type6='Treasury' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or type6='Treasury' ";
                        isFound = true;
                    }

                if (isFound == false)
                    SQL += " 1=1 ";

                //COUPON TYPE BELOW
                SQL += ") and (";

                isFound = false;

                if (coupon_AdjustablecheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " coupon_type='ADJUST' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or coupon_type='ADJUST' ";
                        isFound = true;
                    }

                if (coupon_FixedcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " coupon_type='FIXED' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or coupon_type='FIXED' ";
                        isFound = true;
                    }

                if (coupon_MulticheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " coupon_type='MULTI' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or coupon_type='MULTI' ";
                        isFound = true;
                    }

                if (coupon_SinglecheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " coupon_type='SINGLE' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or coupon_type='SINGLE' ";
                        isFound = true;
                    }

                if (isFound == false)
                    SQL += " 1=1 ";

                SQL += ") and (";
                isFound = false;
                /// NOW THE MOODYS RATING
                /// 

                if (moody_A1checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='A1' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='A1' ";
                        isFound = true;
                    }

                if (moody_A2checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='A2' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='A2' ";
                        isFound = true;
                    }

                if (moody_A3checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='A3' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += "  OR moodys='A3' ";
                        isFound = true;
                    }

                if (moody_AA1checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Aa1' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR moodys='Aa1' ";
                        isFound = true;
                    }

                if (moody_AA2checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Aa2' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR moodys='Aa2' ";
                        isFound = true;
                    }

                if (moody_AA3checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Aa3' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='Aa3' ";
                        isFound = true;
                    }

                if (moody_AAAcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Aaa' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='Aaa' ";
                        isFound = true;
                    }

                if (moody_BAA1checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Baa1' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='Baa1' ";
                        isFound = true;
                    }

                if (moody_BAA2checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Baa2' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='Baa2' ";
                        isFound = true;
                    }

                if (moody_BAA3checkBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys='Baa3' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys='Baa3' ";
                        isFound = true;
                    }

                if (moody_OthercheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " moodys not in ('Baa3','Baa2','Baa1','Aaa','Aa3','Aa2','Aa1','A3','A2','A1') ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or moodys not in ('Baa3','Baa2','Baa1','Aaa','Aa3','Aa2','Aa1','A3','A2','A1') ";
                        isFound = true;
                    }

                if (isFound == false)
                    SQL += " 1=1 ";

                SQL += ") and (";
                isFound = false;
                /// NOW THE SP RATING
                /// 

                if (sp_AAAcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='AAA' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='AAA' ";
                        isFound = true;
                    }

                if (sp_AAcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='AA' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='AA' ";
                        isFound = true;
                    }

                if (sp_AAMcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='AA-' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR s_and_p='AA-' ";
                        isFound = true;
                    }

                if (sp_AAPcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='AA+' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR s_and_p='AA+' ";
                        isFound = true;
                    }

                if (sp_AcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='A' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " OR s_and_p='A' ";
                        isFound = true;
                    }

                if (sp_AMcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='A-' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='A-' ";
                        isFound = true;
                    }

                if (sp_APcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='A+' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='A+' ";
                        isFound = true;
                    }

                if (sp_BBBcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='BBB' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='BBB' ";
                        isFound = true;
                    }

                if (sp_BBBMcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='BBB-' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='BBB-' ";
                        isFound = true;
                    }

                if (sp_BBBPcheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p='BBB+' ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p='BBB+' ";
                        isFound = true;
                    }

                if (sp_OthercheckBox.CheckState.ToString().Equals("Checked"))
                    if (isFound == false)
                    {
                        SQL += " s_and_p not in ('BBB+','BBB-','BBB','AAA','AA+','AA','AA-','A+','A','A-' ) ";
                        isFound = true;
                    }
                    else
                    {
                        SQL += " or s_and_p not in ('BBB+','BBB-','BBB','AAA','AA+','AA','AA-','A+','A','A-' ) ";
                        isFound = true;
                    }

                if (isFound == false)
                    SQL += " 1=1 ";


                SQL += " ) ";

                cmd = cn.CreateCommand();
                //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                cmd.CommandText = "select criteriaField, minVal, maxVal from tmp_Client_criteria" + userID;

                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.GetValue(1).ToString().Equals(""))
                    {
                        SQL += " and " + rdr.GetValue(0).ToString() + " >= " + rdr.GetValue(1).ToString();
                    }
                    if (!rdr.GetValue(2).ToString().Equals(""))
                    {
                        SQL += " and " + rdr.GetValue(0).ToString() + " <= " + rdr.GetValue(2).ToString();
                    }

                }
                rdr.Close();

                cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();

            }


            Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_Client_finder", "ClientBond");

        }


        private void AllCollateralCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkForAllPrincipalType()
        {

            if (pt_adCheckBox.Checked == true && pt_cstrCheckBox.Checked == true &&
               pt_exchCheckBox.Checked == true && pt_fltCheckBox.Checked == true &&
               pt_mrCheckBox.Checked == true && pt_otherCheckBox.Checked == true &&
               pt_pac1CheckBox.Checked == true && pt_pac2CheckBox.Checked == true &&
               pt_rtlCheckBox.Checked == true && pt_scCheckBox.Checked == true &&
               pt_supCheckBox.Checked == true && pt_zCheckBox.Checked == true)
            {
                AllPrincipalType.Checked = true;
            }
            else
            {
                AllPrincipalType.Checked = false;
            }

        }

        private void checkForAll()
        {
            if (fifteenYrCheckBox.Checked == true && twentyYrCheckBox.Checked == true &&
                  thirtyYrCheckBox.Checked == true && FNMAcheckBox.Checked == true &&
                  FHGLDcheckBox.Checked == true && GNMAcheckBox.Checked == true &&
                  GNMA2checkBox.Checked == true && tenYrCheckBox.Checked == true)
            {
                AllCollateralCheckBox.Checked = true;
            }
            else
            {
                AllCollateralCheckBox.Checked = false;
            }
        }

        private void checkForAllIssuer()
        {
            if (fannieCheckBox.Checked == true && freddieCheckBox.Checked == true &&
                  ginnieCheckBox.Checked == true && issuerOtherCheckBox.Checked == true)
            {
                AllIssuerCheckBox.Checked = true;
            }
            else
            {
                AllIssuerCheckBox.Checked = false;
            }

        }

        private void checkForAllClientSectorType()
        {
            if (sect_FixCMOcheckBox.Checked == true && sect_FixCorpcheckBox.Checked == true &&
                 sect_FixedAgencycheckBox.Checked == true && sect_FixMBScheckBox.Checked == true &&
                 sect_FixOthercheckBox.Checked == true && sect_floatAgencycheckBox.Checked == true &&
                 sect_floatCMOcheckBox.Checked == true && sect_floatCorpcheckBox.Checked == true &&
                 sect_floatMBScheckBox.Checked == true && sect_floatOthercheckBox.Checked == true &&
                 sect_taxFreeMunicheckBox.Checked == true && sect_taxMunicheckBox.Checked == true &&
                 sect_treasurycheckBox.Checked == true)
            {
                sector_AllcheckBox.Checked = true;
            }
            else
            {
                sector_AllcheckBox.Checked = false;
            }

        }

        private void checkForAllClientCouponType()
        {
            if (coupon_AdjustablecheckBox.Checked == true && coupon_FixedcheckBox.Checked == true &&
               coupon_MulticheckBox.Checked == true && coupon_SinglecheckBox.Checked == true)
            {
                coupon_AllcheckBox.Checked = true;
            }
            else
            {
                coupon_AllcheckBox.Checked = false;
            }
        }

        private void checkForAllClientMoodyType()
        {
            if (moody_A1checkBox.Checked == true && moody_A2checkBox.Checked == true &&
               moody_A3checkBox.Checked == true && moody_AA1checkBox.Checked == true &&
               moody_AA2checkBox.Checked == true && moody_AA3checkBox.Checked == true &&
               moody_AAAcheckBox.Checked == true && moody_BAA1checkBox.Checked == true &&
               moody_BAA2checkBox.Checked == true && moody_BAA3checkBox.Checked == true &&
               moody_OthercheckBox.Checked == true)
            {
                moody_AllcheckBox.Checked = true;
            }
            else
            {
                moody_AllcheckBox.Checked = false;
            }
        }

        private void checkForAllClientSandP()
        {
            if (sp_AAAcheckBox.Checked == true && sp_AAcheckBox.Checked == true &&
               sp_AAMcheckBox.Checked == true && sp_AAPcheckBox.Checked == true &&
               sp_AcheckBox.Checked == true && sp_AMcheckBox.Checked == true &&
               sp_APcheckBox.Checked == true && sp_BBBcheckBox.Checked == true &&
               sp_BBBMcheckBox.Checked == true && sp_BBBPcheckBox.Checked == true &&
               sp_OthercheckBox.Checked == true)
            {
                sp_AllcheckBox.Checked = true;
            }
            else
            {
                sp_AllcheckBox.Checked = false;
            }
        }

        private void tenYrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (tenYrCheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "10 Yr");

                if (tenYrCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "10 Yr");
            }
            else
            {
                if (tenYrCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                tenYrCheckBox.Checked = false;

            }


        }

        private void fifteenYrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (fifteenYrCheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "15 Yr");

                if (fifteenYrCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "15 Yr");
            }
            else
            {
                if (fifteenYrCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                fifteenYrCheckBox.Checked = false;
            }


        }

        private void AllPrincipalType_Click(object sender, System.EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                if (AllPrincipalType.CheckState.ToString().Equals("Checked"))
                {
                    pt_adCheckBox.Checked = true;
                    pt_cstrCheckBox.Checked = true;
                    pt_exchCheckBox.Checked = true;
                    pt_fltCheckBox.Checked = true;
                    pt_mrCheckBox.Checked = true;
                    pt_otherCheckBox.Checked = true;
                    pt_pac1CheckBox.Checked = true;
                    pt_pac2CheckBox.Checked = true;
                    pt_rtlCheckBox.Checked = true;
                    pt_scCheckBox.Checked = true;
                    pt_supCheckBox.Checked = true;
                    pt_zCheckBox.Checked = true;
                }

                if (AllPrincipalType.CheckState.ToString().Equals("Unchecked"))
                {
                    pt_adCheckBox.Checked = false;
                    pt_cstrCheckBox.Checked = false;
                    pt_exchCheckBox.Checked = false;
                    pt_fltCheckBox.Checked = false;
                    pt_mrCheckBox.Checked = false;
                    pt_otherCheckBox.Checked = false;
                    pt_pac1CheckBox.Checked = false;
                    pt_pac2CheckBox.Checked = false;
                    pt_rtlCheckBox.Checked = false;
                    pt_scCheckBox.Checked = false;
                    pt_supCheckBox.Checked = false;
                    pt_zCheckBox.Checked = false;
                }
            }
            else
            {
                if (AllPrincipalType.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                AllPrincipalType.Checked = false;
            }

        }

        private void AllCollateralCheckBox_Click(object sender, System.EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                if (AllCollateralCheckBox.CheckState.ToString().Equals("Checked"))
                {
                    tenYrCheckBox.Checked = true;
                    fifteenYrCheckBox.Checked = true;
                    twentyYrCheckBox.Checked = true;
                    thirtyYrCheckBox.Checked = true;
                    FNMAcheckBox.Checked = true;
                    FHGLDcheckBox.Checked = true;
                    GNMAcheckBox.Checked = true;
                    GNMA2checkBox.Checked = true;
                }

                if (AllCollateralCheckBox.CheckState.ToString().Equals("Unchecked"))
                {
                    tenYrCheckBox.Checked = false;
                    fifteenYrCheckBox.Checked = false;
                    twentyYrCheckBox.Checked = false;
                    thirtyYrCheckBox.Checked = false;
                    FNMAcheckBox.Checked = false;
                    FHGLDcheckBox.Checked = false;
                    GNMAcheckBox.Checked = false;
                    GNMA2checkBox.Checked = false;
                }
            }
            else
            {
                if (AllCollateralCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                AllCollateralCheckBox.Checked = false;
            }

        }

        private void twentyYrCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (twentyYrCheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "20 Yr");

                if (twentyYrCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "20 Yr");
            }
            else
            {
                if (twentyYrCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                twentyYrCheckBox.Checked = false;
            }

        }

        private void thirtyYrCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (thirtyYrCheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "30 Yr");

                if (thirtyYrCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "30 Yr");
            }
            else
            {
                if (thirtyYrCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                thirtyYrCheckBox.Checked = false;
            }


        }

        private void FNMAcheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (FNMAcheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "FNMA");

                if (FNMAcheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "FNMA");
            }
            else
            {
                if (FNMAcheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                FNMAcheckBox.Checked = false;
            }


        }

        private void FHGLDcheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (FHGLDcheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "FHGLD");

                if (FHGLDcheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "FHGLD");
            }
            else
            {
                if (FHGLDcheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                FHGLDcheckBox.Checked = false;
            }


        }

        private void GNMAcheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (GNMAcheckBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "GNMA");

                if (GNMAcheckBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "GNMA");
            }
            else
            {
                if (GNMAcheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                GNMAcheckBox.Checked = false;
            }

        }

        private void GNMA2checkBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAll();
                if (GNMA2checkBox.Checked.ToString().Equals("True"))
                    addCMOCT(SavedSearchescomboBox.Text.ToString(), "GNMA2");

                if (GNMA2checkBox.Checked.ToString().Equals("False"))
                    deleteCMOCT(SavedSearchescomboBox.Text.ToString(), "GNMA2");
            }
            else
            {
                if (GNMA2checkBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                GNMA2checkBox.Checked = false;
            }

        }

        private void AllPrincipalType_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pt_adCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();
                if (pt_adCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "AD");


                if (pt_adCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "AD");
            }
            else
            {
                if (pt_adCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_adCheckBox.Checked = false;
            }

        }

        private void pt_cstrCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();
                if (pt_cstrCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "CSTR");


                if (pt_cstrCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "CSTR");
            }
            else
            {
                if (pt_cstrCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_cstrCheckBox.Checked = false;
            }


        }

        private void pt_exchCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_exchCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "EXCH");

                if (pt_exchCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "EXCH");
            }
            else
            {
                if (pt_exchCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_exchCheckBox.Checked = false;
            }


        }

        private void pt_fltCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_fltCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "FLT");

                if (pt_fltCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "FLT");
            }
            else
            {
                if (pt_fltCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_fltCheckBox.Checked = false;
            }


        }

        private void pt_mrCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_mrCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "MR");

                if (pt_mrCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "MR");
            }
            else
            {
                if (pt_mrCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_mrCheckBox.Checked = false;
            }

        }

        private void pt_rtlCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_rtlCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "RTL");

                if (pt_rtlCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "RTL");
            }
            else
            {
                if (pt_rtlCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_rtlCheckBox.Checked = false;
            }

        }

        private void pt_pac1CheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_pac1CheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "PAC1");

                if (pt_pac1CheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "PAC1");
            }
            else
            {
                if (pt_pac1CheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_pac1CheckBox.Checked = false;
            }

        }

        private void pt_pac2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_pac2CheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "PAC2");

                if (pt_pac2CheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "PAC2");
            }
            else
            {
                if (pt_pac2CheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_pac2CheckBox.Checked = false;
            }

        }

        private void pt_scCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_scCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "SC");

                if (pt_scCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "SC");
            }
            else
            {
                if (pt_scCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_scCheckBox.Checked = false;
            }

        }

        private void pt_supCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_supCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "SUP");

                if (pt_supCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "SUP");
            }
            else
            {
                if (pt_supCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_supCheckBox.Checked = false;
            }

        }

        private void pt_zCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_zCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "Z");

                if (pt_zCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "Z");
            }
            else
            {
                if (pt_zCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_zCheckBox.Checked = false;
            }


        }

        private void pt_otherCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllPrincipalType();

                if (pt_otherCheckBox.Checked.ToString().Equals("True"))
                    addCMOPT(SavedSearchescomboBox.Text.ToString(), "OTHER");

                if (pt_otherCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOPT(SavedSearchescomboBox.Text.ToString(), "OTHER");
            }
            else
            {
                if (pt_otherCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                pt_otherCheckBox.Checked = false;
            }

        }

        private void AllIssuerCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AllIssuerCheckBox_Click(object sender, System.EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                if (AllIssuerCheckBox.CheckState.ToString().Equals("Checked"))
                {
                    fannieCheckBox.Checked = true;
                    freddieCheckBox.Checked = true;
                    ginnieCheckBox.Checked = true;
                    issuerOtherCheckBox.Checked = true;
                }

                if (AllIssuerCheckBox.CheckState.ToString().Equals("Unchecked"))
                {
                    fannieCheckBox.Checked = false;
                    freddieCheckBox.Checked = false;
                    ginnieCheckBox.Checked = false;
                    issuerOtherCheckBox.Checked = false;
                }
            }
            else
            {
                if (AllIssuerCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                AllIssuerCheckBox.Checked = false;
            }
        }

        private void fannieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllIssuer();
                if (fannieCheckBox.Checked.ToString().Equals("True"))
                    addCMOIssuer(SavedSearchescomboBox.Text.ToString(), "FANNIE");

                if (fannieCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOIssuer(SavedSearchescomboBox.Text.ToString(), "FANNIE");
            }
            else
            {
                if (fannieCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                fannieCheckBox.Checked = false;

            }

        }

        private void freddieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {

                checkForAllIssuer();
                if (freddieCheckBox.Checked.ToString().Equals("True"))
                    addCMOIssuer(SavedSearchescomboBox.Text.ToString(), "FREDDIE");

                if (freddieCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOIssuer(SavedSearchescomboBox.Text.ToString(), "FREDDIE");
            }
            else
            {
                if (freddieCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                freddieCheckBox.Checked = false;
            }
        }

        private void ginnieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllIssuer();
                if (ginnieCheckBox.Checked.ToString().Equals("True"))
                    addCMOIssuer(SavedSearchescomboBox.Text.ToString(), "GINNIE");

                if (ginnieCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOIssuer(SavedSearchescomboBox.Text.ToString(), "GINNIE");
            }
            else
            {
                if (ginnieCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                ginnieCheckBox.Checked = false;
            }

        }

        private void issuerOtherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Search") && !SavedSearchescomboBox.Text.Equals(""))
            {
                checkForAllIssuer();
                if (issuerOtherCheckBox.Checked.ToString().Equals("True"))
                    addCMOIssuer(SavedSearchescomboBox.Text.ToString(), "OTHER");

                if (issuerOtherCheckBox.Checked.ToString().Equals("False"))
                    deleteCMOIssuer(SavedSearchescomboBox.Text.ToString(), "OTHER");
            }
            else
            {
                if (issuerOtherCheckBox.Checked == true)
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                issuerOtherCheckBox.Checked = false;
            }

        }

        private void agencyCMOTab_Click(object sender, EventArgs e)
        {

        }

        private void checkClientcriteriaList()
        {
            int i;

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //string SQL = "select distinct criteriafield from tmp_Client_criteria" + userID;
                //MySqlCommand cmd = new MySqlCommand(SQL, cn);
                //MySqlDataReader rdr;
                //rdr = cmd.ExecuteReader();

                //while (rdr.Read())
                //{
                //    i = 0;
                //    while (i < clientCriteriacheckedListBox.Items.Count)
                //    {
                //        if (clientCriteriacheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                //            clientCriteriacheckedListBox.SetItemChecked(i, true);

                //        i++;
                //    }
                //}
                //rdr.Close();
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteriafield from tmp_Client_criteria" + userID;
                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < clientCriteriacheckedListBox.Items.Count)
                    {
                        if (clientCriteriacheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            clientCriteriacheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOcriteriaList()
        {
            int i;

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //string SQL = "select distinct criteriafield from tmp_CMO_criteria" + userID;
                //MySqlCommand cmd = new MySqlCommand(SQL, cn);
                //MySqlDataReader rdr;
                //rdr = cmd.ExecuteReader();

                //while (rdr.Read())
                //{
                //    i = 0;
                //    while (i < criteriaChooserCheckedListBox.Items.Count)
                //    {
                //        if (criteriaChooserCheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                //            criteriaChooserCheckedListBox.SetItemChecked(i, true);

                //        //if (clientAssignmentCheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                //        //   clientAssignmentCheckedListBox.SetItemChecked(i, true);

                //        i++;
                //    }
                //}
                //rdr.Close();
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_field from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + SavedSearchescomboBox.Text.ToString() + "' and criteria_Descriptor='Range' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                i = 0;
                while (i < criteriaChooserCheckedListBox.Items.Count)
                {
                    criteriaChooserCheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                while (rdr.Read())
                {
                    //MessageBox.Show(rdr.GetValue(0).ToString());
                    i = 0;
                    while (i < criteriaChooserCheckedListBox.Items.Count)
                    {
                        if (criteriaChooserCheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            criteriaChooserCheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOCollatType()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='CMO' and criteria_searchName='" +
                    SavedSearchescomboBox.Text.ToString() + "' and criteria_Field='CType' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                i = 0;
                while (i < CMOCollateralcheckedListBox.Items.Count)
                {
                    CMOCollateralcheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                while (rdr.Read())
                {
                    //MessageBox.Show(rdr.GetValue(0).ToString());
                    i = 0;
                    while (i < CMOCollateralcheckedListBox.Items.Count)
                    {
                        if (CMOCollateralcheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            CMOCollateralcheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOPrinType()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='CMO' and criteria_searchName='" +
                    SavedSearchescomboBox.Text.ToString() + "' and criteria_Field='PType' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                i = 0;
                while (i < cmoPrincipalcheckedListBox.Items.Count)
                {
                    cmoPrincipalcheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                while (rdr.Read())
                {
                    //MessageBox.Show(rdr.GetValue(0).ToString());
                    i = 0;
                    while (i < cmoPrincipalcheckedListBox.Items.Count)
                    {
                        if (cmoPrincipalcheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            cmoPrincipalcheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOTicker()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='CMO' and criteria_searchName='" +
                    SavedSearchescomboBox.Text.ToString() + "' and criteria_Field='Ticker' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                i = 0;
                while (i < CMOTickercheckedListBox.Items.Count)
                {
                    CMOTickercheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                while (rdr.Read())
                {
                    //MessageBox.Show(rdr.GetValue(0).ToString());
                    i = 0;
                    while (i < CMOTickercheckedListBox.Items.Count)
                    {
                        if (CMOTickercheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            CMOTickercheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMBSRangescriteriaList()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                i = 0;
                while (i < mbsRangescheckedListBox.Items.Count)
                {
                    mbsRangescheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_field='blm_sector' and " +
                    " criteria_SearchName='" + MBSSearchescomboBox.Text.ToString() + "';";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < mbsRangescheckedListBox.Items.Count)
                    {
                        if (mbsRangescheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            mbsRangescheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOIssuer()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='CMO' and criteria_field='Issuer' and " +
                    " criteria_SearchName='" + SavedSearchescomboBox.Text.ToString() + "';";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                fannieCheckBox.Checked = false;
                freddieCheckBox.Checked = false;
                ginnieCheckBox.Checked = false;
                issuerOtherCheckBox.Checked = false;

                while (rdr.Read())
                {

                    if (rdr.GetValue(0).ToString().Equals("FANNIE"))
                        fannieCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("FREDDIE"))
                        freddieCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("GINNIE"))
                        ginnieCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("OTHER"))
                        issuerOtherCheckBox.Checked = true;

                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOCType()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='CMO' and criteria_field='CType' and " +
                    " criteria_SearchName='" + SavedSearchescomboBox.Text.ToString() + "';";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                tenYrCheckBox.Checked = false;
                fifteenYrCheckBox.Checked = false;
                twentyYrCheckBox.Checked = false;
                thirtyYrCheckBox.Checked = false;
                FNMAcheckBox.Checked = false;
                FHGLDcheckBox.Checked = false;
                GNMAcheckBox.Checked = false;
                GNMA2checkBox.Checked = false;

                while (rdr.Read())
                {

                    if (rdr.GetValue(0).ToString().Equals("10 Yr"))
                        tenYrCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("15 Yr"))
                        fifteenYrCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("20 Yr"))
                        twentyYrCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("30 Yr"))
                        thirtyYrCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("FNMA"))
                        FNMAcheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("FHGLD"))
                        FHGLDcheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("GNMA"))
                        GNMAcheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("GNMA2"))
                        GNMA2checkBox.Checked = true;

                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkCMOPType()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                // MessageBox.Show(MBSSearchescomboBox.Text);

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='CMO' and criteria_field='PType' and " +
                    " criteria_SearchName='" + SavedSearchescomboBox.Text.ToString() + "';";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                pt_adCheckBox.Checked = false;
                pt_cstrCheckBox.Checked = false;
                pt_exchCheckBox.Checked = false;
                pt_fltCheckBox.Checked = false;
                pt_mrCheckBox.Checked = false;
                pt_otherCheckBox.Checked = false;
                pt_pac1CheckBox.Checked = false;
                pt_pac2CheckBox.Checked = false;
                pt_rtlCheckBox.Checked = false;
                pt_scCheckBox.Checked = false;
                pt_supCheckBox.Checked = false;
                pt_zCheckBox.Checked = false;

                while (rdr.Read())
                {

                    if (rdr.GetValue(0).ToString().Equals("AD"))
                        pt_adCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("CSTR"))
                        pt_cstrCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("EXCH"))
                        pt_exchCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("FLT"))
                        pt_fltCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("MR"))
                        pt_mrCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("OTHER"))
                        pt_otherCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("PAC1"))
                        pt_pac1CheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("PAC2"))
                        pt_pac2CheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("RTL"))
                        pt_rtlCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("SC"))
                        pt_scCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("SUP"))
                        pt_supCheckBox.Checked = true;

                    if (rdr.GetValue(0).ToString().Equals("Z"))
                        pt_zCheckBox.Checked = true;

                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMBSTypecriteriaList()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                i = 0;
                while (i < mbsClientcheckedListBox.Items.Count)
                {
                    mbsClientcheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                i = 0;
                while (i < mbsTypecheckedListBox.Items.Count)
                {
                    mbsTypecheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MBS' and criteria_field='blm_sector' and " +
                  " criteria_SearchName='" + MBSSearchescomboBox.Text.ToString() + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < mbsTypecheckedListBox.Items.Count)
                    {
                        if (mbsTypecheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            mbsTypecheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMuniStateList()
        {
            int i;
            string stateValue;
            stateValue = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                i = 0;
                while (i < MuniStatecheckedListBox.Items.Count)
                {
                    MuniStatecheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='State' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < MuniStatecheckedListBox.Items.Count)
                    {
                        stateValue = MuniStatecheckedListBox.Items[i].ToString();

                        if (stateValue.IndexOf(":") > 0)
                            stateValue = stateValue.Substring(0, stateValue.IndexOf(":"));

                        if (stateValue.Equals(rdr.GetValue(0).ToString()))
                            MuniStatecheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMuniMoody()
        {
            int i;
            string strValue;
            strValue = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                i = 0;
                while (i < MuniMoodycheckedListBox.Items.Count)
                {
                    MuniMoodycheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='Moody' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < MuniMoodycheckedListBox.Items.Count)
                    {

                        strValue = MuniMoodycheckedListBox.Items[i].ToString();

                        if (strValue.IndexOf(":") > 0)
                            strValue = strValue.Substring(0, strValue.IndexOf(":"));

                        if (strValue.Equals(rdr.GetValue(0).ToString()))
                            MuniMoodycheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMuniSP()
        {
            int i;
            string strValue;
            strValue = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                i = 0;
                while (i < MuniSPcheckedListBox.Items.Count)
                {
                    MuniSPcheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='S_P' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < MuniSPcheckedListBox.Items.Count)
                    {

                        strValue = MuniSPcheckedListBox.Items[i].ToString();

                        if (strValue.IndexOf(":") > 0)
                            strValue = strValue.Substring(0, strValue.IndexOf(":"));

                        if (strValue.Equals(rdr.GetValue(0).ToString()))
                            MuniSPcheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMuniType()
        {
            int i;
            string strVal;
            strVal = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                i = 0;
                while (i < MuniTypecheckedListBox.Items.Count)
                {
                    MuniTypecheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='Type' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < MuniTypecheckedListBox.Items.Count)
                    {
                        strVal = MuniTypecheckedListBox.Items[i].ToString();

                        if (strVal.IndexOf(":") > 0)
                            strVal = strVal.Substring(0, strVal.IndexOf(":"));

                        if (strVal.Equals(rdr.GetValue(0).ToString()))
                            MuniTypecheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMUNIBQ()
        {


            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='Bank_Qualified' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                MuniBQcheckBox.CheckState = CheckState.Unchecked;
                MUNIbqLabel.Text = "All";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals("N"))
                    {
                        MuniBQcheckBox.CheckState = CheckState.Indeterminate;
                        MUNIbqLabel.Text = "Non Bank Qualified";
                    }
                    if (rdr.GetValue(0).ToString().Equals("Y"))
                    {
                        MuniBQcheckBox.CheckState = CheckState.Checked;
                        MUNIbqLabel.Text = "Bank Qualified";
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMUNIrefunded()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='Refunded' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                MuniRefundedcheckBox.CheckState = CheckState.Unchecked;
                MuniRefundedlabel.Text = "All";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals("N"))
                    {
                        MuniRefundedcheckBox.CheckState = CheckState.Indeterminate;
                        MuniRefundedlabel.Text = "Non Refunded";
                    }
                    if (rdr.GetValue(0).ToString().Equals("Y"))
                    {
                        MuniRefundedcheckBox.CheckState = CheckState.Checked;
                        MuniRefundedlabel.Text = "Refunded";
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMUNIFedTax()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='Fed_Tax' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                MuniFedTaxcheckBox.CheckState = CheckState.Unchecked;
                MUNIfedTaxlabel.Text = "All";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals("N"))
                    {
                        MuniFedTaxcheckBox.CheckState = CheckState.Indeterminate;
                        MUNIfedTaxlabel.Text = "Non Taxable";
                    }
                    if (rdr.GetValue(0).ToString().Equals("Y"))
                    {
                        MuniFedTaxcheckBox.CheckState = CheckState.Checked;
                        MUNIfedTaxlabel.Text = "Fed Taxable";
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMUNIStateTax()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                  " where criteria_sector='MUNI' and criteria_field='State_Tax' and " +
                  " criteria_SearchName='" + muniSearchescomboBox.Text.ToString() + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }


                muniStateTaxcheckBox.CheckState = CheckState.Unchecked;
                muniStateTaxlabel.Text = "All";

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetValue(0).ToString().Equals("N"))
                    {
                        muniStateTaxcheckBox.CheckState = CheckState.Indeterminate;
                        muniStateTaxlabel.Text = "Non Taxable";
                    }
                    if (rdr.GetValue(0).ToString().Equals("Y"))
                    {
                        muniStateTaxcheckBox.CheckState = CheckState.Checked;
                        muniStateTaxlabel.Text = "State Taxable";
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMBSClientcriteriaList()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();


                i = 0;
                while (i < mbsTypecheckedListBox.Items.Count)
                {
                    mbsClientcheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_field='Client' and " +
                    " criteria_SearchName='" + MBSSearchescomboBox.Text.ToString() + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < mbsClientcheckedListBox.Items.Count)
                    {

                        if (mbsClientcheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            mbsClientcheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        private void checkMBSTickercriteriaList()
        {
            int i;

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();


                i = 0;
                while (i < mbsTickercheckedListBox.Items.Count)
                {
                    mbsTickercheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                string SQL = "select distinct criteria_min from InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_field='ticker' and " +
                    " criteria_SearchName='" + MBSSearchescomboBox.Text.ToString() + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    i = 0;
                    while (i < mbsTickercheckedListBox.Items.Count)
                    {
                        if (mbsTickercheckedListBox.Items[i].ToString().Equals(rdr.GetValue(0).ToString()))
                            mbsTickercheckedListBox.SetItemChecked(i, true);

                        i++;
                    }
                }
                rdr.Close();
                cn.Close();
            }

        }

        public void fillSavedSearchesCombo()
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.CreateCommand();

                //MySqlCommand cmd = cn.CreateCommand();
                //MySqlDataReader Rdr;

                //cmd.CommandText = "select distinct criteria_SearchName from InventoryCriteriaSaved " 
                //   + " where criteria_sector='CMO';";

                //cn.Open();
                //Rdr = cmd.ExecuteReader();
                //SavedSearchescomboBox.Items.Clear();
                //SavedSearchescomboBox.Text = "";

                //while (Rdr.Read())
                //{
                //    SavedSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
                //}

                //cn.Close();
                //try
                //{
                //    SavedSearchescomboBox.SelectedIndex = 0;
                //}
                //catch
                //{
                //    SavedSearchescomboBox.SelectedIndex = -1;
                //}
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("delete from InventoryCriteriaSaved where criteria_sector='CMO' "
                  + " and criteria_searchname='New Search' ", cn);

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct criteria_SearchName from InventoryCriteriaSaved "
                   + " where criteria_sector='CMO' and criteria_searchname<>'New Search'";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                Rdr = cmd.ExecuteReader();
                SavedSearchescomboBox.Items.Clear();
                SavedSearchescomboBox.Text = "";
                SavedSearchescomboBox.Items.Add("New Search");
                while (Rdr.Read())
                {
                    SavedSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
                }

                cn.Close();
                try
                {
                    SavedSearchescomboBox.SelectedIndex = 0;
                }
                catch
                {
                    SavedSearchescomboBox.SelectedIndex = -1;
                }
            }

        }

        public void fillMBSsearchesComboBox()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("delete from InventoryCriteriaSaved where criteria_sector='MBS' "
                     + " and criteria_searchname='New Search' ", cn);

                if (MBSradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }
                cmd.ExecuteNonQuery();


                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct criteria_SearchName from InventoryCriteriaSaved "
                   + " where criteria_sector='MBS' and criteria_searchname <> 'New Search' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    //MessageBox.Show("Group Checked");
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                Rdr = cmd.ExecuteReader();
                MBSSearchescomboBox.Text = "";
                MBSSearchescomboBox.Items.Clear();

                MBSSearchescomboBox.Items.Add("New Search");
                while (Rdr.Read())
                {
                    MBSSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
                }

                cn.Close();
                try
                {
                    MBSSearchescomboBox.SelectedIndex = 0;
                }
                catch
                {
                    MBSSearchescomboBox.SelectedIndex = -1;
                }
            }

        }


        public void fillMUNIsearchesComboBox()
        {
            if (usingSQLServer == false)
            {

            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("delete from InventoryCriteriaSaved where criteria_sector='MUNI' "
                     + " and criteria_searchname='New Search' ", cn);

                if (MuniradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }
                cmd.ExecuteNonQuery();


                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = "select distinct criteria_SearchName from InventoryCriteriaSaved "
                   + " where criteria_sector='MUNI' and criteria_searchname <> 'New Search' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    //MessageBox.Show("Group Checked");
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                Rdr = cmd.ExecuteReader();
                muniSearchescomboBox.Text = "";
                muniSearchescomboBox.Items.Clear();

                muniSearchescomboBox.Items.Add("New Search");
                while (Rdr.Read())
                {
                    muniSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
                }

                cn.Close();
                try
                {
                    muniSearchescomboBox.SelectedIndex = 0;
                }
                catch
                {
                    muniSearchescomboBox.SelectedIndex = -1;
                }
            }

        }


        //public void fillTypeCombo()
        //{
        //    if (usingSQLServer == false)
        //    {
        //        //MySqlConnection cn = new MySqlConnection(MyConString);
        //        //cn.ConnectionString = MyConString;
        //        //cn.CreateCommand();

        //        //MySqlCommand cmd = cn.CreateCommand();
        //        //MySqlDataReader Rdr;

        //        //cmd.CommandText = "select distinct criteria_SearchName from InventoryCriteriaSaved " 
        //        //   + " where criteria_sector='CMO';";

        //        //cn.Open();
        //        //Rdr = cmd.ExecuteReader();
        //        //SavedSearchescomboBox.Items.Clear();
        //        //SavedSearchescomboBox.Text = "";

        //        //while (Rdr.Read())
        //        //{
        //        //    SavedSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
        //        //}

        //        //cn.Close();
        //        //try
        //        //{
        //        //    SavedSearchescomboBox.SelectedIndex = 0;
        //        //}
        //        //catch
        //        //{
        //        //    SavedSearchescomboBox.SelectedIndex = -1;
        //        //}
        //    }

        //    if (usingSQLServer == true)
        //    {
        //        SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
        //           "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

        //        cn.Open();

        //        SqlCommand cmd = cn.CreateCommand();
        //        SqlDataReader Rdr;

        //        cmd.CommandText = "select distinct criteria_SearchName from InventoryCriteriaSaved "
        //           + " where criteria_sector='CMO';";

        //        Rdr = cmd.ExecuteReader();
        //        SavedSearchescomboBox.Items.Clear();
        //        SavedSearchescomboBox.Text = "";

        //        while (Rdr.Read())
        //        {
        //            SavedSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
        //        }

        //        cn.Close();
        //        try
        //        {
        //            SavedSearchescomboBox.SelectedIndex = 0;
        //        }
        //        catch
        //        {
        //            SavedSearchescomboBox.SelectedIndex = -1;
        //        }
        //    }

        //}

        public void fillMBSClient()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                string SQL = "select distinct  rtrim(ltrim(CLIENT))  as client from  [ZM_GALLAGHER].[dbo].cmospreadsheet " +
                                   " where sector='MBS' order by CLIENT;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                mbsClientcheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        mbsClientcheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        mbsClientcheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //*** DONE FILLING CLIENT LIST ***//
                cn.Close();
            }
        }


        public void fillMBSTicker()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                //*** FILL TICKER LIST BOX ***//
                string SQL = "select distinct  rtrim(ltrim(CRITERIA_VALUE)) from  [ZM_GALLAGHER].[dbo].SearchCriteria " +
                    " where criteria_sector='MBS' and criteria_SearchBox='mbsTicker';";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                mbsTickercheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        mbsTickercheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        mbsTickercheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //**** DONE FILLING TICKER LIST BOX ***//

                cn.Close();
            }
        }

        public void FillCorpDealer()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                string SQL = "select distinct  rtrim(ltrim(DEALER))  as DEALER from  [ZM_GALLAGHER].[dbo].PW_INVENTORIES " +
                                   " where sector='CORP' order by DEALER;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                CORPdealercheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        CORPdealercheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        CORPdealercheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //*** DONE FILLING DEALER LIST ***//
                cn.Close();
            }
        }

        public void FillCorpTicker()
        {
            string[] ticks;
            int i = 0;


            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                //---see what's already checked for tickers
                // i++;
                // Array.Resize(ref colName, i);
                // Array.Resize(ref colID, i);
                ticks = new string[0];
                i = 0;
                for (int x=0; x < CORPTickercheckedListBox.Items.Count; x++)
                {
                    if (CORPTickercheckedListBox.GetItemChecked(x))
                    {
                        i++;
                        Array.Resize(ref ticks, i);
                        ticks[i-1] = CORPTickercheckedListBox.Items[x].ToString();
                    }
                }

                string SQL = "select distinct  rtrim(ltrim(TICKER))  as ticker from  [ZM_GALLAGHER].[dbo].PW_INVENTORIES " +
                                   " where sector='CORP' ";

                //--- LOOK FOR DEALER CHECKS
                if (CORPdealercheckedListBox.CheckedItems.Count > 0)
                {
                    SQL += " and DEALER in (";
                    for (int x = 0; x < CORPdealercheckedListBox.Items.Count; x++)
                    {

                        if (CORPdealercheckedListBox.GetItemChecked(x))
                            SQL += "'" + CORPdealercheckedListBox.Items[x].ToString() + "',";
                    }
                    SQL = SQL.Substring(0, SQL.Length - 1) + ") ";
                }

                SQL += " order by TICKER;";

                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                CORPTickercheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        CORPTickercheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        CORPTickercheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //*** DONE FILLING ticker LIST ***//
                cn.Close();

                //-- NOW CHECK TICKERS THAT WERE PREVIOUSLY CHECKED
                foreach (string tickFld in ticks)
                {
                    for (int x=0; x < CORPTickercheckedListBox.Items.Count; x++)
                    {
                        if (CORPTickercheckedListBox.Items[x].ToString().Equals(tickFld))
                        {
                            CORPTickercheckedListBox.SetItemChecked(x,true);
                            break;
                        }
                    }
                }

            }
        }

        public void fillMBSRanges()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                string SQL = "select distinct  rtrim(ltrim(CRITERIA_VALUE)) from  [ZM_GALLAGHER].[dbo].SearchCriteria " +
                                  " where criteria_sector='MBS' and criteria_SearchBox='mbsRange';";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                mbsRangescheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        mbsRangescheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        mbsRangescheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //*** DONE FILLING MBS RANGES ***//
                cn.Close();
            }
        }

        public void FillCORPRanges()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;


                string SQL = "select distinct  rtrim(ltrim(CRITERIA_VALUE)) from  [ZM_GALLAGHER].[dbo].SearchCriteria " +
                                  " where criteria_sector='CORP' and criteria_SearchBox='CORPRange';";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                CORPRangescheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        CORPRangescheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        CORPRangescheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //*** DONE FILLING CORP RANGES ***//
                cn.Close();
            }
        }
        public void fillMBSTypeList()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                //---GETTING TYPE
                SQL = "select distinct  rtrim(ltrim(blm_sector)) from  [ZM_GALLAGHER].[dbo].cmospreadsheet where sector='MBS';";
                // SQL = "select distinct  rtrim(ltrim(type)) from  [ZM_GALLAGHER].[dbo].cmospreadsheet where sector='MBS';";
                cmd = cn.CreateCommand();
                //SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                //cn.Open();

                mbsTypecheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        mbsTypecheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        mbsTypecheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //---DONE GETTING TYPE 

                //*** FILL TICKER LIST BOX ***//
                //fillMBSTicker();

                //SQL = "select distinct  rtrim(ltrim(CRITERIA_VALUE)) from  [ZM_GALLAGHER].[dbo].SearchCriteria " +
                //    " where criteria_sector='MBS' and criteria_SearchBox='mbsTicker';";
                //cmd = cn.CreateCommand();

                //cmd.CommandText = SQL;
                //Rdr = cmd.ExecuteReader();

                //mbsTickercheckedListBox.Items.Clear();
                //while (Rdr.Read())
                //{
                //    if (Rdr.GetValue(0).ToString().Equals(""))
                //    {
                //        mbsTickercheckedListBox.Items.Add("Missing", false);
                //    }
                //    else
                //    {
                //        mbsTickercheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                //    }

                //}
                //Rdr.Close();
                //**** DONE FILLING TICKER LIST BOX ***//

                //**** FILL CLIENT LIST ***//
                //SQL = "select distinct  rtrim(ltrim(CLIENT))  as client from  [ZM_GALLAGHER].[dbo].cmospreadsheet " +
                //     " where sector='MBS' order by CLIENT;";
                //cmd = cn.CreateCommand();

                //cmd.CommandText = SQL;
                //Rdr = cmd.ExecuteReader();

                //mbsClientcheckedListBox.Items.Clear();
                //while (Rdr.Read())
                //{
                //    if (Rdr.GetValue(0).ToString().Equals(""))
                //    {
                //        mbsClientcheckedListBox.Items.Add("Missing", false);
                //    }
                //    else
                //    {
                //        mbsClientcheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                //    }

                //}
                //Rdr.Close();
                ////*** DONE FILLING CLIENT LIST ***//



                //*** FILLING MBS RANGES ***//
                //SQL = "select distinct  rtrim(ltrim(CRITERIA_VALUE)) from  [ZM_GALLAGHER].[dbo].SearchCriteria " +
                //    " where criteria_sector='MBS' and criteria_SearchBox='mbsRange';";
                //cmd = cn.CreateCommand();

                //cmd.CommandText = SQL;
                //Rdr = cmd.ExecuteReader();

                //mbsRangescheckedListBox.Items.Clear();
                //while (Rdr.Read())
                //{
                //    if (Rdr.GetValue(0).ToString().Equals(""))
                //    {
                //        mbsRangescheckedListBox.Items.Add("Missing", false);
                //    }
                //    else
                //    {
                //        mbsRangescheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                //    }

                //}
                //Rdr.Close();

                ////*** DONE FILLING MBS RANGES ***//
                //fillMBSsearchesComboBox();

                //cmd.CommandText = "select distinct rtrim(ltrim(criteria_SearchName)) from InventoryCriteriaSaved "
                // + " where criteria_sector='MBS' ";

                //if (MBSradioButtonGroup.Checked == true)
                //    MessageBox.Show("Checked:");

                //Rdr = cmd.ExecuteReader();

                //MBSSearchescomboBox.Items.Clear();
                //MBSSearchescomboBox.Text = "";

                //while (Rdr.Read())
                //{
                //    MBSSearchescomboBox.Items.Add(Rdr.GetValue(0).ToString());
                //}

                //cn.Close();

                //try
                //{
                //    MBSSearchescomboBox.SelectedIndex = 0;
                //}
                //catch
                //{
                //    MBSSearchescomboBox.SelectedIndex = -1;
                //}

                //fillMBSClientList();
                cn.Close();
            }

            //checkMBSTypecriteriaList();
            //checkCMOcriteriaList();

        }

        public void fillMuniStateList()
        {

            string[] txtFlds;
            txtFlds = new string[2];

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='MUNI'" +
                    " and criteria_field='State' and criteria_min not in " +
                    " (select distinct  rtrim(ltrim(State)) from  [ZM_GALLAGHER].[dbo].munilookup);";

                cmd.ExecuteNonQuery();

                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                //---GETTING State
                SQL = "select rtrim(ltrim(State)), count(*) as frq from  [ZM_GALLAGHER].[dbo].munilookup " +
                    " group by state;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                MuniStatecheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        MuniStatecheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        txtFlds[0] = Rdr.GetValue(0).ToString();
                        txtFlds[1] = Rdr.GetValue(1).ToString();
                        MuniStatecheckedListBox.Items.Add(txtFlds[0] + ":\t" + txtFlds[1], false);
                    }

                }
                Rdr.Close();
                //---DONE GETTING State 
            }

        }

        public void fillMuniTypeList()
        {
            string strVal;
            strVal = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                //---GETTING Type
                SQL = "select rtrim(ltrim(Type)), count(*) from  [ZM_GALLAGHER].[dbo].munilookup group by Type;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                MuniTypecheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        strVal = "Missing:";
                        while (strVal.Length < 30)
                            strVal += " ";

                        MuniTypecheckedListBox.Items.Add(strVal + "\t" + Rdr.GetValue(1).ToString(), false);
                    }
                    else
                    {
                        strVal = Rdr.GetValue(0).ToString() + ":";
                        while (strVal.Length < 30)
                            strVal += " ";

                        MuniTypecheckedListBox.Items.Add(strVal + "\t" + Rdr.GetValue(1).ToString(), false);
                    }

                }
                Rdr.Close();
                cn.Close();
                //---DONE GETTING Type
            }

        }

        public void fillMuniMoodyList()
        {
            string strVal;
            strVal = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                //---GETTING Moody
                SQL = "select rtrim(ltrim(Moody)), count(*) from  [ZM_GALLAGHER].[dbo].munilookup group by Moody;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                MuniMoodycheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        MuniMoodycheckedListBox.Items.Add("Missing:\t" + Rdr.GetValue(1).ToString(), false);
                    }
                    else
                    {
                        strVal = Rdr.GetValue(0).ToString() + ":";
                        while (strVal.Length < 8)
                            strVal += " ";

                        MuniMoodycheckedListBox.Items.Add(strVal + "\t" + Rdr.GetValue(1).ToString(), false);
                    }

                }
                Rdr.Close();
                cn.Close();
                //---DONE GETTING Moody
            }

        }

        public void fillMuniSPList()
        {
            string strVal;
            strVal = "";

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                //---GETTING SP
                SQL = "select rtrim(ltrim(S_P)), count(*) from  [ZM_GALLAGHER].[dbo].munilookup group by S_P;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                MuniSPcheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        MuniSPcheckedListBox.Items.Add("Missing:\t" + Rdr.GetValue(1).ToString(), false);
                    }
                    else
                    {
                        strVal = Rdr.GetValue(0).ToString() + ":";
                        while (strVal.Length < 8)
                            strVal += " ";

                        MuniSPcheckedListBox.Items.Add(strVal + "\t" + Rdr.GetValue(1).ToString(), false);
                    }

                }
                Rdr.Close();
                cn.Close();
                //---DONE GETTING SP
            }

        }



        public void fillMBSClientList()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                //---GETTING CLIENT
                SQL = "select distinct  rtrim(ltrim(CLIENT))  as client from  [ZM_GALLAGHER].[dbo].cmospreadsheet " +
                    " where sector='MBS' order by CLIENT;";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                mbsClientcheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        mbsClientcheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        mbsClientcheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                    }

                }
                Rdr.Close();
                //---DONE GETTING CLIENT 
            }

        }

        public void fillCMOPrincipalType()
        {

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                string SQL = "select distinct tranche_val from FI_CMO_TRANCHE;";

                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                cmoPrincipalcheckedListBox.Items.Clear();

                while (Rdr.Read())
                {
                    cmoPrincipalcheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                }

                cn.Close();
            }

        }

        public void fillCMOCollateralType()
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                string SQL = "select distinct COLLATERAL_VAL from FI_CMO_COLLATERAL;";

                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                CMOCollateralcheckedListBox.Items.Clear();

                while (Rdr.Read())
                {
                    CMOCollateralcheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                }

                cn.Close();
            }

        }

        public void fillCMOTickerType()
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                string SQL = "select ticker, count(*) as frq from CMOlookup WHERE CHARINDEX('cmo',TYPE)>0 " +
                     " group by ticker having count(*) > 5 order by frq;";

                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                CMOTickercheckedListBox.Items.Clear();

                while (Rdr.Read())
                {
                    CMOTickercheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                }

                cn.Close();
            }

        }

        public void fillCMOcriteraiList()
        {

            //MessageBox.Show (SavedSearchescomboBox.Text);

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand();

                //cmd = new MySqlCommand("create table if not exists fig.tmp_CMO_criteria"
                //   + userID + " (criteriaField varchar(55), minVal varchar(25), maxVal varchar(25), primary key ( criteriaField ));", cn);

                //cmd.ExecuteNonQuery();

                //string SQL = "select criteriafield from bond_criteria where criteriaSector='CMO';";
                //cmd = cn.CreateCommand();
                //MySqlDataReader Rdr;

                //cmd.CommandText = SQL;

                ////cn.Open();
                //Rdr = cmd.ExecuteReader();

                //criteriaChooserCheckedListBox.Items.Clear();
                //while (Rdr.Read())
                //{
                //    criteriaChooserCheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                //}

                //cn.Close();
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.inventoryCriteriaSaved" +
                    "', 'U') is null select '0' else select '1';";

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('inventoryCriteriaSaved', 'U') IS NOT NULL DROP TABLE inventoryCriteriaSaved", cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table ZM_GALLAGHER.dbo.InventoryCriteriaSaved (criteria_sector char(25)," +
                        " criteria_SearchName varchar(55), criteria_Field varchar(55), criteria_min varchar(25), " +
                        " criteria_max varchar(25), criteria_descriptor varchar(25) );";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                SQL = "select distinct criteria_value from SEARCHCRITERIA where criteria_Sector='CMO' and " +
                    " criteria_searchBox='cmoRange'  order by criteria_value ;";


                cmd = cn.CreateCommand();
                //SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                //cn.Open();

                criteriaChooserCheckedListBox.Items.Clear();

                while (Rdr.Read())
                {
                    criteriaChooserCheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                }

                cn.Close();
            }

            //checkCMOcriteriaList();//why
        }

        public void fillCMOcriteraiList_OLD()
        {

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand();

                //cmd = new MySqlCommand("create table if not exists fig.tmp_CMO_criteria"
                //   + userID + " (criteriaField varchar(55), minVal varchar(25), maxVal varchar(25), primary key ( criteriaField ));", cn);

                //cmd.ExecuteNonQuery();

                //string SQL = "select criteriafield from bond_criteria where criteriaSector='CMO';";
                //cmd = cn.CreateCommand();
                //MySqlDataReader Rdr;

                //cmd.CommandText = SQL;

                ////cn.Open();
                //Rdr = cmd.ExecuteReader();

                //criteriaChooserCheckedListBox.Items.Clear();
                //while (Rdr.Read())
                //{
                //    criteriaChooserCheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                //}

                //cn.Close();
            }

            if (usingSQLServer == true)
            {

                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.tmp_CMO_criteria" + userID +
                    "', 'U') is null select '0' else select '1';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('tmp_CMO_criteria" + userID + "', 'U') IS NOT NULL DROP TABLE tmp_CMO_criteria" + userID, cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table tmp_CMO_criteria" + userID + " (criteriaField varchar(55) PRIMARY KEY, minVal varchar(25), maxVal varchar(25));";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                SQL = "select criteriafield from bond_criteria where criteriaSector='CMO';";
                cmd = cn.CreateCommand();
                //SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                //cn.Open();

                criteriaChooserCheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    criteriaChooserCheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                }

                cn.Close();
            }


            checkCMOcriteriaList();
        }

        public void fillClientcriteraiList()
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand();

                //cmd = new MySqlCommand("create table if not exists fig.tmp_Client_criteria"
                //   + userID + " (criteriaField varchar(55), minVal varchar(25), maxVal varchar(25), primary key ( criteriaField ));", cn);

                //cmd.ExecuteNonQuery();

                //string SQL = "select criteriafield from bond_criteria where criteriaSector='Client';";
                //cmd = cn.CreateCommand();
                //MySqlDataReader Rdr;

                //cmd.CommandText = SQL;

                ////cn.Open();
                //Rdr = cmd.ExecuteReader();

                //clientCriteriacheckedListBox.Items.Clear();
                //while (Rdr.Read())
                //{
                //    clientCriteriacheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                //}
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "if  OBJECT_ID('ZM_GALLAGHER.dbo.tmp_Client_criteria" + userID +
                   "', 'U') is null select '0' else select '1';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();
                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("IF OBJECT_ID('tmp_Client_criteria" + userID +
                        "', 'U') IS NOT NULL DROP TABLE tmp_Client_criteria" + userID, cn);
                    cmd.ExecuteNonQuery();

                    cmd = cn.CreateCommand();
                    cmd.CommandText = "create table tmp_Client_criteria" +
                       userID + " (criteriaField varchar(55) PRIMARY KEY, minVal varchar(25), maxVal varchar(25));";
                    cmd.ExecuteNonQuery();
                }
                else { Rdr.Close(); }

                SQL = "select criteriafield from bond_criteria where criteriaSector='Client';";
                cmd = cn.CreateCommand();
                //SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                clientCriteriacheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    clientCriteriacheckedListBox.Items.Add(Rdr.GetValue(0).ToString(), false);
                }

                cn.Close();
            }

            checkClientcriteriaList();
        }

        private void initializeCMOCriterialist()
        {
            int i;
            i = 0;

            while (i < mbsRangescheckedListBox.Items.Count)
            {
                mbsRangescheckedListBox.SetItemChecked(i, false);
                i++;
            }

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "select distinct  rtrim(ltrim(CRITERIA_field)) from  [ZM_GALLAGHER].[dbo].InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_SearchName='" + MBSSearchescomboBox.Text.ToString() +
                    "' and criteria_Descriptor='Range';";
                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                SqlDataReader Rdr;
                Rdr = cmd.ExecuteReader();

                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        // mbsTickercheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        i = 0;
                        while (i < mbsRangescheckedListBox.Items.Count)
                        {
                            if (Rdr.GetValue(0).ToString().Equals(mbsRangescheckedListBox.Items[i].ToString()))
                                mbsRangescheckedListBox.SetItemChecked(i, true);

                            i++;
                        }
                    }

                }
                Rdr.Close();
                cn.Close();
            }

        }

        private void initializeMBSCriterialist()
        {
            int i;
            i = 0;

            while (i < mbsRangescheckedListBox.Items.Count)
            {
                mbsRangescheckedListBox.SetItemChecked(i, false);
                i++;
            }

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "select distinct  rtrim(ltrim(CRITERIA_field)) from  [ZM_GALLAGHER].[dbo].InventoryCriteriaSaved " +
                    " where criteria_sector='MBS' and criteria_SearchName='" + MBSSearchescomboBox.Text.ToString() +
                    "' and criteria_Descriptor='Range' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' ;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' ;";
                }

                cmd = cn.CreateCommand();

                cmd.CommandText = SQL;
                SqlDataReader Rdr;
                Rdr = cmd.ExecuteReader();

                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        // mbsTickercheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        i = 0;
                        while (i < mbsRangescheckedListBox.Items.Count)
                        {
                            if (Rdr.GetValue(0).ToString().Equals(mbsRangescheckedListBox.Items[i].ToString()))
                                mbsRangescheckedListBox.SetItemChecked(i, true);

                            i++;
                        }
                    }

                }
                Rdr.Close();
                cn.Close();

            }

        }

        private void fillMBSCriteriaRanges()
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string SQL = "select criteria_Field as Field, criteria_min as Min, criteria_max as Max, ID " +
                    " from InventoryCriteriaSaved where criteria_sector='MBS' and criteria_searchName='" +
                    MBSSearchescomboBox.Text.ToString() + "' and criteria_Descriptor='Range'  ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP' order by ID";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "' order by ID";
                }

                SQLdaCriteria = new SqlDataAdapter(SQL, cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(SQLdaCriteria);

                dsCriteria = new DataSet();
                try
                {
                    SQLdaCriteria.Fill(dsCriteria, "InventoryCriteriaSaved");
                    mbsRangedataGridView.DataSource = dsCriteria;
                    mbsRangedataGridView.DataMember = "InventoryCriteriaSaved";
                    mbsRangedataGridView.Columns[0].ReadOnly = true;
                    mbsRangedataGridView.Columns[0].Width = 85;
                    mbsRangedataGridView.Columns[1].Width = 60;
                    mbsRangedataGridView.Columns[2].Width = 60;
                    mbsRangedataGridView.Columns[3].Visible = false;
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }

                cn.Close();
            }

        }

        private void fillCriteriaRanges()
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection connection = new MySqlConnection(MyConString);
                //connection.ConnectionString = MyConString;
                //connection.CreateCommand();

                //connection.Open();

                //string SQL = "select criteriaField, minVal, maxVal "
                //   + " from tmp_CMO_criteria" + userID + " order by criteriaField";

                //daCriteria = new MySqlDataAdapter(SQL, connection);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCriteria);

                //dsCriteria = new DataSet();
                //daCriteria.Fill(dsCriteria, "tmp_CMO_criteria" + userID);
                //criteriaDataGridView.DataSource = dsCriteria;
                //criteriaDataGridView.DataMember = "tmp_CMO_criteria" + userID;
                //criteriaDataGridView.Columns[0].ReadOnly = true;
                //criteriaDataGridView.Columns[0].Width = 126;
                //criteriaDataGridView.Columns[1].Width = 55;
                //criteriaDataGridView.Columns[2].Width = 55;
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select criteria_Field as Field, criteria_min as Min, criteria_max as Max, id " +
                    " from InventoryCriteriaSaved where criteria_sector='CMO' and criteria_searchName='" +
                    SavedSearchescomboBox.Text.ToString() + "' and criteria_descriptor='Range' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP'  order by criteria_Field;";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "'  order by criteria_Field;";
                }

                SQLdaCriteria = new SqlDataAdapter(SQL, cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(SQLdaCriteria);

                dsCriteria = new DataSet();
                try
                {
                    SQLdaCriteria.Fill(dsCriteria, "InventoryCriteriaSaved");
                    criteriaDataGridView.DataSource = dsCriteria;
                    criteriaDataGridView.DataMember = "InventoryCriteriaSaved";
                    criteriaDataGridView.Columns[0].ReadOnly = true;
                    criteriaDataGridView.Columns[0].Width = 85;
                    criteriaDataGridView.Columns[1].Width = 55;
                    criteriaDataGridView.Columns[2].Width = 55;
                    criteriaDataGridView.Columns[3].Visible = false;
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
                cn.Close();
            }

        }

        private void fillClientCriteriaRanges()
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection connection = new MySqlConnection(MyConString);
                //connection.ConnectionString = MyConString;
                //connection.CreateCommand();

                //connection.Open();

                //string SQL = "select criteriaField, minVal, maxVal "
                //   + " from tmp_Client_criteria" + userID + " order by criteriaField";

                //daCriteria = new MySqlDataAdapter(SQL, connection);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCriteria);

                //dsCriteria = new DataSet();
                //daCriteria.Fill(dsCriteria, "tmp_Client_criteria" + userID);
                //clientCriteriadataGridView.DataSource = dsCriteria;
                //clientCriteriadataGridView.DataMember = "tmp_Client_criteria" + userID;
                //clientCriteriadataGridView.Columns[0].ReadOnly = true;
                //clientCriteriadataGridView.Columns[0].Width = 126;
                //clientCriteriadataGridView.Columns[1].Width = 55;
                //clientCriteriadataGridView.Columns[2].Width = 55;
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "select criteriaField, minVal, maxVal "
                   + " from tmp_Client_criteria" + userID + " order by criteriaField";

                SQLdaCriteria = new SqlDataAdapter(SQL, cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(SQLdaCriteria);

                dsCriteria = new DataSet();
                SQLdaCriteria.Fill(dsCriteria, "tmp_Client_criteria" + userID);
                clientCriteriadataGridView.DataSource = dsCriteria;
                clientCriteriadataGridView.DataMember = "tmp_Client_criteria" + userID;
                clientCriteriadataGridView.Columns[0].ReadOnly = true;
                clientCriteriadataGridView.Columns[0].Width = 126;
                clientCriteriadataGridView.Columns[1].Width = 55;
                clientCriteriadataGridView.Columns[2].Width = 55;

                cn.Close();
            }

        }

        private void criteriaDataGridView_RowStateChanged(object sender, System.Windows.Forms.DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                //if (usingSQLServer==false)
                //daCriteria.Update(dsCriteria, "tmp_CMO_criteria" + userID);

                if (usingSQLServer == true)
                    SQLdaCriteria.Update(dsCriteria, "InventoryCriteriaSaved");

            }
            catch
            {

            }

        }

        private void criteriaChooserCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (SavedSearchescomboBox.Text.Equals("New Search"))
            //   criteriaChooserCheckedListBox.SetItemChecked(criteriaChooserCheckedListBox.SelectedIndex, false);
        }

        private void criteriaChooserCheckedListBox_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (!SavedSearchescomboBox.Text.Equals("New Searchx") && !SavedSearchescomboBox.Text.Equals(""))
            {
                if (e.NewValue.ToString().Equals("Checked"))
                {
                    addCMOCriteria(criteriaChooserCheckedListBox.Items[e.Index].ToString());
                }
                else
                {
                    deleteCMOCriteria(criteriaChooserCheckedListBox.Items[e.Index].ToString());
                }

                fillCriteriaRanges();
            }
            else
            {
                if (e.NewValue.ToString().Equals("Checked"))
                {
                    MessageBox.Show("'New Search' is not a valid Search name.  You must first enter a value!");
                    for (int i = 0; i < criteriaChooserCheckedListBox.Items.Count; i++)
                    {
                        criteriaChooserCheckedListBox.SetItemChecked(i, false);
                    }
                }
                //MessageBox.Show(criteriaChooserCheckedListBox.SelectedIndex.ToString());

            }

        }

        private void clientCriteriacheckedListBox_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addClientCriteria(clientCriteriacheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteClientCriteria(clientCriteriacheckedListBox.Items[e.Index].ToString());
            }
            fillClientCriteriaRanges();
        }

        private void deleteClientCriteria(string criteria)
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand("delete from fig.tmp_Client_criteria"
                //   + userID + " where criteriaField ='" + criteria + "';", cn);
                //cmd.ExecuteNonQuery();
                //cn.Close();
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");
                cn.Open();

                SqlCommand cmd = new SqlCommand("delete from tmp_Client_criteria"
                   + userID + " where criteriaField ='" + criteria + "';", cn);
                cmd.ExecuteNonQuery();
                cn.Close();

            }

        }

        private void deleteCMOCriteria(string criteria)
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand("delete from fig.tmp_CMO_criteria"
                //   + userID + " where criteriaField ='" + criteria + "';", cn);
                //cmd.ExecuteNonQuery();
                //cn.Close();
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where " +
                    " criteria_sector='CMO' and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_Field ='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();
                cn.Close();
            }

        }

        private void addCMOCriteria(string criteria)
        {
            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand();
                //string SQL = "select count(*) from fig.tmp_CMO_criteria" + userID + " where criteriaField='"
                //   + criteria + "';";
                //cmd = cn.CreateCommand();
                //MySqlDataReader Rdr;

                //cmd.CommandText = SQL;
                //Rdr = cmd.ExecuteReader();
                //Rdr.Read();

                //if (Rdr.GetValue(0).ToString().Equals("0"))
                //{
                //    Rdr.Close();
                //    cmd = new MySqlCommand("replace into fig.tmp_CMO_criteria"
                //    + userID + " values ('" + criteria + "', '','');", cn);
                //    cmd.ExecuteNonQuery();
                //}
                //else
                //{
                //    Rdr.Close();
                //}

                //cn.Close();
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' " +
                    " and criteria_searchName='" + SavedSearchescomboBox.Text.ToString() +
                    "' and criteria_Field='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" +
                          SavedSearchescomboBox.Text.ToString() + "','" + criteria + "', '','','Range','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" +
                          SavedSearchescomboBox.Text.ToString() + "','" + criteria + "', '','','Range','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }

        }

        private void deleteMBSRangeCriteria(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where " +
                    " criteria_sector='MBS' and criteria_searchName='" + savedSearch +
                    "' and criteria_Field='" + criteria + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " AND criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " AND criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();
                cn.Close();
            }

        }

        private void addMBSRangeCriteria(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_Field='" + criteria + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " AND criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " AND criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();

                    if (MBSradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','" + criteria + "', '','','Range','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','" + criteria + "', '','','Range','" + userID + "');";
                    }

                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('MBS','" + savedSearch + 
                    //    "','" + criteria + "', '','','Range');", cn);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }

        }

        private void addClientCriteria(string criteria)
        {

            if (usingSQLServer == false)
            {
                //MySqlConnection cn = new MySqlConnection(MyConString);
                //cn.ConnectionString = MyConString;
                //cn.Open();

                //MySqlCommand cmd = new MySqlCommand();
                //string SQL = "select count(*) from fig.tmp_Client_criteria" + userID + " where criteriaField='"
                //   + criteria + "';";
                //cmd = cn.CreateCommand();
                //MySqlDataReader Rdr;

                //cmd.CommandText = SQL;
                //Rdr = cmd.ExecuteReader();
                //Rdr.Read();

                //if (Rdr.GetValue(0).ToString().Equals("0"))
                //{
                //    Rdr.Close();
                //    cmd = new MySqlCommand("replace into fig.tmp_Client_criteria"
                //    + userID + " values ('" + criteria + "', '','');", cn);
                //    cmd.ExecuteNonQuery();
                //}
                //else
                //{
                //    Rdr.Close();
                //}

                //cn.Close();
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                string SQL = "select count(*) from tmp_Client_criteria" + userID + " where criteriaField='"
                   + criteria + "';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    cmd = new SqlCommand("insert into tmp_Client_criteria"
                    + userID + " values ('" + criteria + "', '','');", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }

        }


        private void criteriaDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void clientSearchbutton_Click(object sender, EventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("GaryTest");
            //searchClient();
        }

        void sector_AllcheckBox_Click(object sender, System.EventArgs e)
        {
            if (sector_AllcheckBox.CheckState.ToString().Equals("Checked"))
            {
                sect_FixCMOcheckBox.Checked = true;
                sect_FixCorpcheckBox.Checked = true;
                sect_FixedAgencycheckBox.Checked = true;
                sect_FixMBScheckBox.Checked = true;
                sect_FixOthercheckBox.Checked = true;
                sect_floatAgencycheckBox.Checked = true;
                sect_floatCMOcheckBox.Checked = true;
                sect_floatCorpcheckBox.Checked = true;
                sect_floatMBScheckBox.Checked = true;
                sect_floatOthercheckBox.Checked = true;
                sect_taxFreeMunicheckBox.Checked = true;
                sect_taxMunicheckBox.Checked = true;
                sect_treasurycheckBox.Checked = true;
            }

            if (sector_AllcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                sect_FixCMOcheckBox.Checked = false;
                sect_FixCorpcheckBox.Checked = false;
                sect_FixedAgencycheckBox.Checked = false;
                sect_FixMBScheckBox.Checked = false;
                sect_FixOthercheckBox.Checked = false;
                sect_floatAgencycheckBox.Checked = false;
                sect_floatCMOcheckBox.Checked = false;
                sect_floatCorpcheckBox.Checked = false;
                sect_floatMBScheckBox.Checked = false;
                sect_floatOthercheckBox.Checked = false;
                sect_taxFreeMunicheckBox.Checked = false;
                sect_taxMunicheckBox.Checked = false;
                sect_treasurycheckBox.Checked = false;
            }

        }

        void coupon_AllcheckBox_Click(object sender, System.EventArgs e)
        {
            if (coupon_AllcheckBox.CheckState.ToString().Equals("Checked"))
            {
                coupon_AdjustablecheckBox.Checked = true;
                coupon_FixedcheckBox.Checked = true;
                coupon_MulticheckBox.Checked = true;
                coupon_SinglecheckBox.Checked = true;
            }
            if (coupon_AllcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                coupon_AdjustablecheckBox.Checked = false;
                coupon_FixedcheckBox.Checked = false;
                coupon_MulticheckBox.Checked = false;
                coupon_SinglecheckBox.Checked = false;
            }
        }

        void moody_AllcheckBox_Click(object sender, System.EventArgs e)
        {
            if (moody_AllcheckBox.CheckState.ToString().Equals("Checked"))
            {
                moody_A1checkBox.Checked = true;
                moody_A2checkBox.Checked = true;
                moody_A3checkBox.Checked = true;
                moody_AA1checkBox.Checked = true;
                moody_AA2checkBox.Checked = true;
                moody_AA3checkBox.Checked = true;
                moody_AAAcheckBox.Checked = true;
                moody_BAA1checkBox.Checked = true;
                moody_BAA2checkBox.Checked = true;
                moody_BAA3checkBox.Checked = true;
                moody_OthercheckBox.Checked = true;
            }
            if (moody_AllcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                moody_A1checkBox.Checked = false;
                moody_A2checkBox.Checked = false;
                moody_A3checkBox.Checked = false;
                moody_AA1checkBox.Checked = false;
                moody_AA2checkBox.Checked = false;
                moody_AA3checkBox.Checked = false;
                moody_AAAcheckBox.Checked = false;
                moody_BAA1checkBox.Checked = false;
                moody_BAA2checkBox.Checked = false;
                moody_BAA3checkBox.Checked = false;
                moody_OthercheckBox.Checked = false;
            }
        }

        void sp_AllcheckBox_Click(object sender, System.EventArgs e)
        {
            if (sp_AllcheckBox.CheckState.ToString().Equals("Checked"))
            {
                sp_AAAcheckBox.Checked = true;
                sp_AAcheckBox.Checked = true;
                sp_AAMcheckBox.Checked = true;
                sp_AAPcheckBox.Checked = true;
                sp_AcheckBox.Checked = true;
                sp_AMcheckBox.Checked = true;
                sp_APcheckBox.Checked = true;
                sp_BBBcheckBox.Checked = true;
                sp_BBBMcheckBox.Checked = true;
                sp_BBBPcheckBox.Checked = true;
                sp_OthercheckBox.Checked = true;
            }
            if (sp_AllcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                sp_AAAcheckBox.Checked = false;
                sp_AAcheckBox.Checked = false;
                sp_AAMcheckBox.Checked = false;
                sp_AAPcheckBox.Checked = false;
                sp_AcheckBox.Checked = false;
                sp_AMcheckBox.Checked = false;
                sp_APcheckBox.Checked = false;
                sp_BBBcheckBox.Checked = false;
                sp_BBBMcheckBox.Checked = false;
                sp_BBBPcheckBox.Checked = false;
                sp_OthercheckBox.Checked = false;
            }
        }

        private void sector_AllcheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void sect_FixedAgencycheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_FixCMOcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_FixCorpcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_FixMBScheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_FixOthercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_floatAgencycheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_floatCMOcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_floatCorpcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_floatMBScheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_floatOthercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_taxMunicheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_taxFreeMunicheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void sect_treasurycheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSectorType();
        }

        private void coupon_AdjustablecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientCouponType();
        }

        private void coupon_FixedcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientCouponType();
        }

        private void coupon_MulticheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientCouponType();
        }

        private void coupon_SinglecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientCouponType();
        }

        private void moody_AAAcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_AA3checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_AA2checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_AA1checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_A3checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_A2checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_A1checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_BAA3checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_BAA2checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_BAA1checkBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void moody_OthercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientMoodyType();
        }

        private void sp_AAAcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_AAPcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_AAcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_AAMcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_APcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_AcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_AMcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_BBBPcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_BBBcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_BBBMcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void sp_OthercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForAllClientSandP();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bondFinder_Load(object sender, EventArgs e)
        {

            //CORP Section
            FillCorpDealer();
            FillCorpTicker();
            FillCORPRanges();

            ////CMO section
            //fillCMOcriteraiList();
            //fillCMOPrincipalType();
            //fillCMOCollateralType();
            //fillCMOTickerType();

            //CMOradioButtonUser.Text = Environment.UserName + " CMO Searches";
            //if (CMOradioButtonGroup.Checked == true)
            //{
            //    if (CMOradioButtonGroup.Checked == false)
            //        CMOradioButtonGroup.Checked = true;
            //}
            //else
            //{
            //    if (CMOradioButtonUser.Checked == false)
            //        CMOradioButtonUser.Checked = true;
            //}


            ////MBS section
            //fillMBSClient();
            //fillMBSTypeList();
            //fillMBSTicker();
            //fillMBSRanges();

            //MBSradioButtonUser.Text = Environment.UserName + " MBS Searches";
            //if (MBSradioButtonGroup.Checked == true)
            //{
            //    if (MBSradioButtonGroup.Checked == false)
            //        MBSradioButtonGroup.Checked = true;
            //}
            //else
            //{
            //    if (MBSradioButtonUser.Checked == false)
            //        MBSradioButtonUser.Checked = true;
            //}

            ////fillMBSsearchesComboBox();

            ////MUNI
            //fillMuniStateList();
            //fillMuniTypeList();
            //fillMuniMoodyList();
            //fillMuniSPList();

            //MuniradioButtonUser.Text = Environment.UserName + " MUNI Searches";
            //if (MuniradioButtonGroup.Checked == true)
            //{
            //    if (MuniradioButtonGroup.Checked == false)
            //        MBSradioButtonGroup.Checked = true;
            //}
            //else
            //{
            //    if (MuniradioButtonUser.Checked == false)
            //        MuniradioButtonUser.Checked = true;
            //}

            //  MessageBox.Show( MBSSearchescomboBox.SelectedIndex.ToString() );
        }

        private void clientCriteriadataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void clientCriteriadataGridView_RowStateChanged(object sender, System.Windows.Forms.DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (usingSQLServer == false)
                    //daCriteria.Update(dsCriteria, "tmp_Client_criteria" + userID);

                    if (usingSQLServer == true)
                        SQLdaCriteria.Update(dsCriteria, "tmp_Client_criteria" + userID);

            }
            catch (System.IO.IOException m)
            {
                MessageBox.Show(m.Message.ToString());
            }
        }

        private void MBSSearchescomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setMBSComboStyle();

            //if (MBSSearchescomboBox.SelectedIndex == 0)
            //{
            //    mbsTypecheckedListBox.Enabled = false;
            //    mbsTickercheckedListBox.Enabled =false;
            //    mbsRangescheckedListBox.Enabled =false;
            //}
            //else
            //{
            //    mbsTypecheckedListBox.Enabled = true;
            //    mbsTickercheckedListBox.Enabled = true;
            //    mbsRangescheckedListBox.Enabled = true;
            //}

            //Updated
            //fillMBSClientList();
            checkMBSSearchCombo();
            checkMBSTypecriteriaList();
            checkMBSTickercriteriaList();
            checkMBSClientcriteriaList();
            fillMBSCriteriaRanges();
            initializeMBSCriterialist();
        }

        private void checkMBSSearchCombo()
        {
            int i;
            bool isNew;

            isNew = true;
            //MessageBox.Show( currentSearch.ToString() );
            i = 0;
            while (i < MBSSearchescomboBox.Items.Count)
            {
                if (!String.IsNullOrEmpty(currentSearch))
                    if (MBSSearchescomboBox.Items[i].ToString().Equals(currentSearch.ToString()))
                        isNew = false;

                i++;
            }

            if (!String.IsNullOrEmpty(currentSearch))
                if (isNew == true && currentSearch.Length > 0)
                    MBSSearchescomboBox.Items.Add(currentSearch.ToString());
        }

        private void checkMUNISearchCombo()
        {
            int i;
            bool isNew;

            isNew = true;
            //MessageBox.Show( currentSearch.ToString() );
            i = 0;
            while (i < muniSearchescomboBox.Items.Count)
            {
                if (!String.IsNullOrEmpty(currentSearch))
                    if (muniSearchescomboBox.Items[i].ToString().Equals(currentSearch.ToString()))
                        isNew = false;

                i++;
            }

            if (!String.IsNullOrEmpty(currentSearch))
                if (isNew == true && currentSearch.Length > 0)
                    muniSearchescomboBox.Items.Add(currentSearch.ToString());
        }

        private void mbsRangescheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mbsRangescheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // MessageBox.Show(criteriaChooserCheckedListBox.Items[e.Index].ToString());
            //MessageBox.Show(e.NewValue.ToString());

            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMBSRangeCriteria(MBSSearchescomboBox.Text.ToString(), mbsRangescheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMBSRangeCriteria(MBSSearchescomboBox.Text.ToString(), mbsRangescheckedListBox.Items[e.Index].ToString());
            }

            fillMBSCriteriaRanges();
        }

        private void mbsRangedataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                // if (usingSQLServer == false)
                //daCriteria.Update(dsCriteria, "InventoryCriteriaSaved");

                if (usingSQLServer == true)
                    SQLdaCriteria.Update(dsCriteria, "InventoryCriteriaSaved");

            }
            catch (System.IO.IOException m)
            {
                MessageBox.Show(m.Message.ToString());
            }
        }

        private void mbsRangedataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mbsRangedataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.ToString());
        }

        private void mbsRangedataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //  MessageBox.Show(e.ToString());

            try
            {
                // if (usingSQLServer == false)
                //daCriteria.Update(dsCriteria, "InventoryCriteriaSaved");

                if (usingSQLServer == true)
                    SQLdaCriteria.Update(dsCriteria, "InventoryCriteriaSaved");

            }
            catch (System.IO.IOException m)
            {
                MessageBox.Show(m.Message.ToString());
            }
        }

        private void criteriaDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (usingSQLServer == true)
                SQLdaCriteria.Update(dsCriteria, "InventoryCriteriaSaved");

        }

        private void searchMBSbutton_Click(object sender, EventArgs e)
        {
            SearchMBS();
        }

        private void MBSSearchescomboBox_TextChanged(object sender, EventArgs e)
        {
            //updated
            //MessageBox.Show(MBSSearchescomboBox.Text.ToString());

            if (!MBSSearchescomboBox.Text.Equals("New Search"))
            {
                mbsTypecheckedListBox.Enabled = true;
                mbsTickercheckedListBox.Enabled = true;
                mbsRangescheckedListBox.Enabled = true;
            }

            int i;
            i = 0;

            if (MBSSearchescomboBox.SelectedIndex > 0)
            {

            }

            if (i == 1)
            {
                while (i < mbsTypecheckedListBox.Items.Count)
                {
                    mbsTypecheckedListBox.SetItemChecked(i, false);
                    i++;
                }

                i = 0;
                while (i < mbsTickercheckedListBox.Items.Count)
                {
                    mbsTickercheckedListBox.SetItemChecked(i, false);
                    i++;
                }
            }

        }

        private void mbsTypecheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMBSType(MBSSearchescomboBox.Text.ToString(), mbsTypecheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMBSType(MBSSearchescomboBox.Text.ToString(), mbsTypecheckedListBox.Items[e.Index].ToString());
            }

        }

        private void addMBSType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                //string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                //    " criteria_searchName='" + savedSearch + "' and criteria_field='Type' and criteria_min='" + criteria + "' ";

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='blm_sector' and criteria_min='" + criteria + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MBSradioButtonGroup.Checked == true)
                    {
                        //cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        //"','Type','" + criteria + "', '','Text','GROUP');";
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','blm_sector','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        //cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        //"','Type','" + criteria + "', '','Text','" + userID + "');";
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','blm_sector','" + criteria + "', '','Text','" + userID + "');";
                    }

                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                    //    "','Type','" + criteria + "', '','Text');", cn);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMUNIState(string savedSearch, string criteria)
        {
            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='State' and " +
                    " criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','State','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','State','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMUNIType(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Type' and " +
                    " criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Type','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Type','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMuniMoody(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Moody' and " +
                    " criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Moody','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Moody','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMuniSP(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='S_P' and " +
                    " criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','S_P','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','S_P','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMUNIbq(string savedSearch, string criteria)
        {
            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Bank_Qualified' " +
                    " and criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Bank_Qualified','" + criteria + "', '','BQ','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Bank_Qualified','" + criteria + "', '','BQ','" + userID + "');";
                    }
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMUNIrefunded(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Refunded' " +
                    " and criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Refunded','" + criteria + "', '','Refunded','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Refunded','" + criteria + "', '','Refunded','" + userID + "');";
                    }
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMUNIFedTax(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Fed_Tax' " +
                    " and criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Fed_Tax','" + criteria + "', '','FedTaxable','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','Fed_Tax','" + criteria + "', '','FedTaxable','" + userID + "');";
                    }
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addMUNIStateTax(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='State_Tax' " +
                    " and criteria_min='" + criteria + "' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','State_Tax','" + criteria + "', '','StateTaxable','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                        "','State_Tax','" + criteria + "', '','StateTaxable','" + userID + "');";
                    }
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteMUNIbq(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Bank_Qualified' " +
                    " and criteria_min ='" + criteria + "' and criteria_descriptor='BQ' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMUNIrefunded(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Refunded' " +
                    " and criteria_min ='" + criteria + "' and criteria_descriptor='Refunded' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMUNIFedTax(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Fed_Tax' " +
                    " and criteria_min ='" + criteria + "' and criteria_descriptor='FedTaxable' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMUNIStateTax(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='State_Tax' " +
                    " and criteria_min ='" + criteria + "' and criteria_descriptor='StateTaxable' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMBSType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='blm_sector' and criteria_min ='" + criteria +
                    "' and criteria_descriptor='Text' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMuniState(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='State' and " +
                    " criteria_min ='" + criteria + "' and criteria_descriptor='Text' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMuniType(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Type' and " +
                    " criteria_min ='" + criteria + "' and criteria_descriptor='Text' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMuniMoody(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Moody' and " +
                    " criteria_min ='" + criteria + "' and criteria_descriptor='Text' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteMuniSP(string savedSearch, string criteria)
        {

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='S_P' and " +
                    " criteria_min ='" + criteria + "' and criteria_descriptor='Text' ";

                if (MuniradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                SqlCommand cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }


        private void addMBSClient(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Client' and criteria_min='" + criteria + "';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MBSradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','Client','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','Client','" + criteria + "', '','Text','" + userID + "');";
                    }

                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                    //    "','Client','" + criteria + "', '','Text');", cn);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteMBSClient(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("delete from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Client' and criteria_min ='" + criteria +
                    "' and criteria_descriptor='Text';", cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void addCMOCollatType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='CType' and criteria_min='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner= 'GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner= '" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','CType','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','CType','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteCMOCollatType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='CType' and criteria_min='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_SearchOwner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_SearchOwner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cn.Close();
            }
        }

        private void addCMOTickerType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Ticker' and criteria_min='" +
                    criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner= 'GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner= '" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','Ticker','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','Ticker','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteCMOTickerType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Ticker' and criteria_min='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_SearchOwner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_SearchOwner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cn.Close();
            }
        }

        private void addCMOPrinType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='PType' and criteria_min='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','PType','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','PType','" + criteria + "', '','Text','" + userID + "');";
                    }

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteCMOPrinType(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='PType' and criteria_min='" + criteria + "' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_SearchOwner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_SearchOwner='" + userID + "';";
                }


                cmd.ExecuteNonQuery();

                cn.Close();
            }
        }

        private void mbsTickercheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMBSTicker(MBSSearchescomboBox.Text.ToString(), mbsTickercheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMBSTicker(MBSSearchescomboBox.Text.ToString(), mbsTickercheckedListBox.Items[e.Index].ToString());
            }

        }

        private void addCMOIssuer(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Issuer' and criteria_min='" +
                    criteria + "';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (MBSradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','Issuer','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','Issuer','" + criteria + "', '','Text','" + userID + "');";
                    }
                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                    //    "','Issuer','" + criteria + "', '','Text');", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addCMOCT(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='CType' and criteria_min='" +
                    criteria + "';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0") && !savedSearch.ToString().Equals(""))
                {
                    Rdr.Close();
                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','CType','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','CType','" + criteria + "', '','Text','" + userID + "');";
                    }

                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                    //    "','CType','" + criteria + "', '','Text','GROUP');", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addCMOPT(string savedSearch, string pType)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='PType' and criteria_min='" +
                    pType + "';";
                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','PType','" + pType + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                        "','PType','" + pType + "', '','Text','" + userID + "');";
                    }
                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('CMO','" + savedSearch +
                    //    "','PType','" + pType + "', '','Text');", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteCMOPT(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();

                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='PType' and criteria_min ='" + criteria +
                    "' and criteria_descriptor='Text' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteCMOIssuer(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();

                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Issuer' and criteria_min ='" + criteria +
                    "' and criteria_descriptor='Text' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void deleteCMOCT(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();

                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='CType' and criteria_min ='" + criteria +
                    "' and criteria_descriptor='Text' ";

                if (CMOradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void addMBSTicker(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName='" + savedSearch + "' and criteria_field='Ticker' and criteria_min='" + criteria + "' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    SQL += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    SQL += " and criteria_searchowner='" + userID + "';";
                }

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();

                    if (MBSradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','Ticker','" + criteria + "', '','Text','GROUP');";
                    }
                    else
                    {
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                        "','Ticker','" + criteria + "', '','Text','" + userID + "');";
                    }

                    //cmd = new SqlCommand("insert into InventoryCriteriaSaved values ('MBS','" + savedSearch +
                    //    "','Ticker','" + criteria + "', '','Text');", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void deleteMBSTicker(string savedSearch, string criteria)
        {
            if (usingSQLServer == false)
            {
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();

                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                    " criteria_searchName = '" + savedSearch + "' and criteria_field='Ticker' and criteria_min ='" +
                    criteria + "' and criteria_descriptor='Text' ";

                if (MBSradioButtonGroup.Checked == true)
                {
                    cmd.CommandText += " and criteria_searchowner='GROUP';";
                }
                else
                {
                    cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                }

                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }


        private void setMBSComboStyle()
        {
            if (MBSSearchescomboBox.SelectedIndex == 0)
            {
                MBSSearchescomboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                MBSSearchescomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

        }

        private void setCMOComboStyle()
        {
            if (SavedSearchescomboBox.SelectedIndex == 0)
            {
                SavedSearchescomboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                SavedSearchescomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

        }

        private void MBSSearchescomboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void SavedSearchescomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCMOComboStyle();
            checkCMOcriteriaList(); //think so!
            checkCMOCollatType();
            checkCMOPrinType();
            checkCMOTicker();

            //---WORK HERE
            //checkCMOPType();
            //checkCMOCType();
            //checkCMOIssuer();

            fillCriteriaRanges();

        }

        private void sectorTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  MessageBox.Show(sectorTabControl.SelectedTab.ToString());
        }

        private void mbsTypecheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mbsClientcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMBSClient(MBSSearchescomboBox.Text.ToString(), mbsClientcheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMBSClient(MBSSearchescomboBox.Text.ToString(), mbsClientcheckedListBox.Items[e.Index].ToString());
            }
        }

        private void mbsClearCriteriabutton_Click(object sender, EventArgs e)
        {
            int i;

            i = 0;
            while (i < mbsTypecheckedListBox.Items.Count)
            {
                mbsTypecheckedListBox.SetItemChecked(i, false);
                i++;
            }

            i = 0;
            while (i < mbsTickercheckedListBox.Items.Count)
            {
                mbsTickercheckedListBox.SetItemChecked(i, false);
                i++;
            }
            i = 0;
            while (i < mbsRangescheckedListBox.Items.Count)
            {
                mbsRangescheckedListBox.SetItemChecked(i, false);
                i++;
            }

        }

        private void MBSradioButtonUser_CheckedChanged(object sender, EventArgs e)
        {
            fillMBSsearchesComboBox();
        }

        private void MBSradioButtonGroup_CheckedChanged(object sender, EventArgs e)
        {
            fillMBSsearchesComboBox();
        }

        private void MBSSearchescomboBox_TextUpdate(object sender, EventArgs e)
        {
            //MessageBox.Show(MBSSearchescomboBox.Text.ToString());
        }

        private void mbsDeleteCriteriabutton_Click(object sender, EventArgs e)
        {
            deleteMBScriteria();
        }

        private void deleteMBScriteria()
        {
            if (MBSSearchescomboBox.SelectedIndex != 0)
            {
                //***NOT New Search...INDEX=0
                if (usingSQLServer == true)
                {
                    SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                        "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd = cn.CreateCommand();
                    cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='MBS' and " +
                        " criteria_searchName = '" + MBSSearchescomboBox.Text.ToString() + "' ";

                    if (MBSradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText += " and criteria_searchowner='GROUP';";
                    }
                    else
                    {
                        cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                    }

                    cmd.ExecuteNonQuery();

                    cn.Close();
                    fillMBSsearchesComboBox();
                }

            }

        } //***END OF DELETEMBSCRITERIA()

        private void deleteMUNIcriteria()
        {
            if (muniSearchescomboBox.SelectedIndex != 0)
            {
                //***NOT New Search...INDEX=0
                if (usingSQLServer == true)
                {
                    SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                        "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd = cn.CreateCommand();
                    cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='MUNI' and " +
                        " criteria_searchName = '" + muniSearchescomboBox.Text.ToString() + "' ";

                    if (MuniradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText += " and criteria_searchowner='GROUP';";
                    }
                    else
                    {
                        cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                    }

                    cmd.ExecuteNonQuery();

                    cn.Close();
                    fillMUNIsearchesComboBox();
                }

            }

        } //***END OF DELETEMUNICRITERIA()

        private void deleteCMOcriteria()
        {
            if (SavedSearchescomboBox.SelectedIndex != 0)
            {
                //***NOT New Search...INDEX=0
                if (usingSQLServer == true)
                {
                    SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                        "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd = cn.CreateCommand();
                    cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='CMO' and " +
                        " criteria_searchName = '" + SavedSearchescomboBox.Text.ToString() + "' ";

                    if (CMOradioButtonGroup.Checked == true)
                    {
                        cmd.CommandText += " and criteria_searchowner='GROUP';";
                    }
                    else
                    {
                        cmd.CommandText += " and criteria_searchowner='" + userID + "';";
                    }

                    cmd.ExecuteNonQuery();
                    cn.Close();
                    fillCMOcriteraiList();
                }
            }

        }

        private void CMOdeleteCriteriabutton_Click(object sender, EventArgs e)
        {
            deleteCMOcriteria();
        }

        private void CMOradioButtonUser_CheckedChanged(object sender, EventArgs e)
        {
            fillSavedSearchesCombo();
        }

        private void CMOradioButtonGroup_CheckedChanged(object sender, EventArgs e)
        {
            fillSavedSearchesCombo();
        }

        private void criteriaChooserCheckedListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SavedSearchescomboBox.Text.Equals("New Search"))
                criteriaChooserCheckedListBox.SetItemChecked(criteriaChooserCheckedListBox.SelectedIndex, false);

        }

        private void criteriaChooserCheckedListBox_Click(object sender, EventArgs e)
        {

        }

        private void criteriaChooserCheckedListBox_DoubleClick(object sender, EventArgs e)
        {
            //if (SavedSearchescomboBox.Text.Equals("New Search"))
            //  criteriaChooserCheckedListBox.SetItemChecked(criteriaChooserCheckedListBox.SelectedIndex, false);

        }

        private void criteriaChooserCheckedListBox_Enter(object sender, EventArgs e)
        {

        }

        private void criteriaChooserCheckedListBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            //MessageBox.Show(e.KeyCode.ToString() );
            //MessageBox.Show(e.KeyValue.ToString() );

            //if (SavedSearchescomboBox.Text.Equals("New Search"))
            //criteriaChooserCheckedListBox.SetItemChecked(criteriaChooserCheckedListBox.SelectedIndex, false);

        }

        private void criteriaChooserCheckedListBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void criteriaChooserCheckedListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (SavedSearchescomboBox.Text.Equals("New Search") || SavedSearchescomboBox.Text.Equals(""))
                criteriaChooserCheckedListBox.SetItemChecked(criteriaChooserCheckedListBox.SelectedIndex, false);

        }

        private void cmoPrincipalcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addCMOPrinType(SavedSearchescomboBox.Text.ToString(), cmoPrincipalcheckedListBox.Items[e.Index].ToString());

            }
            else
            {
                deleteCMOPrinType(SavedSearchescomboBox.Text.ToString(), cmoPrincipalcheckedListBox.Items[e.Index].ToString());
            }
        }

        private void CMOCollateralcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addCMOCollatType(SavedSearchescomboBox.Text.ToString(), CMOCollateralcheckedListBox.Items[e.Index].ToString());

            }
            else
            {
                deleteCMOCollatType(SavedSearchescomboBox.Text.ToString(), CMOCollateralcheckedListBox.Items[e.Index].ToString());
            }
        }

        private void CMOTickercheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addCMOTickerType(SavedSearchescomboBox.Text.ToString(), CMOTickercheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteCMOTickerType(SavedSearchescomboBox.Text.ToString(), CMOTickercheckedListBox.Items[e.Index].ToString());
            }
        }

        private void bondFinder_FormClosed(object sender, FormClosedEventArgs e)
        {
            // this.Close();
        }

        private void Adminbutton_Click(object sender, EventArgs e)
        {
            if (AdminTaskcomboBox.Text.Equals("New Field in Search Index"))
            {
                AdminAddSearchIndex();
            }
        }

        private void AdminAddSearchIndex()
        {
            string Sector;
            string SearchBox;

            Sector = AdminSectorcomboBox.Text.ToString();

            //*** ADD FIELD TO CMOLOOKUP
            //*** ADD FIELD TO BLP_FIELDS
            //*** ADD TO SEARCHCRITERIA
            if (AdminBloomFieldtextBox.Text.Equals("") || AdminFieldLabeltextBox.Text.Equals(""))
            {
                MessageBox.Show("You must enter values for both the Bloomberg Field and Field label text boxes!");
                return;
            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from blp_fields where blp_field='" + AdminBloomFieldtextBox.Text +
                    "' and blp_type='" + Sector + "' and blp_label ='" + AdminFieldLabeltextBox.Text + "';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "insert into blp_fields values ('" + AdminBloomFieldtextBox.Text +
                    "', '" + Sector + "', '" + AdminFieldLabeltextBox.Text + "','','');";
                cmd.ExecuteNonQuery();

                //*** ADD TO CMOLOOKUP
                cmd.CommandText = "IF Not EXISTS(SELECT * FROM sys.columns where Name=N'" +
                  AdminFieldLabeltextBox.Text + "' and Object_ID= Object_ID(N'CMOlookup')) begin " +
                  " ALTER TABLE CMOlookup ADD  " + AdminFieldLabeltextBox.Text + " varchar(255); end";


                cmd.ExecuteNonQuery();

                if (AdminSectorcomboBox.Text.Equals("CMO"))
                {
                    SearchBox = "cmo";
                }
                else
                {
                    SearchBox = "mbs";
                }
                if (AdminFieldTypecomboBox.Text.Equals("Text"))
                {
                    SearchBox += "Ticker";
                }
                else
                {
                    SearchBox += "Range";
                }

                cmd.CommandText = "delete from searchcriteria where criteria_sector='" + Sector +
                    "' and  criteria_searchbox='" + SearchBox + "' and criteria_value= '" + AdminFieldLabeltextBox.Text + "';";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "insert into searchcriteria values ('" + Sector +
                  "', '" + SearchBox + "', '" + AdminFieldLabeltextBox.Text + "');";
                cmd.ExecuteNonQuery();

                cn.Close();

                MessageBox.Show("Update xsl.xsl");

            }


        }

        private void mbsTickercheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CMOclearCriteriabutton_Click(object sender, EventArgs e)
        {
            int i;

            i = 0;
            while (i < CMOCollateralcheckedListBox.Items.Count)
            {
                CMOCollateralcheckedListBox.SetItemChecked(i, false);
                i++;
            }

            i = 0;
            while (i < CMOTickercheckedListBox.Items.Count)
            {
                CMOTickercheckedListBox.SetItemChecked(i, false);
                i++;
            }

            i = 0;
            while (i < criteriaChooserCheckedListBox.Items.Count)
            {
                criteriaChooserCheckedListBox.SetItemChecked(i, false);
                i++;
            }

        }

        private void MuniSearchbutton_Click(object sender, EventArgs e)
        {
            searchMUNI();
        }

        private void MuniBQcheckBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(MuniBQcheckBox.CheckState.ToString());

            if (MuniBQcheckBox.CheckState.ToString().Equals("Checked"))
            {
                deleteMUNIbq(muniSearchescomboBox.Text.ToString(), "N");
                addMUNIbq(muniSearchescomboBox.Text.ToString(), "Y");
                MUNIbqLabel.Text = "Bank Qualified";
            }

            if (MuniBQcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                deleteMUNIbq(muniSearchescomboBox.Text.ToString(), "N");
                deleteMUNIbq(muniSearchescomboBox.Text.ToString(), "Y");
                MUNIbqLabel.Text = "All";
            }

            if (MuniBQcheckBox.CheckState.ToString().Equals("Indeterminate"))
            {
                deleteMUNIbq(muniSearchescomboBox.Text.ToString(), "Y");
                addMUNIbq(muniSearchescomboBox.Text.ToString(), "N");
                MUNIbqLabel.Text = "Non Bank Qualified";
            }

        }

        private void MuniBQcheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MuniradioButtonGroup_CheckedChanged(object sender, EventArgs e)
        {
            fillMUNIsearchesComboBox();
        }

        private void MuniradioButtonUser_CheckedChanged(object sender, EventArgs e)
        {
            fillMUNIsearchesComboBox();
        }

        private void muniSearchescomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //checkMUNISearchCombo();

            checkMUNIBQ();
            checkMUNIrefunded();
            checkMUNIFedTax();
            checkMUNIStateTax();
            checkMuniStateList();
            checkMuniType();
            checkMuniMoody();
            checkMuniSP();

        }

        private void MunideleteCriteriabutton_Click(object sender, EventArgs e)
        {
            deleteMUNIcriteria();
        }

        private void MuniRefundedcheckBox_Click(object sender, EventArgs e)
        {
            if (MuniRefundedcheckBox.CheckState.ToString().Equals("Checked"))
            {
                deleteMUNIrefunded(muniSearchescomboBox.Text.ToString(), "N");
                addMUNIrefunded(muniSearchescomboBox.Text.ToString(), "Y");
                MuniRefundedlabel.Text = "Refunded";
            }

            if (MuniRefundedcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                deleteMUNIrefunded(muniSearchescomboBox.Text.ToString(), "N");
                deleteMUNIrefunded(muniSearchescomboBox.Text.ToString(), "Y");
                MuniRefundedlabel.Text = "All";
            }

            if (MuniRefundedcheckBox.CheckState.ToString().Equals("Indeterminate"))
            {
                deleteMUNIrefunded(muniSearchescomboBox.Text.ToString(), "Y");
                addMUNIrefunded(muniSearchescomboBox.Text.ToString(), "N");
                MuniRefundedlabel.Text = "Non Refunded";
            }

        }

        private void MuniFedTaxcheckBox_Click(object sender, EventArgs e)
        {

            if (MuniFedTaxcheckBox.CheckState.ToString().Equals("Checked"))
            {
                deleteMUNIFedTax(muniSearchescomboBox.Text.ToString(), "N");
                addMUNIFedTax(muniSearchescomboBox.Text.ToString(), "Y");
                MUNIfedTaxlabel.Text = "Taxable";
            }

            if (MuniFedTaxcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                deleteMUNIFedTax(muniSearchescomboBox.Text.ToString(), "N");
                deleteMUNIFedTax(muniSearchescomboBox.Text.ToString(), "Y");
                MUNIfedTaxlabel.Text = "All";
            }

            if (MuniFedTaxcheckBox.CheckState.ToString().Equals("Indeterminate"))
            {
                deleteMUNIFedTax(muniSearchescomboBox.Text.ToString(), "Y");
                addMUNIFedTax(muniSearchescomboBox.Text.ToString(), "N");
                MUNIfedTaxlabel.Text = "Non Taxable";
            }

        }

        private void muniStateTaxcheckBox_Click(object sender, EventArgs e)
        {
            if (muniStateTaxcheckBox.CheckState.ToString().Equals("Checked"))
            {
                deleteMUNIStateTax(muniSearchescomboBox.Text.ToString(), "N");
                addMUNIStateTax(muniSearchescomboBox.Text.ToString(), "Y");
                muniStateTaxlabel.Text = "Taxable";
            }

            if (muniStateTaxcheckBox.CheckState.ToString().Equals("Unchecked"))
            {
                deleteMUNIStateTax(muniSearchescomboBox.Text.ToString(), "N");
                deleteMUNIStateTax(muniSearchescomboBox.Text.ToString(), "Y");
                muniStateTaxlabel.Text = "All";
            }

            if (muniStateTaxcheckBox.CheckState.ToString().Equals("Indeterminate"))
            {
                deleteMUNIStateTax(muniSearchescomboBox.Text.ToString(), "Y");
                addMUNIStateTax(muniSearchescomboBox.Text.ToString(), "N");
                muniStateTaxlabel.Text = "Non Taxable";
            }

        }

        private void MuniStatecheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMUNIState(muniSearchescomboBox.Text.ToString(), MuniStatecheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMuniState(muniSearchescomboBox.Text.ToString(), MuniStatecheckedListBox.Items[e.Index].ToString());
            }

        }

        private void MuniTypecheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMUNIType(muniSearchescomboBox.Text.ToString(), MuniTypecheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMuniType(muniSearchescomboBox.Text.ToString(), MuniTypecheckedListBox.Items[e.Index].ToString());
            }

        }

        private void MuniMoodycheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMuniMoody(muniSearchescomboBox.Text.ToString(), MuniMoodycheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMuniMoody(muniSearchescomboBox.Text.ToString(), MuniMoodycheckedListBox.Items[e.Index].ToString());
            }
        }

        private void MuniSPcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addMuniSP(muniSearchescomboBox.Text.ToString(), MuniSPcheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteMuniSP(muniSearchescomboBox.Text.ToString(), MuniSPcheckedListBox.Items[e.Index].ToString());
            }

        }

        private void MuniStatecheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CORPTickercheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCORP();

            if (CORPTickercheckedListBox.Items.Count == CORPTickercheckedListBox.CheckedItems.Count)
            {
                CorpTickerSelectAllcheckBox.Checked = true;
            }
            else
            {
                CorpTickerSelectAllcheckBox.Checked = false;
            }
        }

        private void pullCORPbutton_Click(object sender, EventArgs e)
        {
            pullCORP();
        }

        private void CORPdealercheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCorpTicker();
            SearchCORP();
            if (CORPdealercheckedListBox.Items.Count == CORPdealercheckedListBox.CheckedItems.Count)
            {
                CORPDealerSelectAllcheckBox.Checked = true;
            }
            else
            {
                CORPDealerSelectAllcheckBox.Checked = false;
            }
        }

        private void CORPDealerSelectAllcheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CORPDealerSelectAllcheckBox_Click(object sender, EventArgs e)
        {
            if (CORPDealerSelectAllcheckBox.Checked)
            {
                for (int x = 0; x < CORPdealercheckedListBox.Items.Count; x++)
                {
                    CORPdealercheckedListBox.SetItemChecked(x, true);
                }

            }
            else
            {
                for (int x = 0; x < CORPdealercheckedListBox.Items.Count; x++)
                {
                    CORPdealercheckedListBox.SetItemChecked(x, false);
                }

            }
			FillCorpTicker();
			SearchCORP();
		}


        private void CorpTickerSelectAllcheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void CorpTickerSelectAllcheckBox_Click(object sender, EventArgs e)
        {
            if (CorpTickerSelectAllcheckBox.Checked)
            {
                for (int x = 0; x < CORPTickercheckedListBox.Items.Count; x++)
                {
                    CORPTickercheckedListBox.SetItemChecked(x, true);
                }

            }
            else
            {
                for (int x = 0; x < CORPTickercheckedListBox.Items.Count; x++)
                {
                    CORPTickercheckedListBox.SetItemChecked(x, false);
                }
            }

			SearchCORP();
		}



    }
}


    




    