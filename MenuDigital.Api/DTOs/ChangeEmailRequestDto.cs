using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Api.DTOs;

public class ChangeEmailRequestDto
{
    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string NewEmail { get; set; } = string.Empty;

    [Required]
    public string CurrentPassword { get; set; } = string.Empty;
}