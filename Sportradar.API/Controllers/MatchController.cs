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
        public readonly IMatchService matchService;

        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpGet]
        public List<MatchEntity> GetOrderedScore()
        {
            return matchService.GetOrderedScore();
        }

        [HttpPost]
        public bool StartMatch(MatchEntity match)
        {
            return matchService.StartMatch(match);
        }

        [HttpPut]
        public bool UpdateMatch(MatchEntity match)
        {
            return matchService.UpdateMatch(match);
        }

        [HttpPost]
        public bool EndMatch(MatchEntity match)
        {
            return matchService.EndMatch(match);
        }        
    }
}
