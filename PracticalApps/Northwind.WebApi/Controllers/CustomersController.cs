﻿using Microsoft.AspNetCore.Mvc; // [Route], [ApiController],ControllerBase
using Packt.Shared; // Customer
using Northwind.WebApi.Repositories; // IcustomerRepository
namespace Northwind.WebApi.Controllers;


// базовый адрес: api/customers
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository repo;
    private readonly ILogger<CustomersController> _logger;
    // конструктор внедряет репозиторий, зарегистрированный в Startup
    public CustomersController(ICustomerRepository repo, ILogger<CustomersController> logger)
    {
        this.repo = repo;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            _logger.LogInformation("Received a GET request for customers.");
            return await repo.RetrieveAllAsync();
           
        }
        else
        {
            return (await repo.RetrieveAllAsync())
        .Where(customer => customer.Country == country);
        }
    }
    // GET: api/customers/[id]
    [HttpGet("{id}", Name = nameof(GetCustomer))] // именованный маршрут
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        Customer? c = await repo.RetrieveAsync(id);
        if (c == null)
        {
            return NotFound(); // 404 – ресурс не найден
        }
        return Ok(c); // 200 – OK, с клиентом в теле
    }

    // POST: api/customers
    // BODY: Customer (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer c)
    {
        if (c == null)
        {
            return BadRequest(); // 400 – некорректный запрос
        }
        Customer? addedCustomer = await repo.CreateAsync(c);
        if (addedCustomer == null)
        {
            return BadRequest("Repository failed to create customer.");
        }
        else
        {
            return CreatedAtRoute( // 201 – ресурс создан
            routeName: nameof(GetCustomer),
            routeValues: new { id = addedCustomer.CustomerId.ToLower() },
            value: addedCustomer);
        }
    }

    // PUT: api/customers/[id]
    // BODY: Customer (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] Customer c)
    {
        id = id.ToUpper();
        c.CustomerId = c.CustomerId.ToUpper();
        if (c == null || c.CustomerId != id)
        {
            return BadRequest(); // 400 – некорректный запрос
        }
        Customer? existing = await repo.RetrieveAsync(id);
        if (existing == null)
        {
            return NotFound(); // 404 – ресурс не найден
        }
        await repo.UpdateAsync(id, c);
        return new NoContentResult(); // 204 – контент отсутствует
    }

    // DELETE: api/customers/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == "bad")
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5001/customers/failed-to-delete",
                Title = $"Customer ID {id} found but failed to delete.",
                Detail = "More details like Company Name, Country and so on.",
                Instance = HttpContext.Request.Path
            };
            return BadRequest(problemDetails); // 400 – некорректный запрос
        }


        Customer? existing = await repo.RetrieveAsync(id);
        if (existing == null)
        {
            return NotFound(); // 404 – ресурс не найден
        }
        bool? deleted = await repo.DeleteAsync(id);
        if (deleted.HasValue && deleted.Value) // короткое замыкание AND
        {
            return new NoContentResult(); // 204 – контент отсутствует
        }
        else
        {
            return BadRequest( // 400 – некорректный запрос
            $"Customer {id} was found but failed to delete.");
        }
    }

}