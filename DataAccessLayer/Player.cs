using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Player
    {
        public int PlayerID { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Prefered Gender (optional)")]
        public string Gender { get; set; }
        [Display(Name = "Age (optional)")]
        public int Age { get; set; }
        [Display(Name = "State (optional)")]
        public string State { get; set; }
        [Display(Name = "Address (optional")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
