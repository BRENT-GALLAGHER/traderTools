using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace traderTools
{
  public class FIUser
   {
      private string uid;
      private string pwd;
      private string uRole;
      private bool isConnected; 
      private string MyCnString = "SERVER=10.20.0.141;" + "DATABASE=FIG;" + "UID=brent;" +
        "PASSWORD=S1m0n3001;";
      private bool isSQLServer;

      public FIUser()
      {
         isSQLServer=true;
      }
       
      public string getConnectionString()
      {
         MyCnString = "SERVER=10.20.0.141;" + "DATABASE=FIG;" + "UID=" + user
             + ";PASSWORD=" + password + ";";

         return MyCnString;
      }

      private string MyConString
      {
         get
         {
            return "SERVER=10.20.0.141;" + "DATABASE=FIG;"
           + "UID=" + user + ";PASSWORD=" + password + ";";
         }
      }

      public bool connectionStatus
      {
         get
         {
            return isConnected;
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

      public string userRole
      {
         get
         {
            return uRole;
         }
         set
         {
            uRole = value;
         }
      }

      public void logIn()
      {
         string usr = user;
         string pass = password;
         string cnString = getConnectionString();

         if (isSQLServer.Equals(false))
         {

             //MySqlConnection cn = new MySqlConnection(cnString);
             //try
             //{
             //    cn.Open();
             //    isConnected = true;
             //}
             //catch (Exception ex)
             //{
             //    isConnected = false;
             //    MessageBox.Show(ex.ToString());
             //}
             //cn.Close();
         }

         if (isSQLServer.Equals(true))
         {
             SqlConnection cn = new SqlConnection();
             cn.Open();
             isConnected = true;
         }

      }

      public void passwordReset( string newPass)
      {

         //MySqlConnection cn = new MySqlConnection(MyConString);
         //try
         //{
         //   cn.Open();
         //   MySqlCommand cmd = new MySqlCommand("set password= password('" + newPass + "');", cn);
         //   cmd.ExecuteNonQuery();

         //}
         //catch
         //{
         //   isConnected = false;
         //}
         //cn.Close();

      }

      public void logIn(string usr, string pass)
      {
         user = usr;
         password = pass;
         string cnString = getConnectionString();

         if (isSQLServer == false)
         {
             //MySqlConnection cn = new MySqlConnection(cnString);

             //try
             //{
             //    cn.Open();
             //    isConnected = true;
             //    //cn.Close();
             //    cn.CreateCommand();

             //    MySqlCommand command = cn.CreateCommand();
             //    MySqlDataReader Reader;
             //    command.CommandText = "select user_abc from users where user_id='" + user + "'";
             //    //cn.Open();
             //    Reader = command.ExecuteReader();
             //    Reader.Read();

             //    userRole = Reader.GetValue(0).ToString();
             //    cn.Close();
             //}
             //catch (Exception ex)
             //{
             //    Console.WriteLine(ex.ToString());
             //    isConnected = false;
             //    //MessageBox.Show(ex.ToString());
             //}
             //cn.Close();
         }

         if (isSQLServer == true)
         {
             SqlConnection cn = new SqlConnection("Data Source=gtfecmsql;" +
                  "Initial Catalog=FicmAnalytic; Integrated Security=SSPI;");
             try
             {
                 cn.Open();
                 isConnected = true;
                 cn.Close();
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.ToString());
                 MessageBox.Show(e.ToString());
                 isConnected = false;
             }
         }

      }

  }
}
