using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportradar.Business;
using Sportradar.DataAccess.Models;
using System.Collections.Generic;

namespace Sportradar.Tests
{
    [TestClass]
    public class MatchTests
    {
        private readonly IMatchService matchService;
        private readonly List<MatchEntity> mockEntities;


        public MatchTests()
        {
            this.mockEntities = new List<MatchEntity>();
            this.matchService = new MatchService();
        }

        private List<MatchEntity> Get_mochListMaches()
        {
            return new List<MatchEntity> {
                new MatchEntity{ TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5 },
                new MatchEntity{ TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2 },
                new MatchEntity{ TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2 },
                new MatchEntity{ TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6 },
                new MatchEntity{ TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1 },
            };
        }
    }
}
