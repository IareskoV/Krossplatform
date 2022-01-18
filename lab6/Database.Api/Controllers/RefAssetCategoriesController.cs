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
    public class RefAssetCategoriesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public RefAssetCategoriesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Ref_Asset_Categories> GetList()
        {
            return abstractContext.Ref_Asset_Categories.ToList();
        }
        [HttpGet("{id}")]
        public Ref_Asset_Categories GetList(string id)
        {
            return abstractContext.Ref_Asset_Categories.FirstOrDefault(obj => obj.Asset_Category_Code.Equals(id));
        }

        [HttpPost]
        public Ref_Asset_Categories Create(Ref_Asset_Categories entity)
        {
            var res = abstractContext.Ref_Asset_Categories.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Ref_Asset_Categories Update(Ref_Asset_Categories entity)
        {
            var res = abstractContext.Ref_Asset_Categories.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = abstractContext.Ref_Asset_Categories.FirstOrDefault(obj => obj.Asset_Category_Code.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
