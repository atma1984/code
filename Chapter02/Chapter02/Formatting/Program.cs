
int numberOfApples = 12;
decimal pricePerApple = 0.35M;
Console.WriteLine(
format: "{0} apples costs {1:C}",
arg0: numberOfApples,
arg1: pricePerApple * numberOfApples);
string formatted = string.Format(
format: "{0} apples costs {1:C}",
numberOfApples,
pricePerApple * numberOfApples);
Console.WriteLine($"{numberOfApples} apples costs {pricePerApple *numberOfApples:C}");

//WriteToFile(formatted); // записывает строку в файл
Console.WriteLine(new String('-', 50));

string firstname = "Omar";
 string lastname = "Rudberg";
 string fullname = firstname + " " + lastname;
 string Allfullname = $"{firstname}_ _{lastname}_ __{numberOfApples}";
Console.WriteLine(Allfullname);
Console.WriteLine(new String('-', 50));
string applesText = "Apples";
int applesCount = 1234;
string bananasText = "Bananas";
int bananasCount = 156789;
Console.WriteLine(
format: "{0,-7} {1,10}",
arg0: "Name",
arg1: "Count");
Console.WriteLine(
format: "{0,-7} {1,10:N0}",
arg0: applesText,
arg1: applesCount);
Console.WriteLine(
format: "{0,-7} {1,10:N0}",
arg0: bananasText,
arg1: bananasCount);

Console.WriteLine(new String('-', 50));


Console.WriteLine("Press any key combination: ");
ConsoleKeyInfo key = Console.ReadKey();
Console.WriteLine();
Console.WriteLine("Key: {0}, Char: {1}, Modifiers: {2}",
arg0: key.Key,
arg1: key.KeyChar,
arg2: key.Modifiers);

