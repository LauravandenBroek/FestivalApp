using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITicketRepository
    {
        public void AddTicket();
        public List<Ticket> GetTickets();
        public Ticket? GetTicketById(int id);
        public void DeleteTicket(int id);
    }
}
