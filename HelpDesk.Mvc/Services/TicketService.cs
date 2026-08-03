using HelpDesk.Mvc.Models;

namespace HelpDesk.Mvc.Services
{
    public class TicketService
    {
        HttpClient _httpClient;
        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // service for getting all tickets
        public async Task<List<Ticket>?> GetAllTickets() 
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>("All");
        }
    }
}
