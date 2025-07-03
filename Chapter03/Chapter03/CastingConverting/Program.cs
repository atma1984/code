//using static System.Convert;

//int a = 10;
//double b = a; // тип int можно безопасно привести к double
//Console.WriteLine(b);


//double c = 9.8;
//int d = (int)c; // компилятор выдаст ошибку на этой строке
//Console.WriteLine(d);

//Console.WriteLine(new string('-', 30));
//Console.WriteLine(new string('-', 30));

//long e = 10;
//int f = (int)e;
//Console.WriteLine($"e is {e:N0} and f is {f:N0}");
//e = 5_000_000_000;
//f = (int)e;
//Console.WriteLine($"e is {e:N0} and f is {f:N0}");

//Console.WriteLine(new string('-', 30));
//Console.WriteLine(new string('-', 30));

//double g = 9.8;
///*int h = ToInt32(g)*/; // метод System.Convert
//int h = Convert.ToInt32(g);

//Console.WriteLine($"g is {g} and h is {h}");

//Console.WriteLine(new string('-', 30));
//Console.WriteLine(new string('-', 30));

//double[] doubles = new[]
//{ 9.49, 9.5, 9.51, 10.49, 10.5, 10.51, 6.5 };
//foreach (double n in doubles)
//{
//    Console.WriteLine($"ToInt32({n}) is {ToInt32(n)}");
//}
//Console.WriteLine(new string('-', 30));
//foreach (double n in doubles)
//{
//    Console.WriteLine(format:
//    "Math.Round({0}, 0, MidpointRounding.AwayFromZero) is {1}",
//    arg0: n,
//    arg1: Math.Round(value: n, digits: 0,mode: MidpointRounding.AwayFromZero));

//    Console.WriteLine(new string('-', 30));
//    Console.WriteLine(new string('-', 30));
//}


//    // выделение массива из 128 байт
//    byte[] binaryObject = new byte[128];
//    // заполнение массива случайными байтами
//    new Random().NextBytes(binaryObject);
//    Console.WriteLine("Binary Object as bytes:");
//    for (int index = 0; index < binaryObject.Length; index++)
//    {
//    //Console.Write($"{(binaryObject[index], 2).ToString().PadLeft(8, '0')} ");
//    Console.Write($"{binaryObject[index]:X} ");
//}
//    Console.WriteLine();
//    // преобразование в строку Base64 и вывод в виде текста
//    string encoded = Convert.ToBase64String(binaryObject);
//    Console.WriteLine($"Binary Object as Base64: {encoded}");

//ValueTuple<int,int,int,int, int, int, int> t = (2, 6, 2,3,4,2,5);


//int age = int.Parse("27");
//DateTime birthday = DateTime.Parse("4 July 1980");
//Console.WriteLine($"I was born {age} years ago.");
//Console.WriteLine($"My birthday is {birthday}.");
//Console.WriteLine($"My birthday is {birthday:ddMMyyyy}.");


//int count = int.TryParse("abc");
string abc ="5" ;
if (int.TryParse(abc, out int n))
    {
    Console.WriteLine($" Good = {n} ");
}
else
{
    Console.WriteLine(" Fuck ");
}


