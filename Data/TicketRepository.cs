using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TicketRepository : DatabaseConnection, ITicketRepository
    {
        public TicketRepository(string connectionString) : base(connectionString)
        {
        }

        void ITicketRepository.AddTicket()
        {
            throw new NotImplementedException();
        }

        void ITicketRepository.DeleteTicket(int id)
        {
            throw new NotImplementedException();
        }

        Ticket? ITicketRepository.GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        List<Ticket> ITicketRepository.GetTickets()
        {
            throw new NotImplementedException();
        }
    }
}
