using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Lab6.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lab6.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient client;
        public DatabaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var jsonToken = HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(jsonToken);
            this.client = client;
        }
        public async Task<ViewResult> Assets()
        {
            var databaseHandler = new DatabaseHandler<Asset>(client, Tables.Assets);
            ViewBag.Assets = await databaseHandler.GetList();
            return View();
        }
        [HttpGet]
        public async Task<ViewResult> AssetsLifeCycleEvents()
        {
            var databaseHandler = new DatabaseHandler<Assets_Life_Cycle_Events>(client, Tables.AssetsLifeCycleEvents);
            ViewBag.Assets = await databaseHandler.GetList();
            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Locations()
        {
            var databaseHandler = new DatabaseHandler<Location>(client, Tables.Locations);
            ViewBag.Entities = await databaseHandler.GetList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateLocation(Location location)
        {
            var databaseHandler = new DatabaseHandler<Location>(client, Tables.Locations);
            await databaseHandler.Update(location);
            return RedirectToAction("Locations");
        }

        [HttpPost]
        public async Task<ActionResult> CreateLocation(Location location)
        {
            var databaseHandler = new DatabaseHandler<Location>(client, Tables.Locations);
            await databaseHandler.Create(location);
            return RedirectToAction("Locations");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteLocation(Location location)
        {
            var databaseHandler = new DatabaseHandler<Location>(client, Tables.Locations);
            await databaseHandler.Delete(location.Location_ID);
            return RedirectToAction("Locations");
        }
        [HttpGet]
        public async Task<ViewResult> RefStatuses()
        {
            var databaseHandler = new DatabaseHandler<Ref_Status>(client, Tables.RefStatuses);
            ViewBag.Entities = await databaseHandler.GetList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRefStatus(Ref_Status location)
        {
            var databaseHandler = new DatabaseHandler<Ref_Status>(client, Tables.RefStatuses);
            await databaseHandler.Create(location);
            return RedirectToAction("RefStatuses");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRefStatus(Ref_Status location)
        {
            var databaseHandler = new DatabaseHandler<Ref_Status>(client, Tables.RefStatuses);
            await databaseHandler.Update(location);
            return RedirectToAction("RefStatuses");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRefStatus(Ref_Status location)
        {
            var databaseHandler = new DatabaseHandler<Ref_Status>(client, Tables.RefStatuses);
            await databaseHandler.Delete(location.Status_Code);
            return RedirectToAction("RefStatuses");
        }
    }
}
