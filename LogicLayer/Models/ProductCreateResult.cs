using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public class ProductCreateResult
    {
        public bool Created { get; set; }
        public Product Product{ get; set; }
    }
}
