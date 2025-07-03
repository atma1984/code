
//Console.WriteLine("Before parsing");
//string? input = "49"; // или используйте значение "49" в блокноте


//Console.WriteLine("Before parsing");
//Console.Write("What is your age? ");
//string? input = Console.ReadLine(); // или используйте значение "49" в блокноте

//try
//{
//    int age = int.Parse(input);
//    Console.WriteLine($"You are {age} years old.");
//}

//catch (FormatException)
//{
//    Console.WriteLine("Неверный формат входящей строки");
//}
//catch (OverflowException)
//{
//    Console.WriteLine("Очень большое или маленькое значение числа");

//}
//catch (Exception ex)
//{
//    Console.WriteLine($"{ex.GetType()} says {ex.Message}");
//}


//Console.WriteLine("After parsing");


Console.WriteLine("Enter an amount: ");
//string? amount = Console.ReadLine();
string amount1 = "dfgdfgdfdfg";
try
{
    int n = int.Parse(amount1);
    //decimal amountValue = decimal.Parse(amount);
}
catch (FormatException)
{
    Console.WriteLine("Amounts must only contain 1111111111111!");
}
catch (Exception) when (amount1.Contains("df"))
{
    Console.WriteLine("Amounts cannot use the dollar sign dfdfdfdfdffd!");
}
