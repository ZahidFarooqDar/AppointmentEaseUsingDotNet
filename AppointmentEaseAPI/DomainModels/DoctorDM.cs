﻿using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.DomainModels
{
    public class DoctorDM
    {
        [Key]
        public int DoctorId { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        public string Specialization { get; set; }
        public virtual ICollection<AppointmentDM> Appointments { get; set; }
    }
}
