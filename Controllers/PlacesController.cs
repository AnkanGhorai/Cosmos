using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmos.Services;
using Cosmos.Models;

namespace Cosmos.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IPlacesDBServices _placesDBServices;

        //public ItemController();
        public PlacesController(IPlacesDBServices placesDBServices)
        {
            _placesDBServices = placesDBServices;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _placesDBServices.GetItemsAsync("SELECT * FROM c"));
        }
        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Zip,Country,State")] Places places)
        {
            if (ModelState.IsValid)
            {
                //item.Id = Guid.NewGuid().ToString();
                await _placesDBServices.AddItemAsync(places);
                return RedirectToAction("Index");
            }

            return View(places);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id, string state)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Places item = await _placesDBServices.GetItemAsync(id, state);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Zip,Country,State")] Places places)
        {
            if (ModelState.IsValid)
            {
                await _placesDBServices.UpdateItemAsync(places.Id, places);
                return RedirectToAction("Index");
            }

            return View(places);
        }


        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id,string type)
        {
            if (id == null)
                return BadRequest();
            Places places = await _placesDBServices.GetItemAsync(id, type);

            if (places == null)
                return NotFound();
            return View(places);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id, string state) {
            await _placesDBServices.DeleteItemAsync(id, state);
            return RedirectToAction("Index");
        }

    }
}
