using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext context;
        public TicketRepository(TicketDbContext context)
        {
            this.context = context;
        }
        public async Task<int> CreateTicketAsync(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();
            return ticket.Id;
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);

            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            var ticketList = await context.Tickets.ToListAsync();
            return ticketList;
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
        {
            if (!Enum.TryParse<Status>(status, true, out var ticketStatus))
            {
                return new List<Ticket>();
            }

            return await context.Tickets
                                 .Where(t => t.Status == ticketStatus)
                                 .ToListAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            context.Tickets.Update(ticket);
            
            await context.SaveChangesAsync();
        }
    }
}
