using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace TestFinal.Models
{
    public class FileModel
    {
        //This is just for file names, the actual files are in the wwwroot/reports folder in this project
        public string FileName { get; set; }
    }
}
