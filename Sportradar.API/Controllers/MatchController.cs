using Microsoft.AspNetCore.Mvc;
using Sportradar.Business;
using Sportradar.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sportradar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        public readonly IMatchService matchService;

        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpGet]
        public async Task<List<MatchEntity>> GetOrderedScore()
        {
            return await matchService.GetOrderedScore();
        }

        [HttpPost]
        public async Task<bool> StartMatch(MatchEntity match)
        {
            return await matchService.StartMatch(match);
        }

        [HttpPut]
        public async Task<bool> UpdateMatch(MatchEntity match)
        {
            return await matchService.UpdateMatch(match);
        }

        [HttpPost]
        public async Task<bool> EndMatch(MatchEntity match)
        {
            return await matchService.EndMatch(match);
        }        
    }
}
