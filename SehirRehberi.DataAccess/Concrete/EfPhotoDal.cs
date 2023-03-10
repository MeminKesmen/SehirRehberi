using SehirRehberi.Core.DataAccess.EntityFramework;
using SehirRehberi.DataAccess.Abstract;
using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.DataAccess.Concrete
{
    public class EfPhotoDal:EfEntityRepositoryBase<Photo, Context>,IPhotoDal
    {
    }
}
