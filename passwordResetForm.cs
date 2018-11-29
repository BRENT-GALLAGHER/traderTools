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

namespace FI_Analytics
{
   public partial class passwordResetForm : Form
   {
      private string uid;
      private string pwd;

      public string userID
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

      public passwordResetForm()
      {
         InitializeComponent();
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.Hide();
      }

      private void passwordChange()
      {
         if ( oldPassTextBox.Text.Equals(userPassword))
         {
            // OLD MATCHES EXISTING SO NOW CHECK NEW AND CONFIRM
            if ( newPassTextBox.Text.Equals(confirmPassTextBox.Text) )
            {
               //all good reset password!
               //update mySQL database!
               string sql;

               MySqlConnection connection = new MySqlConnection(MyConString);
               connection.ConnectionString = MyConString;
               connection.Open();

               sql = "set password=password('" + newPassTextBox.Text.ToString() 
                  + "'); ";

               MySqlCommand cmd = new MySqlCommand(sql, connection);
               cmd.ExecuteNonQuery();
               connection.Close();

            }

         }

      }

      private void resetPassButton_Click(object sender, EventArgs e)
      {
         passwordChange();
      }


   }
}
