using Sportradar.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.DataAccess.Repositories
{
    public interface IMatchRepository
    {
        /// <summary>
        /// Get started matches
        /// </summary>
        /// <returns>List of matches</returns>
        List<MatchEntity> GetStartedMatchesEntities();

        /// <summary>
        /// Get list of matches
        /// </summary>
        /// <returns></returns>
        List<MatchEntity> GetMatchesEntities();
    }
}
