using System.ComponentModel.DataAnnotations;

public enum Status
{
    [Display(Name = "Open")]
    Open,

    [Display(Name = "In Progress")]
    InProgress,

    [Display(Name = "Closed")]
    Closed
}
