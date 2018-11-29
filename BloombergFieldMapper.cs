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
    public partial class BloombergFieldMapper : Form
    {
        public BloombergFieldMapper()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void mapBlmFieldbutton_Click(object sender, EventArgs e)
        {
            Globals.ThisAddIn.Application._Run2("addMappings", blmSectorcomboBox.Text.ToString() ,blmFieldtextBox.Text,blmLabeltextBox.Text,blmORidestextBox.Text,blmFormattextBox.Text );
        }
    }
}
