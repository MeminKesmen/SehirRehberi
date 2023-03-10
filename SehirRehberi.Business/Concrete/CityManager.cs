using SehirRehberi.Business.Abstract;
using SehirRehberi.DataAccess.Abstract;
using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.Business.Concrete
{
    public class CityManager : ICityService
    {
        ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public void Add(City entity)
        {
            _cityDal.Add(entity);
        }

        public int Count(Expression<Func<City, bool>> filter = null)
        {
           return _cityDal.Count(filter);
        }

        public void Delete(City entity)
        {
            _cityDal.Delete(entity);
        }

        public City Get(Expression<Func<City, bool>> filter)
        {
            return _cityDal.Get(filter);
        }

        public List<City> GetAll(Expression<Func<City, bool>> filter = null)
        {
            return _cityDal.GetAll(filter);
        }

        public List<City> GetAllWithPhotos(Expression<Func<City, bool>> filter = null)
        {
            return _cityDal.GetAllWithPhotos(filter);
        }

        public City GetWithPhotos(Expression<Func<City, bool>> filter)
        {
            return _cityDal.GetWithPhotos(filter);
        }

        public void Update(City entity)
        {
            _cityDal.Update(entity);
        }
    }
}
