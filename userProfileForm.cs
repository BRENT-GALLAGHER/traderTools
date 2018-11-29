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
   public partial class userProfileForm : Form
   {
      private string uid;
      private string pwd;

      public string userId
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
           + "UID=" + userId + ";PASSWORD=" + userPassword + ";";
         }
      }

      public userProfileForm()
      {
         InitializeComponent();
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.Hide();
      }

      private void userProfileForm_Load(object sender, EventArgs e)
      {
         fillUserProfile();
      }

      private void fillUserProfile()
      {
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.CreateCommand();

         MySqlCommand command = connection.CreateCommand();
         MySqlDataReader Reader;
         command.CommandText = "select user_Name, user_Phone, user_mobile, "
            + " user_Address, user_City, user_State, user_ZIP, user_Email "
            + " from users where user_id='" + userId + "'";
         connection.Open();
         Reader = command.ExecuteReader();
         Reader.Read();

         userNameTextBox.Text = (Reader.GetValue(0).ToString());
         userPrimaryPhoneTextBox.Text = Reader.GetValue(1).ToString();
         userSecondaryPhoneTextBox.Text = Reader.GetValue(2).ToString();
         userAddressTextBox.Text = Reader.GetValue(3).ToString();
         userCityTextBox.Text = Reader.GetValue(4).ToString();
         userStateTextBox.Text = Reader.GetValue(5).ToString();
         userZipTextBox.Text = Reader.GetValue(6).ToString();
         userEmailTextBox.Text = Reader.GetValue(7).ToString();

         connection.Close();

      }

      private void userCancelButton_Click(object sender, EventArgs e)
      {
         fillUserProfile();
      }

      private void updateUser()
      {
         string sql;

         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.Open();

         sql = "update users set user_Name='" + userNameTextBox.Text.ToString() 
            + "', user_Phone = '" + userPrimaryPhoneTextBox.Text.ToString() 
            + "' , user_mobile ='" + userSecondaryPhoneTextBox.Text.ToString()
            + "', user_Address='" + userAddressTextBox.Text.ToString() + "', user_City='"
            + userCityTextBox.Text.ToString() + "' , user_State='" 
            + userStateTextBox.Text.ToString() + "', user_ZIP='"
            + userZipTextBox.Text.ToString() + "', user_Email='"
            + userEmailTextBox.Text.ToString() + "' where user_id='" + userId + "'";

         MySqlCommand cmd = new MySqlCommand(sql, connection);
         cmd.ExecuteNonQuery();
         connection.Close();
      }

      private void userUpdateButton_Click(object sender, EventArgs e)
      {
         updateUser();
      }
   }
}
