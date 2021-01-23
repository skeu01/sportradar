using Sportradar.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sportradar.Business
{
    public class MatchService : IMatchService
    {
        public Task<bool> StartMatch(List<MatchEntity> matches)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMatch(MatchEntity match)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EndMatch(MatchEntity match)
        {
            throw new NotImplementedException();
        }

        public Task<List<MatchEntity>> GetOrderScore(List<MatchEntity> matches)
        {
            throw new NotImplementedException();
        }
    }
}
