using System.Collections.Generic;

namespace LogicLayer.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public bool IsActive { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
