using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02
{
    internal class Rectangle : Shape
    {

        public override double Area => base.Height*base.Width;

        public Rectangle(double height, double width) : base(height, width) { }


    }
}
