using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPFModernVerticalMenu.Model;
using Newtonsoft.Json;

namespace WPFModernVerticalMenu.Services
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<Category>> GetCategories()
        {
            string url = "http://localhost/backendAppExamen/list_categories.php";
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public async Task<List<Product>> GetProducts()
        {
            string url = "http://localhost/backendAppExamen/list_produits.php";
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task AddCategory(Category category)
        {
            string url = "http://localhost/backendAppExamen/create_category.php";
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, content);
        }

        public async Task UpdateCategory(Category category)
        {
            string url = "http://localhost/backendAppExamen/update_category.php";
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync(url, content);
        }

        public async Task DeleteCategory(int id)
        {
            string url = "http://localhost/backendAppExamen/delete_category.php";
            var data = new { id };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, content);
        }

        public async Task AddProduct(Product product)
        {
            string url = "http://localhost/backendAppExamen/create_produit.php"; // Assurez-vous que cette URL est correcte
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from server: {responseBody}"); // Log the response for debugging
        }



        public async Task UpdateProduct(Product product)
        {
            string url = "http://localhost/backendAppExamen/update_produit.php"; // Assurez-vous que cette URL est correcte
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from server: {responseBody}"); // Log the response for debugging
        }


        public async Task DeleteProduct(int id)
        {
            string url = "http://localhost/backendAppExamen/delete_produit.php";
            var data = new { id };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, content);
        }
    }
}
