using Sportradar.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.DataAccess.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public List<MatchEntity> GetStartedMatchesEntities() =>
            new List<MatchEntity> { new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Australia", TeamAwayScore = 0 } };

        public List<MatchEntity> GetMatchesEntities() =>
            new List<MatchEntity> {
                new MatchEntity{ TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5 },
                new MatchEntity{ TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2 },
                new MatchEntity { TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2 },
                new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6 },
                new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1 },
            };
    }
}
