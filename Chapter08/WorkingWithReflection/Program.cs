
using System.Reflection; // сборка
using System.Reflection.Metadata;
using static System.Console;
using System.Runtime.CompilerServices; // CompilerGeneratedAttribute
using Packt.Shared; // CoderAttribute


WriteLine("Assembly metadata:");
Assembly? assembly = Assembly.GetEntryAssembly();
if (assembly is null)
{
    WriteLine("Failed to get entry assembly.");
    return;
}
WriteLine($" Full name: {assembly.FullName}");
WriteLine($" Location: {assembly.Location}");
IEnumerable<Attribute> attributes = assembly.GetCustomAttributes();
WriteLine($" Assembly-level attributes:");
foreach (Attribute a in attributes)
{
    if (a is AssemblyTitleAttribute titleAttr)
    {
        WriteLine($"This title {titleAttr.Title} of attribute {a}");
    }

    WriteLine($" {a.GetType()} ");
}

WriteLine(new string('-', 50));
WriteLine(new string('-', 50));

AssemblyInformationalVersionAttribute? version = assembly
.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
WriteLine($" Version: {version?.InformationalVersion}");
AssemblyCompanyAttribute? company = assembly
.GetCustomAttribute<AssemblyCompanyAttribute>();
WriteLine($" Company: {company?.Company}");

AssemblyFileVersionAttribute? file_version = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
WriteLine($" FileVersion: {file_version?.Version}");

WriteLine(new string('-', 50));
WriteLine(new string('-', 50));

WriteLine();
WriteLine($"* Types:");
Type[] types = assembly.GetTypes();
foreach (Type type in types)
{
    WriteLine();
    WriteLine($"Type: {type.FullName}");
    MemberInfo[] members = type.GetMembers();
    foreach (MemberInfo member in members)
    {
        WriteLine("{0}: {1} ({2})",
        arg0: member.MemberType,
        arg1: member.Name,
        arg2: member.DeclaringType?.Name);
        IOrderedEnumerable<CoderAttribute> coders =
        member.GetCustomAttributes<CoderAttribute>()
        .OrderByDescending(c => c.LastModified);
        foreach (CoderAttribute coder in coders)
        {
            WriteLine("-> Modified by {0} on {1}",
            coder.Coder, coder.LastModified.ToShortDateString());
        }
    }
}




class Animal
{
    [CoderAttribute("Mark Price", "22 August 2021")]
    [CoderAttribute("Johnni Rasmussen", "13 September 2021")]
    public void Speak()
    {
        WriteLine("Woof...");
    }
}
