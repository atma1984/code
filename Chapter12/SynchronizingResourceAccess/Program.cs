using static System.Console;
using System.Diagnostics;


WriteLine("Please wait for the tasks to complete.");
Stopwatch watch = Stopwatch.StartNew();
Task a = Task.Factory.StartNew(MethodA);
Task b = Task.Factory.StartNew(MethodB);
Task.WaitAll(new Task[] { a, b });
WriteLine();
WriteLine($"Results: {SharedObjects.Message}.");
WriteLine($"{watch.ElapsedMilliseconds:N0} elapsed milliseconds.");
WriteLine($"{SharedObjects.Counter} string modifications.");



static void MethodA()
{
    try
    {
        if (Monitor.TryEnter(SharedObjects.Conch, TimeSpan.FromSeconds(15)))
        {
            for (int i = 0; i < 10; i++)
            {
                SharedObjects.Message += "A";
                Interlocked.Increment(ref SharedObjects.Counter);
                Write(".");
            }
        }
        else
        {
            WriteLine("Method A timed out when entering a monitor on conch.");
        }
    }
finally
{
        Monitor.Exit(SharedObjects.Conch);
    }
    //Thread.Sleep(SharedObjects.Random.Next(2000));
                //try
                //{
                //    Monitor.Enter(SharedObjects.Conch);
                //    for (int i = 0; i < 5; i++)
                //    {
                //        Thread.Sleep(SharedObjects.Random.Next(2000));
                //        SharedObjects.Message += "A";
                //        Write(".");
                //    }
                //}
                //finally
                //{
                //    Monitor.Exit(SharedObjects.Conch);
                //}
            }
static void MethodB()
{
    lock (SharedObjects.Conch)
    {
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(SharedObjects.Random.Next(2000));
            SharedObjects.Message += "B";
            Interlocked.Increment(ref SharedObjects.Counter);
            Write(".");
        }
    }
}
static class SharedObjects
{
    public static object Conch = new();
    public static Random Random = new();
    public static string? Message; // общий ресурс
    public static int Counter; // иной общий ресурс
}