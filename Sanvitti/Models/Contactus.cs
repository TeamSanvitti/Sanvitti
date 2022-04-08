using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sanvitti.Models
{
    public class Contactus
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(50)]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Use letters and space only")]

        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(100)]

        public string Email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Mobile")]

        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Comment")]

        [StringLength(500)]

        public string Comment { get; set; }


    }
}
