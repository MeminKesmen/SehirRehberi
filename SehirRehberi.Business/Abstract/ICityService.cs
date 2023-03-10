using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.Business.Abstract
{
    public interface ICityService:IGenericService<City>
    {
        List<City> GetAllWithPhotos(Expression<Func<City, bool>> filter = null);
        City GetWithPhotos(Expression<Func<City, bool>> filter);
    }
}
