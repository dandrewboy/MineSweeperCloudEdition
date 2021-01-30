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
            DatabaseManager dbReadAllEmployees = new DatabaseManager();
            IEnumerable<Player> allEmployees = dbReadAllEmployees.GetAllPlayers();
            return allEmployees;
        }
        public bool AddPlayer(Player ply)
        {
            DatabaseManager addOnePlayer = new DatabaseManager();
            bool isSuccesful = addOnePlayer.AddPlayer(ply);
            return isSuccesful;
        }
    }
}
