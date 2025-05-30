using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using System.Xml.Serialization;

namespace Logic.Managers
{
    public class RaveManager
    {
        private readonly IRaveRepository _raveRepository;

        public RaveManager(IRaveRepository raveRepository)
        {
            _raveRepository = raveRepository;
        }
        public void AddRave(AddRaveViewModel input)
        {
            var newRave = new Rave
            {
                Name = input.Name,
                Location = input.Location,
                Date = input.Date,
                Website = input.Website,
                Minimum_age = input.Minimum_age,
                Ticket_link = input.Ticket_link,
                Description = input.Description,
                Time = input.Time,
                Image = input.Image

            };

            _raveRepository.AddRave(newRave);
        }

        public List<Rave> GetRaves()
        {
            return _raveRepository.GetRaves();
        }

        public void UpdateRave(Rave rave)
        {
            _raveRepository.UpdateRave(rave);
        }

        public Rave? GetRaveById(int id)
        {
            return _raveRepository.GetRaveById(id);
        }

        public void DeleteRave(int id)
        {
            _raveRepository.DeleteRave(id);
        }

        public List<Rave> GetUpcomingRaves(int count)
        {
            return _raveRepository.GetUpcomingRaves(count);
        }

        public List<Rave> GetRavesPaged(int page, int pageSize)
        {

            return _raveRepository.GetRavesPaged(page, pageSize);
        }

        public int GetTotalRaveCount()
        {
            return GetRaves().Count();
        }

    }
}
