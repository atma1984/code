using GraphQL.Server; // GraphQLOptions
using Northwind.GraphQL; // GreetQuery, NorthwindSchema
using Packt.Shared; // метод AddNorthwindContext

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5005/");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<NorthwindSchema>();
builder.Services.AddGraphQL()
.AddGraphTypes(typeof(NorthwindSchema), ServiceLifetime.Scoped)
.AddDataLoader()
.AddSystemTextJson(); // сериализовация ответов в формате JSON
builder.Services.AddNorthwindContext();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground(); // путь по умолчанию: /ui/playground
}
app.UseGraphQL<NorthwindSchema>(); // путь по умолчанию: /graphql

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
