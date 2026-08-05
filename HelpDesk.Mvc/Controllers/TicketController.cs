using HelpDesk.Mvc.Models;
using HelpDesk.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Mvc.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _service;

        public TicketController(TicketService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _service.GetAllTickets();

            return View(tickets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            ticket.Status = Status.Open;
            ticket.CreatedDate = DateTime.Now;

            bool success = await _service.CreateTicket(ticket);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Unable to create ticket.");

            return View(ticket);
        }
    }
}
