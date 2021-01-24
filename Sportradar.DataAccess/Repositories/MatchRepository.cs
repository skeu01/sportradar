using Sportradar.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.DataAccess.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public List<MatchEntity> GetStartedMatchesEntities() =>
            new List<MatchEntity> { new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 0, TeamAwayName = "Australia", TeamAwayScore = 0, Created = DateTime.Now } };

        public List<MatchEntity> GetMatchesEntities() =>
            new List<MatchEntity> {
                new MatchEntity{ TeamHomeName = "Mexico", TeamHomeScore = 0, TeamAwayName = "Canada", TeamAwayScore = 5, Created = DateTime.Parse("2021/01/22 21:00") },
                new MatchEntity{ TeamHomeName = "Spain", TeamHomeScore = 10, TeamAwayName = "Brazil", TeamAwayScore = 2, Created =DateTime.Parse("2021/01/23 18:00") },
                new MatchEntity { TeamHomeName = "Germany", TeamHomeScore = 2, TeamAwayName = "France", TeamAwayScore = 2, Created = DateTime.Parse("2021/01/24 21:00") },
                new MatchEntity { TeamHomeName = "Uruguay", TeamHomeScore = 6, TeamAwayName = "Italy", TeamAwayScore = 6, Created = DateTime.Parse("2021/01/23 15:00") },
                new MatchEntity { TeamHomeName = "Argentina", TeamHomeScore = 3, TeamAwayName = "Australia", TeamAwayScore = 1, Created = DateTime.Parse("2021/01/24 17:00") }
            };
    }
}
