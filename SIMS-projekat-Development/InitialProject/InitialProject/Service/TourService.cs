using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore;
﻿using InitialProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Linq;
using System.CodeDom;

namespace InitialProject.Service
{
    internal class TourService
    {
        private readonly TourRepository _tourRepository;
        private readonly TouristRepository _touristRepository;
        private readonly LocationRepository _locationRepository;
        private readonly UserRepository _userRepository;
        private readonly TouristService _touristService;
        private readonly GuideRepository _guideRepository;
        private MyDbContext context;

        private readonly VoucherService _voucherService;

        public TourService(MyDbContext context)
        {
            this.context = context;
        }

        public TourService(TourRepository tourRepository, GuideRepository guideRepository) : this(tourRepository)
        {
            _guideRepository = guideRepository;
        }

        public TourService(TourRepository tourRepository, LocationRepository locationRepository, UserRepository userRepository)
        {
            _tourRepository = tourRepository;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
        }

        public TourService(TourRepository tourRepository, TouristRepository touristRepository)
        {
            _tourRepository = tourRepository;
            _touristRepository = touristRepository;
        }

        public TourService(TourRepository tourRepository, LocationRepository locationRepository)
        {
            _tourRepository = tourRepository;
            _locationRepository = locationRepository;
        }

        public TourService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public TourService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public TourService(TourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public TourService(TourRepository tourRepository, LocationRepository locationRepository, VoucherService voucherService)
        {
            _tourRepository = tourRepository;
            _locationRepository = locationRepository;
            _voucherService = voucherService;
        }


        //Method that filters list of Tours by given parameters
        //Any or all of parameters can be null
        //If all parameters are null method returns all Tours from database
        public List<TourLocationDto> Search(string? country = null, string? city = null, string? language = null, int? numberOfTourists = null, int? durationInHours = null)
        {
            var tours = _tourRepository.GetAll();

            tours = FilterByLanguage(tours, language);
            tours = FilterByNumberOfTourists(tours, numberOfTourists);
            tours = FilterByDurationInHours(tours, durationInHours);
            tours = FilterByLocation(tours, country, city);

            //Converting Tour to TourLocationDto by adding locations to corresponding tours
            return AddLocationsToTours(tours.ToList());
        }

        //Method filters list of Tours by DurationInHours
        //If parameter durationInHours is null, method returns original list of tours
        private List<Tour> FilterByDurationInHours(List<Tour> tours, int? durationInHours)
        {
            if (durationInHours == null)
            {
                return tours;
            }
            return tours.Where(t => t.DurationInHours == durationInHours).ToList();
        }

        //Method filters list of Tours by language
        //If parameter language is null, method returns original list of tours
        private List<Tour> FilterByLanguage(List<Tour> tours, string? language)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                return tours;
            }
            return tours.Where(t => t.Language == language).ToList();
        }

        //Method filters list of Tours by NumberOfTourists
        //If parameter numberOfTourists is null, method returns original list of tours
        private List<Tour> FilterByNumberOfTourists(List<Tour> tours, int? numberOfTourists)
        {
            if (numberOfTourists == null)
            {
                return tours;
            }
            return tours.Where(t => t.MaxTourists >= numberOfTourists).ToList();
        }

        //Method filters list of Tours by country and city (Location)
        //Both country and city can be null
        private List<Tour> FilterByLocation(List<Tour> tours, string? country, string? city)
        {
            if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(country))
            {
                return tours;
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
                var locationTours = locations.SelectMany(l => l.Tours).ToList();
                return tours.Intersect(locationTours).ToList();
            }

            return tours;
        }


        public List<TourLocationDto> AddLocationsToTours(List<Tour> tours)
        {
            var tourLocations = new List<TourLocationDto>();

            foreach (var tour in tours)
            {
                Location location = _locationRepository.GetByTourId(tour.Id);

                tourLocations.Add(new TourLocationDto
                {
                    TourId = tour.Id,
                    Name = tour.Name,
                    Description = tour.Description,
                    Language = tour.Language,
                    MaxTourists = tour.MaxTourists,
                    StartingDate = tour.StartingDate,
                    DurationInHours = tour.DurationInHours,
                    CurrentNumberOfTourists = tour.CurrentNumberOfTourists,
                    Country = location.Country,
                    City = location.City
                });
            }

            return tourLocations;
        }




        public BookInformationDto BookTour(int tourId, int numberOfTourists, int? voucherId)
        {
            Tour tour = _tourRepository.GetById(tourId);

            if (tour.MaxTourists - tour.CurrentNumberOfTourists < numberOfTourists)
            {
                return new BookInformationDto
                {
                    TourId = tour.Id,
                    IsAccepted = false,
                    FreePlaces = tour.MaxTourists - tour.CurrentNumberOfTourists
                };
            }

            tour.CurrentNumberOfTourists += numberOfTourists;
            _tourRepository.Update(tour);
            
            if(voucherId != null)
            {
                _voucherService.UseVoucher(voucherId, tourId);
            }

            return new BookInformationDto
            {
                TourId = tour.Id,
                IsAccepted = true,
                FreePlaces = tour.MaxTourists - tour.CurrentNumberOfTourists
            };
        }

        public void MakeTour(Tour tour, Location location, List<TourImage> tourImages, List<CheckPoint> checkPoints)
        {
            using (var context = new MyDbContext())
            {
                Location existingLocation = _locationRepository.GetLocationByCountryAndCity(location.Country, location.City);

                if (existingLocation != null)
                {
                    location = existingLocation;
                    location.Tours.Add(tour);
                    context.Locations.Update(existingLocation);

                }
                else
                {
                    location.Tours.Add(tour);
                    context.Locations.Add(location);

                }

                //location.Tours.Add(tour); 
                foreach (var image in tourImages)
                {
                    context.TourImages.Add(image);
                    tour.Images.Add(image);
                }
                foreach (var checkPoint in checkPoints)
                {
                    context.CheckPoints.Add(checkPoint);
                    tour.CheckPoints.Add(checkPoint);
                }

                context.SaveChanges();

            }
        }

        public List<Tour> ShowTodayTours()
        {
            List<Tour> allTours = _tourRepository.GetAll();

            List<Tour> toursStartingToday = new List<Tour>();

            using (var context = new MyDbContext())
            {
                DateTime today = DateTime.Today;
                toursStartingToday = context.Tours.Where(t => t.StartingDate == DateTime.Today && !t.HasStarted).ToList();

            }

            return toursStartingToday;
        }
        public bool CancelTour(int tourId)
        {
            Tour tour = _tourRepository.GetById(tourId);
            DateTime StartingDate = tour.StartingDate;
            DateTime currentTime = DateTime.Now;

            if ((StartingDate - currentTime).TotalHours >= 48)
            {
                foreach (Tourist tourist in tour.Tourists)
                {
                    _touristService.AssignVoucher(tourist.Id);
                }
                _tourRepository.Delete(tourId);

                return true;
            }
            return false;
        }
        public List<Tour> MostVisited()
        {
            var tours = _tourRepository.GetAll()
                .OrderByDescending(t => t.CurrentNumberOfTourists)
                .ToList();

            var mostVisitedTours = tours.TakeWhile(t => t.CurrentNumberOfTourists == tours.First().CurrentNumberOfTourists);
            return mostVisitedTours.ToList();
        }
        public List<Tour> MostVisitedByYear(int year)
        {
            var toursInYear = _tourRepository.GetAll()
                                         .Where(t => t.StartingDate.Year == year)
                                         .ToList();
            if (toursInYear.Count == 0)
            {
                return null;
            }

            var mostVisitedTour = toursInYear.First();
            foreach (Tour tour in toursInYear)
            {
                if (tour.CurrentNumberOfTourists > mostVisitedTour.CurrentNumberOfTourists)
                {
                    mostVisitedTour = tour;
                }
            }
            var mostVisitedTours = toursInYear.Where(t => t.CurrentNumberOfTourists == mostVisitedTour.CurrentNumberOfTourists)
                                          .ToList();

            return mostVisitedTours;
        }

        public List<Tour> FinishedTours()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Tour> finishedTours = new List<Tour>();
            DateTime currentTime = DateTime.Now;
            foreach (Tour tour in tours)
            {
                DateTime finishDate = tour.StartingDate.AddHours(tour.DurationInHours);
                if (DateTime.Compare(finishDate, currentTime) < 0)
                {
                    finishedTours.Add(tour);
                }
            }
            return finishedTours;
        }
            DateTime currentTime = DateTime.Now; 

            if ((StartingDate - currentTime).TotalHours >= 48)
            {
                foreach (Tourist tourist in tour.Tourists)
                {
                    _touristService.AssignVoucher(tourist.Id);
                }
                _tourRepository.Delete(tourId);

                return true;
            }
            return false;
        }
        public List<Tour> MostVisited()
        {
            var tours = _tourRepository.GetAll()
                .OrderByDescending(t => t.CurrentNumberOfTourists)
                .ToList();

            var mostVisitedTours = tours.TakeWhile(t => t.CurrentNumberOfTourists == tours.First().CurrentNumberOfTourists);
            return mostVisitedTours.ToList();
        }
        public List<Tour> MostVisitedByYear(int year)
        {
            var toursInYear = _tourRepository.GetAll()
                                         .Where(t => t.StartingDate.Year == year)
                                         .ToList();
            if (toursInYear.Count == 0)
            {
                return null;
            }

            var mostVisitedTour = toursInYear.First();
            foreach (Tour tour in toursInYear)
            {
                if (tour.CurrentNumberOfTourists > mostVisitedTour.CurrentNumberOfTourists)
                {
                    mostVisitedTour = tour;
                }
            }
            var mostVisitedTours = toursInYear.Where(t => t.CurrentNumberOfTourists == mostVisitedTour.CurrentNumberOfTourists)
                                          .ToList();

            return mostVisitedTours;
        }

        public List<Tour> FinishedTours(int guideId)
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<Tour> finishedTours = new List<Tour>();
            DateTime currentTime = DateTime.Now;
            Guide guide = _guideRepository.GetById(guideId);

            if(guide != null)
            {
                foreach (Tour tour in guide.Tours)
                {
                    DateTime finishDate = tour.StartingDate.AddHours(tour.DurationInHours);
                    if (DateTime.Compare(finishDate, currentTime) < 0)
                    {
                        finishedTours.Add(tour);
                    }

                }
            }
            
            return finishedTours;
        }

        public TourStatisticDto TourStatistic(int tourId)
        {
            Tour tour = _tourRepository.GetById(tourId);
            int touristUnder18 = 0;
            int touristBetween18and50 = 0;
            int touristsOver50 = 0;
            int touristsWithVoucher = 0;
            int touristsWithoutVoucher = 0;

            int totalTourists = 0;
            
            foreach (Tourist tourist in tour.Tourists)
            {
                
                totalTourists++;
                foreach (Voucher voucher in tourist.Vouchers)
                {
                    if (voucher.UsedOnTourId == tourId)
                    {
                        touristsWithVoucher++;
                    }
                    else
                    {
                        touristsWithoutVoucher++;
                    }
                }
                if (tourist.Age <= 18)
                {
                    touristUnder18++;
                }

                else if(tourist.Age <= 50)
    
            {
                    touristBetween18and50++;
                }
                else if(tourist.Age > 50)
    
            {
                    touristsOver50++;
                }

            }
            double touristsWithVoucherPercentage = ((double)touristsWithVoucher / totalTourists) * 100;
            double touristsWithoutVoucherPercentage = ((double)touristsWithoutVoucher / totalTourists) * 100;
            TourStatisticDto tourStatisticDto = new TourStatisticDto(touristUnder18, touristBetween18and50, touristsOver50, touristsWithVoucherPercentage, touristsWithoutVoucherPercentage);
            return tourStatisticDto;
        }

        /*public List<TourReviewDto> ShowReview(int tourId)
        {
            Tour tour  = _tourRepository.GetById(tourId);
            List<TourReviewDto> tourReviews = new List<TourReviewDto>();
            foreach(var tourist in tour.Tourists)
            {
                foreach( var review in tourist.TourReviews)
                {
                    if( review.TouristId == tourist.Id)
                    {
                        string name = tourist.Username;
                        TourReviewDto reviewDto = new TourReviewDto(name, review.GouideKnoweladge, review.TourInterest, review.GuideLagnuage, review.Comment, review.IsValid);
                        tourReviews.Add(reviewDto);
                    }
                }
            }
            return tourReviews;
        }*/

        /* public List<CheckPoint> StartTour(int tourId, int guideId)
         {
             List<CheckPoint> checkPoints = _tourRepository.GetCheckPointsByTourId(tourId);

             Guide guide = _userRepository.GetById(guideId);

             Tour tour = _tourRepository.GetTourById(tourId);

             if(guide != null)
             {
                 tour.HasStarted = true;

                 return checkPoints;
             }
             return null;
         }*/

        /* public void MarkCheckPoints(int tourId, int guideId, int checkPointId) 
         {
             List<CheckPoint> checkPoints = StartTour(tourId, guideId);
            foreach (var checkPoint in checkPoints)
             {
                 if(checkPoint.Id == checkPointId)
                 {
                     checkPoint.Active = true;
                 }
             }
         }*/
    }
}
