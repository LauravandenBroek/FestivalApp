using Interfaces.Models;
using Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FavoriteArtistRepository : DatabaseConnection, IFavoriteArtistRepository
    {
        public FavoriteArtistRepository(string connectionString) : base(connectionString)
        {
        }
        public void AddArtistToFavorites(int userId, int artistId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"INSERT INTO UserArtist (User_ID, Artist_ID) 
                           VALUES (@User_ID, @Artist_ID)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Artist_ID", artistId);


                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Artist> GetFavoriteArtistsByUserId(int userId, int limit = 0)
        {
            var artists = new List<Artist>();

            using (var connection = GetConnection())
            {
                connection.Open();

                string sql = @"SELECT A.* 
                       FROM Artist A
                       INNER JOIN UserArtist UA ON A.ID = UA.Artist_ID
                       WHERE UA.User_ID = @UserId";

                if (limit > 0)
                {
                    sql += " ORDER BY A.Name ASC OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY";
                }

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    if (limit > 0)
                    {
                        command.Parameters.AddWithValue("@Limit", limit);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                                ? null
                                : (byte[])reader["Image"];

                            artists.Add(new Artist(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("Nationality")),
                                reader.GetString(reader.GetOrdinal("Genre")),
                                reader.GetString(reader.GetOrdinal("Description")),
                                image
                            ));
                        }
                    }
                }
            }

            return artists;
        }


        public void RemoveArtistFromFavorites(int userId, int artistId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"DELETE FROM UserArtist 
                       WHERE User_ID = @User_ID AND Artist_ID = @Artist_ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Artist_ID", artistId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

