using ItemsTransactions.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ItemsTransactions.Controllers
{
    public class CategoriesController : Controller
    {
         
        // base url 
        Uri baseAddress = new Uri("https://localhost:7103/api/");
        HttpClient client;
        public CategoriesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress+"Categories/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(data);
            }
            return View(categories);
        }

        public IActionResult Delete(int id)
        {
            
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "Categories/" + id).Result;
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Category category = new Category();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress+"Categories/"+ id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(data);
            }

            return View(category);
        }

        [HttpGet]

        public IActionResult Update(int id)
        {
            Category category = new Category();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Categories/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(data);
            }

            return View(category);

           
        }
        [HttpPost]
        public IActionResult Update(int id,Category newCategory)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest("Update failed");
            }

            client.BaseAddress = new Uri(baseAddress + "categories/"+id);
            //client has its default baseadress
            var updatedCategory = client.PutAsJsonAsync<Category>($"{id}", newCategory).Result;
            
            
            if (updatedCategory.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



    }
}
