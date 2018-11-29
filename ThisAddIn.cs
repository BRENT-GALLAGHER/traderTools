using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
//using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
//using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;

namespace traderTools
{
    public partial class ThisAddIn
    {
        private FixedIncome FI;

        protected override object RequestComAddInAutomationService()
        {
            if (FI == null)
                FI = new FixedIncome();

            return FI;
        }
        
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Workbook WB = Globals.ThisAddIn.Application.ActiveWorkbook;
            
            //Workbook WB = Application.ActiveWorkbook;
           
            // Application.AddIns.Add()
          
            try
            {
               // WB.Application.AddIns.Add("I:\\BreanFIGribbon\\FixedIncome.xlam").Installed = true;
               // WAS GOOD?? WB.Application.AddIns.Add("I:\\BreanFIGribbon\\Excel AddIn\\FixedIncome.xlam", true).Installed = true;


               // Globals.ThisAddIn.Application.AddIns.Add("I:\\BreanFIGribbon\\FixedIncome.xlam",true).Installed = true;

            }
            catch (NullReferenceException er)
            {
                Console.WriteLine(er.ToString());
               // MessageBox.Show(er.ToString());
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
