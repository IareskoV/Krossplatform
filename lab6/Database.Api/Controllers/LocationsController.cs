using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Api.Db;
using Database.Api.Models;

namespace Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private AbstractContext abstractContext;
        public LocationsController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Location> GetList()
        {
            return abstractContext.Locations.ToList();
        }
        [HttpGet("{id}")]
        public Location GetList(int id)
        {
            return abstractContext.Locations.FirstOrDefault(obj => obj.Location_ID.Equals(id));
        }

        [HttpPost]
        public Location Create(Location entity)
        {
            var res = abstractContext.Locations.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Location Update(Location entity)
        {
            var res = abstractContext.Locations.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity = abstractContext.Locations.FirstOrDefault(obj => obj.Location_ID.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
