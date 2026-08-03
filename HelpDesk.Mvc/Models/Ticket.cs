namespace HelpDesk.Mvc.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string RaisedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
