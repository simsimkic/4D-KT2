using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Serializer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class UserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext context)
        {
            _dbContext = context;
        }

        public UserRepository()
        {
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Set<User>().ToList();
        }
        
        public User GetById(int id)
        {
            List<User> users = GetAllUsers();
            User user = new User();

            foreach (User u in users)
            {
                if (u.Id == id)
                {
                    user = u;
                }
            }
            return user;
        }

        /* public List<Guide> GetAllGuides()
         {
             return _dbContext.Set<User>().OfType<Guide>().ToList();
         }
         public Guide GetById(int id)
         {
             List<Guide> guides = GetAllGuides();
             Guide guide = new Guide();

             foreach (Guide g in guides)
             {
                 if (g.Id == id)
                 {
                     guide = g;
                 }
             }
             return guide;
         }*/
    }
}
