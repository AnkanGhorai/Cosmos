using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmos.Services;
using Cosmos.Models;


namespace Cosmos.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsDBServices _productsDBServices;

        //public ItemController();
        public ProductsController(IProductsDBServices productsDBServices)
        {
            _productsDBServices = productsDBServices;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productsDBServices.GetItemsAsync("SELECT * FROM c"));
        }
        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Type,Name,Model")] Products item)
        {
            if (ModelState.IsValid)
            {
                //item.Id = Guid.NewGuid().ToString();
                await _productsDBServices.AddItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id, string type)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Products item = await _productsDBServices.GetItemAsync(id, type);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Type,Name,Model")] Products products)
        {
            if (ModelState.IsValid)
            {
                await _productsDBServices.UpdateItemAsync(products.Type, products);
                return RedirectToAction("Index");
            }

            return View(products);
        }


        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id, string type)
        {
            if (id == null)
                return BadRequest();
            Products products = await _productsDBServices.GetItemAsync(id, type);

            if (products == null)
                return NotFound();
            return View(products);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id, string type)
        {
            await _productsDBServices.DeleteItemAsync(id, type);
            return RedirectToAction("Index");
        }
    }
}
