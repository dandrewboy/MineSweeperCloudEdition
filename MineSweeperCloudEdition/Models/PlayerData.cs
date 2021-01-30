using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using DataAccessLayer;

namespace MineSweeperCloudEdition.Models
{
    public class PlayerData
    {
        DatabaseComs playerDAL = new DatabaseComs();
        public IEnumerable<Player> GetAllPlayers()
        {
            IEnumerable<Player> allPlayers = playerDAL.GetAllPlayers();
            return allPlayers;
        }

        public void AddPlayer(Player ply)
        {
            playerDAL.AddPlayer(ply);
        }

    }
}
