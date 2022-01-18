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
    public class AssetsController : ControllerBase
    {
        private AbstractContext abstractContext;
        public AssetsController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Asset> GetList()
        {
            return abstractContext.Assets.ToList();
        }
        [HttpGet("{id}")]
        public Asset GetList(int id)
        {
            return abstractContext.Assets.FirstOrDefault(obj => obj.Asset_ID.Equals(id));
        }

        [HttpPost]
        public Asset Create(Asset entity)
        {
            var res = abstractContext.Assets.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Asset Update(Asset entity)
        {
            var res = abstractContext.Assets.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity = abstractContext.Assets.FirstOrDefault(obj => obj.Asset_ID.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
