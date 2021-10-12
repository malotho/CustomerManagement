using DataAccessLayer.Models;

namespace LogicLayer.Models
{
    public class CategoryCreateResult
    {
        public bool Created { get; set; }
        public Category Category{ get; set; }
    }
}
