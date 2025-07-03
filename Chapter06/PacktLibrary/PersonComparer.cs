namespace Packt.Shared;

public class PersonComparer : IComparer<Person>
{
    public int Compare(Person? x, Person? y)
    {
        if (x is null || y is null)
        {
            return 0;
        }
        // сравниваем длину имени...
        int result = x.Name.Length.CompareTo(y.Name.Length);
        // ...если равны...
        if (result == 0)
        {
            // ...затем сравнниваем по именам...
            return x.Name.CompareTo(y.Name);
        }
        else // в результате должно получиться -1 или 1
        {
            // ...в противном случае сравниваем по длине
            return result;
        }
    }
}


