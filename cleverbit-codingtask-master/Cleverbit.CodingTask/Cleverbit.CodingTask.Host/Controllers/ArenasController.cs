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
        // [Authorize]
        public ActionResult SubmitEntry(ScoreBoard scoreBoard)
        {
            var userName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var user = this._context.Users.FirstOrDefault(s => s.UserName == userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            scoreBoard.UserId = this._context.Users.FirstOrDefault(s => s.UserName == user.UserName).Id;
            var match = _context.Matches.FirstOrDefault(m => m.Id == scoreBoard.MatchId);
            // check if matchID is valid
            if (match != null)
            {
                var userWithMatch = this._context.ScoreBoards.FirstOrDefault(s => scoreBoard.MatchId == s.MatchId && s.UserId == scoreBoard.UserId);
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

        [HttpGet("match")]
        public ActionResult GetMatches()
        {
            var matches = this._context.Matches.OrderBy(s => s.Expiry).ToList();
            return Ok(matches);
        }
        [HttpGet("match/{id}")]
        [Authorize]
        public ActionResult GetMatch(int id)
        {
            var userName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var user = this._context.Users.FirstOrDefault(s => s.UserName == userName);
            var scores = this._context.Matches.Select(s => new
            {
                s.Id,
                s.Name,
                s.Expiry,
                canPlay = GetConfig(id, user)
            }).FirstOrDefault(s => s.Id == id);
            return Ok(scores);
        }
        [HttpGet("scores")]
        public ActionResult GetScores()
        {
            var matches = this._context.ScoreBoards
                .Include(s => s.Match)
                .Include(s => s.User)
                .OrderBy(s => s.Match.Expiry)
                .Select(s => new
                {
                    matchName = s.Match.Name,
                    username = s.User.UserName,
                    s.Score,
                    s.Id
                })
                .ToList();
            return Ok(matches);
        }

        [HttpGet("scoreboard/{matchId}")]
        public ActionResult GetScoreBoard(int matchId)
        {
            var result = this._context.ScoreBoards
                .Include(s => s.Match)
                .Include(s => s.User)
                .Where(s => s.MatchId == matchId)
                .Select(s => new
                {
                    
                    s.Score,
                    s.Id,
                    UserName = s.User.UserName,
                    MatchName = s.Match.Name

                }).ToList();
            return Ok(result);
        }

        [HttpGet("scores/{id}")]
        [Authorize]
        public ActionResult GetScoresInMatch(int id)
        {
             var userName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var user = this._context.Users.FirstOrDefault(s => s.UserName == userName);
            var scores = this._context.ScoreBoards
                .Where(s => s.MatchId == id && s.UserId == user.Id)
                .Include(s => s.Match).Include(s => s.User).Select(s => new 
            {
                Match = s.Match,
                MatchId = s.MatchId,
                UserId = s.UserId,
                User = new User { Id = s.UserId, UserName = s.User.UserName },
                Id = s.Id,
                Score = s.Score,
                canPlay = GetConfig(id, user)
            }).OrderByDescending(s => s.Score).FirstOrDefault();
            return Ok(scores);
        }

        public Boolean GetConfig(int matchId, User user)
        {
            var canPlay = false;

            
            var scoreBoard = _context.ScoreBoards.Include(s => s.Match).FirstOrDefault(m => m.Match.Id == matchId && user.Id == m.UserId);
            var match = _context.Matches.FirstOrDefault(s => s.Id == matchId);
            if (match == null)
            {
                return false;
            }
            if (scoreBoard == null)
            {
                var matchActive = match.Expiry.Ticks > DateTime.Now.Ticks;
                return matchActive;
            }
            else
            {
                return false;
            }
        }
    }
}
