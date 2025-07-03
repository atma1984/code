using static System.Convert;
int a, b;
try
{
    do
    {
        Console.Write("Enter a number between 0 and 255:");
        a = ToInt32(Console.ReadLine());

    } while ((0>a) | (a>255));
    Console.WriteLine();
    do
    {
        Console.Write("Enter b number between 0 and 255:");
        b = ToInt32(Console.ReadLine());

    } while ((b < 0) | (b > 255));
    Console.WriteLine();
    Console.WriteLine($"{a} divided by {b} is {a / b} ");



}
catch (Exception ex)
{

    Console.WriteLine($"{ex.GetType().Name} : {ex.Message}.");
}


