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
    public class RefAssetTypesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public RefAssetTypesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Ref_Asset_Types> GetList()
        {
            return abstractContext.Ref_Asset_Types.ToList();
        }
        [HttpGet("{id}")]
        public Ref_Asset_Types GetList(string id)
        {
            return abstractContext.Ref_Asset_Types.FirstOrDefault(obj => obj.Asset_Type_Code.Equals(id));
        }

        [HttpPost]
        public Ref_Asset_Types Create(Ref_Asset_Types entity)
        {
            var res = abstractContext.Ref_Asset_Types.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Ref_Asset_Types Update(Ref_Asset_Types entity)
        {
            var res = abstractContext.Ref_Asset_Types.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = abstractContext.Ref_Asset_Types.FirstOrDefault(obj => obj.Asset_Type_Code.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
