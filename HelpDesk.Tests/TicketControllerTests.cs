using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HelpDesk.Tests
{
    public class TicketControllerTests
    {
        [Fact]
        public async Task GetAllTickets_ReturnsOkResult_WhenTicketsExist()
        {
            //Arrange
            var fakeTickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Title = "Login Issue",
                    Description = "Cannot login",
                    Priority = Priority.High,
                    Status = Status.Open,
                    RaisedBy = "Alice",
                    CreatedDate = DateTime.Now
                },

                new Ticket
                {
                    Id = 2,
                    Title = "Printer",
                    Description = "Printer Offline",
                    Priority = Priority.Low,
                    Status = Status.Closed,
                    RaisedBy = "Bob",
                    CreatedDate = DateTime.Now
                }
            };

            //Mock the repo
            var mockRepo = new Mock<ITicketRepository>();
            mockRepo
                .Setup(r => r.GetAllTicketsAsync())
                .ReturnsAsync(fakeTickets);

            var controller = new TicketController(mockRepo.Object);

            // Act 
            var result = await controller.GetAllTickets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTickets = Assert.IsAssignableFrom<List<Ticket>>(okResult.Value);
        }

        [Fact]
        public async Task GetTicketById_ReturnsOkResult_WhenTicketExists()
        {
            // Arrange
            var fakeTicket = new Ticket
            {
                Id = 1,
                Title = "Login Issue",
                Description = "Cannot login",
                Priority = Priority.High,
                Status = Status.Open,
                RaisedBy = "Alice",
                CreatedDate = DateTime.Now
            };

            //  Mock the repo
            var mockRepo = new Mock<ITicketRepository>();
            mockRepo.Setup(r => r.GetTicketByIdAsync(1))
                    .ReturnsAsync(fakeTicket);

            var controller = new TicketController(mockRepo.Object);

            // Act
            var result = await controller.GetTicketById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnedTicket = Assert.IsType<Ticket>(okResult.Value);

            Assert.Equal(fakeTicket.Id, returnedTicket.Id);
            Assert.Equal(fakeTicket.Title, returnedTicket.Title);
            Assert.Equal(fakeTicket.Status, returnedTicket.Status);
        }

        [Fact]
        public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();

            mockRepo.Setup(r => r.GetTicketByIdAsync(1))
                    .ReturnsAsync((Ticket?)null);

            var controller = new TicketController(mockRepo.Object);

            // Act
            var result = await controller.GetTicketById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully()
        {
            // Arrange
            var newTicket = new Ticket
            {
                Title = "Application Crash",
                Description = "Application crashes on startup",
                Priority = Priority.High,
                Status = Status.Open,
                RaisedBy = "Alice",
                CreatedDate = DateTime.Now
            };

            var mockRepo = new Mock<ITicketRepository>();

            mockRepo.Setup(r => r.CreateTicketAsync(newTicket))
                    .ReturnsAsync(10);

            var controller = new TicketController(mockRepo.Object);

            // Act
            var result = await controller.CreateNewTicket(newTicket);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal("Ticket created with Id = 10", okResult.Value);
        }

        [Fact]
        public async Task CreateTicket_ReturnsBadRequest_WhenTicketIsNull()
        {
            // Arrange
            var mockRepo = new Mock<ITicketRepository>();

            var controller = new TicketController(mockRepo.Object);

            // Act
            var result = await controller.CreateNewTicket(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Ticket is null", badRequest.Value);
        }

        [Fact]
        public async Task GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist()
        {
            // Arrange
            var fakeTickets = new List<Ticket>
    {
        new Ticket
        {
            Id = 1,
            Title = "Login Issue",
            Description = "Cannot login",
            Priority = Priority.High,
            Status = Status.Open,
            RaisedBy = "Alice",
            CreatedDate = DateTime.Now
        },

        new Ticket
        {
            Id = 2,
            Title = "Printer",
            Description = "Printer Offline",
            Priority = Priority.Low,
            Status = Status.Open,
            RaisedBy = "Bob",
            CreatedDate = DateTime.Now
        }
    };

            var mockRepo = new Mock<ITicketRepository>();

            mockRepo.Setup(r => r.GetTicketsByStatusAsync("Open"))
                    .ReturnsAsync(fakeTickets);

            var controller = new TicketController(mockRepo.Object);

            // Act
            var result = await controller.GetTicketsByStatus("Open");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnedTickets =
                Assert.IsAssignableFrom<List<Ticket>>(okResult.Value);

            Assert.Equal(2, returnedTickets.Count);

            Assert.All(returnedTickets,
                ticket => Assert.Equal(Status.Open, ticket.Status));
        }
    }
}
