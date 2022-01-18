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
    public class RefAssetSupertypesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public RefAssetSupertypesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Ref_Asset_Supertypes> GetList()
        {
            return abstractContext.Ref_Asset_Supertypes.ToList();
        }
        [HttpGet("{id}")]
        public Ref_Asset_Supertypes GetList(string id)
        {
            return abstractContext.Ref_Asset_Supertypes.FirstOrDefault(obj => obj.Asset_Supertype_Code.Equals(id));
        }

        [HttpPost]
        public Ref_Asset_Supertypes Create(Ref_Asset_Supertypes entity)
        {
            var res = abstractContext.Ref_Asset_Supertypes.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Ref_Asset_Supertypes Update(Ref_Asset_Supertypes entity)
        {
            var res = abstractContext.Ref_Asset_Supertypes.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = abstractContext.Ref_Asset_Supertypes.FirstOrDefault(obj => obj.Asset_Supertype_Code.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
