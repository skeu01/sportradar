using Sportradar.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportradar.DataAccess.Repositories
{
    public interface IMatchRepository
    {
        /// <summary>
        /// Get started matches
        /// </summary>
        /// <returns>List of matches</returns>
        IQueryable<MatchEntity> GetMatchesEntities();

        /// <summary>
        /// Add new Entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool status</returns>
        Task<bool> AddMatchEntity(MatchEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool status</returns>
        Task<bool> UpdateMatchEntity(MatchEntity entity);

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool status</returns>
        Task<bool> EndMatchEntity(MatchEntity entity);
    }
}
