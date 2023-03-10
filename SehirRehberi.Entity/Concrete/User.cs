using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.Entity.Concrete
{
    public class User
    {
        public User()
        {
            Cities = new List<City>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<City> Cities { get; set; }
    }
}
