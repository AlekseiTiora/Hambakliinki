using Microsoft.EntityFrameworkCore;
using Hambakliinki.Models;

namespace Hambakliinki.Models
{
    public class db : DbContext
    {
        public static bool value = true;

        public db(DbContextOptions<db> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<klient> klient { get; set; }
        public DbSet<hambaarst> hambaarst { get; set; }
        public DbSet<hambakliinik> hambakliinik{ get; set; }
        public DbSet<teenuseid> teenuseid { get; set; }


    }

}
