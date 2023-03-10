using Microsoft.EntityFrameworkCore;
using SehirRehberi.Core.DataAccess.EntityFramework;
using SehirRehberi.DataAccess.Abstract;
using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.DataAccess.Concrete
{
    public class EfCityDal : EfEntityRepositoryBase<City, Context>, ICityDal
    {
        DbContext _context;
        public EfCityDal(DbContext context)
        {
            _context = context;
        }
        public List<City> GetAllWithPhotos(Expression<Func<City, bool>> filter = null)
        {
            return filter == null ? _context.Set<City>().Include(c => c.Photos).ToList()
                : _context.Set<City>().Include(c => c.Photos).Where(filter).ToList();
        }

        public City GetWithPhotos(Expression<Func<City, bool>> filter)
        {
            return _context.Set<City>().Include(c => c.Photos).FirstOrDefault(filter);
        }
    }
}
