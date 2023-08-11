using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Security.Permissions;

namespace traderTools
{
    public partial class PortfolioAnalysis : Form
    {

        private string uid;
        //private string pwd;
        //MySqlDataAdapter daCriteria;
       // SqlDataAdapter SQLdaCriteria;
       // DataSet dsCriteria;
        bool isSQLServer;
 
        public string userID
        {
            get
            {
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
        public PortfolioAnalysis()
        {
            InitializeComponent();
        }

        private void buttonImportPortDetail_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV Files|*.CSV";
            openFileDialog1.Title = "Select a CSV File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //System.IO.StreamReader sr = new
                //System.IO.StreamReader(openFileDialog1.FileName);
                ////MessageBox.Show(sr.ReadToEnd());
                //MessageBox.Show(sr.ReadLine());
                //sr.Close();

                Globals.ThisAddIn.Application._Run2("importDetailToDB", openFileDialog1.FileName);

                //MessageBox.Show( openFileDialog1.FileName );
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonImportPortfolioCash_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Globals.ThisAddIn.Application._Run2("importCashToDB", openFileDialog1.FileName);
                //MessageBox.Show( openFileDialog1.FileName );
            }

        }
    }
}
