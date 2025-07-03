

try
{
checked
{
    int max = 500;
    for (byte i = 0; i < max; i++)
    {
        Console.WriteLine(i);
    }
}
}
catch (OverflowException ex)
{
    Console.WriteLine($"Тип исключения - {ex.GetType()}\nСообщение исключения - {ex.Message}");
	
}
Console.WriteLine("Rpogramm end");
