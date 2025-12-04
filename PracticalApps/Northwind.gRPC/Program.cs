using Microsoft.Extensions.Logging;
using Northwind.gRPC.Services;
using Packt.Shared; // метод расширения AddNorthwindContext

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders(); // Очистить все провайдеры логирования по умолчанию
builder.Logging.AddConsole(); // Добавить вывод в консоль
builder.Logging.AddDebug();
builder.Logging.AddFile("Logs/myapp-{Date}.txt");
builder.WebHost.UseUrls("https://localhost:5006/");
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddNorthwindContext();
var app = builder.Build();

app.UseRouting(); // Это необходимо для правильной маршрутизации запросов

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ShipperService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
