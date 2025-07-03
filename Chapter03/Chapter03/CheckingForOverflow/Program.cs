


//try
//{
//    checked
//    {
//        int y = int.MaxValue;
//        Console.WriteLine($"Initial value max: {y}");

//        int x = int.MaxValue - 1;
//        Console.WriteLine($"Initial value: {x}");
//        x++;
//        Console.WriteLine($"After incrementing: {x}");
//        x++;
//        Console.WriteLine($"After incrementing: {x}");
//        x++;
//        Console.WriteLine($"After incrementing: {x}");
//    }
//}
//catch (ArithmeticException ex)
//{

//    Console.WriteLine($"Type of ex - {ex.GetType()} , Message - {ex.Message} "  );
//}
//unchecked
//{
//    int y = int.MaxValue + 1;
//    Console.WriteLine($"Initial value: {y}");
//    y--;
//    Console.WriteLine($"After decrementing: {y}");
//    y--;
//    Console.WriteLine($"After decrementing: {y}");
//}


double n = 5.5;
int g = 0;
double t = n / g;
Console.WriteLine(  t);

for (; true;) { Thread.Sleep(1000);  Console.WriteLine("Run"); } ;