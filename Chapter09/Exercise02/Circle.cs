
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packt.Figures;

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area{get{ return Math.PI* Math.Pow(Radius, 2); }}

    public Circle() { }
}

