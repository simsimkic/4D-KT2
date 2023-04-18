using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class TourReview
    {
        [Key]
        public int Id { get; set; }
        public int TourId { get; set; }
        public int TouristId { get; set; }
        public int GouideKnoweladge { get; set; }
        public int GuideLagnuage { get; set; }
        public int TourInterest { get; set; }
        public string Comment { get; set; }
        public bool IsValid { get; set; }  
        public List<TourReviewImage> Images { get; set; }

        public TourReview(int tourId, int touristId, int gouideKnoweladge, int guideLagnuage, int tourInterest, string comment)
        { 
            TourId = tourId;
            TouristId = touristId; 
            GouideKnoweladge = gouideKnoweladge;
            GuideLagnuage = guideLagnuage;
            TourInterest = tourInterest;
            Comment = comment;
            Images = new List<TourReviewImage>();
        }
    }
}
