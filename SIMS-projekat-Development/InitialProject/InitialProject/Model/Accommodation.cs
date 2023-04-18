using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Accommodation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int MinimalReservationPeriod { get; set; }
        public int CancelPeriod { get; set; }
        public List<AccommodationImage> Images { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Accommodation()
        {
            Images = new List<AccommodationImage>();
            Reservations = new List<Reservation>();
        }

        public Accommodation(string name, AccommodationType accommodationType, int maxNumberOfGuests, int minimalReservationPeriod, int cancelPeriod)
        {
            Name = name;
            AccommodationType = accommodationType;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinimalReservationPeriod = minimalReservationPeriod;
            CancelPeriod = cancelPeriod;
        }

        public Accommodation(int id, string name, AccommodationType accommodationType, int maxNumberOfGuests, int minimalReservationPeriod, int cancelPeriod, List<AccommodationImage> images)
        {
            Id = id;
            Name = name;
            AccommodationType = accommodationType;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinimalReservationPeriod = minimalReservationPeriod;
            CancelPeriod = cancelPeriod;
            Images = images;
            Images = new List<AccommodationImage>();
            Reservations = new List<Reservation>();
        }

    }
}
