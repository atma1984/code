using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared; // ����� ���������� AddNorthwindContext
using static System.Console;
using Northwind.WebApi.Repositories;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI; // SubmitMethod

using Microsoft.AspNetCore.HttpLogging; // HttpLoggingFields


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5002/");
builder.Services.AddNorthwindContext();

//builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Debug);

// ��������� HttpLogging
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All; // �������� ���
    options.RequestBodyLogLimit = 4096;  // ����������� �� ���� �������
    options.ResponseBodyLogLimit = 4096; // ����������� �� ���� ������
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
        else // ����� ��������� ������ � ��������������� ��������������
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

//builder.Logging.AddConsole();  // ���������� ������ � �������
//builder.Logging.SetMinimumLevel(LogLevel.Information);  // ������������� ������� ����������� �� Debug
//builder.Configuration.AddJsonFile("appsettings.json");
//builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddHealthChecks().AddDbContextCheck<NorthwindContext>();


// ��������� ���������� �����������


var app = builder.Build();

// ������������ HTTP-���������
app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins(
    "https://localhost:5001" // ����������� ������� �� ������� MVC
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
// ��������� HttpLogging - �����, ����� ��� ���� �� UseRouting
app.UseHttpLogging();  // ����������� ������ ���� ������ middleware

app.UseHttpsRedirection();
app.UseRouting();  // ��������� �������������
app.UseAuthorization();


app.UseHealthChecks(path: "/howdoyoufeel");
app.MapControllers();

app.Run();



