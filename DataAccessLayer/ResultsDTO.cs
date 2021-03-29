using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    public class ResultsDTO
    {
        public int PlayerId { get; set; }
        public int Results { get; set; }
        public string Time { get; set; }
        public int Clicks { get; set; }

        public ResultsDTO(int playerId, int results, string time, int clicks)
        {
            PlayerId = playerId;
            Results = results;
            Time = time;
            Clicks = clicks;
        }

        public ResultsDTO()
        {
        }
    }
}
