

using System.Diagnostics.CodeAnalysis;

for (int i = 1; i <= 100; i++)
{
	switch (i)
	{
		case int when ((i % 3 == 0) & (i % 5 == 0)):
            Console.Write("FizzBuzz, ");
			break;
        case int when (i % 3 == 0):
            Console.Write("Fizz, ");
            break;
        case int when (i % 5 == 0):
            Console.Write("Buzz, ");
            break;
        default:
            Console.Write($"{i}, ");
            break;
    }
}