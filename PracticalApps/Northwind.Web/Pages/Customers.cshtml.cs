using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class CustomersModel : PageModel
    {
        private NorthwindContext _db;
        public IEnumerable<Customer>? Customers { get; set; }

        public CustomersModel(NorthwindContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Customers = _db.Customers.OrderBy(c => c.CompanyName).ToList();
        }
    }
}
