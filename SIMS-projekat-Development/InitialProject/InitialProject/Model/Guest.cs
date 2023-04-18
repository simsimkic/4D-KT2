using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class Guest : User
    {
        public List<Accommodation> Accommodations { get; set; }
        public List<Comment> Comments { get; set; }
        public List<GuestRating> GuestRatings { get; set; }
        public List<Reservation> Reservations { get; set; }


        public Guest()
        {
            Accommodations = new List<Accommodation>();
            Comments = new List<Comment>();
            GuestRatings = new List<GuestRating>();
            Reservations = new List<Reservation>();
        }

        public Guest(string username, string password, UserType userType) : base(username, password, userType)
        {
            Accommodations = new List<Accommodation>();
            Comments = new List<Comment>();
            GuestRatings = new List<GuestRating>();
            Reservations = new List<Reservation>();
        }

        public Guest(List<Accommodation> accommodations, List<Comment> comments, List<GuestRating> guestRatings, List<Reservation> reservations)
        {
            Accommodations = accommodations;
            Comments = comments;
            GuestRatings = guestRatings;
            Reservations = reservations;
        }
    }
}
