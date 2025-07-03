

using System.Runtime.InteropServices;

Console.WriteLine($"There are {args.Length} arguments.");
for ( int i = 0; i < args.Length; i++) 
{
    Console.Write(args[i]+"\t");

}

Console.WriteLine();
Console.WriteLine(new String('-', 50));

foreach (string arg in args)
{
    Console.WriteLine(arg);
}
Console.WriteLine();

if (args.Length < 3)
{
    Console.WriteLine("You must specify two colors and cursor size, e.g.");
    Console.WriteLine("dotnet run red yellow 50");
    return; // прекращение запуска
}
args[2] = "100";
Console.ForegroundColor = (ConsoleColor)Enum.Parse(
enumType: typeof(ConsoleColor),
value: args[0],
ignoreCase: true);
Console.BackgroundColor = (ConsoleColor)Enum.Parse(
enumType: typeof(ConsoleColor),
value: args[1],
ignoreCase: true);





try
{

    Console.CursorSize = int.Parse(args[2]);
}
catch (PlatformNotSupportedException) 
{
    Console.WriteLine("The current platform Mac OS does not support changing the size of the cursor.");

}



 if (OperatingSystem.IsWindowsVersionAtLeast(major: 10))
{
    try
    {
        Console.WriteLine(new String('-', 50));
        Console.WriteLine("Windows code 10 end later");
      
        args[2] = "100";
        Console.ForegroundColor = (ConsoleColor)Enum.Parse(
        enumType: typeof(ConsoleColor),
        value: args[0],
        ignoreCase: true);
        Console.BackgroundColor = (ConsoleColor)Enum.Parse(
        enumType: typeof(ConsoleColor),
        value: args[1],
        ignoreCase: true);
        Console.CursorSize = int.Parse(args[2]);
        Console.WriteLine(new String('-', 50));
        Console.WriteLine(new String('-', 50));
    }
    catch (PlatformNotSupportedException)
    {
        Console.WriteLine("The current platform Mac OS does not support changing the size of the cursor.");

    }

    Console.WriteLine(new String('-', 50));
#if NET6_0
 Console.WriteLine("NET6_0 this is my directive");
#endif
}

