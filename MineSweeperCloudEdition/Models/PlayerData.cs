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
        //business layer class that pass data between the controller and database communications layer.
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

        /// <summary>
        /// If login validation returns false, we set player id to 0, as there is no player with id -1.
        /// </summary>
        /// <param name="ply"></param>
        public void ValidatePlayer(Player ply)
        {
            if (playerDAL.ValidatePlayer(ply) == false)
            {
                ply.PlayerID = -1; //realistically could do this method in one line of code.
            }
        }

    }
}
