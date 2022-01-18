using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Api.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database.Api.Db
{
    public class SqliteDbContext : AbstractContext
    {
        private const int SqliteUniqueConstraintViolationErrorCode = 19;
        public SqliteDbContext(IConfiguration configuration):base(configuration)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetValue<string>("SqliteConnection"));
        }

        public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
        {
            return exception.InnerException is SqliteException sqliteException &&
                   sqliteException.SqliteErrorCode == SqliteUniqueConstraintViolationErrorCode;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Assets_Life_Cycle_Events>()
                .Property(o => o.Date_From)
                .HasColumnType("TEXT");
            builder.Entity<Assets_Life_Cycle_Events>()
                .Property(o => o.Date_To)
                .HasColumnType("TEXT");
        }
    }
}
