using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    internal class TourLocationDto
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public DateTime StartingDate { get; set; }
        public int DurationInHours { get; set; }
        public int CurrentNumberOfTourists { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public TourLocationDto() { }

        public TourLocationDto(int tourId, string name, string description, string language, int maxTourists, DateTime startingDate, int durationInHours, string country, string city)
        {
            TourId = TourId;
            Name = name;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            StartingDate = startingDate;
            DurationInHours = durationInHours;
            Country = country;
            City = city;
        }
    }
}
