using InitialProject.Context;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class ReserationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private MyDbContext db;

        public ReserationService(MyDbContext db)
        {
            this.db = db;
        }

        public ReserationService(ReservationRepository reservationRepository, AccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _reservationRepository = reservationRepository;
        }
        public List<ReservationDto> GetAvailableDates(int accommodationId, DateTime startDate, DateTime endDate, int reservationDuration)
        {
            var availableDates = new List<ReservationDto>();

            Accommodation accommodation = _accommodationRepository.FindById(accommodationId);

            // Get all reserved dates for the accommodation
            var reservedDates = accommodation.Reservations
                .SelectMany(r => Enumerable.Range(0, (r.CheckOutDate.Date - r.CheckInDate.Date).Days)
                    .Select(i => r.CheckInDate.AddDays(i)))
                .ToList();

            // Create a list of all possible dates with start and end
            var allDates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days + 1)
                .Select(i => startDate.AddDays(i))
                .ToList();

            // Filter out reserved dates and dates within a reservation
            // Doesn't work :(
            var availableDateTimes = allDates.Except(reservedDates)
                .Where(date => accommodation.Reservations.All(r => date < r.CheckInDate || date >= r.CheckOutDate))
                .ToList();

            // Return
            availableDates = availableDateTimes.Select(date => new ReservationDto
            {
                StartDate = date,
                EndDate = date.AddDays(reservationDuration)
            }).ToList();

            return availableDates;
        }

    }
}
