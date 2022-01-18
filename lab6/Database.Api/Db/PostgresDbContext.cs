using Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Database.Api.Db
{
    public class PostgresDbContext : AbstractContext
    {
        private const int UniqueConstraintViolationErrorCode = 23505;
        public PostgresDbContext(IConfiguration configuration) : base(configuration)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetValue<string>("PostgreSqlConnection"));
        }

        public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
        {
            return exception.InnerException is PostgresException postgresException &&
                   int.TryParse(postgresException.SqlState, out var code) &&
                   code == UniqueConstraintViolationErrorCode;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Assets_Life_Cycle_Events>()
                .Property(p => p.Date_From)
                .HasColumnType("varchar(50)");

            builder.Entity<Assets_Life_Cycle_Events>()
                .Property(p => p.Date_To)
                .HasColumnType("varchar(50)");
        }
    }
}