﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exDate.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string User_Name { get; set; }    
        public string User_Email { get; set; }
        public string Address { get; set; }
        public string Country_Id { get; set; }
        public string State_Id { get; set; }
        public string District_Id { get; set; }
        public int Mobile_No { get; set; }
        public int? Password { get; set; }
        public bool RememberMe { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
       

    }
}