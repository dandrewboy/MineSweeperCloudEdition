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
                            ply.PlayerID = Convert.ToInt32(dataReader["EmployeeID"].ToString());
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
                            rDTO.PlayerId = Convert.ToInt32(dataReader["EmployeeID"].ToString());
                        }
                        catch
                        {
                            rDTO.PlayerId = 0;
                        }
                        rDTO.Results = Convert.ToInt32(dataReader["Results"].ToString());
                        rDTO.Time = dataReader["Time"].ToString();
                        rDTO.Clicks = Convert.ToInt32(dataReader["Clicks"].ToString());
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
    }
}
