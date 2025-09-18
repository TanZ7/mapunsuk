using System.ComponentModel.DataAnnotations;

namespace mapunsuk.Models
{
    public class ManageProfileViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        // For display only
        public string Email { get; set; }
    }
}