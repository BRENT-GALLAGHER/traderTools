using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

//using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using MathNet.Numerics.Providers.LinearAlgebra;
//using MathNet.Numerics.Providers.LinearAlgebra;

namespace traderTools
{
    public class equity
    {
        double cPrice;
        double pPrice;

        public equity()
        {

        }


        public double callPrice
        {
            get
            {
                return cPrice;
            }
            set
            {
                cPrice = value;
            }
        }

        public double putPrice
        {
            get
            {
                return pPrice;
            }
            set
            {
                pPrice = value;
            }
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

        public void BlackScholes(Double price, double optionStrike, int daysToStrike, double volatility, double risklessRate)
        {
            //double optionStrike;
            //int daysToStrike;
            //double volatility;
            //double price;
            //double risklessRate;

            double db1;
            double db2;

            //price = 19.1;
            //daysToStrike = 7;
            //volatility = .93;
            //risklessRate = .001;
            //optionStrike = 19;

            db1 = (Math.Log(price / optionStrike) + (risklessRate + Math.Pow(volatility,2)/2)* Convert.ToDouble(daysToStrike)/365)/(volatility*Math.Sqrt(Convert.ToDouble( daysToStrike)/365));
            db2 = db1 - volatility * (Math.Sqrt(Convert.ToDouble(daysToStrike) / 365));

            var gamma = new Gamma(2.0, 1.5);
            double mean = gamma.Mean;

            var normal = Normal.WithMeanVariance(0, 1);

            callPrice = price * normal.CumulativeDistribution(db1) - optionStrike * Math.Exp(-risklessRate * daysToStrike / 365) 
                * normal.CumulativeDistribution(db2);

            putPrice = normal.CumulativeDistribution(-db2) * optionStrike * Math.Exp(-risklessRate * daysToStrike / 365)
                    - normal.CumulativeDistribution(-db1) * price;
            //return callPrice;
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

        public void createEquityFidelityRaw()
        {
            SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = cn.CreateCommand();
            SqlDataReader rdr;

            cmd.CommandText = "IF OBJECT_ID (N'EQUITY_FIDELITY_RAW',N'U') IS NOT NULL SELECT 1 AS RES ELSE SELECT 0 AS RES;";

            rdr = cmd.ExecuteReader();
            rdr.Read();

            if (rdr.GetValue(0).ToString() == "0")
            {
                rdr.Close();
                cmd.CommandText = "CREATE TABLE [dbo].[EQUITY_FIDELITY_RAW]([ID][int] IDENTITY(1, 1) NOT NULL, [RAW_ACCT_ID] [INT] NOT NULL, [RAW_TKT_ID] [INT] NOT NULL," +
                    " [RAW_SYMBOL][VARCHAR](255), [RAW_DESCRIPTION][VARCHAR](255) NOT NULL, [RAW_QUANTITY] [INT] NOT NULL, [RAW_BASIS_SHARE][DECIMAL](9,3)," +
                    " [RAW_PROCEEDS_SHARE] [DECIMAL] (9,3), [RAW_BASIS][DECIMAL](9,3), [RAW_PROCEEDS][DECIMAL] (9,3) NOT NULL, [RAW_SHORT_GL][DECIMAL](9,3), " +
                    " [RAW_LONG_GL] [DECIMAL](9,3), [RAW_UN_BASIS_SHARE][DECIMAL](9,3), [RAW_UN_BASIS][DECIMAL](9,3), [RAW_UN_SHORT_GL][DECIMAL](9,3), " +
                    " [RAW_UN_LONG_GL][DECIMAL](9,3)) ON[PRIMARY]";

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
