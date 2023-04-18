using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    class TourReviewDto
    {
        public int idTour { get; set; }
        public string TouristName { get; set; }
        public int GouideKnoweladge { get; set; }
        public int GuideLagnuage { get; set; }
        public int TourInterest { get; set; }
        public string Comment { get; set; }
        public bool IsValid { get; set; }
        //public TourReviewDto() { }
        public TourReviewDto(string touristName, int gouideKnoweladge, int guideLagnuage, int tourInterest, string comment, bool isValid)
        {
            TouristName = touristName;
            GouideKnoweladge = gouideKnoweladge;
            GuideLagnuage = guideLagnuage;
            TourInterest = tourInterest;
            Comment = comment;
            IsValid = isValid;
        }
    }
}
