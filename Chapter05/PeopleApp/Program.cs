using Packt.Shared;

using static System.Console;
using static Packt.Shared.WondersOfTheWorld;


Person bob = new(); 
//WriteLine(bob.ToString());

bob.Name = "Bob Smith";
bob.DateOfBirth = new DateTime(1965, 12, 22);
bob.mail = "mail.ru";
bob.FavoriteAncientWonder = WondersOfTheAncientWorld.HangingGardensOfBabylon;

#region
//WondersOfTheAncientWorld myWonders = WondersOfTheAncientWorld.GreatPyramidOfGiza | WondersOfTheAncientWorld.HangingGardensOfBabylon;
//Console.WriteLine(myWonders);

//WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}",
//arg0: bob.Name,
//arg1: bob.DateOfBirth);
//WriteLine(
//format: "{0}'s favorite wonder is {1}. Its integer is {2}.",
//arg0: bob.Name,
//arg1: bob.FavoriteAncientWonder,
//arg2: (int)bob.FavoriteAncientWonder);

#endregion
Person alice = new()
{
    Name = "Alice Jones",
    DateOfBirth = new(2000, 8, 1) // C# 9.0 и более поздние версии
};
#region
////WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}",
////arg0: alice.Name,
////arg1: alice.DateOfBirth);


//Console.WriteLine(new string('-',50)  );

////bob.BucketList =
////WondersOfTheAncientWorld.GreatPyramidOfGiza | WondersOfTheAncientWorld.HangingGardensOfBabylon;

//// bob.BucketList = (WondersOfTheAncientWorld)18;
//WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");
//Console.WriteLine(new string('-', 50));
//Console.WriteLine(new string('-', 50));

//WondersOfTheAncientWorld1 wonders2 = WondersOfTheAncientWorld1.StatueOfZeusAtOlympia | WondersOfTheAncientWorld1.HangingGardensOfBabylon;
//Console.WriteLine(wonders2);
#endregion

bob.Children.Add(new() { Name = "Alfred" });
bob.Children.Add(new() { Name = "Zoe" });
//WriteLine(
//$"{bob.Name} has {bob.Children.Count} children:");
//for (int childIndex = 0; childIndex < bob.Children.Count; childIndex++)
//{
//    WriteLine($" {bob.Children[childIndex].Name}");
//}
alice.Children.Add(new() { Name = "Viktor" });
alice.Children.Add(new() { Name = "Anna" });
alice.Children.Add(new() { Name = "Boris" });
//foreach (Person child in bob.Children)
//{
//    WriteLine(child.Name);
//}
//Console.WriteLine(new string('-', 50));

//BankAccount.InterestRate = 0.012M;

//BankAccount jonesAccount = new(); // C# 9.0 и более поздние версии
//jonesAccount.AccountName = "Mrs. Jones";
//jonesAccount.Balance = 2400;

//WriteLine(format: "{0} earned {1:C} interest.",
//arg0: jonesAccount.AccountName,
//arg1: jonesAccount.Balance * BankAccount.InterestRate);
//BankAccount gerrierAccount = new();
//gerrierAccount.AccountName = "Ms. Gerrier";
//gerrierAccount.Balance = 98;
//WriteLine(format: "{0} earned {1:C} interest.",
//arg0: gerrierAccount.AccountName,
//arg1: gerrierAccount.Balance * BankAccount.InterestRate);
//Console.WriteLine();
//Console.WriteLine();
//WriteLine($"{bob.Name} is a {Person.Species}");
//Console.WriteLine();
//WriteLine($"{bob.Name} was born on {bob.HomePlanet}");
//Console.WriteLine(new string('-', 70));


Person blankPerson = new();
//WriteLine(format:
//"{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
//arg0: blankPerson.Name,
//arg1: blankPerson.HomePlanet,
//arg2: blankPerson.Instantiated);
//Console.WriteLine(new string('-', 70));

Person MarsPerson = new("Steave","Mars"); 
//WriteLine(format:
//"{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
//arg0: MarsPerson.Name,
//arg1: MarsPerson.HomePlanet,
//arg2: MarsPerson.Instantiated);
//Console.WriteLine(new string('-', 70));



//MarsPerson.WriteToConsole();
//Console.WriteLine(new string('-', 70));
//Console.WriteLine(MarsPerson.GetOrigin());

(string TheName, int TheNumber) fruit = MarsPerson.GetFruit();

//Console.WriteLine($"{fruit.TheName} - {fruit.TheNumber}");

var thing = (alice.Name, alice.Children.Count);
//Console.WriteLine($"{thing.Name} has {thing.Count} childrens");


var (name1, dob1,dob2) = bob;
WriteLine($"Deconstructed: {name1}, {dob1}, {dob2}");
var (name2, dob22, fav2, col) = bob;
WriteLine($"Deconstructed: {name2}, {dob22}, {fav2}, {col}");


