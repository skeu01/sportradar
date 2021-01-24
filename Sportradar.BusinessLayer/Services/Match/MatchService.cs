using Microsoft.EntityFrameworkCore;
using Sportradar.DataAccess.Repositories;
using Sportradar.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportradar.Business
{

    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository) { _matchRepository = matchRepository; }

        public async Task<List<MatchEntity>> GetOrderedScore()
        {
            //get data
            List<MatchEntity> matches = await _matchRepository.GetMatchesEntities().ToListAsync();

            return matches.OrderByDescending(x => x.Created).ToList();
        }

        public async Task<bool> StartMatch(MatchEntity match)
        {
            //get data
            List<MatchEntity> matches = await _matchRepository.GetMatchesEntities().ToListAsync();

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


        public async Task<bool> UpdateMatch(MatchEntity match)
        {
            //get data
            List<MatchEntity> matches = await _matchRepository.GetMatchesEntities().ToListAsync();

            //validate match data
            if (!ValidateMatchValues(match)) return false;

            //Check if any score is negative
            if (match.TeamHomeScore < 0 || match.TeamAwayScore < 0) return false;

            //check that record exists with same teams in same order
            MatchEntity entity = matches.Where(x => x.TeamHomeName == match.TeamHomeName && x.TeamAwayName == match.TeamAwayName).FirstOrDefault();

            if (entity == null) return false;

            return await _matchRepository.UpdateMatchEntity(entity);
        }

        public async Task<bool> EndMatch(MatchEntity match)
        {
            //get data
            List<MatchEntity> matches = await _matchRepository.GetMatchesEntities().ToListAsync();

            //validate match data
            if (!ValidateMatchValues(match)) return false;

            //check that record exists with same teams in same order
            MatchEntity entity = matches.Where(x => x.TeamHomeName == match.TeamHomeName && x.TeamAwayName == match.TeamAwayName).FirstOrDefault();

            if (entity == null) return false;

            return await _matchRepository.EndMatchEntity(entity);
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
