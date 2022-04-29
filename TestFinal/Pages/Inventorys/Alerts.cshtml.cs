using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestFinal.Models;


namespace TestFinal.Pages.Inventorys
{
    public class AlertsModel : PageModel
    {
        //used for sorting
        private readonly TestFinal.Data.TestFinalContext _context;
        // used for paging
        private readonly IConfiguration Configuration;

        //used for paging and sorting
        public AlertsModel(TestFinal.Data.TestFinalContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        //sort vars and filter variables declaired 
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
       
        //page var
        public PaginatedList<Inventory> InventorysList;
        [BindProperty(SupportsGet = true)]
        public IList<Inventory> Inventorys { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, string CurrentFilter, int sortNum, int? pageIndex, int MinThresh, int CurrentInv)
        {
            
            //paging 
            CurrentSort = sortOrder;
            
            //paging logic
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }
            //
            CurrentFilter = searchString;

            //calling the data from the database
            IQueryable<Inventory> searchTool = from s in _context.Inventory
                                               where s.ItemCount <= s.MinAlertThresh
                                               select s;
            //sorting logic
            //uses the sql query above to search through 
            if (!String.IsNullOrEmpty(searchString))
            {
                searchTool = searchTool.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                default:
                    searchTool = searchTool.OrderBy(s => (s.ItemCount >= s.MinAlertThresh));
                    break;
            }

            //is needed to handle pagging
            //sends the sort data to the page
            var pageSize = Configuration.GetValue("PageSize", 4);
            InventorysList = await PaginatedList<Inventory>.CreateAsync(
                searchTool.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        
        }
}
