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
    public class PhotoManager:IPhotoService
    {
        IPhotoDal _photoDal;
        public PhotoManager(IPhotoDal photoDal)
        {
            _photoDal = photoDal;
        }

        public void Add(Photo entity)
        {
            _photoDal.Add(entity);
        }

        public int Count(Expression<Func<Photo, bool>> filter = null)
        {
            return _photoDal.Count(filter);
        }

        public void Delete(Photo entity)
        {
            _photoDal.Delete(entity);
        }

        public Photo Get(Expression<Func<Photo, bool>> filter)
        {
            return _photoDal.Get(filter);
        }

        public List<Photo> GetAll(Expression<Func<Photo, bool>> filter = null)
        {
            return _photoDal.GetAll(filter);
        }

        public void Update(Photo entity)
        {
            _photoDal.Update(entity);
        }
    }
}
