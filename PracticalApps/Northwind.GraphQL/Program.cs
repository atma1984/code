using GraphQL.Server; // GraphQLOptions
using Northwind.GraphQL; // GreetQuery, NorthwindSchema
using Packt.Shared; // ����� AddNorthwindContext

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5005/");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<NorthwindSchema>();
builder.Services.AddGraphQL()
.AddGraphTypes(typeof(NorthwindSchema), ServiceLifetime.Scoped)
.AddDataLoader()
.AddSystemTextJson(); // �������������� ������� � ������� JSON
builder.Services.AddNorthwindContext();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground(); // ���� �� ���������: /ui/playground
}
app.UseGraphQL<NorthwindSchema>(); // ���� �� ���������: /graphql

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
