using Sportradar.DataAccess.Models;
using Sportradar.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportradar.Business
{

    public class MatchService : IMatchService
    {
        private readonly IMatchRepository matchRepository;

        public MatchService(IMatchRepository matchRepository) { this.matchRepository = matchRepository; }

        public bool StartMatch(MatchEntity match)
        {
            //get data
            List<MatchEntity> matches = matchRepository.GetStartedMatchesEntities();

            //validate match data
            if (!ValidateMatchValues(match)) return false;

            //Check if any score 
            if (match.TeamHomeScore != 0 || match.TeamAwayScore != 0) return false;

            //Check repeated team
            if (matches != null && matches.Any(x => x.TeamHomeName.Contains(match.TeamHomeName) || x.TeamHomeName.Contains(match.TeamAwayName)
                                || x.TeamAwayName.Contains(match.TeamHomeName) || x.TeamAwayName.Contains(match.TeamAwayName)))
                return false;

            matches.Add(match);

            return true;
        }

        public bool UpdateMatch(MatchEntity match)
        {
            //get data
            List<MatchEntity> matches = matchRepository.GetMatchesEntities();

            //validate match data
            if (!ValidateMatchValues(match)) return false;

            //Check if any score is negative
            if (match.TeamHomeScore < 0 || match.TeamAwayScore < 0) return false;

            //check that record exists with same teams in same order
            MatchEntity entity = matches.Where(x => x.TeamHomeName == match.TeamHomeName && x.TeamAwayName == match.TeamAwayName).FirstOrDefault();

            if (entity == null) return false;

            entity.TeamHomeScore = match.TeamHomeScore;
            entity.TeamAwayScore = match.TeamAwayScore;

            return true;
        }

        public bool EndMatch(MatchEntity match)
        {
            //get data
            List<MatchEntity> matches = matchRepository.GetMatchesEntities();

            //validate match data
            if (!ValidateMatchValues(match)) return false;

            //check that record exists with same teams in same order
            MatchEntity entity = matches.Where(x => x.TeamHomeName == match.TeamHomeName && x.TeamAwayName == match.TeamAwayName).FirstOrDefault();

            if (entity == null) return false;

            matches.Remove(entity);

            return true;
        }

        public List<MatchEntity> GetOrderedScore()
        {
            //get data
            List<MatchEntity> matches = matchRepository.GetMatchesEntities();

            return matches.OrderByDescending(x => x.Created).ToList();
        }

        /// <summary>
        /// check input match values
        /// </summary>
        /// <param name="match">match entity</param>
        /// <returns>validation</returns>
        private bool ValidateMatchValues(MatchEntity match) 
        {
            //check null match value or empty team name
            if (match == null || (match != null && string.IsNullOrEmpty(match.TeamHomeName) || string.IsNullOrEmpty(match.TeamAwayName))) return false;

            return true;
        }
    }
}
