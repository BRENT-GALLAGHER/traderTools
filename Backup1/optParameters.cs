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
    public partial class optParameters : Form
    {
        private string optPField;
        private string optPMin;
        private string optPMax;
        private string optPSumorAvg;
        private int optPMinMaxIndex;
        //optParamMinMaxcomboBox
        private int optPSumAvgIndex;
        //optParamSumorAveragecomboBox
        private int optCol;

        public int parameterCol
        {
            get
            {
                return optCol;
            }
            set
            {
                optCol = value;
            }
        }

        public string optFieldName
        {
            get
            {
                return optPField;
            }
            set
            {
                optPField = value;
            }
        }

        public string optMin
        {
            get
            {
                return optPMin;
            }
            set
            {
                optPMin = value;
            }
        }

        public string optMax
        {
            get
            {
                return optPMax;
            }
            set
            {
                optPMax = value;
            }
        }

        public string optSumorAvg
        {
            get
            {
                return optPSumorAvg;
            }
            set
            {
                optPSumorAvg = value;
            }
        }

        public int optMinMaxIndex
        {
            get
            {
                return optPMinMaxIndex;
            }
            set
            {
                optPMinMaxIndex = value;
            }

        }

        public int optSumAvgIndex
        {
            get
            {
                return optPSumAvgIndex;
            }
            set
            {
                optPSumAvgIndex = value;
            }
        }

        public optParameters()
        {

            InitializeComponent();

        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }

        }

        private void optParameters_Load(object sender, EventArgs e)
        {
            optParamFieldlabel.Text = optFieldName;
            optParamMintextBox.Text = optMin;
            optParamMaximumtextBox.Text = optMax;

            optParamMinMaxcomboBox.SelectedIndex = optMinMaxIndex;
            optParamSumorAveragecomboBox.SelectedIndex = optSumAvgIndex;
       
        }

        private void optParamFieldlabel_Click(object sender, EventArgs e)
        {

        }

        private void fillOptParameter()
        {
            Workbook bk = Globals.ThisAddIn.Application.ActiveWorkbook;
            Worksheet sheet = null;
            sheet = (Worksheet)bk.ActiveSheet;

            optMin = optParamMintextBox.Text;
            optMax = optParamMaximumtextBox.Text;

            if (optParamMinMaxcomboBox.SelectedIndex == 0)
            {
                sheet.Cells[1, parameterCol] = "MIN";
                sheet.Cells[2, parameterCol] = optMin;
            }

            if (optParamMinMaxcomboBox.SelectedIndex == 1)
            {
                sheet.Cells[1, parameterCol] = "MAX";
                sheet.Cells[2, parameterCol] = optMax;
            }

            if (optParamMinMaxcomboBox.SelectedIndex == 2)
            {
                sheet.Cells[1, parameterCol] = "RANGE";
                sheet.Cells[2, parameterCol] = optMin + "|" + optMax;
            }

            if (optParamMinMaxcomboBox.SelectedIndex == 3)
            {
                sheet.Cells[1, parameterCol] = "SIZE";
                if (optMin.Equals(""))
                {
                    sheet.Cells[2, parameterCol] = optMax;
                }
                else
                {
                    if (Convert.ToDouble(optMin) > 0)
                    {
                        sheet.Cells[2, parameterCol] = optMin + "|" + optMax;
                    }
                    else
                    {
                        sheet.Cells[2, parameterCol] = optMax;
                    }
                }
            }

            if (optParamSumorAveragecomboBox.SelectedIndex == 0)
            {
                sheet.Cells[3, parameterCol] = "SUM";
            } else
            {
                sheet.Cells[3, parameterCol] = "AVG";
            }

        }

        private void optPUpdatebutton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(parameterCol.ToString());
            fillOptParameter();
            try
            {
                this.Close();
            }
            catch { }

        }
    }
}
