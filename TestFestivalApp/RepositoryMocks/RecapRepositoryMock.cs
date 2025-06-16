using Interfaces;
using Interfaces.Models;

namespace TestFestivalApp.RepositoryMocks
{
    public class RecapRepositoryMock : IRecapRepository
    {

        public List<Recap> Recaps { get; set; } = new List<Recap>
        {
        new Recap(
            id: 1,
            description: "Awakenings was magical. The lasers during Amelie Lens' set were insane.",
            rave: "Awakenings 2024",
            album: new List<byte[]>
            {
                new byte[] { 1, 2, 3 },
                new byte[] { 4, 5, 6 }
            }
        ),
        new Recap(
            id: 2,
            description: "Verknipt Warehouse was a total sweat-fest. Heavy kicks and an amazing crowd.",
            rave: "Verknipt Warehouse Special",
            album: new List<byte[]>
            {
                new byte[] { 10, 20, 30 }
            }
        ),
        new Recap(
            id: 3,
            description: "Time Warp in Mannheim was pure techno heaven. 12 hours of non-stop vibes.",
            rave: "Time Warp 2024",
            album: new List<byte[]>
            {
                new byte[] { 100, 101, 102 },
                new byte[] { 110, 111, 112 }
            }
        )
        };
        
        public void AddRecap(Recap recap, int UserId)
        {
            Recaps.Add(recap);
        }

        public void DeleteRecap(int id)
        {
            throw new NotImplementedException();
        }

        public Recap? GetRecapById(int id)
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

        List<Recap> IRecapRepository.GetRecapsByUserId(int UserId, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
