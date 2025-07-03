using System.Numerics;
using Humanizer;


public static class Exst_Class
{
    public static void Metod_to_words(this BigInteger a) 
    {
        Console.WriteLine($"{a} this is {a.ToWords()}");
    
    }
}


