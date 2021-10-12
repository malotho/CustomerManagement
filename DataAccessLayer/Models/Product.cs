using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Product : Auditable
    {
        public int ID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCode { get; set; }
        public string Description { get; set; }        
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
    }
}
