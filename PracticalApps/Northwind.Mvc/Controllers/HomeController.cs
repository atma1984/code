using System.Collections.Generic;
using System.Diagnostics;// Activity
using System.IO.Pipelines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; // Controller, IactionResult
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Models; // ErrorViewModel
using Packt.Shared; // NorthwindContext
using static System.Console;


namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext db;

        public HomeController(ILogger<HomeController> logger,NorthwindContext injectedContext)

        {
            _logger = logger;
            db = injectedContext;
        }
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            //_logger.LogError("This is a serious error (not really!)");
            //_logger.LogWarning("This is your first warning!");
            //_logger.LogWarning("Second warning!");
            //_logger.LogInformation("I am in the Index method of the HomeController.");
            HomeIndexViewModel model = new
            (
            VisitorCount: (new Random()).Next(1, 1001),
            Categories: await db.Categories.ToListAsync(),
            Products: await db.Products.ToListAsync()
            );
             return View(model); // �������� ������ �������������
        }

        public IActionResult CategoryDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("You must pass a Category ID in the route, for example, / Home / CategoryDetail / 21");
                
            }
            Category? model = db.Categories
            .SingleOrDefault(p => p.CategoryId == id);
            if (model == null)
            {
                return NotFound($"CategoryId {id} not found.");
            }
            return View(model); // �������� ������ ��� ��������� � ���������� ���������
        }

        //public IActionResult View()
        //{
           
        //    return View(); // �������� ������ �������������
        //}

        [Route("private")]
        [Authorize(Roles = "Administrators")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ModelBinding()
        {
            return View(); // �������� � ������
        }

        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            HomeModelBindingViewModel model = new(
                                                  thing,
                                                  !ModelState.IsValid,
                                                  ModelState.Values
                                                  .SelectMany(state => state.Errors)
                                                  .Select(error => error.ErrorMessage)
                                                  );
            return View(model);
            //return View(thing); // ����������� � ������ ������
        }
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("You must pass a product ID in the route, for example, / Home / ProductDetail / 21");
               
            }
            Product? model = await db.Products
            .SingleOrDefaultAsync(p => p.ProductId == id);
            if (model == null)
            {
                return NotFound($"ProductId {id} not found.");
            }
            return View(model); // �������� ������ ��� ��������� � ���������� ���������
        }

        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {
            if (!price.HasValue)
            {
                return BadRequest("You must pass a product price in the query string, for example, / Home / ProductsThatCostMoreThan ? price = 50");
                
            }

            IEnumerable<Product> model = db.Products
         
            .Include(p => p.Category).Include(p => p.Supplier)
               .Where(p => p.UnitPrice > price);

            if (!model.Any())
            {
                return NotFound(
                $"No products cost more than {price:C}.");
            }
            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model); // �������� ������ �������������
        }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}
