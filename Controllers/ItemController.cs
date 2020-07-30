using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmos.Services;
using Cosmos.Models;

namespace Cosmos.Controllers
{
    public class ItemController : Controller
    {
        private readonly ICosmosDBService _cosmosDbService;

        //public ItemController();
        public ItemController(ICosmosDBService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Surname,Age")] People item)
        {
            if (ModelState.IsValid)
            {
                //item.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Name,Surname,Age")] People item)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(item.Surname, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id, string surname)
        {
            if (id == null)
            {
                return BadRequest();
            }

            People item = await _cosmosDbService.GetItemAsync(id, surname);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id,string surname)
        {
            if (id == null)
            {
                return BadRequest();
            }

            People item = await _cosmosDbService.GetItemAsync(id,surname);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id, string surname)
        {
            await _cosmosDbService.DeleteItemAsync(id,surname);
            return RedirectToAction("Index");
        }
    }
}
