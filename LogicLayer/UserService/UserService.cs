using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        int UsersCount();
        User GetById(int id);
        User Add(User user);
        void Update(User user);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Add(User user)
        {
            // validation
            if (string.IsNullOrWhiteSpace(user.password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.username == user.username))
                throw new AppException("Username \"" + user.username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.password, out passwordHash, out passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam)
        {
            var user = _context.Users.Find(userParam.id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.username) && userParam.username != user.username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.username == userParam.username))
                    throw new AppException("Username " + userParam.username + " is already taken");

                user.username = userParam.username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.firstName))
                user.firstName = userParam.firstName;

            if (!string.IsNullOrWhiteSpace(userParam.lastName))
                user.lastName = userParam.lastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(user.password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.password, out passwordHash, out passwordSalt);

                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public int UsersCount()
        {
            return _context.Users.Count();
        }
    }
}
