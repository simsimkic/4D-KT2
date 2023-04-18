using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class AccommodationLocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int MinimalReservationPeriod { get; set; }
        public int CancelPeriod { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public AccommodationLocationDto()
        {
        }

        public AccommodationLocationDto(string name, AccommodationType accommodationType, int maxNumberOfGuests, int minimalReservationPeriod, int cancelPeriod, string city, string country)
        {
            Name = name;
            AccommodationType = accommodationType;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinimalReservationPeriod = minimalReservationPeriod;
            CancelPeriod = cancelPeriod;
            City = city;
            Country = country;
        }
    }
}
