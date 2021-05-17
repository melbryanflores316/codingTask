using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArenasController : ControllerBase
    {
        private readonly CodingTaskContext _context;

        public ArenasController(CodingTaskContext context)
        {
            _context = context;
        }
        [HttpPost("entry")]
        [Authorize]
        public ActionResult SubmitEntry(ScoreBoard scoreBoard)
        {
            var userName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var user= this._context.Users.FirstOrDefault(s => s.UserName == userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var match = _context.Matches.FirstOrDefault(m => m.Id == scoreBoard.MatchId);
            // check if matchID is valid
            if (match != null)
            {
                var userWithMatch = this._context.ScoreBoards.FirstOrDefault(s => s.UserId == scoreBoard.UserId);
                // check if user has already entered this match
                if (userWithMatch != null)
                {
                    return BadRequest("User has already an entry for this match");
                }

                var validMatch = match.Expiry.Ticks > DateTime.Now.Ticks;
                // check if the match is expired or not
                if (validMatch)
                {
                    scoreBoard.UserId = user.Id;
                    // scoreBoard.
                    _context.ScoreBoards.Add(scoreBoard);
                    _context.SaveChanges();
                    return Ok("Submitted successfully");
                }
                return BadRequest("Match expired");
            }

            
            return BadRequest("Match not found, or not valid");
        }

        [HttpPost("match")]
        public ActionResult AddMatch()
        {
            var expiry = DateTime.Now.AddMinutes(10);
            var match = new Match
            {
                CreatedDate = DateTime.Now,
                Name = $"Match{expiry}",
                Expiry = expiry
            };
            this._context.Matches.Add(match);
            this._context.SaveChanges();
            return Ok();
        }
        
        [HttpGet("match/all")]
        public ActionResult GetMatches()
        {
            var matches = this._context.ScoreBoards.Include(s => s.Match)
                                                                .OrderBy(s => s.Match.Expiry).ToList();
            return Ok(matches);
        }
        [HttpGet("match/{id}")]
        public ActionResult GetScoresInMatch(int id)
        {
            var scores = this._context.ScoreBoards.Where(s => s.MatchId == id).OrderByDescending(s => s.Score).ToList();
            return Ok(scores);
        }
    }
}
