using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;


namespace traderTools
{
    public partial class BWIC : Form
    {
        private string uid;
        private string pwd;
        //SqlDataAdapter SQLdaCriteria;
        //DataSet dsCriteria;
        bool isSQLServer;
        private string curSearch;
        private Boolean isArchived;
        //string[] bwicDates;
        //public List<string> lBWICDates;
        List<string> lBWICDates = new List<string>();
        List<string> lBWICLists = new List<string>();

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

        public Boolean archived
        {
            get
            {
                return isArchived;
            }
            set
            {
                isArchived = value;
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

        public BWIC()
        {
            isSQLServer = true;
            
            InitializeComponent();
        }

        //static void Main()
        //{
        //}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BWIC_Load(object sender, EventArgs e)
        {
            fillBWICdate();
           // fillBWIClist();
        }

        public void fillBWICdate()
        {
            string[] txtFlds;
            txtFlds = new string[2];
            
            if (usingSQLServer ==  true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='BWIC'" +
                    " and criteria_field='BWICID'";

                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                string SQL = "SELECT DATE_ENTERED, COUNT(*) AS CNT FROM BWICINVENTORY " ;

                if (archived == false)
                    SQL += " where da_archived='' ";

                SQL += " GROUP BY DATE_ENTERED ORDER BY DATE_ENTERED DESC;";
                
                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                BWICdatecheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        BWICdatecheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        txtFlds[0] = Rdr.GetValue(0).ToString();
                        txtFlds[1] = Rdr.GetValue(1).ToString();
                        BWICdatecheckedListBox.Items.Add(txtFlds[0] + ":     \t" + txtFlds[1], false);
                    }

                }
                Rdr.Close();

            }

        }

        public void fillBWIClist(String sDate)
        {
            string[] txtFlds;
            txtFlds = new string[2];

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='BWIC'" +
                    " and criteria_field='BWICID'";

                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                string SQL = "SELECT  BWIC_NAME, COUNT(*) AS CNT FROM BWICINVENTORY GROUP BY BWIC_NAME ORDER BY BWIC_NAME ASC;";
                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                BWICListcheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        BWICListcheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        txtFlds[0] = Rdr.GetValue(0).ToString();
                        txtFlds[1] = Rdr.GetValue(1).ToString();
                        BWICListcheckedListBox.Items.Add(txtFlds[0] + ":     \t" + txtFlds[1], false);
                    }

                }
                Rdr.Close();
               
            }

        }

        public void fillBWIClist()
        {
            string[] txtFlds;
            txtFlds = new string[2];

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                cmd.CommandText = "delete from InventoryCriteriaSaved where criteria_sector='BWIC'" +
                    " and criteria_field='BWICID'";

                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                string SQL = "SELECT  BWIC_NAME, COUNT(*) AS CNT FROM BWICINVENTORY GROUP BY BWIC_NAME ORDER BY BWIC_NAME ASC;";
                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();

                BWICListcheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        BWICListcheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        txtFlds[0] = Rdr.GetValue(0).ToString();
                        txtFlds[1] = Rdr.GetValue(1).ToString();
                        BWICListcheckedListBox.Items.Add(txtFlds[0] + ":     \t" + txtFlds[1], false);
                    }

                }
                Rdr.Close();

            }

        }

        private void BWICListcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addBWICList("", BWICListcheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteBWICList("", BWICListcheckedListBox.Items[e.Index].ToString());
            }
        }

        private void deleteBWICList(string savedSearch, string criteria)
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

                string SQL = "delete from InventoryCriteriaSaved where criteria_sector='BWIC' and " +
                    "  criteria_field='BWICID' and criteria_min ='" + criteria + 
                    "' and criteria_descriptor='Text' ";

                //if (MuniradioButtonGroup.Checked == true)
                //{
                //    SQL += " and criteria_searchowner='GROUP';";
                //}
                //else
                //{
                    SQL += " and criteria_searchowner='" + userID + "';";
                //}

                SqlCommand cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

        }

        private void addBWICList(string savedSearch, string criteria)
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

                string SQL = "select count(*) from InventoryCriteriaSaved where criteria_sector='BWIC' and " +
                    "  criteria_field='BWICID' and " +
                    " criteria_min='" + criteria + "' ";

                //if (MuniradioButtonGroup.Checked == true)
                //{
                //    SQL += " and criteria_searchowner='GROUP';";
                //}
                //else
                //{
                    SQL += " and criteria_searchowner='" + userID + "';";
                //}

                cmd = cn.CreateCommand();
                SqlDataReader Rdr;

                cmd.CommandText = SQL;
                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                if (Rdr.GetValue(0).ToString().Equals("0"))
                {
                    Rdr.Close();
                    //if (MuniradioButtonGroup.Checked == true)
                    //{
                    //    cmd.CommandText = "insert into InventoryCriteriaSaved values ('MUNI','" + savedSearch +
                    //    "','State','" + criteria + "', '','Text','GROUP');";
                    //}
                    //else
                    //{
                        cmd.CommandText = "insert into InventoryCriteriaSaved values ('BWIC','','BWICID','" + criteria + "', '','Text','" + userID + "');";
                    //}

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Rdr.Close();
                }

                cn.Close();
            }
        }

        private void addBWICdate( string criteria)
        {
            string SQL;
            string[] txtFlds;
            txtFlds = new string[2];

            if (criteria.IndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.IndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            SQL = "select BWIC_NAME, COUNT(*) AS CNT from BWICinventory where  " +
                " FILE_DATE='" + criteria + "' ";

            if (archived == false)
                SQL += " and da_archived='' ";

            SQL += " group by bwic_name;";

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader rdr;

                cmd.CommandText = SQL;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtFlds[0] = rdr.GetValue(0).ToString();
                    txtFlds[1] = rdr.GetValue(1).ToString();
                    BWICListcheckedListBox.Items.Add(txtFlds[0] + ":     \t" + txtFlds[1], false);
                }
                    rdr.Close();
                
                cn.Close();
            }
        }

        private void deleteBWICdate(string criteria)
        {
            int i;
            string SQL;
            string lstID;
            string[] txtFlds;
            txtFlds = new string[2];
            i = 0;
            lstID = "";

            if (criteria.LastIndexOf(":") > 0)
                criteria = criteria.Substring(0, criteria.LastIndexOf(":"));

            if (usingSQLServer == false)
            {
            }

            SQL = "select BWIC_NAME, COUNT(*) AS CNT from BWICinventory where  " +
                " FILE_DATE='" + criteria + "' group by bwic_name;";

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                    "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd = cn.CreateCommand();
                SqlDataReader rdr;

                cmd.CommandText = SQL;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtFlds[0] = rdr.GetValue(0).ToString();
                    txtFlds[1] = rdr.GetValue(1).ToString();
                    //MessageBox.Show("OUTER:" + txtFlds[0].ToString());
                    i = 0;
                    while (i < BWICListcheckedListBox.Items.Count)
                    {
                        lstID = BWICListcheckedListBox.Items[i].ToString();
                        if (lstID.LastIndexOf(":") > 0)
                            lstID = lstID.Substring(0, lstID.LastIndexOf(":"));

                        if (txtFlds[0].Equals(lstID))
                        {
                            //MessageBox.Show("Match:" + txtFlds[0].ToString());
                            if (BWICListcheckedListBox.GetItemCheckState(i).ToString().Equals("Checked"))
                                deleteBWICList("", BWICListcheckedListBox.Items[i].ToString());
                            BWICListcheckedListBox.Items.RemoveAt(i);
                        }
                        //MessageBox.Show(lstID);
                        //MessageBox.Show(BWICListcheckedListBox.Items[i].ToString());
                        i++;
                    }
                    //BWICListcheckedListBox.Items.Add(txtFlds[0] + ":     \t" + txtFlds[1], false);
                }
                rdr.Close();

                cn.Close();
            }
        }


        private void BWICSearchbutton_Click(object sender, EventArgs e)
        {
            searchBWIC();
        }

        private void searchBWIC()
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

                //SQL = " create table tmp_BWIC_finder as select b.* from BWIClookup a, "
                //   + " muniinventory b where a.id = b.id AND B.SECTOR='MUNI' AND ( ";

                SQL = " create table tmp_BWIC_finder as select * from BWICINVENTORY "
                   + " where  ( ";

            }

            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('tmp_BWIC_finder" + userID + "', 'U') IS NOT NULL " +
                    "DROP TABLE tmp_BWIC_finder" + userID + ";", cn);
                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                cmd.CommandText="update BWICinventory set bid=0 where bid is null;";  
                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                cmd.CommandText="update BWICinventory set disposition='' where disposition is null;";  
                cmd.ExecuteNonQuery();

                cmd = cn.CreateCommand();
                cmd.CommandText = "update BWICinventory set da_color='' where da_color is null;";  
                cmd.ExecuteNonQuery();

                //SQL = " select b.* into tmp_BWIC_finder" + userID + " from munilookup a, "
                //   + " muniinventory b where a.id = b.id  AND B.SECTOR='MUNI' ";

                SQL = " select * into tmp_BWIC_finder" + userID + " from BWICinventory  where  ";

                cmd = cn.CreateCommand();

                //*** READ TMP_CRITERIA + USERID TABLE FOR RANGES
                //MessageBox.Show(MBSSearchescomboBox.Text.ToString());
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='BWIC' and criteria_descriptor='Text' ";

                //if (MuniradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}

                SqlDataReader rdr;
                
                //********* bwicid ******************
                cmd.CommandText = "select criteria_Field, criteria_min, criteria_max from InventoryCriteriaSaved " +
                    " where criteria_sector='BWIC' and criteria_descriptor='Text' " +
                    " and criteria_Field='BWICID' ";

                //if (MuniradioButtonGroup.Checked == true)
                //{
                //    cmd.CommandText += " and criteria_searchowner='GROUP' ;";
                //}
                //else
                //{
                    cmd.CommandText += " and criteria_searchowner='" + userID + "' ;";
                //}

                SQL += " BWIC_NAME IN ( ";

                rdr = cmd.ExecuteReader();                
                    while (rdr.Read())
                    {
                        if (!rdr.GetValue(0).ToString().Equals("Missing"))
                        {
                            SQL += " '" + rdr.GetValue(1).ToString() + "',";
                        }
                        else
                        {
                            SQL += " '',";
                        }
                    }
                    rdr.Close();
                    SQL = SQL.Substring(0, SQL.Length - 1);
                    SQL += ") ";

                    if (SQL.IndexOf("()") > 0)
                        SQL = SQL.Substring(0, SQL.IndexOf("()") - 13) + " 1=2 ";

                    if (archived == false)
                        SQL += " and da_archived='' ";

                SQL += " order by BWIC_NAME;";

                //MessageBox.Show(SQL);
                cmd = new SqlCommand(SQL, cn);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            Globals.ThisAddIn.Application._Run2("OpenTemplate", "tmp_BWIC_finder" + userID, "BWIC");
        }

        private void BWICdatecheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //buildBWICcriteria();
        }

        private void BWICdatecheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //MessageBox.Show(e.NewValue.ToString());
            //MessageBox.Show(BWICdatecheckedListBox.Items[e.Index].ToString() );
            // e.NewValue.ToString().Equals("Checked")
            //buildBWICcriteria();
            if (e.NewValue.ToString().Equals("Checked"))
            {
                addBWICdate(BWICdatecheckedListBox.Items[e.Index].ToString());
            }
            else
            {
                deleteBWICdate(BWICdatecheckedListBox.Items[e.Index].ToString());
            }

        }

        private void buildBWICcriteria()
        {
            string SQL;
            string sPiece;
            int i;

            string[] txtFlds;
            txtFlds = new string[2];

            //*** 

            SQL = "select BWIC_NAME, COUNT(*) AS CNT from BWICinventory where 1=1 ";
            i = 0;
            //BWIC_NAME, COUNT(*) AS CNT FROM BWICINVENTORY GROUP BY BWIC_NAME ORDER BY BWIC_NAME ASC;
            //mbsRangescheckedListBox.Items.Count

            if (BWICdatecheckedListBox.CheckedItems.Count > 0)
            {
                SQL += " and FILE_DATE IN ("; 
                while (i < BWICdatecheckedListBox.Items.Count)
                {
                    if (BWICdatecheckedListBox.GetItemCheckState(i).ToString().Equals("Checked"))
                    {
                        sPiece = BWICdatecheckedListBox.Items[i].ToString();
                        if (sPiece.IndexOf(":") > 0)
                            sPiece = sPiece.Substring(0, sPiece.IndexOf(":"));

                        SQL += "'" + sPiece + "',";
                    }
                    i++;
                }
                SQL = SQL.Substring(0, SQL.Length - 1);
                SQL += ") ";
            }

            SQL += " GROUP BY BWIC_NAME ORDER BY BWIC_NAME ASC;";
            if (usingSQLServer == true)
            {
                SqlConnection cn = new SqlConnection("Data Source=BMC-NY-ZM01;" +
                   "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                cn.Open();

                SqlCommand cmd = new SqlCommand();
                
                cmd = cn.CreateCommand();
                cmd.CommandText = SQL;
                SqlDataReader Rdr;

                Rdr = cmd.ExecuteReader();

                BWICListcheckedListBox.Items.Clear();
                while (Rdr.Read())
                {
                    if (Rdr.GetValue(0).ToString().Equals(""))
                    {
                        BWICListcheckedListBox.Items.Add("Missing", false);
                    }
                    else
                    {
                        txtFlds[0] = Rdr.GetValue(0).ToString();
                        txtFlds[1] = Rdr.GetValue(1).ToString();
                        BWICListcheckedListBox.Items.Add(txtFlds[0] + ":     \t" + txtFlds[1], false);
                    }

                }
                Rdr.Close();

            }            

        }

        private void BWICdatecheckedListBox_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void BWICListcheckedListBox_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("Leave");
        }

        private void BWICarchivedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string strVal;
            int i;

            strVal = "";
            
            //MessageBox.Show(BWICarchivedCheckBox.CheckState.ToString() );
            if (BWICarchivedCheckBox.CheckState.ToString().Equals("Checked"))
            {
                archived = true;
            }
            else
            {
                archived = false;
            }
            //*** READ DATES AND NOTE THE ONES THAT ARE CHECKED
            i = 0;
            while (i < BWICdatecheckedListBox.Items.Count)
            {
                strVal = BWICdatecheckedListBox.Items[i].ToString();
                if (strVal.IndexOf(":") > 0)
                    strVal = strVal.Substring(0, strVal.IndexOf(":"));

                if (BWICdatecheckedListBox.GetItemChecked(i))
                    lBWICDates.Add(strVal);

                i++;
            }

            i = 0;
            while (i < BWICListcheckedListBox.Items.Count)
            {
                strVal = BWICListcheckedListBox.Items[i].ToString();
                if (strVal.IndexOf(":") > 0)
                    strVal = strVal.Substring(0, strVal.IndexOf(":"));

                if (BWICListcheckedListBox.GetItemChecked(i))
                    lBWICLists.Add(strVal);

                i++;
            }

            fillBWICdate();
            BWICListcheckedListBox.Items.Clear();
            //fillBWIClist();
            i = 0;
            while (i < BWICdatecheckedListBox.Items.Count)
            {
                strVal = BWICdatecheckedListBox.Items[i].ToString();
                if (strVal.IndexOf(":") > 0)
                    strVal = strVal.Substring(0, strVal.IndexOf(":"));

                foreach (object element in lBWICDates)
                {
                    //MessageBox.Show(element.ToString());
                    if (strVal.Equals(element.ToString()))
                        BWICdatecheckedListBox.SetItemChecked(i,true);
                }
                i++;
            }

            i = 0;
            while (i < BWICListcheckedListBox.Items.Count)
            {
                strVal = BWICListcheckedListBox.Items[i].ToString();
                if (strVal.IndexOf(":") > 0)
                    strVal = strVal.Substring(0, strVal.IndexOf(":"));

                foreach (object element in lBWICLists)
                {
                    //MessageBox.Show(element.ToString());
                    if (strVal.Equals(element.ToString()))
                        BWICListcheckedListBox.SetItemChecked(i, true);
                }
                i++;
            }

            //foreach (object element in lBWICDates)
            //{
            //    MessageBox.Show(element.ToString());
            //}

            //fillBWICdate();

            lBWICDates.Clear();
            lBWICLists.Clear();

        }

    }
}
