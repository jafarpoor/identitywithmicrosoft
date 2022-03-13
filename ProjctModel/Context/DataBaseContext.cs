using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjctModel.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjctModel.Context
{
   public class DataBaseContext:IdentityDbContext<User ,Role ,int>
    {
      public  DataBaseContext() { }
        public DataBaseContext(DbContextOptions options ):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
