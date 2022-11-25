using Microsoft.EntityFrameworkCore;
using Hambakliinki.Models;

namespace Hambakliinki.Models
{
    public class db : DbContext
    {
        public DbSet<klient> klient { get; set; }
        public DbSet<hambaarst> hambaarst { get; set; }
        public DbSet<hambakliinik> hambakliinik { get; set; }
        public DbSet<teenuseid> teenuseid { get; set; }

        public db(DbContextOptions<db> options) : base(options)
        {
            Database.EnsureCreated();
        }

       

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<klient>().HasData(
                new klient
                {
                    Id = 1,
                    nimi = "aleksei",
                    perekonnanimi = "Tiora",
                    Email = "aleksei@gmail.com",
                    Phone = "+372 4329 2873",
                    vanus = 18
                },
                new klient
                {
                    Id = 2,
                    nimi = "artjom",
                    perekonnanimi = "Kabilov",
                    Email = "artjom.kabilov@gmail.com",
                    Phone = "+372 2340 8472",
                    vanus = 19
                }
                );
            modelBuilder.Entity<teenuseid>().HasData(
                new teenuseid { teenuseidId = 1, teenuse = "Visiiditasu", hind = 10 },
                new teenuseid { teenuseidId = 2, teenuse = "3D röntgen", hind = 60 },
                new teenuseid { teenuseidId = 3, teenuse = "Konsultatsioon koos raviplaaniga", hind = 35 - 100 },
                new teenuseid { teenuseidId = 4, teenuse = "Valguskõvastuv täidis (väike)", hind = 50 },
                new teenuseid { teenuseidId = 5, teenuse = "Ravimi asetamine närvile", hind = 20 },
                new teenuseid { teenuseidId = 6, teenuse = "Juurekanali avamine (3 kanalit)", hind = 80 },
                new teenuseid { teenuseidId = 7, teenuse = "Hamba raske eemaldamine", hind = 100 },
                new teenuseid { teenuseidId = 8, teenuse = "Hamba eemaldamine", hind = 50 }
                );
            modelBuilder.Entity<hambaarst>().HasData(
                new hambaarst
                {
                    hambaarstId = 1,
                    nimi = "Nikita",
                    perekonnanimi = "Rimitsen",
                    spetsialiseerumine = "Üldine ennetus"

                },
                new hambaarst
                {
                    hambaarstId = 2,
                    nimi = "Oleg",
                    perekonnanimi = "Metrovski",
                    spetsialiseerumine = "Täidised"

                },
                new hambaarst
                {
                    hambaarstId = 3,
                    nimi = "Sergey",
                    perekonnanimi = "Zhzhonov",
                    spetsialiseerumine = "Juureravi"

                },
                new hambaarst
                {
                    hambaarstId = 4,
                    nimi = "Arina",
                    perekonnanimi = "Mihhailova",
                    spetsialiseerumine = "Kirurgilised hambaraviteenused"

                }
                );
        }

    }

}
