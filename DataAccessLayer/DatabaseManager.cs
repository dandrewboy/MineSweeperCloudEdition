using MineSweeperCloudEdition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class DatabaseManager
    {
        public string dbConnection { get; set; }

        public DatabaseManager()
        {
            this.dbConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbMineSweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            List<Player> plyList = new List<Player>();
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAllPLayers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Player ply = new Player();
                        try
                        {
                            ply.PlayerID = Convert.ToInt32(dataReader["PlayerID"].ToString());
                        }
                        catch
                        {
                            ply.PlayerID = 0;
                        }
                        ply.Firstname = dataReader["Firstname"].ToString();
                        ply.Lastname = dataReader["Lastname"].ToString();
                        ply.Username = dataReader["Username"].ToString();
                        ply.Password = dataReader["Password"].ToString();
                        ply.Gender = dataReader["Gender"].ToString();
                        ply.Age = Convert.ToInt32(dataReader["Age"]);
                        ply.Address = dataReader["Address"].ToString();
                        ply.State = dataReader["State"].ToString();
                        ply.Email = dataReader["Email"].ToString();
                        plyList.Add(ply);
                    }
                }
                conn.Close();
            }
            return plyList;
        }
        public bool AddPlayer(Player ply)
        {
            bool isSuccessful = true;
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertPlayer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Firstname", ply.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", ply.Lastname);
                    cmd.Parameters.AddWithValue("@Username", ply.Username);
                    cmd.Parameters.AddWithValue("@Password", ply.Password);
                    cmd.Parameters.AddWithValue("@Gender", ply.Gender);
                    cmd.Parameters.AddWithValue("@Age", ply.Age);
                    cmd.Parameters.AddWithValue("@Address", ply.Address);
                    cmd.Parameters.AddWithValue("@State", ply.State);
                    cmd.Parameters.AddWithValue("@Email", ply.Email);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return isSuccessful;
        }

        public IEnumerable<ResultsDTO> GetAllResults()
        {
            List<ResultsDTO> rList = new List<ResultsDTO>();
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAllResults", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ResultsDTO rDTO = new ResultsDTO();
                        try
                        {
                            rDTO.PlayerId = Convert.ToInt32(dataReader["PlayerId"].ToString());
                        }
                        catch
                        {
                            rDTO.PlayerId = 0;
                        }
                        rDTO.Results = Convert.ToInt32(dataReader["Result"].ToString());
                        rDTO.Time = dataReader["Time"].ToString();
                        rDTO.Clicks = Convert.ToInt32(dataReader["Clicks"].ToString());
                        rList.Add(rDTO);
                    }
                }
                conn.Close();
            }
            return rList;
        }
        public bool AddResult(ResultsDTO rDTO)
        {
            bool isSuccessful = true;
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertResult", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlayerId", rDTO.PlayerId);
                    cmd.Parameters.AddWithValue("@Result", rDTO.Results);
                    cmd.Parameters.AddWithValue("@Time", rDTO.Time);
                    cmd.Parameters.AddWithValue("@Clicks", rDTO.Clicks);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return isSuccessful;
        }

        public bool CheckSave(int playerID, string str)
        {
            bool returning;
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SelectGAMEWhere", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlayerID", playerID);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        returning = true;
                    }
                    else
                    {
                        returning = false;
                    }
                }
                conn.Close();
            }
            if (str.Equals("insert"))
            {
                CheckSaveOverwrite(returning, playerID);
            }
            return returning;
        }

        public void CheckSaveOverwrite(bool overwrite, int playerID)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                if (overwrite == true)
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeleteGameData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PlayerID", playerID);
                        conn.Open();
                        cmd.ExecuteReader();
                    }
                }
                conn.Close();
            }
        }

        public void SaveGame(List<GameDTO> gameData, int PlayerId)
        {
            CheckSave(PlayerId,"insert");
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                foreach (GameDTO cdto in gameData)
                {
                    using (SqlCommand cmd = new SqlCommand("usp_InsertGameDTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", cdto.Id);
                        cmd.Parameters.AddWithValue("@row", cdto.row);
                        cmd.Parameters.AddWithValue("@col", cdto.col);
                        cmd.Parameters.AddWithValue("@visited", cdto.visited);
                        cmd.Parameters.AddWithValue("@live", cdto.live);
                        cmd.Parameters.AddWithValue("@flagged", cdto.flagged);
                        cmd.Parameters.AddWithValue("@liveNeighbors", cdto.liveNeighbors);
                        cmd.Parameters.AddWithValue("@playerId", cdto.PlayerId);
                        cmd.Parameters.AddWithValue("@time", cdto.Time);
                        cmd.Parameters.AddWithValue("@clicks", cdto.Clicks);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        public List<GameDTO> LoadGame(int PlayerId)
        {
            List<GameDTO> gameList = new List<GameDTO>();
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SelectGAMEWhere", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlayerID", PlayerId);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        GameDTO cdto = new GameDTO();
                        cdto.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        cdto.row = Convert.ToInt32(dataReader["Row"].ToString());
                        cdto.col = Convert.ToInt32(dataReader["Col"]);
                        cdto.visited = Convert.ToBoolean(dataReader["Visited"].ToString());
                        cdto.live = Convert.ToBoolean(dataReader["Live"].ToString());
                        cdto.flagged = Convert.ToBoolean(dataReader["Flagged"].ToString());
                        cdto.liveNeighbors = Convert.ToInt32(dataReader["LiveNeighbors"].ToString());
                        cdto.PlayerId = Convert.ToInt32(dataReader["PlayerId"].ToString());
                        cdto.Time = dataReader["Time"].ToString();
                        cdto.Clicks = Convert.ToInt32(dataReader["Clicks"].ToString());
                        gameList.Add(cdto);
                    }
                }
                conn.Close();
            }
            return gameList;
        }


        
    }
}
