using Microsoft.EntityFrameworkCore;
﻿using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class LocationRepository
    {
        private readonly DbContext _dbContext;
        public LocationRepository() { }

        public LocationRepository(DbContext context)
        {
            _dbContext = context;
        }

        

        public List<Location> GetAll()
        {
            return _dbContext.Set<Location>().ToList();
        }

        public Location GetById(int id)
        {
            List<Location> locations = GetAll();
            Location location = new Location();

            foreach (Location l in locations)
            {
                if (l.Id == id)
                {
                    location = l;
                }
            }
            return location;
        }


        public List<Location> GetByCountry(string country)
        {
            return _dbContext.Set<Location>()
                .Where(l => l.Country == country)
                .ToList();
        }

        public List<Location> GetByCity(string city)
        {
            return _dbContext.Set<Location>()
                .Where(l => l.City == city)
                .ToList();
        }

        public List<Location> GetByCountryAndCity(string country, string city)
        {
            return _dbContext.Set<Location>()
                .Where(l => l.City == city && l.Country == country)
                .ToList();
        }

        public Location GetByTourId(int tourId)
        {
            return _dbContext.Set<Location>()
                             .FirstOrDefault(l => l.Tours.Any(t => t.Id == tourId));
        }
        
        public void Add(Location location)
        {
            _dbContext.Set<Location>().Add(location);
            _dbContext.SaveChanges();
        }
        public Location GetLocationByAccommodationId(int accommodationId)
        {
            return _dbContext.Set<Location>()
                             .FirstOrDefault(l => l.Accommodations.Any(t => t.Id == accommodationId));
        }

        public Location GetLocationByCountryAndCity(string country, string city)
        {
            return _dbContext.Set<Location>()
                .FirstOrDefault(l => l.City == city && l.Country == country);
        }

        
        public void AddAccommodation(Location location, Accommodation accommodation)
        {
            // Make sure the Accommodations property is not null
            if (location.Accommodations == null)
            {
                location.Accommodations = new List<Accommodation>();
            }

            // Add the new accommodation to the list
            location.Accommodations.Add(accommodation);
            
            

            // Update the location in the database
            _dbContext.SaveChanges();
        }

    }
}
