// целое число без знака означает положительное целое число или 0
using System;

uint naturalNumber = 23;
// целое число означает отрицательное или положительное целое число или 0
int integerNumber = -23;
// float означает число одинарной точности с плавающей запятой
// суффикс F указывает, что это литерал типа float
float realNumber = 2.3F;
// double означает число двойной точности с плавающей запятой
double anotherRealNumber = 2.3; // литерал типа double

Console.WriteLine(new String('-',50));

// три переменные, которые хранят число 2 миллиона
int decimalNotation = 2_000_000;
int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
int hexadecimalNotation = 0x_001E_8480;
// убедитесь, что три переменные имеют одинаковое значение
// оба оператора выводят true
Console.WriteLine($"{decimalNotation == binaryNotation}");
Console.WriteLine($"{decimalNotation == hexadecimalNotation}");



Console.WriteLine(new String('-', 50));
Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range { int.MinValue:N0} to {int.MaxValue:N0}.");


Console.WriteLine($"double uses {sizeof(double)} bytes and can storenumbers in the range { double.MinValue:N0} to {double.MaxValue:N0}.");
Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can storenumbers in the range { decimal.MinValue:N0} to {decimal.MaxValue:N0}.");

Console.WriteLine(new String('-', 100));
Console.WriteLine("Using decimal:");
decimal c = 0.1M;
decimal d = 0.2M;
if (c + d == 0.3M)
{
    Console.WriteLine($"{c} + {d} equals {0.3}");
}
else
{
    Console.WriteLine($"{c} + {d} does NOT equal {0.3}");
}
Console.WriteLine(new string('-', 80));
Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}.");
Console.WriteLine(new string('-', 80));
Console.WriteLine($"decimal uses {sizeof(uint)} bytes and can storenumbers in the range {uint.MinValue:N0} to {uint.MaxValue:N0}.");

Console.WriteLine(new string('-',80));
Console.WriteLine($"decimal uses {sizeof(ulong)} bytes and can storenumbers in the range {ulong.MinValue:N0} to {ulong.MaxValue:N0}.");
Console.WriteLine(new string('-', 80));
Console.WriteLine(new string('-', 80));
Console.WriteLine($"decimal uses {sizeof(ushort)} bytes and can storenumbers in the range {ushort.MinValue:N0} to {ushort.MaxValue:N0}.");
Console.WriteLine(new string('-', 80));