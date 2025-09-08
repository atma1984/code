using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class CustomerinfoModel : PageModel
    {
       
            private NorthwindContext _db;
            public Customer Customer { get; set; }
            public List<Order> Orders { get; set; }

        public CustomerinfoModel(NorthwindContext db)
            {
                _db = db;
            }
            public void OnGet()
            {
            var customerId = Request.Query["id"].ToString();

            Customer = _db.Customers
            .FirstOrDefault(c => c.CustomerId.ToString() == customerId);

            if (Customer != null)
            {
                // Получаем заказы клиента
                Orders = _db.Orders
                    .Where(o => o.CustomerId == Customer.CustomerId)
                    .ToList();
            }

        }
        }
    
}
