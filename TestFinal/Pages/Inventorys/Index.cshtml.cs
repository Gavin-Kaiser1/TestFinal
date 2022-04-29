#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestFinal.Models;

//This is the base index page for the entire inventory
//This is the temporary landing page


namespace TestFinal.Pages.Inventorys
{

    public class IndexModel : PageModel
    {
        
        private readonly TestFinal.Data.TestFinalContext _context;
        
        private readonly IConfiguration Configuration;

        //used for paging and sorting
        public IndexModel(TestFinal.Data.TestFinalContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        //sort vars
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string WeightSort { get; set; }

        //page var
        public PaginatedList<Inventory> InventorysList;

        

        public async Task OnGetAsync(string sortOrder, string searchString, string CurrentFilter, int sortNum, int? pageIndex)
        {

            //paging 
            CurrentSort = sortOrder;
            //Use LINQ to get list of Names.
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            WeightSort = sortOrder == "Int" ? "weight" : "weight_desc";

            //paging logic
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }

            CurrentFilter = searchString;

            //getting the database
            IQueryable<Inventory> searchTool = from s in _context.Inventory
                                               select s;
            //sorting logic
            if (!String.IsNullOrEmpty(searchString))
            {
                searchTool = searchTool.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    searchTool = searchTool.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    searchTool = searchTool.OrderBy(s => s.TimeArrived);
                    break;
                case "date_desc":
                    searchTool = searchTool.OrderByDescending(s => s.TimeArrived);
                    break;
                case "weight":
                    searchTool = searchTool.OrderBy(s => s.Weight);
                    break;
                case "weight_desc":
                    searchTool = searchTool.OrderByDescending(s => s.Weight);
                    break;
                default:
                    searchTool = searchTool.OrderBy(s => s.Name);
                    break;
            }
            //paging
            //sends the sort data to the page
            var pageSize = Configuration.GetValue("PageSize", 4);
            InventorysList = await PaginatedList<Inventory>.CreateAsync(
                searchTool.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
