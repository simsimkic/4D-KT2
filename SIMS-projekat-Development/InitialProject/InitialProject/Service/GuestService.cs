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
using System.Windows;

namespace InitialProject.Service
{
    internal class GuestService
    {
        private GuestRepository _guestRepository;
        private MyDbContext db;
        public GuestService(GuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public GuestService(MyDbContext db)
        {
            this.db = db;
        }

        public List<GuestDto> getUnratedGuests()
        {
            List<Guest> allGuests = _guestRepository.GetAll();
            List<GuestDto> unratedGuests = new List<GuestDto>();
            GuestDto temp = new GuestDto();

            foreach (var guest in allGuests)
            {
                //MessageBox.Show("Guest Id is " + guest.Username);
                
                foreach (var reservation in guest.Reservations)
                {
                    //MessageBox.Show("Reservation Id iss " + reservation.Id.ToString());
                    if (reservation.CheckOutDate <= DateTime.Now.AddDays(5) && reservation.CheckOutDate >= DateTime.Now && reservation.IsRated == false)
                    {
                        temp.CheckInDate = reservation.CheckInDate;
                        temp.CheckOutDate = reservation.CheckOutDate;
                        temp.ReservationId = reservation.Id;
                        temp.GuestId = guest.Id;
                        temp.Username = guest.Username;

                        unratedGuests.Add(temp);
                    }
                }
            }
            return unratedGuests;
        }

        public void GuestRatingInput(int cleanliness, int ruleCompliance, string comment, int guestID)
        {
            GuestRating guestRating = new GuestRating(cleanliness, ruleCompliance ,comment);           
            Guest guest = _guestRepository.GetById(guestID);
            _guestRepository.Rate(guest, guestRating);           
        }
    }
}
