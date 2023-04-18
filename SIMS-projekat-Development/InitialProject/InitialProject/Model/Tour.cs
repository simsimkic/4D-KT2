using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language {  get; set; }
        public int MaxTourists { get; set; }
        public DateTime StartingDate { get; set; }
        public int DurationInHours { get; set; }
        public bool HasStarted { get; set; }
        public List<TourImage>? Images { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public List<Tourist> Tourists { get; set; }
        public int CurrentNumberOfTourists { get; set; }

        public Tour()
        {
            Images = new List<TourImage>();
            CheckPoints = new List<CheckPoint>();
        }

        public Tour(string name, string description, string language, int maxTourists, DateTime startingDate, int durationInHours, bool hasStarted, List<TourImage> images, List<CheckPoint> checkPoints, List<Tourist> tourists)
        {
            Name = name;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            StartingDate = startingDate;
            DurationInHours = durationInHours;
            HasStarted = hasStarted;
            Images = images;
            CheckPoints = checkPoints;
            Tourists = tourists;
            CurrentNumberOfTourists = 0;
        }
    }
}
