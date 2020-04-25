using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace traderTools
{
    public class equity
    {

        public equity()
        {

        }

        public void creatEquityTicket()
        {

            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();

            SqlCommand CMD = new SqlCommand();
            CMD = cn.CreateCommand();
            SqlDataReader RDR;

            //CMD.CommandText = "IF OBJECT_ID('EQUITY_TICKET', 'U') IS NOT NULL DROP TABLE EQUITY_TICKET;";
            CMD.CommandText = "IF OBJECT_ID (N'EQUITY_TICKET',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";

            RDR = CMD.ExecuteReader();
            RDR.Read();

            if (RDR.GetValue(0).ToString() == "0")
            {
                RDR.Close();
                CMD.CommandText = "CREATE TABLE EQUITY_TICKET( [ID][int] IDENTITY(1, 1) NOT NULL, TKT_USER[VARCHAR](255) NOT NULL, " +
                    " TKT_ACCOUNT_ID [INT] NOT NULL, TKT_TICKER[VARCHAR](10) NOT NULL, TKT_DATE[datetime] NOT NULL )";

                CMD.ExecuteNonQuery();
            }

            RDR.Close();
            cn.Close();
        }

        public void createEquityUser()
        {
            string user = Environment.UserName.ToString();

            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = cn.CreateCommand();
            SqlDataReader rdr;

            cmd.CommandText = "IF OBJECT_ID (N'EQUITY_USER',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";

            rdr = cmd.ExecuteReader();
            rdr.Read();
            if (rdr.GetValue(0).ToString()=="0")
            {
                rdr.Close();
                cmd.CommandText = "CREATE TABLE [dbo].[EQUITY_USER]([ID][int] IDENTITY(1, 1) NOT NULL, [USR_ID] [varchar] (255) NOT NULL, " +
                    " [USR_FIRST_NAME][VARCHAR](55) NOT NULL, [USR_LAST_NAME][VARCHAR] (55) NOT NULL, ) ON[PRIMARY]";
                

                cmd.ExecuteNonQuery();
            }
            rdr.Close();
            cn.Close();
        }

        public void createEquityTicketOverview()
        {

            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = cn.CreateCommand();
            SqlDataReader rdr;

            cmd.CommandText = "IF OBJECT_ID (N'EQUITY_TICKET_OVERVIEW',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";

            rdr = cmd.ExecuteReader();
            rdr.Read();

            if (rdr.GetValue(0).ToString()=="0")
            {
                rdr.Close();
                cmd.CommandText = "CREATE TABLE [dbo].[EQUITY_TICKET_OVERVIEW]([ID][int] IDENTITY(1, 1) NOT NULL, " +
                    " [OVR_TKT_ID][INT], [OVR_DATE][datetime]  NOT NULL, " +
                    " [OVR_ACTION] [varchar] (6) NOT NULL, [OVR_NUMBER][INT] NOT NULL, [OVR_PRICE][DECIMAL] (8,4) NOT NULL, ) ON[PRIMARY]";

                cmd.ExecuteNonQuery();
            }

            rdr.Close();
            cn.Close();
        }

        public void createEquityTicketOption()
        {
            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = cn.CreateCommand();
            SqlDataReader rdr;

            cmd.CommandText = "IF OBJECT_ID (N'EQUITY_OPTION_DETAIL',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";

            rdr = cmd.ExecuteReader();
            rdr.Read();

            if (rdr.GetValue(0).ToString()=="0")
            {
                rdr.Close();
                cmd.CommandText = "CREATE TABLE [dbo].[EQUITY_OPTION_DETAIL]([ID][int] IDENTITY(1, 1) NOT NULL, [OPT_TKT_ID][INT] NOT NULL, " +
                    " [OPT_DATE][datetime] NOT NULL, " +
                    " [OPT_ACTION] [varchar] (6) NOT NULL, [OPT_NUMBER][INT] NOT NULL, [OPT_PRICE][DECIMAL] (8,4) NOT NULL, " +
                    " [OPT_STRIKE][DECIMAL] (8,4) NOT NULL, [OPT_EXDATE][DATETIME] NOT NULL) ON[PRIMARY]";

                cmd.ExecuteNonQuery();
            }

            rdr.Close();
            cn.Close();

        }

        public void createEquityAccount()
        {
            
            SqlConnection cn = new SqlConnection("Data Source = ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = cn.CreateCommand();
            SqlDataReader rdr;

            cmd.CommandText = "IF OBJECT_ID (N'EQUITY_ACCOUNT',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";

            rdr = cmd.ExecuteReader();
            rdr.Read();

            if (rdr.GetValue(0).ToString()=="0")
            {
                rdr.Close();
                cmd.CommandText = "CREATE TABLE [dbo].[EQUITY_ACCOUNT]( [ID][int] IDENTITY(1, 1) NOT NULL," +
                    " [ACCT_USER] [varchar] (255) NOT NULL, [ACCT_ACCOUNT] [varchar] (55) NOT NULL, " +
                    " [ACCT_OPEN_DATE] [datetime] NOT NULL, [ACCT_OPENING_BAL] [DECIMAL] (8,2) NOT NULL ) ON[PRIMARY]";

                cmd.ExecuteNonQuery();
            }
            rdr.Close();
            cn.Close();

        }

    }
}
