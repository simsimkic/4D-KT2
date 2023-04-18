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
    internal class OwnerRepository
    {
        private readonly DbContext _dbContext;

        public OwnerRepository(DbContext context)
        {
            _dbContext = context;
        }

        public List<Owner> GetAllOwners()
        {
            
                return _dbContext.Set<User>().OfType<Owner>().ToList();
            
        }
        public Owner GetById(int id)
        {
            List<Owner> owners = GetAllOwners();
            Owner owner = new Owner();

            foreach (Owner t in owners)
            {
                if (t.Id == id)
                {
                    owner = t;
                }
            }
            return owner;
        }
        public void UpdateOwner(Owner owner)
        {
            _dbContext.Entry(owner).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void AddAccommodation(Owner owner, Accommodation accommodation)
        {
            owner.Accommodations.Add(accommodation);

            _dbContext.SaveChanges();
            
        }
        public void AddAccommodationToOwner(int ownerId, Accommodation accommodation)
        {           
                var owner = _dbContext.Set<Owner>().Include(o => o.Accommodations).FirstOrDefault(o => o.Id == ownerId);
                if (owner != null)
                {
                    owner.Accommodations.Add(accommodation);
                    _dbContext.SaveChanges();
                }
            
        }


    }
}
