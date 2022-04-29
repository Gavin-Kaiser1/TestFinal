using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestFinal.Models;

namespace TestFinal.Pages.Inventorys
{
    public class DownloadModel : PageModel
    {
        
        //The list that is searched
        public List<FileModel> Files { get; set; }
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        public DownloadModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            this.Environment = _environment;
           
        }
        //This is the search variable, it is used to search the database
        public string testSe { get; set; }
        
        public async Task OnGetAsync(string testSe)
        {
            //gets all the files in the directory
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Reports/"));

            //Copy File names to Model collection.
            this.Files = new List<FileModel>();
            //the searching logic navigates the file names looking for matches to the search term
            foreach (string filePath in filePaths)
            {
                if (testSe != null)
                {
                    if (filePath.Contains(testSe))
                    {
                        this.Files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
                    }
                }
                else
                {
                    this.Files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
                }
                
            }
        }
        //This is the object that downloads the file
        public FileResult OnGetDownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Reports/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
