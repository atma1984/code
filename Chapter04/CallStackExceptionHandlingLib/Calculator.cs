using static System.Console;
namespace Packt;
public class Calculator
{
    public static void Gamma() // public – для вызова вне фукнции
    {
        WriteLine("In Gamma");
        Delta();
    }
    private static void Delta() // private – для вызова только внутри функции
    {
        WriteLine("In Delta");
        File.OpenText("bad file path");
    }
}
