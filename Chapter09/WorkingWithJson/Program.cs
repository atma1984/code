
using System.ComponentModel;
using System.Text.Json; // класс JsonSerializer
using System.Text.Json.Serialization; // атрибут [JsonInclude]
using static System.Console;
using static System.Environment;
using static System.IO.Path;



Book csharp10 = new(title:
"C# 10 and .NET 6 - Modern Cross-platform Development")
{
    Author = "Mark J Price",
    PublishDate = new(year: 2021, month: 11, day: 9),
    Pages = 823,
    Created = DateTimeOffset.UtcNow,
};
JsonSerializerOptions options = new()
{
    //IncludeFields = false, // включает в себя все поля
    //PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};
//options.Converters.Add(new DateOnlyConverter());

string filePath = Combine(CurrentDirectory, "book.json");
using (Stream fileStream = File.Create(filePath))
{
    JsonSerializer.Serialize<Book>(
    utf8Json: fileStream, value: csharp10, options);
}
WriteLine("Written {0:N0} bytes of JSON to {1}",
arg0: new FileInfo(filePath).Length,
arg1: filePath);
WriteLine();
// отображаем сериализованный граф объектов
WriteLine(File.ReadAllText(filePath));

public class Book
{
    // конструктор для установки свойства, не допускающего null
    public Book(string title)
    {
        Title = title;
    }
    // свойства
    public string Title { get; set; }
    public string? Author { get; set; }
    // поля
    //[JsonInclude] // включаем это поле
    public DateOnly PublishDate;
    //[JsonInclude] // включаем это поле
    public DateTimeOffset Created;
    //[JsonInclude] // включаем это поле
    public ushort Pages;
}

//public class DateOnlyConverter : JsonConverter<DateOnly>
//{
//    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        // читаем как строку
//        return DateOnly.Parse(reader.GetString()!);
//    }

//    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
//    {
//        writer.WriteStartObject();
//        writer.WriteNumber("year", value.Year);
//        writer.WriteNumber("month", value.Month);
//        writer.WriteNumber("day", value.Day);
//        writer.WriteNumber("dayOfWeek", (int)value.DayOfWeek);
//        writer.WriteNumber("dayOfYear", value.DayOfYear);
//        writer.WriteNumber("dayNumber", value.DayNumber);
//        writer.WriteEndObject();
//    }
//}
