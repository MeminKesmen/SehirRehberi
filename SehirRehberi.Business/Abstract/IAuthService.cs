using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.Business.Abstract
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName,string password);
        Task<bool> UserExist(string userName);
    }
}
