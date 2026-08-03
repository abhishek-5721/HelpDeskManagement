namespace HelpDesk.Mvc.Services
{
    public class TicketService
    {
        HttpClient _httpClient;
        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
