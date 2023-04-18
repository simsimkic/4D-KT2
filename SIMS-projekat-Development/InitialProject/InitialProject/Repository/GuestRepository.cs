using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class GuestRepository
    {
        public GuestRepository() { }
        private readonly DbContext _dbContext;

        public GuestRepository(DbContext context)
        {
            _dbContext = context;
        }
        public List<Guest> GetAll()
        {
                return _dbContext.Set<User>().OfType<Guest>().ToList();
            
        }

        public Guest GetById(int id)
        {
            List<Guest> guests = GetAll();
            Guest guest = new Guest();

            foreach (Guest t in guests)
            {
                if (t.Id == id)
                {
                    guest = t;
                }
            }
            return guest;
        }

        public void UpdateGuest(Guest guest)
        {
            _dbContext.Entry(guest).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void Rate(Guest guest , GuestRating guestRating)
        {
            if (guest.GuestRatings == null)
            {
                guest.GuestRatings = new List<GuestRating>();
            }

            guest.GuestRatings.Add(guestRating);

            UpdateGuest(guest);
        }
        
    }
}
