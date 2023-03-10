using SehirRehberi.Business.Abstract;
using SehirRehberi.DataAccess.Abstract;
using SehirRehberi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberi.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserDal _userDal;
        public AuthManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = _userDal.Get(u => u.UserName == userName);
            if (user == null) return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;
            return user;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Add(user);
            return user;
        }

        public async Task<bool> UserExist(string userName)
        {
            var user = _userDal.Get(u => u.UserName == userName);
            if (user == null) return false;
            return true;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) { if (computedHash[i] != passwordHash[i]) return false; }
                return true;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
