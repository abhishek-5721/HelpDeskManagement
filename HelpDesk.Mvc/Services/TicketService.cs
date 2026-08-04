using HelpDesk.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Mvc.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;
        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // service for getting all tickets
        public async Task<List<Ticket>?> GetAllTickets() 
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>("All");
        }

        // service for getting tickets by id
        public async Task<Ticket?> GetTicketById(int id) 
        {
            return await _httpClient.GetFromJsonAsync<Ticket>($"{id}");
        }

        // service for creating a new ticket
        public async Task<bool> CreateTicket(Ticket ticket) 
        {
            var response = await _httpClient.PostAsJsonAsync<Ticket>("",ticket);
            return response.IsSuccessStatusCode;
        }

        // service to update a ticket by id
        public async Task<bool> UpdateTicket(int id, Ticket updatedTicket) 
        {
            var response = await _httpClient.PutAsJsonAsync<Ticket>($"{id}", updatedTicket);
            return response.IsSuccessStatusCode;
        }

        // service to delete a ticket
        public async Task<bool> DeleteTicket(int id) 
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        // service to get all tickets by status
        public async Task<List<Ticket>?> GetTicketsByStatus(string status) 
        {
            var ticketList = await _httpClient.GetFromJsonAsync<List<Ticket>>($"Status/{status}");
            return ticketList;
        }
    }
}
