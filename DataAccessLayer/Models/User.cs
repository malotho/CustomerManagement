using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User : Auditable
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string token { get; set; }
    }
}
