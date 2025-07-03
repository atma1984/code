using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02
{
    internal class Circle:Shape
    {

        public override double Area => 2 * Math.PI * (this.Height / 2);

        public Circle(double height) : base(height) { }
    }
}
