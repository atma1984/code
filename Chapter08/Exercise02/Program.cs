using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using static System.Console;



WriteLine("The default regular expression checks for at least one digit."); 

string? regex_str ;
string? user_str;

while (true)
{
    
    Write($"Enter a regular expression (or press ENTER to use the default):");
    regex_str = ReadLine();
    WriteLine();
    Write("Enter some input:");
    user_str = ReadLine();
    var regex = new Regex(regex_str);
    WriteLine($"{user_str} matches {regex_str} ? {regex.IsMatch(user_str)}");
    WriteLine("Press ESC to end or any key to try again.");
   
        
        var key = Console.ReadKey(intercept: true).Key; // Чтение нажатой клавиши
       
        if (key == ConsoleKey.Escape) // Если клавиша ESC, выходим из цикла
        {
            Console.WriteLine("Выход была нажата клавиша ESC");
            break;
        } 

    

    // Ваш код внутри цикла может быть здесь
}