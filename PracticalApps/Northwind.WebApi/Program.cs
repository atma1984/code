using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared; // метод расширения AddNorthwindContext
using static System.Console;
using Northwind.WebApi.Repositories;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI; // SubmitMethod

using Microsoft.AspNetCore.HttpLogging; // HttpLoggingFields


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5002/");
builder.Services.AddNorthwindContext();

//builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Debug);

// Настройка HttpLogging
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All; // Логируем все
    options.RequestBodyLogLimit = 4096;  // Ограничение на тело запроса
    options.ResponseBodyLogLimit = 4096; // Ограничение на тело ответа
});

builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters:");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter == null)
        {
            WriteLine($" {formatter.GetType().Name}");
        }
        else // класс форматера вывода с поддерживаемыми медиаформатами
        {
            WriteLine(" {0}, Media types: {1}",
            arg0: mediaFormatter.GetType().Name,
            arg1: string.Join(", ",
            mediaFormatter.SupportedMediaTypes));
        }
    }
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();
builder.Services.AddCors();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Northwind Service API", Version = "v1" });
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//builder.Logging.AddConsole();  // Добавление вывода в консоль
//builder.Logging.SetMinimumLevel(LogLevel.Information);  // Устанавливаем уровень логирования на Debug
//builder.Configuration.AddJsonFile("appsettings.json");
//builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddHealthChecks().AddDbContextCheck<NorthwindContext>();


// Настроить консольное логирование


var app = builder.Build();

// Конфигурация HTTP-конвейера
app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins(
    "https://localhost:5001" // допускаются запросы от клиента MVC
    );
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Northwind Service API Version 1");
        c.SupportedSubmitMethods(new[] {SubmitMethod.Get, SubmitMethod.Post,SubmitMethod.Put, SubmitMethod.Delete });
    });
}
app.UseMiddleware<SecurityHeaders>();
// Настройка HttpLogging - Важно, чтобы это было до UseRouting
app.UseHttpLogging();  // Логирование должно быть первым middleware

app.UseHttpsRedirection();
app.UseRouting();  // Добавляем маршрутизацию
app.UseAuthorization();


app.UseHealthChecks(path: "/howdoyoufeel");
app.MapControllers();

app.Run();



