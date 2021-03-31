using DataAccessLayer;
using MineSweeperCloudEdition.Models;
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
                        ply.PlayerID = p.PlayerID;
                        ply.Firstname = p.Firstname;
                        ply.Lastname = p.Lastname;
                        ply.Username = p.Username;
                        ply.Password = p.Password;
                        ply.Email = p.Email;
                        ply.Gender = p.Gender;
                        ply.Age = p.Age;
                        ply.State = p.State;
                        ply.Address = p.Address;
                        isSuccessful = true;
                    }
                }
            }
            return isSuccessful;
        }
        public bool addResult(ResultsDTO rDTO)
        {
            DatabaseManager addOneResult = new DatabaseManager();
            bool isSuccesful = addOneResult.AddResult(rDTO);
            return isSuccesful;
        }
        public IEnumerable<ResultsDTO> GetAllResults()
        {
            DatabaseManager dbReadAllResults = new DatabaseManager();
            IEnumerable<ResultsDTO> allResults = dbReadAllResults.GetAllResults();
            return allResults;
        }

        public List<GameDTO> LoadGame(int PlayerID)
        {
            DatabaseManager resumeGame = new DatabaseManager();
            return resumeGame.LoadGame(PlayerID);
        }
        public void SaveGame(List<GameDTO> gameData, int playerId)
        {
            DatabaseManager GameDB = new DatabaseManager();
            GameDB.SaveGame(gameData, playerId);
        }

        public bool CheckSave(int playerID, string str)
        {
            DatabaseManager GameDB = new DatabaseManager();
            return GameDB.CheckSave(playerID, str);
        }

    }
}
