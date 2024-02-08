using System.ComponentModel.DataAnnotations;

namespace GooglePay.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? AccountHolderName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string? Password { get; set; }
    }
}
