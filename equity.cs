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
                    " TKT_ACCOUNT [VARCHAR](55) NOT NULL, TKT_TICKER[VARCHAR](10) NOT NULL, TKT_DATE[datetime] NOT NULL )";

                CMD.ExecuteNonQuery();
            }

            RDR.Close();
            cn.Close();
        }

    }
}
