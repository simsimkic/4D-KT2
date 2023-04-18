using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class Location
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public List<Tour> Tours { get; set; }

        public Location()
        {
            Tours = new List<Tour>();
            Accommodations = new List<Accommodation>();
        }

        public Location(string country, string city)
        {
            Country = country;
            City = city;
            Accommodations = new List<Accommodation>();
            Tours = new List<Tour>();
        }

        public Location(string country, string city, List<Accommodation> accommodations, List<Tour> tours) : this(country, city)
        {
            Accommodations = accommodations;
            Tours = tours;
        }
    }
}
