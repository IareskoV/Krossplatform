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
    public class LifeCyclePhasesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public LifeCyclePhasesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Life_Cycle_Phases> GetList()
        {
            return abstractContext.Life_Cycle_Phases.ToList();
        }
        [HttpGet("{id}")]
        public Life_Cycle_Phases GetList(string id)
        {
            return abstractContext.Life_Cycle_Phases.FirstOrDefault(obj => obj.Life_Cycle_Code.Equals(id));
        }

        [HttpPost]
        public Life_Cycle_Phases Create(Life_Cycle_Phases entity)
        {
            var res = abstractContext.Life_Cycle_Phases.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Life_Cycle_Phases Update(Life_Cycle_Phases entity)
        {
            var res = abstractContext.Life_Cycle_Phases.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = abstractContext.Life_Cycle_Phases.FirstOrDefault(obj => obj.Life_Cycle_Code.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}