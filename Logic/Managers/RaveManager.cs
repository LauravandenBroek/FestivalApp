using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


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
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Location) || string.IsNullOrWhiteSpace(input.Website) || string.IsNullOrWhiteSpace(input.Ticket_link) || string.IsNullOrWhiteSpace(input.Description) || string.IsNullOrWhiteSpace(input.Time) || input.Image == null || input.Image.Length == 0)
            {
                throw new ValidationException("Please fill in all the fields.");
            }

            if (input.Name.Length > 50)
            {
                throw new ValidationException("Name can be max 50 characters.");
            }
            if (input.Location.Length > 50)
            {
                throw new ValidationException("Location can be max 50 characters.");
            }
            if (input.Website.Length > 50)
            {
                throw new ValidationException("Website can be max 50 characters.");
            }
            if (input.Ticket_link.Length > 50)
            {
                throw new ValidationException("Ticket link can be max 50 characters.");
            }

            if (input.Description.Length > 800)
            {
                throw new ValidationException("Description can be max 800 characters.");
            }

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

        public void UpdateRave(EditRaveViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Location) || string.IsNullOrWhiteSpace(input.Website) || string.IsNullOrWhiteSpace(input.Ticket_link) || string.IsNullOrWhiteSpace(input.Description) || string.IsNullOrWhiteSpace(input.Time) || input.Image == null || input.Image.Length == 0)
            {
                throw new ValidationException("Please fill in all the fields.");
            }
            if (input.Name.Length > 50)
            {
                throw new ValidationException("Name can be max 50 characters.");
            }
            if (input.Location.Length > 50)
            {
                throw new ValidationException("Location can be max 50 characters.");
            }
            if (input.Website.Length > 50)
            {
                throw new ValidationException("Website can be max 50 characters.");
            }
            if (input.Ticket_link.Length > 50)
            {
                throw new ValidationException("Ticket link can be max 50 characters.");
            }

            if (input.Description.Length > 800)
            {
                throw new ValidationException("Description can be max 800 characters.");
            }

            var updatedRave = new Rave
            {
                Id = input.Id,
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
            _raveRepository.UpdateRave(updatedRave);
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
