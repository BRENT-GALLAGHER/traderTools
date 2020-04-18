using System;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;
//using System.Data;

using System.Data.SqlClient;

using Microsoft.Office.Interop.Excel;

using System.Collections.Specialized;
using System.Configuration;

namespace traderTools
{
    public sealed class CustomSection:ConfigurationSection
    {
        public CustomSection()
        {

        }

        [ConfigurationProperty("connectionName",DefaultValue ="default",
            IsRequired =true, IsKey = true)]
        public string ConnectionName
        {
            get
            {
                return (string)this["connectionName"];
            }
            set
            {
                this["connectionName"] = value;
            }

        }

        [ConfigurationProperty("provider",DefaultValue ="SQLServer",IsRequired =true)]
        public string provider
        {
            get
            {
                return (string)this["provider"];
            }
            set
            {
                this["provider"] = value;
            }
        }

        [ConfigurationProperty("userID",DefaultValue ="Admin",IsRequired =false)]
        public string UserID
        {
            get
            {
                return (string)this["userID"];
            }
            set
            {
                this["userID"] = value;
            }
        }

        [ConfigurationProperty("password",DefaultValue ="",IsRequired =false)]
        public string password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }

        }

        [ConfigurationProperty("server",DefaultValue="",IsRequired =false)]
        public string server
        {
            get
            {
                return (string)this["server"];
            }
            set
            {
                this["server"] = value;
            }
        }

        [ConfigurationProperty("initialDB",DefaultValue ="FI",IsRequired =false)]
        public string initialDB
        {
            get
            {
                return (string)this["initialDB"];
            }
            set
            {
                this["initialDB"] = value;
            }
        }

        [ConfigurationProperty("windowsAuthentication",DefaultValue =false,IsRequired =false)]
        public Boolean windowsAuthentication
        {
            get
            {
                return (Boolean)this["windowsAuthentication"];
            }
            set
            {
                this["windowsAuthentication"] = value;
            }
        }

    }

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
            checkConfigurationFile();
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
      
      static void checkConfigurationFile()
        {
            try
            {
                CustomSection customSection = new CustomSection();

                System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (config.Sections["DataConnectionsBTG"] == null)
                {
                    customSection.ConnectionName = "Default";
                    customSection.provider = "mySQL";
                    customSection.UserID = "Setup";
                    config.Sections.Add("DataConnectionsBTG", customSection);
                }

                ConnectionStringSettings connStrSettings = new ConnectionStringSettings();

                config.Save(ConfigurationSaveMode.Full);
            }
            catch(ConfigurationErrorsException err)
            {
                MessageBox.Show("CreateConfigFile: {0}", err.ToString());
            }
        }

      public void SetDBSetting()
      {
            // var title = ConfigurationManager.AppSettings["title"];

            // MessageBox.Show(title.ToString());
            // Properties.Settings.Default. = "Trader";

           // CustomSection customSection = new CustomSection();

            var dbSetting = ConfigurationManager.GetSection("dataEnvironment/activeView") as NameValueCollection;
            //var dbSetting = ConfigurationManager.GetSection("BlogGroup/PostSetting") as NameValueCollection;
            if (dbSetting.Count == 0)
            {
                MessageBox.Show("DB Settings are not defined");
            }
            else
            {
                foreach (var key in dbSetting.AllKeys)
                {
                    MessageBox.Show(key + " = " + dbSetting[key]);
                }
            }

        }

        public void logIn()
      {
         string usr = user;
         string pass = password;
         string cnString = getConnectionString();

            Workbook WB = Globals.ThisAddIn.Application.ActiveWorkbook;
            //Workbook WB = Application.ActiveWorkbook;

            // Application.AddIns.Add()

            try
            {
                //// WB.Application.AddIns.Add("I:\\BreanFIGribbon\\FixedIncome.xlam").Installed = true;
               // WB.Application.AddIns.Add("I:\\BreanFIGribbon\\Excel AddIn\\FixedIncome.xlam", true).Installed = true;
				
                //// Globals.ThisAddIn.Application.AddIns.Add("I:\\BreanFIGribbon\\FixedIncome.xlam",true).Installed = true;

            }
            catch (NullReferenceException er)
            {
                Console.WriteLine(er.ToString());
                // MessageBox.Show(er.ToString());
            }

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

            if (isSQLServer == true)
            {
                //SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                //     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");
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
                MySqlConnection cn = new MySqlConnection(cnString);

                try
                {
                    cn.Open();
                    isConnected = true;
                    //cn.Close();
                    cn.CreateCommand();

                    MySqlCommand command = cn.CreateCommand();
                    MySqlDataReader Reader;
                    command.CommandText = "select user_abc from users where user_id='" + user + "'";
                    //cn.Open();
                    Reader = command.ExecuteReader();
                    Reader.Read();

                    userRole = Reader.GetValue(0).ToString();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    isConnected = false;
                    //MessageBox.Show(ex.ToString());
                }
                cn.Close();
            }

         if (isSQLServer == true)
         {
                //SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                //     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");

                SqlConnection cn = new SqlConnection("Data Source=ZM-SQL-1;" +
                     "Initial Catalog=ZM_GALLAGHER; Integrated Security=SSPI;");
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
