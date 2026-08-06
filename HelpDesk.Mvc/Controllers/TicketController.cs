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

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _service.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _service.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            bool success = await _service.UpdateTicket(id, ticket);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Unable to update ticket.");

            return View(ticket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _service.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool success = await _service.DeleteTicket(id);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Unable to delete ticket.");

            var ticket = await _service.GetTicketById(id);

            return View(ticket);
        }

        public IActionResult FilterByStatus()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterByStatus(Status status)
        {
            var tickets = await _service.GetTicketsByStatus(status.ToString());

            ViewBag.SelectedStatus = status;

            return View(tickets);
        }

        public async Task<IActionResult> Dashboard()
        {
            var tickets = await _service.GetAllTickets();

            ViewBag.TotalTickets = tickets.Count();

            ViewBag.OpenTickets =
                tickets.Count(t => t.Status == Status.Open);

            ViewBag.InProgressTickets =
                tickets.Count(t => t.Status == Status.InProgress);

            ViewBag.ClosedTickets =
                tickets.Count(t => t.Status == Status.Closed);

            return View();
        }
    }
}
