using Interfaces;
using Interfaces.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RecapRepository : DatabaseConnection, IRecapRepository
    {
        public RecapRepository(string connectionString) : base(connectionString)
        {

        }
        public void AddRecap(Recap recap, int UserId)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string RecapSql = @"INSERT INTO UserRecap (User_ID, Rave, Description) 
                               VALUES (@User_ID, @Rave, @Description); SELECT SCOPE_IDENTITY();";
                    int recapId;

                    using (SqlCommand command = new SqlCommand(RecapSql, connection))
                    {
                        command.Parameters.AddWithValue("@User_ID", UserId);
                        command.Parameters.AddWithValue("@Rave", recap.Rave);
                        command.Parameters.AddWithValue("@Description", recap.Description);

                        recapId = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string PhotoSql = @"INSERT INTO Album (UserRecapID, Photo) VALUES (@UserRecapID, @Photo)";

                    foreach (var photo in recap.Album)
                    {
                       
                        using (SqlCommand photoCommand = new SqlCommand(PhotoSql, connection))
                        {
                            photoCommand.Parameters.AddWithValue("@UserRecapID", recapId);
                            photoCommand.Parameters.AddWithValue("@Photo", photo);
                            photoCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error while adding a new recap.", ex);
            }
        }


        public void DeleteRecap(int id)
        {
            throw new NotImplementedException();
        }

        public Recap ? GetRecapById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recap> GetRecapsByUserId(int UserId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecap(Recap recap)
        {
            throw new NotImplementedException();
        }
    }
}
