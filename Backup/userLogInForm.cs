using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace traderTools
{
   
   public partial class userLogInForm : Form
   {
      
      FIUser crtUser = new FIUser();
      
      private string uID;
      private string uPass;
      private string uRole;
      private bool isConnected;

      public userLogInForm()
      {
         InitializeComponent();
      }

      public bool userConnected
      {
         get
         {
            return isConnected;
         }
         set
         {
            isConnected = value;
         }
      }

      public string userID
      {
         get
         {
            return uID;
         }
         set
         {
            uID = value;
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

      public string password
      {
         get
         {
            return uPass;
         }
         set
         {
            uPass = value;
         }
      }

      private string MyConString
      {
         get
         {
            return "SERVER=10.20.0.141;" + "DATABASE=FIG;"
           + "UID=" + userID + ";PASSWORD=" + password + ";";
         }
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.Hide();
      }

      private void userLogInForm_Load(object sender, EventArgs e)
      {

      }

      private void logInButton_Click(object sender, EventArgs e)
      {
         
         verifyUser();
      }


      void userPasswordTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyValue == 13)
         {
            verifyUser();
         }

      }

      public bool testRun()
      {
         this.Show();
         return true;
      }

      public void verifyUser()
      {
         userID = userIDTextBox.Text.ToString();
         password = userPasswordTextBox.Text.ToString();
          crtUser.logIn(userIDTextBox.Text.ToString(), userPasswordTextBox.Text.ToString());
         if (crtUser.connectionStatus == true)
         {
            userConnected = true;
            userRole = crtUser.userRole;
            this.Hide();
         }
         else
         {
            userConnected = false;
            MessageBox.Show("Login Failed!", "Login Failure");
         }
         Globals.Ribbons.Ribbon1.enableButtons();

      }

      void userLogInForm_Deactivate(object sender, System.EventArgs e)
      {
         this.Hide();
      }

      private void userPasswordTextBox_TextChanged(object sender, EventArgs e)
      {

      }

      private void userPasswordTextBox_GotFocus(object sender, System.EventArgs e)
      {
         userPasswordTextBox.Clear();
      }

   }
}
