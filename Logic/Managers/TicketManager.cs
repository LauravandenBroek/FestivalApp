using Interfaces;

namespace Logic.Managers
{
    public class TicketManager
    {
        private readonly ITicketRepository TicketRepository;

        public TicketManager(ITicketRepository ticketRepository)
        {
            TicketRepository = ticketRepository;
        }
    }
}
