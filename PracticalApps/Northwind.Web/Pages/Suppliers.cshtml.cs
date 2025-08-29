using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        private readonly NorthwindContext db;

        public SuppliersModel(NorthwindContext context)
        {
            db = context;
        }

        public IEnumerable<Supplier> Suppliers { get; set; } = Enumerable.Empty<Supplier>();

        [BindProperty]
        public Supplier Supplier { get; set; } = new Supplier();

        public void OnGet()
        {
            Suppliers = db.Suppliers.OrderBy(s => s.Country).ThenBy(s => s.CompanyName).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Suppliers = db.Suppliers.ToList();
                return Page();
            }

            db.Suppliers.Add(Supplier);
            db.SaveChanges();

            return RedirectToPage(); // GET на ту же страницу
        }
    }
}