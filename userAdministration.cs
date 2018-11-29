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
   public partial class userAdministration : Form
   {
      private string uID;
      private string uPwd;
      private bool stp;

      public bool stopGoing
      {
         get
         {
            return stp;
         }
         set
         {
            stp = value;
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

      public string userPassword
      {
         get
         {
            return uPwd;
         }
         set
         {
            uPwd = value;
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

      private string rootConString
      {
         get
         {
            return "SERVER=10.20.0.141;" + "DATABASE=mysql;"
           + "UID=root;PASSWORD=Super.visor;";
         }
      }

      public userAdministration()
      {
         InitializeComponent();
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.Hide();
      }

      private void userProfileGroupBox_Enter(object sender, EventArgs e)
      {
      }

      public void fillClientAssignment()
      {
         int i;
         MySqlConnection cn = new MySqlConnection(MyConString);
         cn.ConnectionString = MyConString;
         cn.CreateCommand();

         MySqlCommand cmd = cn.CreateCommand();
         MySqlDataReader reader;

         cmd.CommandText = "select distinct client_Name from client order by client_name;";
         cn.Open();
         reader = cmd.ExecuteReader();

         

         i = 0;
         while (i < clientAssignmentCheckedListBox.Items.Count)
         {
            clientAssignmentCheckedListBox.Items.RemoveAt(i);
         }

         clientAssignmentCheckedListBox.ClearSelected();
         clientAssignmentCheckedListBox.Items.Add("All Clients");
         while (reader.Read())
         {
            clientAssignmentCheckedListBox.Items.Add(reader.GetValue(0).ToString());
         }
         cn.Close();

      }

      public void fillUserListDropDown()
      {
         MySqlConnection connection = new MySqlConnection(MyConString);
         connection.ConnectionString = MyConString;
         connection.CreateCommand();

         MySqlCommand command = connection.CreateCommand();
         MySqlDataReader Reader;
         command.CommandText = "select user_ID from users";
         connection.Open();
         Reader = command.ExecuteReader();
         userListComboBox.Items.Clear();
         userListComboBox.Text = "";
         userListComboBox.Items.Add("New User");

         userIDTextBox.Text="";
         while (Reader.Read())
         {
            userListComboBox.Items.Add(Reader.GetValue(0).ToString());
         }
         connection.Close();
         try
         {
            userListComboBox.SelectedIndex = 0;
            userRoleComboBox.SelectedIndex = 0;
         }
         catch
         {
            userListComboBox.SelectedIndex = -1;
            userRoleComboBox.SelectedIndex = -1;
        }

         userRoleComboBox.Items.Clear();
         userRoleComboBox.Text = "";
         userRoleComboBox.Items.Add("Analyst");
         userRoleComboBox.Items.Add("Broker");
         userRoleComboBox.Items.Add("Trader");
         userRoleComboBox.Items.Add("Client");
         userRoleComboBox.Items.Add("Administrator");
      }

      public void editUser()
      {
         string sql;
         string uRole;

               switch (userRoleComboBox.Text)
               {
                  case "Analyst":
                     uRole="A";
                     break;
                  case "Broker":
                     uRole="B";
                     break;
                  case "Trader":
                     uRole="T";
                     break;
                  case "Client":
                     uRole="C";
                     break;
                  case "Administrator":
                     uRole="G";
                     break;
                  default:
                     uRole="B";
                     break;
               }

         MySqlConnection cn = new MySqlConnection(MyConString);
         cn.ConnectionString = MyConString;
         cn.Open();

         //check if New User...if yes then add new, if no then update permission
         if (userListComboBox.Text.Equals("New User"))
         {
            sql = "replace into users ( user_ID, user_ABC) values ('" + userIDTextBox.Text 
               + "', '" + uRole + "')";
            
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();

            cn = new MySqlConnection(rootConString);
            cn.ConnectionString = rootConString;
            cn.Open();
            sql = "CREATE USER '" + userIDTextBox.Text + "'@'%' IDENTIFIED BY 'password';";

            cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();

            sql = "GRANT SELECT,INSERT,UPDATE,DELETE,CREATE,DROP ON fig.* TO "
               + "'" + userIDTextBox.Text + "'@'%';";

            cmd = new MySqlCommand(sql, cn); 
            cmd.ExecuteNonQuery();

            cn.Close();
            fillUserListDropDown();
         }
         else
         {
            sql = "update users set user_ABC='" + uRole 
               + "' where user_id='" + userIDTextBox.Text + "'";

            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
         }

      }

      public void dropUser()
      {
         string sql;
         MySqlConnection cn = new MySqlConnection(MyConString);
         cn.ConnectionString = MyConString;
         cn.Open();

            sql = "delete from users where user_ID ='" + userIDTextBox.Text
               + "';";

            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();

            cn = new MySqlConnection(rootConString);
            cn.ConnectionString = rootConString;
            cn.Open();
            sql = "DROP USER '" + userIDTextBox.Text + "'@'%';";

            cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();

            cn.Close();
            fillUserListDropDown();
      }

      private void userListComboBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         fillUserProfile();
         fillClientAssignment();
      }


      private void fillUserProfile()
      {
         if (userListComboBox.Text.Length > 0)
         {
            //int i;
            MySqlConnection connection = new MySqlConnection(MyConString);
            connection.ConnectionString = MyConString;
            connection.CreateCommand();

            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "select user_ID, user_ABC "
               + " from users where user_id='" + userListComboBox.Text + "'";

            connection.Open();
            Reader = command.ExecuteReader();
            userIDTextBox.Text = "";
            while (Reader.Read())
            {
               userIDTextBox.Text = (Reader.GetValue(0).ToString());

               //ComboBox.ObjectCollection itm = userListComboBox.Items;
               //i = 0;
               //while (i < itm.Count)
               //{
               //   MessageBox.Show(itm[i].ToString());
               //   i++;
               //}


               switch (Reader.GetValue(1).ToString())
               {
                  case "G":
                     userRoleComboBox.SelectedIndex = 4;
                     break;
                  case "A":
                     userRoleComboBox.SelectedIndex = 0;
                     break;
                  case "B":
                     userRoleComboBox.SelectedIndex = 1;
                     break;
                  case "T":
                     userRoleComboBox.SelectedIndex = 2;
                     break;
                  case "C":
                     userRoleComboBox.SelectedIndex = 3;
                     break;
                  default:
                     userRoleComboBox.SelectedIndex = -1;
                     break;
               }

            }

            connection.Close();
            if (userListComboBox.Text.Equals("New User"))
            {
               userIDTextBox.Enabled = true;
            }
            else
            {
               userIDTextBox.Enabled = false;
            }

         }
      }

      private void updateUserButton_Click(object sender, EventArgs e)
      {
         editUser();
      }

      private void deleteUserButton_Click(object sender, EventArgs e)
      {
         dropUser();
      }

      private void clientAssignmentCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
      }

      private void clientAssignmentCheckedListBox_SelectedValueChanged(object sender, System.EventArgs e)
      {
      }

      private void clientAssignmentCheckedListBox_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {

         if (stopGoing == true)
            return;
         //MessageBox.Show(e.NewValue.ToString());
         if (e.Index==0)
         {
            int i;
            i = 1;
            if (stopGoing==false)
               stopGoing=true;

            while (i < clientAssignmentCheckedListBox.Items.Count)
            {
               if (e.NewValue.ToString().Equals("Checked"))
               {
                  //clientAssignmentCheckedListBox.SetItemChecked(i, true);
                  clientAssignmentCheckedListBox.SetItemCheckState(i, CheckState.Checked);
               }
               else
               {
                  clientAssignmentCheckedListBox.SetItemChecked(i, false);
               }
               i++;
            }
            stopGoing = false;
         }
         else
         {
            if (e.NewValue.ToString().Equals("Checked"))
            {
               if (stopGoing==false)
                  checkClientForAll(e);
            }
            else
            {
               if (stopGoing == false)
               {
                  stopGoing = true;
                  clientAssignmentCheckedListBox.SetItemChecked(0, false);
                  stopGoing = false;
               }
            }
         }
 
      }

      private void checkClientForAll(System.Windows.Forms.ItemCheckEventArgs e)
      {
         int i;
         bool matchFound;

         matchFound = true;

         i = 1;
         while (i < clientAssignmentCheckedListBox.Items.Count)
         {
            //MessageBox.Show(clientAssignmentCheckedListBox.GetItemCheckState(i).ToString());
            if (i != e.Index)
            {
               if (clientAssignmentCheckedListBox.GetItemChecked(i) == false)
               {
                  matchFound = false;
                  break;
               }
            }

           i++;
         }

         if ( matchFound==true)
            clientAssignmentCheckedListBox.SetItemChecked(0, true);
         
      }

      private void analystCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         checkPermissions();
      }

      private void notesCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         checkPermissions();
      }

      private void clearAssignments()
      {
         int i;

         i = 1;
         while (i < clientAssignmentCheckedListBox.Items.Count)
         {
               clientAssignmentCheckedListBox.SetItemChecked(i,false);
            i++;
         }
         
      }

      private void updatePermissions()
      {
         string sql;
         int i;

         i = 1;

         MySqlConnection cn = new MySqlConnection(MyConString);
         cn.ConnectionString = MyConString;
         cn.Open();

         MySqlCommand cmd = new MySqlCommand();

         while (i < clientAssignmentCheckedListBox.Items.Count)
         {
            if (clientAssignmentCheckedListBox.GetItemChecked(i) == true)
            {
               //grant permissions
               sql = "";

               if (setAnalystCheckBox.Checked == true)
               {
                  sql = "replace into clientassignment values ('" +
                     clientAssignmentCheckedListBox.Items[i].ToString() + "','Analytics','"
                     + userIDTextBox.Text + "')";

                  cmd = new MySqlCommand(sql, cn);
                  cmd.ExecuteNonQuery();
               }
   
               if ( setNotesCheckBox.Checked == true)
               {
                  sql = "replace into clientassignment values ('" +
                     clientAssignmentCheckedListBox.Items[i].ToString() + "','Notes','"
                     + userIDTextBox.Text + "')";

                  cmd = new MySqlCommand(sql, cn);
                  cmd.ExecuteNonQuery();
               }

            }
            else
            {
               //remove permissions
               sql = "";
               if (setAnalystCheckBox.Checked == true)
               {
                  sql = "delete from clientassignment where client_name= '" +
                  clientAssignmentCheckedListBox.Items[i].ToString() + "' and access_type='Analytics' and "
                  + " user_id = '" + userIDTextBox.Text + "'";

                  cmd = new MySqlCommand(sql, cn);
                  cmd.ExecuteNonQuery();
               }

               if (setNotesCheckBox.Checked == true)
               {
                  sql = "delete from clientassignment where client_name= '" +
                  clientAssignmentCheckedListBox.Items[i].ToString() + "' and access_type='Notes' and "
                  + " user_id = '" + userIDTextBox.Text + "'";

                  cmd = new MySqlCommand(sql, cn);
                  cmd.ExecuteNonQuery();
               }

            }


            i++;
         }
         cn.Close();
         
      }


      private void checkPermissions()
      {
         string sql;
         int i;

         clearAssignments();

         MySqlConnection cn = new MySqlConnection(MyConString);
         cn.ConnectionString = MyConString;
         cn.Open();

         MySqlCommand cmd = new MySqlCommand();

         sql = "select distinct client_name from clientassignment where user_id='"
            + userIDTextBox.Text + "' ";

         if (AnalystRadioButton.Checked == true)
            sql += " and access_type = 'Analytics' ";

         if (notesRadioButton.Checked == true)
            sql += " and access_type='Notes' ";

         if (allRadioButton.Checked == true)
         {
            sql = "drop table if exists tmp_" + userID;                        
            cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();

            sql = "create table tmp_" + userID + " as select distinct client_name from "
               + "clientassignment where user_id='"  + userIDTextBox.Text + "' "
               + " and access_type = 'Analytics'";
            cmd = new MySqlCommand(sql, cn);
            cmd.ExecuteNonQuery();

            sql = " select distinct a.client_name from clientassignment a, tmp_" + userID + " b where user_id='" 
               + userIDTextBox.Text + "'  and a.client_name=b.client_name and access_type = 'Notes' ";
         }

         sql += " order by client_name ";

         cn.CreateCommand();

         cmd = cn.CreateCommand();
         MySqlDataReader reader;

         cmd.CommandText=sql;
         reader = cmd.ExecuteReader();

         while (reader.Read())
         {
            i = 1;
            while (i < clientAssignmentCheckedListBox.Items.Count)
            {
               if (clientAssignmentCheckedListBox.Items[i].ToString().Equals(reader.GetValue(0).ToString()))
                  clientAssignmentCheckedListBox.SetItemChecked(i, true);

               i++;
            }
         }
         reader.Close();

         sql = "drop table if exists tmp_" + userID;
         cmd = new MySqlCommand(sql, cn);
         cmd.ExecuteNonQuery();

         cn.Close();
      }

      private void clientAssignmentButton_Click(object sender, EventArgs e)
      {
         updatePermissions();
      }

      private void AnalystRadioButton_CheckedChanged(object sender, EventArgs e)
      {
         checkPermissions();
      }

      private void notesRadioButton_CheckedChanged(object sender, EventArgs e)
      {
         checkPermissions();
      }

      private void allRadioButton_CheckedChanged(object sender, EventArgs e)
      {
         checkPermissions();
      }

   }

}
