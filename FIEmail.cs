using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Net.Security;
//using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace FI_Analytics
{
   class FIEmail
   {
      public void sendEMailThroughOUTLOOK(string e_ToList, string e_Subject, string e_Msg)
      {
         try
         {
            string[] e_recipients;
            // Create the Outlook application.
            Outlook.Application oApp = new Outlook.Application();
            // Create a new mail item.
            Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
            // Set HTMLBody. 
            //add the body of the email
            oMsg.HTMLBody = e_Msg;
            ////Add an attachment.
            // String sDisplayName = "MyAttachment";
            // int iPosition = (int)oMsg.Body.Length + 1;
            // int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
            ////now attached the file
            // Outlook.Attachment oAttach = oMsg.Attachments.Add(@"C:\\batchStart.txt", iAttachType, iPosition, sDisplayName);
            //Subject line
            oMsg.Subject = e_Subject;
            // Add a recipient.
            Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
            // Change the recipient in the next line if necessary.

            //Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(e_ToList);
            //Outlook.Recipient oRecip2 = (Outlook.Recipient)oRecips.Add("brent.gallagher@ymail.com");

            e_ToList = Regex.Replace(e_ToList, ";", ",");
            e_recipients = Regex.Split(e_ToList, ",");

            foreach (string rString in e_recipients)
               oRecips.Add(rString);

            //oRecips.Add(e_ToList);
            //oRecips.Add("brent.gallagher@ymail.com");

            //oRecip.Resolve();
            // Send.
            oMsg.Send();
            // Clean up.
            //oRecip = null;
            oRecips = null;
            oMsg = null;
            oApp = null;
         }//end of try block
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message);
         }//end of catch
      }//end of Email Method


      public void sendEMailThroughOUTLOOK(string e_ToList, string e_Subject, string e_Msg, string e_attch)
      {
         try
         {
            string[] e_recipients;

            // Create the Outlook application.
            Outlook.Application oApp = new Outlook.Application();
            // Create a new mail item.
            Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
            // Set HTMLBody. 
            //add the body of the email
            oMsg.HTMLBody = e_Msg;
            //Add an attachment.
            String sDisplayName = "Client Attachment";
            int iPosition = (int)oMsg.Body.Length + 1;
            int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
            //now attached the file
            Outlook.Attachment oAttach = oMsg.Attachments.Add(@e_attch, iAttachType, iPosition, sDisplayName);
            //Subject line
            oMsg.Subject = e_Subject;
            // Add a recipient.
            Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;

            e_ToList = Regex.Replace(e_ToList, ";", ",");
            e_recipients = Regex.Split(e_ToList, ",");

            foreach (string rString in e_recipients)
               oRecips.Add(rString);

            // Send.
            oMsg.Send();
            // Clean up.
            oRecips = null;
            oMsg = null;
            oApp = null;
         }//end of try block
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);

         }//end of catch
      }//end of Email Method


      public void sendEmail2()
      {
         var from = new MailAddress("bgallagher@dadco.com", "Me");
         var to = new MailAddress("ezra_inez@yahoo.com", "You");

         var message = new MailMessage(from, to)
         {
            Subject = "Greetings!",
            Body = "How are you doing today?",
         };

         //var client = new SmtpClient("CCH-2K3E1.CCH.Local");

         var client = new SmtpClient("10.2.30.15")
         {
            //EnableSsl =true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential
            {
               UserName = "bgallagher",
               Password = "1234F!v3",
            },
         }; using (client)
         {
            try
            {
               client.Send(message);
            }
            catch (SmtpException e)
            {
               Console.WriteLine(e.Message);
               MessageBox.Show(e.ToString());

               MessageBox.Show(e.Message);
            }
         }
      }

      public void sendEmail()
      {
        // string fromEmail = "bgallagher@dadco.com";//sending email from...
        // string ToEmail = "ezra_inez@yahoo.com";//destination email	 ezra_inez@yahoo.com           
        // string body = "This is the body!";
        // string subject = "Test email II";

         MailMessage mail = new MailMessage();
         mail.From = new MailAddress("bgallagher@dadco.com");
         mail.To.Add("ezra_inez@yahoo.com");

         //set the content
         mail.Subject = "This is an email";
         mail.Body = "this is another email.";
         try
         {
            //SmtpClient sMail = new SmtpClient("post.cortcap.com");//exchange or smtp server goes here.
            SmtpClient sMail = new SmtpClient("10.2.30.15");//exchange or smtp server goes here.
            sMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            sMail.Credentials = new NetworkCredential("bgallagher", "1234F!v3");//this line most likely wont be needed if you are already an authenticated user.
            //sMail.Send(fromEmail,ToEmail, subject, body);
            sMail.Send(mail);
         }
         catch (Exception ex)
         {
            //do something after error if there is one
            MessageBox.Show(ex.ToString());
            MessageBox.Show(ex.Message);
         }
      }

   }
}
