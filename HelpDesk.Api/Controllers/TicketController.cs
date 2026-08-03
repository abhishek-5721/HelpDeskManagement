using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.Api.Models;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        ITicketRepository repo;

        public TicketController()
        {
            this.repo = repo;
        }

        // For getting all tickets
        [HttpGet("All")]
        public async Task<IActionResult> GetAllTickets() 
        {
            var ticketList = await repo.GetAllTicketsAsync();
            return Ok(ticketList);
        }

        // For getting ticket  by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id) 
        {
            var ticket = await  repo.GetTicketByIdAsync(id);

            if (ticket == null) 
            {
                return NotFound(ticket);
            }

            return Ok(ticket);
        }

        // To create a new ticket
        [HttpPost]
        public async Task<IActionResult> CreateNewTicket(Ticket ticket) 
        {
            if (ticket == null) 
            {
                return BadRequest("Ticket is null");
            }

            int id = await repo.CreateTicketAsync(ticket);

            return Ok("Product created with Id = " + id);
        }

        // To update an existing ticket
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Ticket ticket) 
        {
            await repo.UpdateTicketAsync(ticket);
            return Ok();
        }

        // To  delete an existing ticket
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id) 
        {
            await repo.DeleteTicketAsync(id);
            return Ok("Record Deleted");
        }

        // To get all tickets by status
        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetTicketsByStatus(string status) 
        {
            var ticketList = await repo.GetTicketsByStatusAsync(status);

            return Ok(ticketList);
        }
    }
}
