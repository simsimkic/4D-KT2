using InitialProject.Context;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Service
{
    internal class AccommodationService
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly OwnerRepository _ownerRepository;
        private MyDbContext db;

        public AccommodationService(AccommodationRepository accommodationRepository, LocationRepository locationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;
        }

        public AccommodationService(AccommodationRepository accommodationRepository, LocationRepository locationRepository, OwnerRepository ownerRepository)
        {
            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;
            _ownerRepository = ownerRepository;
        }

        public AccommodationService(MyDbContext db)
        {
            this.db = db;
        }

        public List<AccommodationLocationDto> Search(string? name, int? accommodationType, string? contry, string? city, int? numberOfGuests, int? reservationDays)
        {
            var accommodations = _accommodationRepository.GetAll();

            accommodations = FilterByName(accommodations, name);
            accommodations = FilterByType(accommodations, accommodationType);
            accommodations = FilterByLocation(accommodations, contry, city);
            accommodations = FilterByNumberOfGuests(accommodations, numberOfGuests);
            accommodations = FilterByAvailableReservationDays(accommodations, reservationDays);

            return AddLocationsToAccommodations(accommodations.ToList());
        }

        public List<AccommodationLocationDto> AddLocationsToAccommodations(List<Accommodation> accommodations)
        {
            var accommodationLocations = new List<AccommodationLocationDto>();

            foreach (var accommodation in accommodations)
            {
                Location location = _locationRepository.GetLocationByAccommodationId(accommodation.Id);
                if (accommodation != null && location != null)
                {
                    accommodationLocations.Add(new AccommodationLocationDto
                    {
                        Id = accommodation.Id,
                        Name = accommodation.Name,
                        AccommodationType = accommodation.AccommodationType,
                        MaxNumberOfGuests = accommodation.MaxNumberOfGuests,
                        MinimalReservationPeriod = accommodation.MinimalReservationPeriod,
                        CancelPeriod = accommodation.CancelPeriod,
                        City = location.City,
                        Country = location.Country
                    });
                }
            }

            return accommodationLocations;

        }

        private List<Accommodation> FilterByName(List<Accommodation> accommodations, string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return accommodations;
            }
            return accommodations.Where(a => a.Name == name).ToList();
        }

        private List<Accommodation> FilterByType(List<Accommodation> accommodations, int? accommodationType)
        {
            if (accommodationType == 3)
            {
                return accommodations;
            }
            return accommodations.Where(a => (int) a.AccommodationType == accommodationType).ToList();
        }

        private List<Accommodation> FilterByLocation(List<Accommodation> accommodations, string? country, string? city)
        {
            if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(country))
            {
                return accommodations;
            }

            var locations = new List<Location>();
            if (!string.IsNullOrWhiteSpace(country) && !string.IsNullOrWhiteSpace(city))
            {
                locations = _locationRepository.GetByCountryAndCity(country, city);
            }
            else if (!string.IsNullOrWhiteSpace(country))
            {
                locations = _locationRepository.GetByCountry(country);
            }
            else if (!string.IsNullOrWhiteSpace(city))
            {
                locations = _locationRepository.GetByCity(city);
            }

            if (locations.Any())
            {
                var locationAccommodations = locations.SelectMany(l => l.Accommodations).ToList();
                return accommodations.Intersect(locationAccommodations).ToList();
            }

            return new List<Accommodation>();
        }

        private List<Accommodation> FilterByNumberOfGuests(List<Accommodation> accommodations, int? numberOfGuests)
        {
            if (numberOfGuests == null)
            {
                return accommodations;
            }
            return accommodations.Where(a => a.MaxNumberOfGuests >= numberOfGuests).ToList();
        }

        private List<Accommodation> FilterByAvailableReservationDays(List<Accommodation> accommodations, int? reservationDays)
        {
            if (reservationDays == null)
            {
                return accommodations;
            }
            return accommodations.Where(a => a.MinimalReservationPeriod <= reservationDays).ToList();
        }

        public void RegisterAccommodation(string country, string city, string accommodationName, int accommodationType, int numberOfGuests, int minimalReservationPeriod, int cancelPeriod)
        {
            Location location = _locationRepository.GetLocationByCountryAndCity(country, city);

            Owner owner = _ownerRepository.GetById(2);

            AccommodationType accommodationTypeEnum = (AccommodationType)Enum.Parse(typeof(AccommodationType), accommodationType.ToString());

            Accommodation accommodation = new Accommodation(accommodationName, accommodationTypeEnum, numberOfGuests, minimalReservationPeriod, cancelPeriod);


            if (location == null) //Checking if location already exists 
            {
                Location locationNew = new Location(country, city);
                //Location doesn't exist , creating new location and adding accomodation to the list 
                locationNew.Accommodations.Add(accommodation);
                _locationRepository.Add(locationNew);
                _ownerRepository.AddAccommodation(owner, accommodation);
            }
            else
            {

                _locationRepository.AddAccommodation(location, accommodation);
                _ownerRepository.AddAccommodation(owner, accommodation);
            }

        }
    }

}
