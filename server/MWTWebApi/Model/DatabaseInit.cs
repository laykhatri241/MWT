using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MWTDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWTWebApi.Model
{
    public static class DatabaseInit
    {
        public static void DatabaseInitialize(this IServiceCollection service, IConfiguration Configuration)
        {
            var opsBuilder = new DbContextOptionsBuilder<StoreAppDbCon>();
            opsBuilder.UseSqlServer(Configuration.GetConnectionString("MWTConn"));

            var context = new StoreAppDbCon(opsBuilder.Options);

            new CheckDatabase(context);

            
        }
    }
}
