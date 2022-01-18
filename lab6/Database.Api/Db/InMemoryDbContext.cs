using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database.Api.Db
{
    public class InMemoryDbContext : AbstractContext
    {
        public InMemoryDbContext(IConfiguration configuration) : base(configuration)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("inmem");
        }

        public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
        {
            throw new NotImplementedException();
        }
    }
}
