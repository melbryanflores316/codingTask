using System.ComponentModel.DataAnnotations;

namespace Cleverbit.CodingTask.Data.Models
{
    public class ScoreBoard
    {
        public int Id { get; set; }
        [Range(0, 100)]
        public int Score { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}