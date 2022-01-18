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
    public class RefStatusesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public RefStatusesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Ref_Status> GetList()
        {
            return abstractContext.Ref_Status.ToList();
        }
        [HttpGet("{id}")]
        public Ref_Status GetList(string id)
        {
            return abstractContext.Ref_Status.FirstOrDefault(obj => obj.Status_Code.Equals(id));
        }

        [HttpPost]
        public Ref_Status Create(Ref_Status entity)
        {
            var res = abstractContext.Ref_Status.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Ref_Status Update(Ref_Status entity)
        {
            var res = abstractContext.Ref_Status.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = abstractContext.Ref_Status.FirstOrDefault(obj => obj.Status_Code.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
