using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Database.Api.Db
{
    public class SqlServerDbContext : AbstractContext
    {

        private const int UniqueConstraintViolationErrorCode = 2627;
        public SqlServerDbContext(IConfiguration configuration) : base(configuration)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetValue<string>("MsSqlConnection"));
        }

        public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
        {
            if (exception.GetBaseException() is SqlException sqlException)
            {
                return sqlException.Errors
                    .OfType<SqlError>()
                    .Any(error => error.Number == UniqueConstraintViolationErrorCode);
            }

            return false;
        }

    }
}
