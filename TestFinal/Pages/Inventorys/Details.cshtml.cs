#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestFinal.Data;
using TestFinal.Models;

namespace TestFinal.Pages.Inventorys
{
    public class DetailsModel : PageModel
    {
        private readonly TestFinal.Data.TestFinalContext _context;

        public DetailsModel(TestFinal.Data.TestFinalContext context)
        {
            _context = context;
        }

        public Inventory Inventory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inventory = await _context.Inventory.FirstOrDefaultAsync(m => m.Id == id);

            if (Inventory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
