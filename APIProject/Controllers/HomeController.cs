using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        public /*async Task<>*/ IActionResult Index()
        {
            var responseMessage = /*await*/ client.GetAsync("http://localhost:49868/api/Products").Result;
            List<Product> products = null;
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                products = JsonConvert.DeserializeObject<List<Product>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var responseMessage = client.GetAsync("http://localhost:49868/api/Products/{id}").Result;

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var product = JsonConvert.DeserializeObject<Product>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(product.Name);
            }
            return View(id);
        }

        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = client.PostAsync("http://localhost:49868/api/Products", content).Result;
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Ekleme işleme başarısız");
            return View();
        }

        public IActionResult Edit(int id)
        {
            var response = client.GetAsync($"http://localhost:49868/api/Products/{id}").Result;
            if (response.StatusCode==HttpStatusCode.OK)
            {
                var product= JsonConvert.DeserializeObject<Product>(response.Content.ReadAsStringAsync().Result);
                return View(product);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = client.PutAsync($"http://localhost:49868/api/Products/{model.Id}", content).Result;
            //if (response.StatusCode == HttpStatusCode.NoContent)
            //{
               
            //}
            ModelState.AddModelError("", "Güncelleme işleme başarısız");
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Delete(int id)
        {
            var response = client.DeleteAsync($"http://localhost:49868/api/Products/{id}").Result;
           
            return RedirectToAction(nameof(Index));
        }
    }
}
