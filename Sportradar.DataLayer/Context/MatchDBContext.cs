using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sportradar.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sportradar.DataLayer
{
    public class MatchDBContext : DbContext
    {
        public MatchDBContext(DbContextOptions<MatchDBContext> options)
            : base(options) { }

        public DbSet<MatchEntity> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

    

    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MatchDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<MatchDBContext>>()))
            {
                
                if (context.Matches.Any())
                {
                    return;
                }

                context.Matches.AddRange(
                    new List<MatchEntity> {
                new MatchEntity{ Id= 1, TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Parse("2021/01/22 21:00") },
                new MatchEntity{ Id= 2, TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2, Created =DateTime.Parse("2021/01/23 18:00") },
                new MatchEntity{ Id= 3 ,TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/24 21:00") },
                new MatchEntity{ Id= 4 ,TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6, Created = DateTime.Parse("2021/01/23 15:00") },
                new MatchEntity{ Id= 5 ,TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1, Created = DateTime.Parse("2021/01/24 17:00") }
                });

                context.SaveChanges();
            }
        }
    }
}
