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
    public class DeleteModel : PageModel
    {
        private readonly TestFinal.Data.TestFinalContext _context;

        public DeleteModel(TestFinal.Data.TestFinalContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inventory = await _context.Inventory.FindAsync(id);

            if (Inventory != null)
            {
                _context.Inventory.Remove(Inventory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
