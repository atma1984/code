using System.Collections.Generic;
using System.Diagnostics;// Activity
using System.IO.Pipelines;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; // Controller, IactionResult
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Models; // ErrorViewModel
using Packt.Shared; // NorthwindContext
using static System.Console;
using Northwind.Common;


namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext db;
        private readonly IHttpClientFactory clientFactory; 

        public HomeController(ILogger<HomeController> logger,NorthwindContext injectedContext, IHttpClientFactory httpClientFactory)

        {

            _logger = logger;
            db = injectedContext;
            clientFactory = httpClientFactory;
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


            try
            {
                HttpClient client = clientFactory.CreateClient(
                name: "Minimal.WebApi");
                HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: "api/weather");
                HttpResponseMessage response = await client.SendAsync(request);
                ViewData["weather"] = await response.Content
                .ReadFromJsonAsync<WeatherForecast[]>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The Minimal.WebApi service is not responding.Exception: { ex.Message}");
                
                
            ViewData["weather"] = Enumerable.Empty<WeatherForecast>().ToArray();
            }
            return View(model); // �������� ������ �������������
        }

        public async Task<IActionResult> Customers(string country)
        {
            string uri;
            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                uri = "api/customers/";
            }
            else
            {
                ViewData["Title"] = $"Customers in {country}";
                uri = $"api/customers/?country={country}";
            }
            HttpClient client = clientFactory.CreateClient(name: "Northwind.WebApi");
            HttpRequestMessage request = new( method: HttpMethod.Get, requestUri: uri);
           
            HttpResponseMessage response = await client.SendAsync(request);
            IEnumerable<Customer>? Model = await response.Content
            .ReadFromJsonAsync<IEnumerable<Customer>>();
            return View(Model);
        }

        public async Task<IActionResult> CustomersAdd()
        {
        
            ViewData["Title"] = "All Customers Worldwide";

            var Model = await db.Customers.ToListAsync();
           


            return View(Model);
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
