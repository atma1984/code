
using PrimeFactorsLib;
int n;

PrimeFactor primeFactor = new();
Console.Write("Enter number :");
if (int.TryParse(Console.ReadLine(),out n))
{
primeFactor.PrimeFactors(n);
Console.WriteLine(primeFactor.PrimeFactors(n));
}

