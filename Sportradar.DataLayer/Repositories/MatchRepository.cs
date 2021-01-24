using Sportradar.DataLayer;
using Sportradar.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportradar.DataAccess.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private MatchDBContext _dbContext;

        public MatchRepository(MatchDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<MatchEntity> GetMatchesEntities()
        {
            return _dbContext.Matches;
        }

        public async Task<bool> AddMatchEntity(MatchEntity entity)
        {
            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateMatchEntity(MatchEntity entity)
        {
            try
            {
                var exist = await _dbContext.Matches.FindAsync(entity.Id);
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EndMatchEntity(MatchEntity entity)
        {
            try
            {
                _dbContext.Matches.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }
    }
}
