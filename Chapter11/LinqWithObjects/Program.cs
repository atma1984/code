using static System.Console;


// массив строк, реализующий IEnumerable<string>
string[] names = new[] { "Michael", "Pam", "Jim", "Dwight",
"Angela", "Kevin", "Toby", "Creed" ,"Viktor"};
WriteLine("Deferred execution");
// Вопрос: какие имена начинаются с буквы M?
// (используем метод расширения LINQ)
var query1 = names.Where(name => name.StartsWith("M"));
// Вопрос: какие имена заканчиваются на букву M?
// (используем синтаксис написания запросов LINQ)
var query2 = from name in names where name.EndsWith("m") select name;

// ответ возвращается в виде массива строк, содержащих Pam и Jim
string[] result1 = query1.ToArray();
// ответ возвращается в виде списка строк, содержащих Pam и Jim
List<string> result2 = query2.ToList();

//WriteLine("Begin symbol M");
//foreach (string s in result1)
//{
//    WriteLine(s);
//}
//WriteLine(new string('-', 20));
//WriteLine("End symbol m");
//foreach (string s in query2)
//{
//    WriteLine(s);
//    //names[2] = "Jim Bin";
//}

WriteLine(new string('-', 20));
WriteLine("Writing queries");
//var query = names.Where(new Func<string, bool>(NameLongerThanFour));
//var query = names.Where(NameLongerThanFour);
IOrderedEnumerable<string> query = names.Where(name => name.Length>4)
                 .OrderBy(name => name.Length)
                 .ThenBy(name => name);
foreach (string s in query) 
{
    WriteLine(s);

}




static bool NameLongerThanFour(string name)
{
    return name.Length > 4;
}

WriteLine(new string('-', 100));
WriteLine("Filtering by type");
List<Exception> exceptions = new()
{
new ArgumentException(),
new SystemException(),
new IndexOutOfRangeException(),
new InvalidOperationException(),
new NullReferenceException(),
new InvalidCastException(),
new OverflowException(),
new DivideByZeroException(),
new FileNotFoundException(),
new PathTooLongException(),
new ApplicationException()
};

IEnumerable<IOException> IOExceptionsQuery =
exceptions.OfType<IOException>();
foreach (IOException exception in IOExceptionsQuery)
{
    WriteLine(exception);
}


