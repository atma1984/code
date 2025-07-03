
int x = 0;
while (x < 10)
{
    Console.WriteLine(x);
    x++;
}
Console.WriteLine(new string('-', 30));
Console.WriteLine(new string('-', 30));

string? password;
do
{
    Console.WriteLine("Enter your password: ");
    password = Console.ReadLine();
}
while (password != "Pa$$w0rd");
Console.WriteLine("Correct!"); 