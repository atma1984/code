
using System.Xml;



object height = 1.88; // хранение double в объекте
object name = "Amir"; // хранение string в объекте
Console.WriteLine($"{name} is {height} metres tall.");
//int length1 = name.Length; // Выдаст ошибку компиляции!
int length2 = ((string)name).Length; // сообщаем компилятору, что это строка
Console.WriteLine($"{name} has {length2} characters.");

Console.WriteLine(new String('-', 50));

// хранение строки в объекте dynamic
// строка имеет свойство Length
dynamic something = "Ahmed";
// int не имеет свойства Length
//something = 12;
// массив любого типа имеет свойство Length
something = new[] { 3, 5, 7, 6, 87, 4 };

// компилируется, но может вызвать исключение во время
// выполнения, если вы позже сохраните тип данных,
// у которого нет свойства Length
Console.WriteLine($"Length is {something.Length}");

Console.WriteLine(new String('-', 50));

var population = 66_000_000; // 66 миллионов человек в Великобритании
var weight = 1.88; // в килограммах
var price = 4.99M; // в фунтах стерлингов
var fruit = "Apples"; // строки в двойных кавычках
var letter = 'Z'; // символы в одиночных кавычках
var happy = true; // логическое значение — true или false

Console.WriteLine(new String('-', 50));

// удачное применение var, поскольку он избегает повторного типа,
// как показано во втором более подробном операторе
var xml1 = new XmlDocument();
XmlDocument xml2 = new XmlDocument();
// неудачное применение var, поскольку мы не можем определить тип,
// поэтому должны использовать конкретное объявление типа,
// как показано во втором операторе
var file1 = File.CreateText("something1.txt");
StreamWriter file2 = File.CreateText("something2.txt");

Console.WriteLine(new String('-', 50));
XmlDocument xml3 = new();

Console.WriteLine(new String('-', 50));

Console.WriteLine($"default(int) = {default(int)}");
Console.WriteLine($"default(bool) = {default(bool)}");
Console.WriteLine($"default(DateTime) = {default(DateTime)}");
Console.WriteLine($"default(string) = {default(string)}");


int number = 13;
Console.WriteLine($"number has been set to: {number}");
number = default;
Console.WriteLine($"number has been reset to its default: {number}");



