using System.Collections.Generic;
using System.Diagnostics;// Activity
using System.Diagnostics.Metrics;
using System.IO.Pipelines;
using System.Net.Http;
using AspNetCoreGeneratedDocument;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; // Controller, IactionResult
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Northwind.Common;
using Northwind.Mvc.Models; // ErrorViewModel
using Packt.Shared; // NorthwindContext
using static System.Console;


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
            return View(model); // передача модели представлению
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
            IEnumerable<Customer>? Model = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
           
            return View(Model);
        }



        public async Task<IActionResult> CreateCustomer(string customer_name,string customer_id)
        {
            string uri;
           
            if (string.IsNullOrEmpty(customer_id))
            {
                //return BadRequest("ѕоле customer_id не может быть пустым");
                return null;
            }
            else
            {
                Customer New_c = new Customer();
                New_c.CustomerId = customer_id.ToUpper();
                New_c.CompanyName = customer_name;
                New_c.ContactName = "Fack";
                ViewData["Title"] = $"Customer is \"{customer_name}\"";
                uri = $"api/customers/?c={New_c}";
                HttpClient client = clientFactory.CreateClient(name: "Northwind.WebApi");
                var content = JsonContent.Create(New_c);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Content = content // ƒобавл€ем контент в запрос (правильный синтаксис)
                };

                    HttpResponseMessage response = await client.SendAsync(request);

              
                    Customer? Model = await response.Content.ReadFromJsonAsync<Customer>();

                    return View(Model);
               
            }
        }


        public async Task<IActionResult> UpdateCustomer(string customer_id,
                                                    string company_name,
                                                    string contact_name,
                                                    string contac_title,
                                                    string phone,
                                                    string city,
                                                    string country)
        {
            string uri;

            if (string.IsNullOrEmpty(customer_id) || string.IsNullOrEmpty(company_name))
            {
                return BadRequest("ѕоле customer_id и company_name не могут быть пустыми");
          
            }
            else
            {
                Customer Update_c = new Customer();
                Update_c.CustomerId = customer_id.ToUpper();
                Update_c.CompanyName = company_name;
                Update_c.ContactName = contact_name;
                Update_c.ContactTitle = contac_title;
                Update_c.Phone = phone;
                Update_c.City = city;
                Update_c.Country = country;              
                ViewData["Title"] = $"Updated company data for \"{company_name}\"";
                uri = $"api/customers/{customer_id}";
                HttpClient client = clientFactory.CreateClient(name: "Northwind.WebApi");
                var content = JsonContent.Create(Update_c);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri)
                {
                    Content = content // ƒобавл€ем контент в запрос (правильный синтаксис)
                };

                HttpResponseMessage response = await client.SendAsync(request);


                // ѕровер€ем, если ответ от API был успешным (например, статус 204)
                if (response.IsSuccessStatusCode)
                {
                    return NoContent();  // ¬озвращаем NoContent, если данных не нужно возвращать
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Response: {response.StatusCode} - {responseContent}");
                    return StatusCode((int)response.StatusCode, $"Failed to update customer. Response: {responseContent}");
                }

            }
        }

        public async Task<IActionResult> FindCustomer(string customer_id)
        {
            string uri;
            if (string.IsNullOrEmpty(customer_id))
            {
                ViewData["Title"] = "Information about All customers";
                uri = "api/customers/";
            }
            else
            {
                ViewData["Title"] = $"Information about the found customer";
                uri = $"api/customers/{customer_id}";
            }
            HttpClient client = clientFactory.CreateClient(name: "Northwind.WebApi");
            HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: uri);

            HttpResponseMessage response = await client.SendAsync(request);
            //var responseContent = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseContent);  // Ћогирование содержимого ответа
            //var responseContent = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseContent);  // Ћогирование содержимого ответа
            Customer? Model = await response.Content.ReadFromJsonAsync<Customer?>();
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
            return View(model); // передаем модель дл€ просмотра и возвращаем результат
        }

        //public IActionResult View()
        //{
           
        //    return View(); // передача модели представлению
        //}

        [Route("private")]
        [Authorize(Roles = "Administrators")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ModelBinding()
        {
            return View(); // страница с формой
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
            //return View(thing); // прив€занный к модели объект
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
            return View(model); // передаем модель дл€ просмотра и возвращаем результат
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
            return View(model); // передача модели представлению
        }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}
