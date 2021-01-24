using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportradar.Business;
using Sportradar.DataAccess.Repositories;
using Sportradar.DataLayer;
using Sportradar.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportradar.Tests
{
    [TestClass]
    public class MatchTests
    {
        DbContextOptions<MatchDBContext> options;
        public MatchTests()
        {
            var builder = new DbContextOptionsBuilder<MatchDBContext>();
            builder.UseInMemoryDatabase("Matches");
            options = builder.Options;
        }

        [TestMethod]
        public async Task Test_Get_Match()
        {
            
            using (var context = new MatchDBContext(options))
            {
                //just to have empty database and insert same element options to compare with the result
                context.Database.EnsureDeleted();

                IMatchRepository _matchRepository = new MatchRepository(context);
                var matchService = new MatchService(_matchRepository);
                
                if (!context.Matches.Any())
                {
                    await context.Matches.AddRangeAsync(Get_mockDefaultListMaches());
                    await context.SaveChangesAsync();
                }

                //can not return same default order
                Assert.AreNotEqual(Get_mockDefaultListMaches(), await matchService.GetOrderedScore());

                //can be same list order from mock data
                CollectionAssert.AreEqual(Get_mockOrderedListMaches(), await matchService.GetOrderedScore(), new MatchComparer());
            }
        }


        [TestMethod]
        public async Task Test_Start_Match()
        {
            using (var context = new MatchDBContext(options))
            {
                IMatchRepository _matchRepository = new MatchRepository(context);
                var matchService = new MatchService(_matchRepository);

                if (!context.Matches.Any())
                {
                    await context.Matches.AddRangeAsync(Get_mockDefaultListMaches());
                    await context.SaveChangesAsync();
                }

                //Entity can not be null
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity()));

                //can not start with score more than 0
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 1, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 1, Created = DateTime.Now }));

                ////both teams must not exist
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity { Id = 1, TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity { Id = 1, TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.StartMatch(new MatchEntity { Id = 1, TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));

                //insert new entity with no errors
                Assert.IsTrue(await matchService.StartMatch(new MatchEntity { Id = 6, TeamHomeName = "EEUU", TeamHomeScore = 0, TeamAwayName = "Portugal", TeamAwayScore = 0, Created = DateTime.Now }));
            }
        }

        [TestMethod]
        public async Task Test_Update_Match()
        {
            using (var context = new MatchDBContext(options))
            {
                IMatchRepository _matchRepository = new MatchRepository(context);
                var matchService = new MatchService(_matchRepository);

                if (!context.Matches.Any())
                {
                    await context.Matches.AddRangeAsync(Get_mockDefaultListMaches());
                    await context.SaveChangesAsync();
                }

                //Entity can not be null
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity()));

                //can not have score less than 0
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = -1, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = -1, Created = DateTime.Now }));


                //both teams must exist at the same order with same teams
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));


                Assert.IsTrue(await matchService.UpdateMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 5, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
            }
        }

        [TestMethod]
        public async Task Test_End_Match()
        {
            using (var context = new MatchDBContext(options))
            {
                IMatchRepository _matchRepository = new MatchRepository(context);
                var matchService = new MatchService(_matchRepository);

                if (!context.Matches.Any())
                {
                    await context.Matches.AddRangeAsync(Get_mockDefaultListMaches());
                    await context.SaveChangesAsync();
                }

                //Entity can not be null
                Assert.IsFalse(await matchService.EndMatch(new MatchEntity()));

                //both teams must exist at the same order with same teams
                Assert.IsFalse(await matchService.EndMatch(new MatchEntity { Id = 1, TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.EndMatch(new MatchEntity { Id = 1, TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.EndMatch(new MatchEntity { Id = 1, TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));
                Assert.IsFalse(await matchService.EndMatch(new MatchEntity { Id = 1, TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));

                //delete entity from list
                Assert.IsTrue(await matchService.EndMatch(new MatchEntity { Id = 1, TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Now }));
            }
        }

       
        private List<MatchEntity> Get_mockDefaultListMaches()
        {
            return new List<MatchEntity>
            {
                new MatchEntity { Id = 1, TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Parse("2021/01/22 21:00") },
                new MatchEntity { Id = 2, TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/23 18:00") },
                new MatchEntity { Id = 3, TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/24 21:00") },
                new MatchEntity { Id = 4, TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6, Created = DateTime.Parse("2021/01/23 15:00") },
                new MatchEntity { Id = 5, TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1, Created = DateTime.Parse("2021/01/24 17:00") }
            };
        }

        private List<MatchEntity> Get_mockOrderedListMaches()
        {
            return new List<MatchEntity>
            {
                new MatchEntity { Id = 3, TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/24 21:00") },
                new MatchEntity { Id = 5, TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1, Created = DateTime.Parse("2021/01/24 17:00") },
                new MatchEntity { Id = 2, TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/23 18:00") },
                new MatchEntity { Id = 4, TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6, Created = DateTime.Parse("2021/01/23 15:00") },
                new MatchEntity { Id = 1, TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Parse("2021/01/22 21:00") },
            };
        }

        private class MatchComparer : Comparer<MatchEntity>
        {
            public override int Compare(MatchEntity x, MatchEntity y)
            {
                if (x.TeamHomeName.CompareTo(y.TeamHomeName) == 1 || x.TeamAwayName.CompareTo(y.TeamAwayName) == 1) return 1;
                return 0;
            }
        }
    }
}
