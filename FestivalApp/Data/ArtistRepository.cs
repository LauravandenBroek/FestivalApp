using FestivalApp.Models;
using Microsoft.Data.SqlClient;


namespace FestivalApp.Data
{
    public class ArtistRepository : DatabaseConnection
    {

//        private readonly DatabaseConnection _databaseConnection;

        public ArtistRepository(string connectionString) 
            : base(connectionString) 
        {
//            _databaseConnection = databaseConnection;
        }

        //Add artist to database/create artist
        public void AddArtist(Artist artist)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"INSERT INTO Artist (Name, Nationality, Genre, Description, Image) 
                           VALUES (@Name, @Nationality, @Genre, @Description, @Image)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", artist.Name);
                    command.Parameters.AddWithValue("@Nationality", artist.Nationality);
                    command.Parameters.AddWithValue("@Genre", artist.Genre);
                    command.Parameters.AddWithValue("@Description", artist.Description);
                    command.Parameters.AddWithValue("@Image", artist.Image);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all artist from database
        public List<Artist> GetArtists()
        {
            List<Artist> artists = new List<Artist>();

            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                connection.Open();
                string sql = "SELECT Id, Name, Nationality, Genre, Description, Image FROM Artist";

                using var command = new SqlCommand(sql, connection);
                using var reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {

                        byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                        ? null
                        : (byte[])reader["Image"];

                        artists.Add(new Artist(
                            reader.GetInt32(0), // Id
                            reader.GetString(1), // Name
                            reader.GetString(2), // Nationality
                            reader.GetString(3), // Genre
                            reader.GetString(4), // Description
                            image
                        ));
                    }
                }
            }
            return artists;
        }

        //Get artist from database by artist ID
        public Artist? GetArtistById(int id)
        {
            using var connection = _databaseConnection.GetConnection();
            connection.Open();

            string sql = "SELECT Id, Name, Nationality, Genre, Description, Image FROM Artist WHERE id = @Id";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Artist(
                    reader.GetInt32(0), // Id
                    reader.GetString(1), // Name
                    reader.GetString(2), // Nationality
                    reader.GetString(3), // Genre
                    reader.GetString(4), // Description
                    reader["Image"] as byte[] // Image
                );
            }

            return null;
        }

        //Update artist in database
        public void UpdateArtist (Artist artist)
        {
            using var connection = _databaseConnection.GetConnection();
            connection.Open();


            string sql = @"UPDATE Artist SET Name = @Name, Nationality = @Nationality, Genre = @Genre, Description = @Description, Image = @Image, WHERE Id = @Id";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", artist.Name);
            command.Parameters.AddWithValue("@Nationality", artist.Nationality);
            command.Parameters.AddWithValue("@Genre", artist.Genre);
            command.Parameters.AddWithValue("@Description", artist.Description);
            command.Parameters.AddWithValue("@Image", artist.Image);

            command.ExecuteNonQuery();
        }

        //Delete artist from database
        public void DeleteArtist (int id)
        {
            using var connection = _databaseConnection.GetConnection();
            connection.Open();

            string sql = "DELETE FROM Artist WHERE Id = @Id";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery ();

        }
    }

}
