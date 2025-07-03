// See https://aka.ms/new-console-template for more information
int a = 10; // 00001010
int b = 6; // 00000110
Console.WriteLine($"a = {a}");
Console.WriteLine($"b = {b}");

Console.WriteLine($"a = {a}");
Console.WriteLine($"b = {b}");
Console.WriteLine($"a & b = {a & b}"); // только столбец 2-го бита
Console.WriteLine($"a | b = {a | b}"); // столбцы 8, 4 и 2-го битов
Console.WriteLine($"a ^ b = {a ^ b}"); // столбцы 8 и 4-го битов
Console.WriteLine($"a << 3 = {a << 3}");
Console.WriteLine($"a * 8 = {a * 8}");

static string ToBinaryString(int value)
{
    return Convert.ToString(value,toBase: 2).PadLeft(8, '0');
    //return Convert.ToString(value, 2);
}
Console.WriteLine(new string('-', 40));
Console.WriteLine();
Console.WriteLine("Outputting integers as binary:");
Console.WriteLine($"a = {ToBinaryString(a)}");
Console.WriteLine($"b = {ToBinaryString(b)}");
Console.WriteLine($"a & b = {ToBinaryString(a & b)}");
Console.WriteLine($"a | b = {ToBinaryString(a | b)}");
Console.WriteLine($"a ^ b = {ToBinaryString(a ^ b)}");

