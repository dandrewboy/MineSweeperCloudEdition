using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    public class GameDTO
    {
        //properties
        public int Id { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public Boolean visited { get; set; }
        public Boolean live { get; set; }
        public bool flagged { get; set; }
        public int liveNeighbors { get; set; }
        public int PlayerId { get; set; }
        public string Time { get; set; }
        public int Clicks { get; set; }

        public GameDTO()
        {

        }
        //constructor for our data transfer object
        public GameDTO(int id, int row, int col, bool visited, bool live, bool flagged, int liveNeighbors, int playerId, string time, int clicks)
        {
            Id = id;
            this.row = row;
            this.col = col;
            this.visited = visited;
            this.live = live;
            this.flagged = flagged;
            this.liveNeighbors = liveNeighbors;
            PlayerId = playerId;
            Time = time;
            Clicks = clicks;
        }
    }
}
