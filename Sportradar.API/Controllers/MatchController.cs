using Microsoft.AspNetCore.Mvc;
using Sportradar.Business;
using Sportradar.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sportradar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        public readonly MatchService matchService;

        public MatchController(MatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpGet]
        public async Task<List<MatchEntity>> GetOrderScore(List<MatchEntity> matches)
        {
            return await matchService.GetOrderScore(matches);
        }

        [HttpPost]
        public async Task<bool> StartMatch(List<MatchEntity> matches)
        {
            return await matchService.StartMatch(matches);
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
