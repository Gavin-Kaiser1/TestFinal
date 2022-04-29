using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MimeKit;
//using System.Net.Mail;
//mailkit packages
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using TestFinal.Models;
using Microsoft.EntityFrameworkCore;
using TestFinal;
using System.ComponentModel.DataAnnotations;

// think about adding other variables to the email report

namespace TestFinal.Pages.Inventorys
{
    public class Index1Model : PageModel
    {
        
        private readonly TestFinal.Data.TestFinalContext _context;
        
        private readonly IConfiguration Configuration;

        //used for paging and sorting
        public Index1Model(TestFinal.Data.TestFinalContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        //public List<Inventory> InventorysList;
        //public IList<Inventory> Inventorys { get; set; }
       
        
        // button event to create a regular report
        public async void OnGetOnClick()
        {
            //creates the file
            string path = ReportGeneration.RegularReport("RG");
            //acceses the database
            var dbList = from s in _context.Inventory
                         select s;
            //gets the content of the reports
            string[] dbArray = dbList.Select(s => s.Name).ToArray();
            int[] dbIdArray = dbList.Select(s => s.Id).ToArray();
            int[] dbLowArray = dbList.Select(s => s.ItemCount).ToArray();
            int[] dbMinArray = dbList.Select(s => s.MinAlertThresh).ToArray();
            DateTime[] dbDArray = dbList.Select(s => s.TimeArrived).ToArray();
            string title = "Inventory Report: \n";
            //fills the report
            ReportGeneration.FillReport(title, path);
            string invItem;
            for (int i = 0; i < dbArray.Length; i++)
            {
                invItem = "Name: " + dbArray[i] + "\nId number: " + dbIdArray[i] + "\nCurrent inventory count: " + dbLowArray[i] +
                    "\nMinimum Alert: " + dbMinArray[i] + "\nDate Modified: " + dbDArray[i] + "\n\n\n";
                ReportGeneration.FillReport(invItem, path);
            }

            return;
        }
        //button event to create a low inventory report
        public async void OnGetOnClick2()
        {
            //creates the file
            string path = ReportGeneration.RegularReport("Low");
            //acceses the database
            var dbList = from s in _context.Inventory
                         where s.ItemCount <= s.MinAlertThresh
                         select s;
            //gets the content of the reports
            string[] dbArray = dbList.Select(s => s.Name).ToArray();
            int[] dbIdArray = dbList.Select(s => s.Id).ToArray();
            int[] dbLowArray = dbList.Select(s => s.ItemCount).ToArray();
            int[] dbMinArray = dbList.Select(s => s.MinAlertThresh).ToArray();
            DateTime[] dbDArray = dbList.Select(s => s.TimeArrived).ToArray();
            string title = "Inventory Report: \n";
            //fills the report
            ReportGeneration.FillReport(title, path);
            string invItem;
            for (int i = 0; i < dbArray.Length; i++)
            {
                invItem = "Name: " + dbArray[i] + "\nId number: " + dbIdArray[i] + "\nCurrent inventory count: " + dbLowArray[i] +
                    "\nMinimum Alert: " + dbMinArray[i] + "\nDate Modified: " + dbDArray[i] + "\n\n\n";
                ReportGeneration.FillReport(invItem, path);
            }

            
            return;
        }
    }
}

