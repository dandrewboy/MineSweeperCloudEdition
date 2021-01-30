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
    }
}
