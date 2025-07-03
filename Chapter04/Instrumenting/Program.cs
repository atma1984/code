using System.Diagnostics;



using Microsoft.Extensions.Configuration;
ConfigurationBuilder builder = new();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json",
optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();
TraceSwitch mySwitch = new TraceSwitch("mySwitch", "mySwitch", "Warning");
configuration.GetSection("PacktSwitch").Bind(mySwitch);




// запись в текстовый файл, расположенный в папке проекта
Trace.Listeners.Add(new TextWriterTraceListener(
File.CreateText(Path.Combine(Environment.GetFolderPath(
Environment.SpecialFolder.DesktopDirectory), "log.txt"))));
// модуль записи текста буферизируется, поэтому данная опция
// вызывает функцию Flush() для всех прослушивателей после записи
Trace.AutoFlush = true;


Debug.WriteLine("Debug says, I am watching!");
Trace.WriteLine("Trace says, I am watching!");
//Trace.WriteLine("Application started.");
//Trace.WriteLine("Processing data...");
//Trace.WriteLine("Application finished.");


// Устанавливаем уровни трассировки и выводим сообщения в зависимости от уровня

// Уровень Verbose (подробный)
if (mySwitch.Level == TraceLevel.Verbose)
{
    Trace.WriteLine("Verbose level: ");
}

// Уровень Information (информация)
if (mySwitch.TraceInfo)
{
    Trace.WriteLine("Information level:");
}

// Уровень Warning (предупреждение)
if (mySwitch.TraceWarning)
{
    Trace.WriteLine("Warning level: ");
}

// Уровень Error (ошибка)
if (mySwitch.TraceError)
{
    Trace.WriteLine("Error level:");
}

// Уровень Off (выключено)
if (mySwitch.Level == TraceLevel.Off)
{
    Trace.WriteLine("Off level: No trace messages.");
}

//Trace.WriteLineIf(mySwitch.TraceError, "Trace error");
//Trace.WriteLineIf(mySwitch.TraceWarning, "Trace warning");
//Trace.WriteLineIf(mySwitch.TraceInfo, "Trace information");
//Trace.WriteLineIf(mySwitch.TraceVerbose, "Trace verbose");










