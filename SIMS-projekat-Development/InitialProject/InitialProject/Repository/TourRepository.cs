using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    internal class TourRepository
    {

        private readonly DbContext _dbContext;
        private readonly LocationRepository _locationRepository;
        private readonly TouristRepository _touristRepository;

        public TourRepository(DbContext context)
        {
            _dbContext = context;
            _locationRepository = new LocationRepository(_dbContext);
            _touristRepository = new TouristRepository(_dbContext);

        }

        public TourRepository()
        {
        }

        public TourRepository(DbContext dbContext, LocationRepository locationRepository) : this(dbContext)
        {
            this._locationRepository = locationRepository;
        }

        public List<Tour> GetAll()
        {
            return _dbContext.Set<Location>()
                .Include(l => l.Tours)
                    .ThenInclude(t => t.Images)
                .Include(l => l.Tours)
                    .ThenInclude(t => t.CheckPoints)
                .Include(l => l.Tours)
                    .ThenInclude(t => t.Tourists)
                .SelectMany(l => l.Tours)
                .ToList();
        }

        public Tour GetById(int id)
        {
            return _dbContext.Set<Location>()
                .Include(l => l.Tours)
                    .ThenInclude(t => t.Images)
                .Include(l => l.Tours)
                    .ThenInclude(t => t.CheckPoints)
                .Include(l => l.Tours)
                    .ThenInclude(t => t.Tourists)
                        .ThenInclude(t => t.Vouchers)
                .SelectMany(l => l.Tours)
                .FirstOrDefault(t => t.Id == id);
        }

        public List<CheckPoint> GetCheckPointsByTourId(int tourId)
        {
            Tour tour = GetById(tourId);

            return tour.CheckPoints.ToList();
        }

        public void Save(Tour tour)
        {
            _dbContext.Set<Tour>().Add(tour);
            _dbContext.SaveChanges();
        }

        public void Update(Tour tour)
        {
            _dbContext.Entry(tour).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var tour = _dbContext.Set<Tour>().Find(id);

            if (tour != null)
            {
                _dbContext.Set<Tour>().Remove(tour);
                _dbContext.SaveChanges();
            }
        }

        public List<Tour> GetByDurationInHours(int durationInHours)
        {
            return _dbContext.Set<Tour>()
                .Where(t => t.DurationInHours <= durationInHours)
                .ToList();
        }

        public List<Tour> GetByLanguage(string language)
        {
            return _dbContext.Set<Tour>()
                .Where(t => t.Language == language)
                .ToList();
        }

        public List<Tour> GetByNumberOfTourists(int numberOfTourists)
        {
            return _dbContext.Set<Tour>()
                .Include(t => t.Tourists)
                .Where(t => t.Tourists.Count < t.MaxTourists && t.MaxTourists >= numberOfTourists)
                .ToList();
        }

        public List<Tour> GetByLocationId(int locationId)
        {
            Location location = _locationRepository.GetById(locationId);
            List<Tour> tours = location.Tours;
            return tours;
        }

        public Tour GetByTouristId(int touristId)
        {
            Tourist tourist = _touristRepository.GetById(touristId);

            List<Tour> tours = GetAll();

            int? tourId = null;

            foreach (Tour tour in tours)
            {
                if (tour.Tourists.Contains(tourist))
                {
                    tourId = tour.Id; break;
                }
            }

            
            if (tourId == null)
                return null;

            Tour retTour = GetById(tourId ?? 0);
            
            return retTour;
        }

    }
}
