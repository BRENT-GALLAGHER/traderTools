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
    public partial class BloomFieldsAvailable : Form
    {
        public BloomFieldsAvailable()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void blmMappedFieldscomboBox_DragLeave(object sender, EventArgs e)
        {
            MessageBox.Show(blmMappedFieldscomboBox.Text);
        }

        private void blmMappedFieldscomboBox_TextChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(blmMappedFieldscomboBox.Text);
        }

  
    }
}
