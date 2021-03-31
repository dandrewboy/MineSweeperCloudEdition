using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using DataAccessLayer;

namespace MineSweeperCloudEdition.Models
{
    public class GameData
    {
        DatabaseComs gameDAL = new DatabaseComs();
        public List<GameDTO> LoadGame(int PlayerID)
        {
            List<GameDTO> LoadGame = gameDAL.LoadGame(PlayerID);
            return LoadGame;
        }

        public void SaveGame(List<GameDTO> game, int playerId)
        {
            gameDAL.SaveGame(game, playerId);
        }

        public bool CheckSave(int playerID, string str)
        {
            bool save = gameDAL.CheckSave(playerID, str);
            return save;
        }
    }
}
