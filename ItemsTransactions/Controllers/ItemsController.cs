using ItemsTransactions.Models;
using ItemTransactions.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ItemsTransactions.Controllers
{
    public class ItemsController : Controller
    {

        HttpClient client;
        Uri baseAddress = new Uri("https://localhost:7103/api/");

        public ItemsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            List<Item> items= new List<Item>();

            HttpResponseMessage response = client.GetAsync(baseAddress+"Items").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<Item>>(data);
            }
            return View(items);
        }
    
        public IActionResult Delete(int id)
        {

            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "Items/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Item item = new Item();

            HttpResponseMessage response = client.GetAsync(baseAddress+"Items/" +id).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                item=JsonConvert.DeserializeObject<Item>(data);

                return View(item);

                
            }
            return View(item);

        }

        [HttpGet]

        public IActionResult Update(int id)
        {
            Item item= new Item();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Items/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<Item>(data);
            }

            return View(item);


        }
        [HttpPost]
        public IActionResult Update(int id, Item newItem)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Update failed");
            }

            client.BaseAddress = new Uri(baseAddress + "Items/" + id);
            //client has its default baseadress
            var UpdatedItem = client.PutAsJsonAsync<Item>($"{id}", newItem).Result;


            if (UpdatedItem.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = new List<Category>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Categories/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(data);
            }
            ViewBag.Categories = categories;

            List<Unit> units = new List<Unit>();

             response = client.GetAsync(client.BaseAddress + "units/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                units = JsonConvert.DeserializeObject<List<Unit>>(data);
            }
            ViewBag.Units = units;




            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Add Item failed");
            }

            client.BaseAddress = new Uri(baseAddress + "Items/");
            //client has its default baseadress
            var AddedItem = client.PostAsJsonAsync<Item>(baseAddress+"Items/", item).Result;

            return RedirectToAction("Index");
        }
    }
}
