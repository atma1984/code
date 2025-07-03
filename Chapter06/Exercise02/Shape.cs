using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02
{
    internal class Shape
    {
        public double Height { get;  }
        public double Width { get;  }
        public virtual double Area { get;}

        public Shape(double height, double width)
        { 
            Height = height;
            Width= width;   
        }

        public Shape(double height)
        {
            Height = height;  
        }
    }
}
