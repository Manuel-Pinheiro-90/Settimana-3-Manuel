
using Settimana_3_Manuel.Context;
using Settimana_3_Manuel.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Settimana_3_Manuel.Service
{
    public class AuthService : IAuthService

    {
        private readonly DataContext _context;
        private readonly IPasswordEncoder _passwordEncoder;

        public AuthService(DataContext context, IPasswordEncoder passwordEncoder)
        {
            _context = context;
            _passwordEncoder = passwordEncoder;
        }

        public User Login(string username, string password)
        {
            var encodedPassword = _passwordEncoder.Encode(password);
            var user = _context.Users.Include(u => u.Roles)
                                     .FirstOrDefault(u => u.Name == username && u.Password == encodedPassword);
            return user;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Register(User user)
        {
            user.Password = _passwordEncoder.Encode(user.Password);

            // Assegna il ruolo 'User' di default
            var userRole = _context.Roles.FirstOrDefault(r => r.Name == "User");
            if (userRole != null)
            {
                user.Roles.Add(userRole);
            }

            return CreateUser(user);


        }
    }
}

