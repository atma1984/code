using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Packt.Figures;
using static System.Console;
using static System.Environment;
using static System.IO.Path;



List<Shape> listOfShapes = new()
{
new Circle { Colour = "Red", Radius = 2.5 },
new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
new Circle { Colour = "Green", Radius = 8.0 },
new Circle { Colour = "Purple", Radius = 12.3 },
new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
};


XmlSerializer xs = new (listOfShapes.GetType());
string path = Combine(CurrentDirectory, "people.xml");
using (FileStream st = File.Create(path))
{
    xs.Serialize(st, listOfShapes);
}
WriteLine(File.ReadAllText(path));

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{// десериализуем и приводим объектный граф в список лиц
    List<Shape>? load_listOfShapes =  xs.Deserialize(xmlLoad) as List<Shape>;


    WriteLine("Loading shapes from XML:");
    if (load_listOfShapes is not null)
    {
       

        foreach (Shape p in load_listOfShapes)
        {
            WriteLine("{0} is {1} and has an area of {2:f2}",
            p.GetType().Name, p.Colour, p.Area);
        }
    }
}
