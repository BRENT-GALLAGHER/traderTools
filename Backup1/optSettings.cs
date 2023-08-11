using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace traderTools
{
    public partial class OptimizerForm : Form
    {
        List<MyObject> myObjects = new List<MyObject>();

        public OptimizerForm()
        {
            InitializeComponent();
           // CreateMyListView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillOptSettingsListView()
        {
            Workbooks bks = Globals.ThisAddIn.Application.Workbooks;
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;
            int x = 0;
            float fNumber;
            string sParam="";
            
            //ListViewItem itemField = new ListViewItem(col.ToString());
            ListViewItem itemField;

            optSettingsListView.View = View.Details;
            optSettingsListView.AllowColumnReorder = true;

            fNumber = 0;
            sheet = (Worksheet)bk.ActiveSheet;
            for (int col=11; col<=60; col++)
            {
                if ( String.IsNullOrEmpty( sheet.Cells[6, col].Value ))
                {

                }
                else
                {
                    itemField = new ListViewItem(col.ToString());

                    if (String.IsNullOrEmpty(sheet.Cells[6, col].Text))
                    {
                    }
                    else
                    {
                        itemField.SubItems.Add(sheet.Cells[6, col].Text);
                    }


                    if (String.IsNullOrEmpty(sheet.Cells[1, col].Text))
                    {
                    }
                    else
                    {
                        //itemField.SubItems.Add(sheet.Cells[1, col].Text);
                        sParam = sheet.Cells[1, col].Text;
                        if ( sParam.ToUpper().Equals("MIN") )
                        {

                        }
                    }

                    if (String.IsNullOrEmpty(sheet.Cells[2, col].Text))
                    {
                    }
                    else
                    {
                        itemField.SubItems.Add(sheet.Cells[2, col].Text);
                    }

 
                    if (String.IsNullOrEmpty(sheet.Cells[3, col].Text))
                    {
                    }
                    else
                    {
                        itemField.SubItems.Add(sheet.Cells[3, col].Text);
                    }

                    optSettingsListView.Items.Add(itemField);

                    x++;
                }
            }

            
            sheet.Cells[2, 2] = "HELLo";
            sheet.Cells[2, 3] = fNumber;
            
        }
        private void CreateMyListView()
        {
            // Create a new ListView control.
            ListView listView1 = new ListView();
            //listView1.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));

            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("item1", 0);
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("1");
            item1.SubItems.Add("2");
            item1.SubItems.Add("3");
            ListViewItem item2 = new ListViewItem("Value 2", 1);
            item2.SubItems.Add("4");
            item2.SubItems.Add("5");
            item2.SubItems.Add("6");
            ListViewItem item3 = new ListViewItem("item3", 0);
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("7");
            item3.SubItems.Add("8");
            item3.SubItems.Add("9");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

            //Add the items to the ListView.
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

            // Add the ListView to the control collection.
            //this.Controls.Add(listView1);
            //tableLayoutPanel1.Controls.Add(listView1);

           // tableLayoutPanel1.SetRow = 1;
           // tableLayoutPanel1.SetColumn = 2;
            tableLayoutPanel1.Controls.Add(listView1,0,0);

        }

        private void OptimizerForm_Load(object sender, EventArgs e)
        {
            fillOptSettingsListView();
        }

        private void optSettingsListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class MyObject
    {
        public string SumOrAvg { get; set; }
        public List<MyComboBoxObject> ComboBoxData { get; set; }
    }

    public class MyComboBoxObject
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
