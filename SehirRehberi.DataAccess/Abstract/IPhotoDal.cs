using SehirRehberi.Core.DataAccess;
using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.DataAccess.Abstract
{
    public interface IPhotoDal: IEntityRepository<Photo>
    {
    }
}
