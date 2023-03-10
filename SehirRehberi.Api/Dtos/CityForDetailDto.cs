using SehirRehberi.Entity.Concrete;

namespace SehirRehberi.Api.Dtos
{
    public class CityForDetailDto
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public List<Photo> Photos { get; set; }
        public string Description { get; set; }
    }
}
