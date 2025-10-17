using Microsoft.AspNetCore.OData; // метод расширения AddOData
using Microsoft.OData.Edm; // IEdmModel
using Microsoft.OData.ModelBuilder; // ODataConventionModelBuilder
using Packt.Shared; // NorthwindContext и модели сущностей

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5004");
// Add services to the container.
builder.Services.AddNorthwindContext();
builder.Services.AddControllers().AddOData(options => options
                                 // регистрация моделей OData различных версий
                                 .AddRouteComponents(routePrefix: "catalog",
                                 model: GetEdmModelForCatalog())

                                 .AddRouteComponents(routePrefix: "ordersystem",
                                 model: GetEdmModelForOrderSystem())
                                 // включение параметра запросов
                                 .Select() // включение $select для проекции
                                 .Expand() // включение $expand для навигации по сущностям
                                 .Filter() // включение $filter
                                 .OrderBy() // включение $orderby
                                 .SetMaxTop(100) // включение $top
                                 .Count() // включение $count
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


IEdmModel GetEdmModelForCatalog()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Category>("Categories");
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Supplier>("Suppliers");
    return builder.GetEdmModel();
}
IEdmModel GetEdmModelForOrderSystem()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<Order>("Orders");
    builder.EntitySet<Employee>("Employees");
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Shipper>("Shippers");
    return builder.GetEdmModel();
}
