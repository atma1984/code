
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;
using static System.Console;
using static System.IO.Path;
using static System.Environment;



WriteLine("This is all categories from base ");
WriteLine(new string('-', 40));
var categories = Get_All_Categories();

// JSON (минимизированный + без null-свойств)
var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};
byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(categories, jsonOptions);
WriteLine("This is automatic serrealisation Json");
WriteLine($"JSON bytes: {jsonBytes.Length}");

WriteLine("This is manual serrealisation Json");

string jsonFile = "categories-and-products.json";
//Создаем Filestream - поток для записи в файл 
using (FileStream jsonStream = File.Create(Combine(CurrentDirectory, jsonFile)))
{
    using (Utf8JsonWriter json = new Utf8JsonWriter(jsonStream, new JsonWriterOptions { Indented=true })) 
    {
       json.WriteStartObject(); // start 1
        json.WriteStartArray("categories"); //start categories
        foreach (var category in categories)
        {
            json.WriteStartObject(); // start 2 category
            json.WriteNumber("Category_ID", category.CategoryId);
            json.WriteString("Category_Name", category.CategoryName);
            json.WriteString("Description_Name", category.Description);
            json.WriteNumber("Products_Counte", category.Products.Count);

            json.WriteStartArray("Products");
            foreach (var p in category.Products)
            {
                json.WriteStartObject();
                json.WriteNumber("Product_ID", p.ProductId);
                json.WriteString("Product_name", p.ProductName);
                json.WriteNumber("Product_cost", p.Cost is null ? 0 : p.Cost.Value);
                json.WriteEndObject();

            }
            json.WriteEndArray(); // Products
            json.WriteEndObject(); // end 2  category

        }
        json.WriteEndArray(); // end categories
        json.WriteEndObject();// end 1
    }
}


    static Category[] Get_All_Categories()
{
    using (Northwind d_base = new()) 
    {
        IQueryable<Category> categories;
        categories = d_base.Categories?.Include(c => c.Products); ;

        foreach (Category c in categories)
        {

            foreach (Product p in c.Products) 
            {
                WriteLine($" {c.CategoryName}, {c.CategoryId}, {p.ProductName},{p.ProductId} ");

            }
          
        }

        return categories.ToArray();
    }
}

