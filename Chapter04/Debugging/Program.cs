static double Add(double a, double b)
{
    return a + b; // Преднамеренная ошибка!
}

double a = 4.5;
double b = 2.5;
double answer = Add(a, b);
Console.WriteLine($"{a} + {b} = {answer}");
Console.WriteLine("Press ENTER to end the app.");
Console.ReadLine();
