using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Mvc.Models
{
    public enum Status
    {
        Open,

        [Display(Name = "In Progress")]
        In_Progress,

        Class
    }
}
