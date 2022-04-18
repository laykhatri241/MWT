using Microsoft.EntityFrameworkCore;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWTDbContext
{
    public class StoreAppDbCon : DbContext
    {
        public StoreAppDbCon(DbContextOptions<StoreAppDbCon> options): base(options){}

        public DbSet<User> users{ get; set; }
        public DbSet<UserRoles> userRoles { get; set; }

        public DbSet<AddressMaster> addressMasters{ get; set; }

        public DbSet<CategoryMaster> categoryMasters { get; set; }

        public DbSet<BusinessDetailsMaster> businessDetailsMasters { get; set; }

    }
}
