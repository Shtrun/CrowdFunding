using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrowdFunding.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Logo { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        public int Goal { get; set; }
        
        public int Raised { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}