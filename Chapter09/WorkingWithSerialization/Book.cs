using System.Text.Json.Serialization;

//public class Book1
//{
//    // конструктор для установки свойства, не допускающего null
//    public Book(string title)
//    {
//        Title = title;
//    }
//    // свойства
//    public string Title { get; set; }
//    public string? Author { get; set; }
//    // поля
//    [JsonInclude] // включаем это поле
//    public DateOnly PublishDate;
//    [JsonInclude] // включаем это поле
//    public DateTimeOffset Created;
//    public ushort Pages;
//}