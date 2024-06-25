using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
  
        [Required(ErrorMessage ="Email Address is required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "confirm password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
