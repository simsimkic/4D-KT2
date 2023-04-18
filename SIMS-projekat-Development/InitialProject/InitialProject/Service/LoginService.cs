using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class LoginService
    {
        private MyDbContext db;
        private readonly UserRepository _userRepository;

        public LoginService(MyDbContext db)
        {
            this.db = db;
        }

        public LoginService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int FindUser(string username, string password, out int userType)
        {
            List<User> userList = _userRepository.GetAllUsers();
            User user = userList.Where(u => u.Username == username && u.Password == password).FirstOrDefault();

            if (user != null)
            {
                userType = (int)user.UserType;
                return user.Id;
            }

            userType = 0;
            return 0;
        }
    }
}
