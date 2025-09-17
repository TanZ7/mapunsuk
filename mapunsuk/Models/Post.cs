using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapunsuk.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "รูปภาพหน้าปกกิจกรรม")]
        public string? CoverImage { get; set; } // สำหรับเก็บ URL ของรูปภาพ

        [Required(ErrorMessage = "กรุณากรอกชื่อกิจกรรม")]
        [Display(Name = "ชื่อกิจกรรมจิตอาสา")]
        public string Title { get; set; }

        [Required(ErrorMessage = "กรุณากรอกรายละเอียดกิจกรรม")]
        [Display(Name = "รายละเอียดกิจกรรม")]
        public string Description { get; set; }

        [Required(ErrorMessage = "กรุณากรอกสถานที่จัดกิจกรรม")]
        [Display(Name = "สถานที่จัดกิจกรรมจิตอาสา")]
        public string Location { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกประเภทกิจกรรม")]
        [Display(Name = "ประเภทกิจกรรม")]
        public string Category { get; set; }

        [Required(ErrorMessage = "กรุณาระบุวันที่เริ่มกิจกรรม")]
        [Display(Name = "วันที่เริ่มกิจกรรม")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "กรุณาระบุวันที่สิ้นสุดกิจกรรม")]
        [Display(Name = "วันที่สิ้นสุดกิจกรรม")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "กรุณาระบุจำนวนอาสาที่ต้องการ")]
        [Range(1, int.MaxValue, ErrorMessage = "จำนวนต้องเป็น 1 คนขึ้นไป")]
        [Display(Name = "จำนวนอาสาที่ต้องการ (คน)")]
        public int MaxParticipants { get; set; }

        [Required(ErrorMessage = "กรุณาระบุวันที่สิ้นสุดการรับสมัคร")]
        [Display(Name = "สิ้นสุดการรับสมัครวันที่")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "วันที่สร้างโพสต์")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "ปิดรับสมัคร")]
        public bool IsClosed { get; set; } = false;

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }
    }
}