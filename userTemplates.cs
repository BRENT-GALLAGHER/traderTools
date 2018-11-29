using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;


namespace FI_Analytics
{
   class userTemplates
   {
      FIUser crtUser = new FIUser();
     // CortCalc myRibbon = new CortCalc();
      //RibbonDropDown myRibDrop;
      
      string curDir = Directory.GetCurrentDirectory();
      //string[] dirList;
      string[] fileArray;

      private FileStream input;
      private StreamReader fileReader;

      
      private string uID;
      private string uPass;
      private string uRole;
      private bool isConnected;

   
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


      public string templateDirectory
      {
         get
         {
            //return hardCopyReturn;
            return "\\\\denfs\\Groups\\Strategy\\traders\\CMO";
            //return Globals.ThisAddIn.Application._Run2("g_userTemplateDirectory"); uncomment once the mod is added to Cortfig
         }
      }

      public void fillUserTemplates()
      {
         string strVal = "hello";
         Globals.Ribbons.Ribbon1.userTemplateDropDown.Items.Clear();

         RibbonDropDownItem initialItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
         initialItem.Label = "New Template";
         Globals.Ribbons.Ribbon1.userTemplateDropDown.Items.Add(initialItem);

         fileArray = Directory.GetFiles(templateDirectory, "*.xlt*");
         foreach (string myFile in fileArray)
         {
            if (Regex.Match(myFile, @"\W*.xltm").Success || Regex.Match(myFile, @"\W*.xltx").Success)
            {
               strVal = myFile.Substring(myFile.LastIndexOf("\\") + 1, myFile.IndexOf(".xlt") - myFile.LastIndexOf("\\") - 1);
               RibbonDropDownItem myItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
               myItem.Label = strVal;
               Globals.Ribbons.Ribbon1.userTemplateDropDown.Items.Add( myItem );
            }

         }


         Globals.Ribbons.Ribbon1.UserTemplateSheetsDropDown.Items.Clear();

         initialItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
         initialItem.Label = "New Sheet";
         Globals.Ribbons.Ribbon1.UserTemplateSheetsDropDown.Items.Add(initialItem);
         //strVal = "Hello";
         fileArray = Directory.GetFiles(templateDirectory, "*.xml");
         foreach (string myFile in fileArray)
         {
            if (Regex.Match(myFile, @"\W*.xml").Success )
            {
               strVal = myFile.Substring(myFile.LastIndexOf("\\") + 1, myFile.IndexOf(".xml") - myFile.LastIndexOf("\\") - 1);
               RibbonDropDownItem myItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
               myItem.Label = strVal;
               Globals.Ribbons.Ribbon1.UserTemplateSheetsDropDown.Items.Add(myItem);
            }

         }

      }

   }
}
