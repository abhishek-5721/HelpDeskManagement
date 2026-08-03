using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Models
{
    public enum Status
    {
        Open,

        [Display(Name = "In Progress")]
        In_Progress,

        Class
    }
}
