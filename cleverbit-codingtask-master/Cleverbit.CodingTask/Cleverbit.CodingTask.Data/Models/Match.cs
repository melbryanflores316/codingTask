using System;

namespace Cleverbit.CodingTask.Data.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Expiry { get; set; }
        
    }
}