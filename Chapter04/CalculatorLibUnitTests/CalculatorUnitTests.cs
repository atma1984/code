using Packt;
using Xunit;

namespace CalculatorLibUnitTests
{
   
        public class CalculatorUnitTests
        {
            [Fact]
            public void TestAdding2And2()
            {
                // размещение
                double a = 2;
                double b = 2;
                double expected = 4;
                Calculator calc = new();
                // действие
                double actual = calc.Add(a, b);
                // утверждение
                Assert.Equal(expected, actual);
            }
            [Fact]
            public void TestAdding2And3()
            {
                // размещение
                double a = 2;
                double b = 3;
                double expected = 5;
                Calculator calc = new();
                // действие
                double actual = calc.Add(a, b);
                // утверждение
                Assert.Equal(expected, actual);
            }
        }
    
}