using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packt.Figures;

    public class Rectangle : Shape
    {
    public double Height { get; set; }
    public double Width { get; set; }
    public  override double Area { get { return Height*Width; } }

    }

