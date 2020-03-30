using System;
using System.Configuration;

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
         fillDataConnections();
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
         //Save configuration settings
         verifyUser();
      }


      void userPasswordTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyValue == 13)
         {
            verifyUser();
         }

      }

      public void fillDataConnections()
      {

            SavedSettingslistBox.ClearSelected();

            while (SavedSettingslistBox.Items.Count>0)
            {
                SavedSettingslistBox.Items.RemoveAt(0);
            }
            //READ CONFIG FILE AND LOAD CONNECTION NAMES!
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration
                (ConfigurationUserLevel.None) as Configuration;
            CustomSection customSection = new CustomSection();

            customSection = config.GetSection("DataConnections") as CustomSection;
            

            MessageBox.Show(customSection.ConnectionName);
        //QS_swapIDdropDown.Items.Clear();

        //while (QS_swapIDdropDown.Items.Count > 0)
        //    QS_swapIDdropDown.Items.RemoveAt(0);

        //RibbonDropDownItem fstItem = this.Factory.CreateRibbonDropDownItem();
        //fstItem.Label = "Select Swap Name";
        //QS_swapIDdropDown.Items.Add(fstItem);

        //while (Rdr.Read())
        //{
        //    RibbonDropDownItem rbnItem = this.Factory.CreateRibbonDropDownItem();
        //    rbnItem.Label = Rdr.GetValue(0).ToString();
        //    QS_swapIDdropDown.Items.Add(rbnItem);
        //}

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

            crtUser.SetDBSetting();

         if (userID.Equals("") || password.Equals(""))
            {
                crtUser.logIn();
            }
            else
            {
                crtUser.logIn(userIDTextBox.Text.ToString(), userPasswordTextBox.Text.ToString());
            }
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

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SavedSettingslistBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    savedNametextBox.Text = SavedSettingslistBox.Text.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        }

        private void SavedSettingslistBox_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private void SavedSettingslistBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void SavedSettingslistBox_DisplayMemberChanged_1(object sender, EventArgs e)
        {
            MessageBox.Show("HELLO");
        }

        private void SavedSettingslistBox_SelectedValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                savedNametextBox.Text = SavedSettingslistBox.Text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SavedSettingslistBox_Click(object sender, EventArgs e)
        {

        }
    }
}
