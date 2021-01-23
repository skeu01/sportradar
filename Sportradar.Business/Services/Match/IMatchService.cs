﻿using Sportradar.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sportradar.Business
{
    public interface IMatchService
    {
        /// <summary>
        /// Start a game
        /// </summary>
        /// <param name="matches">List of matches</param>
        /// <returns></returns>
        Task<bool> StartMatch(List<MatchEntity> matches);

        /// <summary>
        ///  Update match
        /// </summary>
        /// <param name="match">Match</param>
        /// <returns></returns>
        Task<bool> UpdateMatch(MatchEntity match);

        /// <summary>
        /// Remove match from list
        /// </summary>
        /// <param name="match">match</param>
        /// <returns>list of matches without the removed</returns>
        Task<bool> EndMatch(MatchEntity match);

        /// <summary>
        /// Set order of matches depending of the scores
        /// </summary>
        /// <param name="matches">list of maches</param>
        /// <returns>ordered list of matches</returns>
        Task<List<MatchEntity>> GetOrderScore(List<MatchEntity> matches);
    }
}