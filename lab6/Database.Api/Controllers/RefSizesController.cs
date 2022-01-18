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
    public class RefSizesController : ControllerBase
    {
        private AbstractContext abstractContext;
        public RefSizesController(AbstractContext abstractContext)
        {
            this.abstractContext = abstractContext;
        }

        [HttpGet]
        public List<Ref_Sizes> GetList()
        {
            return abstractContext.Ref_Sizes.ToList();
        }
        [HttpGet("{id}")]
        public Ref_Sizes GetList(string id)
        {
            return abstractContext.Ref_Sizes.FirstOrDefault(obj => obj.Size_Code.Equals(id));
        }

        [HttpPost]
        public Ref_Sizes Create(Ref_Sizes entity)
        {
            var res = abstractContext.Ref_Sizes.Add(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpPut("{id}")]
        public Ref_Sizes Update(Ref_Sizes entity)
        {
            var res = abstractContext.Ref_Sizes.Update(entity);
            abstractContext.SaveChanges();
            return res.Entity;
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var entity = abstractContext.Ref_Sizes.FirstOrDefault(obj => obj.Size_Code.Equals(id));
            abstractContext.Remove(entity ?? throw new InvalidOperationException());
            abstractContext.SaveChanges();
        }
    }
}
