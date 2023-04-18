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
    internal class AccommodationRepository
    {
        private readonly DbContext _dbContext;

        public AccommodationRepository(DbContext context)
        {
            _dbContext = context;
        }

        public List<Accommodation> GetAll()
        {
            if (_dbContext == null)
            {
                // handle the error, such as throwing an exception or returning an empty list
                throw new Exception("_dbContext is null");
            }
            return _dbContext.Set<Accommodation>().ToList();
        }

        public Accommodation FindById(int id)
        {
            return _dbContext.Set<Location>()
                .Include(l => l.Accommodations)
                    .ThenInclude(t => t.Images)
                .SelectMany(l => l.Accommodations)
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Accommodation> FindByAccommodationType(AccommodationType type)
        {
            return _dbContext.Set<Location>()
                .Include(l => l.Accommodations)
                    .ThenInclude(t => t.Images)
                .SelectMany(l => l.Accommodations)
                .Where(a => a.AccommodationType == type).ToList();
        }

        public List<Accommodation> FindByName(string name)
        {
            return _dbContext.Set<Location>()
                .Include(l => l.Accommodations)
                    .ThenInclude(t => t.Images)
                .SelectMany(l => l.Accommodations)
                .Where(a => a.Name.Contains(name)).ToList();
        }

        public List<Accommodation> FindByNumberOfGuests(int numberOfGuests)
        {
            return _dbContext.Set<Location>()
                .Include(l => l.Accommodations)
                    .ThenInclude(t => t.Images)
                .SelectMany(l => l.Accommodations)
                .Where(a => a.MaxNumberOfGuests >= numberOfGuests).ToList();
        }
    }
}
