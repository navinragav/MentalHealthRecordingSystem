using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public enum Roles
    {
        Secretary,
        Nurse,
        Consultant
    }
    public class LogClass1
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter User Name")]
        [Display(Name = "User Name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Name Must be min 4 to 20 Characters")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }

        [Display(Name = "Role")]
        [Required]
        public Roles Role { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
    }
    public class LogDB : DbContext
    {
        public LogDB() : base("DefaultConnection")
        {

        }
        public DbSet<LogClass1> LogClass1s { get; set; }

    }
}