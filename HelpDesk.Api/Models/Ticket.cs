using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        [Required]
        public string RaisedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
