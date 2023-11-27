﻿using AppointmentEaseAPI.DomainModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppointmentEaseAPI.ServiceModels
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}
