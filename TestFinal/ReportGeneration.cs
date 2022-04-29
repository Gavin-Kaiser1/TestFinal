

namespace TestFinal.Pages.Inventorys
{
    public class ReportGeneration
    {
        //Method for creating a report 
        public static string RegularReport(string reportType)
        {
            //Gets the date and time to use for report naming
            DateTime localDate = DateTime.Now;
            //generates the new report path/name
            string path = @"wwwroot\Reports\" + reportType + localDate.ToString("MM-dd-yyyy-HH-mm-ss") + ".txt";
            
            //creates the file if it doesnt exist
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {

                }
                
            }
            return path;

        }
        //Fills the document created earlier
        //needs the file path and content
        public static void FillReport(string info, string path)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = File.AppendText(path))
                {

                    sw.WriteLine(info);
                }
            }
        }
    }
}
