using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.Entity.Concrete
{
    public class City
    {
        public City()
        {
            Photos = new List<Photo>();
        }
        public int CityId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
