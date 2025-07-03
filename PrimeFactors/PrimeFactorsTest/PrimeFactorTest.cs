using PrimeFactorsLib;
using Xunit;

namespace PrimeFactorsTest
{
    public class PrimeFactorTest
    {
        [Theory]
        [InlineData(500,2,2,5,5,5)]
        public void PrimeFactor500(int number, int a, int b, int c, int d, int e)
        {
            string expected = $"{number}:{a}x{b}x{c}x{d}x{e}";
            PrimeFactor pr = new();
            // действие
            string actual = pr.PrimeFactors(number);
            // утверждение
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(7)]
        public void PrimeFactor7(int number)
        {
            string expected = $"{number}:{number}";
            PrimeFactor pr = new();
            // действие
            string actual = pr.PrimeFactors(number);
            // утверждение
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(30, 2, 3, 5)]
        public void PrimeFactor30(int number, int a, int b, int c)
        {
            string expected = $"{number}:{a}x{b}x{c}";
            PrimeFactor pr = new();
            // действие
            string actual = pr.PrimeFactors(number);
            // утверждение
            Assert.Equal(expected, actual);
        }

        
    }
}