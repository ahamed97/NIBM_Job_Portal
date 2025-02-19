﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NIBM_Job_Portal.Models
{
    public class Company 
    {
        [Key]
        public int Id { get; set; }
        public string Company_Name { get; set; }
        public string Logo_path { get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email Adddress")]
        public string Email { get; set; }
        public string Physical_Address { get; set; }
        public string Contact_1 { get; set; }
        public string Contact_2 { get; set; }
        public string State { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Please Select the industry")]
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }

        public string Contact_No { get; set; }
        public string Description { get; set; }

        public int IsEnable { get; set; }

         public string Website { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
