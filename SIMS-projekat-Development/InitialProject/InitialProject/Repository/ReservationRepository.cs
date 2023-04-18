using InitialProject.Dto;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    public class ReservationRepository
    {
        private readonly DbContext _dbContext;

        public ReservationRepository(DbContext context)
        {
            _dbContext = context;
        }
        public List<Reservation> GetAllReservations()
        {
            return _dbContext.Set<Reservation>().ToList();

        }

        public Reservation GetById(int id)
        {
            List<Reservation> reservations = GetAllReservations();
            Reservation reservation = new Reservation();

            foreach (Reservation t in reservations)
            {
                if (t.Id == id)
                {
                    reservation = t;
                }
            }
            return reservation;
        }

        public void AddReservation(DateTime startDate, DateTime endDate, Accommodation accommodation, int numberOfGuests)
        {
            var reservation = new Reservation(numberOfGuests, startDate, endDate);
            
            MessageBox.Show("Your reservation has been created! \n Check in date: " + reservation.CheckInDate.ToString("yyyy-dd-MM HH:mm:ss") + "\n Check out date: " + reservation.CheckOutDate.ToString("yyyy-dd-MM HH:mm:ss"));
            
            accommodation.Reservations.Add(reservation);
            _dbContext.SaveChanges();
        }

        public List<Reservation> GetReservationsByAccommodation(Accommodation accommodation)
        {
            return accommodation.Reservations.ToList();
        }


        public List<DateTime> GetReservedDates(Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            return accommodation.Reservations
                .Where(r => (r.CheckInDate < endDate && r.CheckOutDate > startDate))
                .SelectMany(r => Enumerable.Range(0, (int)(r.CheckOutDate - r.CheckInDate).TotalDays).Select(i => r.CheckInDate.AddDays(i)))
                .ToList();
        }

        public void MarkReservation(int reservationId)
        {
            Reservation reservation = GetById(reservationId);

            reservation.IsRated = true;
            _dbContext.SaveChanges();
        }
    }

}
