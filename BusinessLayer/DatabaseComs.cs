using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class DatabaseComs
    {
        public IEnumerable<Player> GetAllPlayers()
        {
            DatabaseManager dbReadAllPlayers = new DatabaseManager();
            IEnumerable<Player> allPlayers = dbReadAllPlayers.GetAllPlayers();
            return allPlayers;
        }
        public bool AddPlayer(Player ply)
        {
            DatabaseManager addOnePlayer = new DatabaseManager();
            bool isSuccesful = addOnePlayer.AddPlayer(ply);
            return isSuccesful;
        }
        /// <summary>
        /// The method to validate a player based off of the login; username & password.
        /// Compares that to the list of players pulled from the database.
        /// </summary>
        /// <param name="ply"></param>
        /// <returns></returns>
        public bool ValidatePlayer(Player ply)
        {
            DatabaseManager selectOnePlayer = new DatabaseManager();
            IEnumerable<Player> AllPlayers = selectOnePlayer.GetAllPlayers();
            bool isSuccessful = false; //default
            foreach (Player p in AllPlayers)
            {
                if (p.Username.ToLower() == ply.Username.ToLower())
                {
                    if (p.Password == ply.Password)
                    {
                        isSuccessful = true;
                    }
                }
            }
            return isSuccessful;
        }
    }
}
