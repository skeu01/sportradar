using System;

namespace Sportradar.DataAccess.Models
{
    public class MatchEntity
    {
        public string TeamHomeName { get; set; }
        public int TeamHomeScore { get; set; }
        public string TeamAwayName { get; set; }
        public int TeamAwayScore { get; set; }
        public DateTime Created { get; set; }
    }
}
