﻿using System.ComponentModel.DataAnnotations;

namespace ArenaPro.Web.Model
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}