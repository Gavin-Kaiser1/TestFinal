#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestFinal.Data;
using TestFinal.Models;

namespace TestFinal.Pages.Inventorys
{
    public class CreateModel : PageModel
    {
        private readonly TestFinal.Data.TestFinalContext _context;

        public CreateModel(TestFinal.Data.TestFinalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Inventory Inventory { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Inventory.Add(Inventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
