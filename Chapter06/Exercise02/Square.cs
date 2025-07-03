using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02
{
    internal class Square : Shape
    {
        public override double Area => base.Height * base.Height;
        public Square(double height) : base(height) { }
    }
}
