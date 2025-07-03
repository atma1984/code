﻿using System.Text.Json.Serialization;
using System.Xml.Serialization; // класс XmlSerializer
using Packt.Shared; // класс Person
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using NewJson = System.Text.Json.JsonSerializer;

List<Person> people = new()
{
              new(30000M)
              {
                FirstName = "Alice",
                LastName = "Smith",
                DateOfBirth = new(1974, 3, 14)
              },
   new(40000M)
   {
    FirstName = "Bob",
    LastName = "Jones",
    DateOfBirth = new(1969, 11, 23),
                                              Children = new()
                                              {
                                                new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 }
                                              }
   },
                                    new(20000M)
                                    {
                                     FirstName = "Charlie",
                                     LastName = "Cox",
                                     DateOfBirth = new(1984, 5, 4),
                                              Children = new()
                                              {
                                                new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Cox",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 },new(0M)
                                                {
                                                 FirstName = "Sally",
                                                 LastName = "Jones",
                                                 DateOfBirth = new(2000, 7, 12)
                                                 }
                                              }
                                    }

};

// создаем объект, который будет форматировать список лиц как XML
XmlSerializer xs = new(people.GetType());
// создаем файл для записи
string path = Combine(CurrentDirectory, "people.xml");
using (FileStream stream = File.Create(path))
{
    // сериализуем объектный граф в поток
    xs.Serialize(stream, people);
}
WriteLine("Written {0:N0} bytes of XML to {1}",
arg0: new FileInfo(path).Length,
arg1: path);
WriteLine();
// отображаем сериализованный граф объектов
WriteLine(File.ReadAllText(path));

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{// десериализуем и приводим объектный граф в список лиц
    List<Person>? loadedPeople =
    xs.Deserialize(xmlLoad) as List<Person>;
    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine("{0} has {1} children.",
            p.FirstName, p.Children?.Count ?? 0);
        }
    }
}


// создаем файл для записи
string jsonPath = Combine(CurrentDirectory, "people.json");
using (StreamWriter jsonStream = File.CreateText(jsonPath))
{
    // создаем объект, который будет форматироваться как JSON
    Newtonsoft.Json.JsonSerializer jss = new();
    // сериализуем объектный граф в строку
    jss.Serialize(jsonStream, people);
}
WriteLine();
WriteLine("Written {0:N0} bytes of JSON to: {1}",
arg0: new FileInfo(jsonPath).Length,
arg1: jsonPath);
// отображаем сериализованный граф объектов
WriteLine(File.ReadAllText(jsonPath));


using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    //десериализуем объектный граф в список лиц ассинхронно
    //List<Person>? loadedPeople2 =
    //await NewJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;
    List<Person>? loadedPeople2 =
    await NewJson.DeserializeAsync<List<Person>>(jsonLoad);
    if (loadedPeople2 is not null)
    {
        foreach (Person p in loadedPeople2)
        {
            WriteLine("{0} has {1} children.",
            p.LastName, p.Children?.Count ?? 0);
        }
    }
}


//// десериализуем объектный граф в список лиц синхронно 
//using FileStream jsonLoad = File.OpenRead(jsonPath);
//List<Person>? loadedPeople1 = NewJson.Deserialize<List<Person>>(jsonLoad);
//if (loadedPeople1 is not null)
//{
//    foreach (Person p in loadedPeople1)
//    {
//        WriteLine("{0} has {1} children.",
//        p.LastName, p.Children?.Count ?? 0);
//    }
//}





