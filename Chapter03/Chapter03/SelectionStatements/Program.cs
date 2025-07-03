
string password = "ninja111";
if (password.Length < 8)
    Console.WriteLine("Your password is too short. Use at least 8 characters.");
else
    Console.WriteLine("Your password is strong.");
Console.WriteLine(new string('-',30));

// добавьте или удалите "", чтобы изменить поведение
object o = "3";
int j = 4;

if (o is int i)
{
    Console.WriteLine($"{i} x {j} = {i * j}");
}
else
{
    Console.WriteLine("o is not an int so it cannot multiply!");
}
Console.WriteLine(new string('-', 30));
Console.WriteLine(new string('-', 30));
//int number = (new Random()).Next(1, 7);
int number =3;
Console.WriteLine($"My random number is {number}");
switch (number)
{
    case 1:
        Console.WriteLine("One");
        break; // переход в конец оператора switch
    case 2:
        Console.WriteLine("Two");
        goto case 1;
    case 3: // блок, содержащий несколько случаев
    case 4:
        Console.WriteLine("Three or four");
        goto case 1;
    case 5:
        goto A_label;
    default:
        Console.WriteLine("Default");
        break;
} // конец оператора switch
Console.WriteLine("After end of switch");
A_label:
Console.WriteLine($"After A_label");

Console.WriteLine(new string('-', 30));
Console.WriteLine(new string('-', 30));

// string path = "/Users/markjprice/Code/Chapter03";
string path = @"C:\Code\Chapter03";
Console.WriteLine("Press R for read-only or W for writeable: ");
ConsoleKeyInfo key = Console.ReadKey();
Console.WriteLine();
Stream? s;
if (key.Key == ConsoleKey.R)
{
    s = File.Open(
    Path.Combine(path, "file.txt"),
    FileMode.OpenOrCreate,
    FileAccess.Read);
}
else
{
    s = File.Open(
    Path.Combine(path, "file.txt"),
    FileMode.OpenOrCreate,
    FileAccess.Write);
}
string message;
//switch (s)
//{
//    case FileStream writeableFile when s.CanWrite:
//        message = "The stream is a file that I can write to.";
//        break;
//    case FileStream readOnlyFile:
//        message = "The stream is a read-only file.";
//        break;
//    case MemoryStream ms:
//        message = "The stream is a memory address.";
//        break;
//    default: // всегда выполняется последним, несмотря на текущее положение
//        message = "The stream is some other type.";
//        break;
//    case null:
//        message = "The stream is null.";
//        break;
//}
//Console.WriteLine(message);


message = s switch
{
FileStream writeableFile when s.CanWrite
=> "The stream is a file that I can write to.",
FileStream readOnlyFile
=> "The stream is a read-only file.",
MemoryStream ms
=> "The stream is a memory address.",
null
=> "The stream is null.",
_
=> "The stream is some other type."
};
Console.WriteLine(message);
