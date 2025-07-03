using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using static Packt.Shared.WondersOfTheWorld;



namespace Packt.Shared
{
    public class Person : object
    {
        public string Name;
        public DateTime DateOfBirth;
        public string mail;
        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;
        public List<Person> Children = new List<Person>();
        public const string Species = "Homo Sapiens";
        public readonly string HomePlanet = "Earth";
        public readonly DateTime Instantiated;

        public Person()
        {
            // установка значений по умолчанию для полей,
            // включая поля только для чтения
            Name = "Unknown";
            Instantiated = DateTime.Now;
        }

        public Person(string initialName, string homePlanet)
        {
            Name = initialName;
            HomePlanet = homePlanet;
            Instantiated = DateTime.Now;
        }
        // методы
        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }
        public string GetOrigin()
        {
            return $"{Name} was born on {HomePlanet}.";
        }

        public (string fruitic, int count) GetFruit()
        {
            return (fruitic: "Apples", count: 5);
        }


        // деконструкторы
        public void Deconstruct(out string name, out DateTime dob, out string dob2)
        {
            name = Name;
            dob = DateOfBirth;
            dob2 = mail;
        }
        public void Deconstruct(out string name,
        out DateTime dob, out WondersOfTheAncientWorld fav,out int count)
        {
            name = Name;
            dob = DateOfBirth;
            fav = FavoriteAncientWonder;
            count = Children.Count;
        }

    }
  
}

