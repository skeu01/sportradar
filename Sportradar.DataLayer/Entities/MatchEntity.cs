using System;

namespace Sportradar.DataLayer.Models
{
    public class MatchEntity
    {
        public int Id { get; set; }
        public string TeamHomeName { get; set; }
        public int TeamHomeScore { get; set; }
        public string TeamAwayName { get; set; }
        public int TeamAwayScore { get; set; }
        public DateTime Created { get; set; }
    }
}
