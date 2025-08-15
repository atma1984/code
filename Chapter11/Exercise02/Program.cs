using static System.Console;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;


IEnumerable<Customer> customers = Get_All_Customers();
WriteLine();
WriteLine(new string('-',100));
WriteLine();
WriteLine("Please enter the sity:");
string sity_select = Console.ReadLine();


var customers_city = customers.Where(c => c.City == sity_select);
WriteLine($"Customers from city {sity_select} is count - {customers_city.Count()}:");
foreach (Customer customer in customers_city)
{
    WriteLine(customer.CompanyName);
}








static Customer[] Get_All_Customers()
{
    using (Northwind d_base = new())
    {
        IQueryable<Customer> customers;
        customers = d_base.Customers ;

        var citys = customers.Select(x => x.City).Distinct() ;

        foreach (string c in citys)
        {
            Write(c+", ");

        }

        return customers.ToArray();
    }
}