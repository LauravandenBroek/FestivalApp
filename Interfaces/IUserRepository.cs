using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Models;

namespace Interfaces
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public User? GetUserByEmail(string email);

    }
}
