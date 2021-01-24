using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportradar.Business;
using Sportradar.DataAccess.Models;
using Sportradar.DataAccess.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sportradar.Tests
{
    [TestClass]
    public class MatchTests
    {
        private IMatchRepository _matchRepository = new MatchRepository();


        [TestMethod]
        public void Test_Start_Match()
        {
            var matchService = new MatchService(_matchRepository);
            //Entity can not be null
            Assert.IsFalse(matchService.StartMatch(new MatchEntity()));

            //can not start with score more than 0
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 1, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 1, Created = DateTime.Now }));

            ////both teams must not exist
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));

            //insert new entity with no errors
            Assert.IsTrue(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
        }

        [TestMethod]
        public void Test_Update_Match()
        {
            var matchService = new MatchService(_matchRepository);

            //Entity can not be null
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity()));

            //can not have score less than 0
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = -1, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = -1, Created = DateTime.Now }));


            //both teams must exist at the same order with same teams
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));


            Assert.IsTrue(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 3, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
        }

        [TestMethod]
        public void Test_Delete_Match()
        {
            var matchService = new MatchService(_matchRepository);

            //Entity can not be null
            Assert.IsFalse(matchService.EndMatch(new MatchEntity()));

            //both teams must exist at the same order with same teams
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0, Created = DateTime.Now }));

            //delete entity from list
            Assert.IsTrue(matchService.EndMatch(new MatchEntity { TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Now }));
        }

        [TestMethod]
        public void Test_Get_Match()
        {
            var matchService = new MatchService(_matchRepository);

            //Can not be null
            Assert.IsNotNull(matchService.GetOrderedScore());

            //can not return same default order
            Assert.AreNotEqual(Get_mockDefaultListMaches(), matchService.GetOrderedScore());

            //can be same list order from mock data
            CollectionAssert.AreEqual(Get_mockOrderedListMaches(), matchService.GetOrderedScore(), new MatchComparer());
        }

        private List<MatchEntity> Get_mockDefaultListMaches()
        {
            return new List<MatchEntity>
            {
                new MatchEntity { TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Parse("2021/01/22 21:00") },
                new MatchEntity { TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/23 18:00") },
                new MatchEntity { TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/24 21:00") },
                new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6, Created = DateTime.Parse("2021/01/23 15:00") },
                new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1, Created = DateTime.Parse("2021/01/24 17:00") }
            };
        }

        private List<MatchEntity> Get_mockOrderedListMaches()
        {
            return new List<MatchEntity>
            {
                new MatchEntity { TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/24 21:00") },
                new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1, Created = DateTime.Parse("2021/01/24 17:00") },
                new MatchEntity { TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/23 18:00") },
                new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6, Created = DateTime.Parse("2021/01/23 15:00") },
                new MatchEntity { TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Parse("2021/01/22 21:00") },
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
