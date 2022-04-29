using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestFinal.Models;
//mailkit is for mailing 
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using System.ComponentModel.DataAnnotations;

//The gmail service may not support mailkit at the end of may, look into making another throw away account with a different service
//Need to add seperate classes for email logic to cut down on code re-use

namespace TestFinal.Pages.Inventorys
{
    public class ReportsModel : PageModel
    {
        private readonly TestFinal.Data.TestFinalContext _context;
        private readonly IConfiguration Configuration;

        //used for paging and sorting
        public ReportsModel(TestFinal.Data.TestFinalContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        //variables for type of report, email
        public List<Inventory> InventorysList;
        public IList<Inventory> Inventorys { get; set; }
        //Gets email addressfor first button
        [BindProperty(SupportsGet = true)]
        [EmailAddress(ErrorMessage = "This is not a vail Email Address")]
        public string emailInput { get; set; }
        //gets email address for second button
        [BindProperty(SupportsGet = true)]
        [EmailAddress(ErrorMessage = "This is not a vail Email Address")]
        public string emailInput2 { get; set; }
        
        
        
        //used for making a regular report and emailing it
        public async void OnGetOnClick(string emailInput)
        {
            //creates the report file
            string path = ReportGeneration.RegularReport("RG");

            //imports the database
            var dbList = from s in _context.Inventory
                         select s;
            //Creates variables filled with the report info to add to the file
            string[] dbArray = dbList.Select(s => s.Name).ToArray();
            int[] dbIdArray = dbList.Select(s => s.Id).ToArray();
            int[] dbLowArray = dbList.Select(s => s.ItemCount).ToArray();
            int[] dbMinArray = dbList.Select(s => s.MinAlertThresh).ToArray();
            DateTime[] dbDArray = dbList.Select(s => s.TimeArrived).ToArray();
            string title = "Inventory Report: \n";

            //Fills the report with the text
            ReportGeneration.FillReport(title, path);
            string invItem;
            for (int i = 0; i < dbArray.Length; i++)
            {
                invItem = "Name: " + dbArray[i] + "\nId number: " + dbIdArray[i] + "\nCurrent inventory count: " + dbLowArray[i] +
                    "\nMinimum Alert: " + dbMinArray[i] + "\nDate Modified: " + dbDArray[i] + "\n\n\n";
                ReportGeneration.FillReport(invItem, path);
            }

            //Begining of email portion

            //Builds the body of the email
            var builder = new BodyBuilder();
            builder.TextBody = "The Report is attached";
            builder.Attachments.Add(path);
            //actually creates the email object
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("watermen720@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailInput));
            email.Subject = "Inventory Report";
            email.Body = builder.ToMessageBody();
            

            // send email
            using var smtp = new SmtpClient();
            //connecting to the email service
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("watermen720@gmail.com", "TestPassword123");
            smtp.Send(email);
            smtp.Disconnect(true);

            string content = string.Empty;
            using (var stream = new StreamReader(path))
            {
                content = stream.ReadToEnd();
            }

            return;
        }

        public async void OnGetOnClick2(string emailInput2)
        {
            //creates the report file
            string path = ReportGeneration.RegularReport("Low");
            //imports the database
            var dbList = from s in _context.Inventory
                         where s.ItemCount <= s.MinAlertThresh
                         select s;
            //Creates variables filled with the report info to add to the file
            string[] dbArray = dbList.Select(s => s.Name).ToArray();
            int[] dbIdArray = dbList.Select(s => s.Id).ToArray();
            int[] dbLowArray = dbList.Select(s => s.ItemCount).ToArray();
            int[] dbMinArray = dbList.Select(s => s.MinAlertThresh).ToArray();
            DateTime[] dbDArray = dbList.Select(s => s.TimeArrived).ToArray();
            string title = "Inventory Report: \n";
            //Fills the report with the text
            ReportGeneration.FillReport(title, path);
            string invItem;
            for (int i = 0; i < dbArray.Length; i++)
            {
                invItem = "Name: " + dbArray[i] + "\nId number: " + dbIdArray[i] + "\nCurrent inventory count: " + dbLowArray[i] +
                    "\nMinimum Alert: " + dbMinArray[i] + "\nDate Modified: " + dbDArray[i] + "\n\n\n";
                ReportGeneration.FillReport(invItem, path);
            }

            //Begining of email portion

            //Builds the body of the email
            var builder = new BodyBuilder();
            builder.TextBody = "The Report is attached";
            builder.Attachments.Add(path);

            //actually creates the email
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("watermen720@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailInput2));
            email.Subject = "Low Inventory Rport";
            email.Body = builder.ToMessageBody();
            

            // send email
            using var smtp = new SmtpClient();
            //connecting to the email service
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("watermen720@gmail.com", "TestPassword123");
            smtp.Send(email);
            smtp.Disconnect(true);
            return;
        }

    }
    
}
