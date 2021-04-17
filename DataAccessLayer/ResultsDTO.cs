using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    public class ResultsDTO
    {
        //properties
        public int PlayerId { get; set; }
        public int Results { get; set; }
        public string Time { get; set; }
        public int Clicks { get; set; }

        //constructor for our data transfer object
        public ResultsDTO(int playerId, int results, string time, int clicks)
        {
            this.PlayerId = playerId;
            this.Results = results;
            this.Time = time;
            this.Clicks = clicks;
        }

        public ResultsDTO()
        {
        }
    }
}
