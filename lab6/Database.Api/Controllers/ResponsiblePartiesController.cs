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
    public class ResponsiblePartiesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public ResponsiblePartiesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Responsible_Party> GetList()
        {
            return abstractContext.Responsible_Party.ToList();
        }
        [HttpGet("{id}")]
        public Responsible_Party GetList(int id)
        {
            return abstractContext.Responsible_Party.FirstOrDefault(obj => obj.Party_ID.Equals(id));
        }

        [HttpPost]
        public Responsible_Party Create(Responsible_Party entity)
        {
            var res = abstractContext.Responsible_Party.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Responsible_Party Update(Responsible_Party entity)
        {
            var res = abstractContext.Responsible_Party.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity = abstractContext.Responsible_Party.FirstOrDefault(obj => obj.Party_ID.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
