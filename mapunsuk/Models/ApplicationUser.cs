// In Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace mapunsuk.Models
{
    public class ApplicationUser : IdentityUser
    {
        // เพิ่ม Properties ที่เราต้องการ
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public string? Gender { get; set; } // ให้เป็น nullable (?) ได้ เผื่อไม่ต้องการบังคับกรอก

        // เบอร์โทรมีอยู่ใน IdentityUser อยู่แล้วในชื่อ PhoneNumber แต่เราจะใช้มัน


        // Additional custom properties can be added here
    }
}
