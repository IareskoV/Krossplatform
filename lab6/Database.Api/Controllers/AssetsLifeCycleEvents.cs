using System;
using System.Collections.Generic;
using System.Linq;
using Database.Api.Db;
using Database.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsLifeCycleEventsController : ControllerBase
    {
        private AbstractContext abstractContext;
        public AssetsLifeCycleEventsController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Assets_Life_Cycle_Events> GetList()
        {
            return abstractContext.Assets_Life_Cycle_Events.ToList();
        }
        [HttpGet("{id}")]
        public Assets_Life_Cycle_Events GetList(int id)
        {
            return abstractContext.Assets_Life_Cycle_Events.FirstOrDefault(obj => obj.Asset_Life_Cycle_Event_ID.Equals(id));
        }

        [HttpPost]
        public Assets_Life_Cycle_Events Create(Assets_Life_Cycle_Events entity)
        {
            var res = abstractContext.Assets_Life_Cycle_Events.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Assets_Life_Cycle_Events Update(Assets_Life_Cycle_Events entity)
        {
            var res = abstractContext.Assets_Life_Cycle_Events.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity = abstractContext.Assets_Life_Cycle_Events.FirstOrDefault(obj => obj.Asset_Life_Cycle_Event_ID.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}