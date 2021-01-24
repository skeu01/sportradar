using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportradar.Business;
using Sportradar.DataAccess.Models;
using Sportradar.DataAccess.Repositories;
using System.Collections.Generic;

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
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 1, TeamAwayName = "Italy", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 1 }));

            ////both teams must not exist
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.StartMatch(new MatchEntity { TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0 }));

            //insert new entity with no errors
            Assert.IsTrue(matchService.StartMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0 }));
        }

        [TestMethod]
        public void Test_Update_Match()
        {
            var matchService = new MatchService(_matchRepository);

            //Entity can not be null
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity()));

            //can not have score less than 0
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = -1, TeamAwayName = "Italy", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = -1 }));


            //both teams must exist at the same order with same teams
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0 }));


            Assert.IsTrue(matchService.UpdateMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 3, TeamAwayName = "Italy", TeamAwayScore = 0 }));
        }

        [TestMethod]
        public void Test_Delete_Match()
        {
            var matchService = new MatchService(_matchRepository);

            //Entity can not be null
            Assert.IsFalse(matchService.EndMatch(new MatchEntity()));

            //can not have score less than 0
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = -1, TeamAwayName = "Italy", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = -1 }));


            //both teams must exist at the same order with same teams
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Italy", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Uruguay", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0 }));
            Assert.IsFalse(matchService.EndMatch(new MatchEntity { TeamHomeName = "Australia", TeamHomeScore = 0, TeamAwayName = "Argentina", TeamAwayScore = 0 }));

            //delete entity from list
            Assert.IsTrue(matchService.EndMatch(new MatchEntity { TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5 }));
        }

        [TestMethod]
        public void Test_Get_Match()
        {
            var matchService = new MatchService(_matchRepository);

            //Can not be null
            Assert.IsNotNull(matchService.GetOrderScore());

            //can not return same default order
            Assert.AreNotEqual(Get_mockDefaultListMaches(), matchService.GetOrderScore());

            //can not have score less than 0
            Assert.AreEqual(Get_mockOrderedListMaches(), matchService.GetOrderScore());
        }

        private List<MatchEntity> Get_mockOrderedListMaches()
        {
            return new List<MatchEntity> {
                new MatchEntity{ TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6 },
                new MatchEntity{ TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2 },
                new MatchEntity{ TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5 },
                new MatchEntity{ TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1 },
                new MatchEntity{ TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2 },
            };
        }

        private List<MatchEntity> Get_mockDefaultListMaches()
        {
            return new List<MatchEntity> {
                new MatchEntity{ TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5 },
                new MatchEntity{ TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2 },
                new MatchEntity { TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2 },
                new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6 },
                new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1 },
            };
        }
    }
}
