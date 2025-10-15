using System.Net.Http.Json;
using Packt.Shared;

namespace Northwind.BlazorWasm.Client.Data
{
    public class NorthwindService : INorthwindService
    {
        private readonly HttpClient http;
        public NorthwindService(HttpClient http)
        {
            this.http = http;
        }


        public async Task<Customer> CreateCustomerAsync(Customer c)
        {
            HttpResponseMessage response = await http.PostAsJsonAsync("api/customers", c);

            return await response.Content.ReadFromJsonAsync<Customer>();
            
        }

        public async Task DeleteCustomerAsync(string id)
        {
            HttpResponseMessage response = await
http.DeleteAsync($"api/customers/{id}");
        }

        public Task<Customer?> GetCustomerAsync(string id)
        {
            return http.GetFromJsonAsync <Customer>($"api/customers/{id}");

        }

        public  Task<List<Customer>> GetCustomersAsync()
        {
            return http.GetFromJsonAsync<List<Customer>>("api/customers");
        }

        public  Task<List<Customer>> GetCustomersAsync(string country)
        {
            return http.GetFromJsonAsync <List<Customer>>($"api/customers/in/{country}");
        }

        public async Task<Customer> UpdateCustomerAsync(Customer c)
        {
            HttpResponseMessage response = await http.PutAsJsonAsync("api/customers", c);
            return await response.Content.ReadFromJsonAsync<Customer>();
        }
    }
}
