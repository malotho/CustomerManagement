using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public CategoryModel Category { get; set; }
    }
}
