using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Category : Auditable
    {
        public int ID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryCode { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
