using System.ComponentModel.DataAnnotations;

public enum Priority
{
    [Display(Name = "Low")]
    Low,

    [Display(Name = "Medium")]
    Medium,

    [Display(Name = "High")]
    High
}