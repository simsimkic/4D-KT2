using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class GuestRating
    {
        public int Id { get; set; }
        public DateTime RatingExperationDate { get; set; }
        public int Cleanliness { get; set; }
        public int RuleCompliance { get; set; }
        public string Comment { get; set; }

        public GuestRating()
        {
        }

        public GuestRating(DateTime ratingExperationDate, int cleanliness, int ruleCompliance, string comment)
        {
            RatingExperationDate = ratingExperationDate;
            Cleanliness = cleanliness;
            RuleCompliance = ruleCompliance;
            Comment = comment;
        }

        public GuestRating(int cleanliness, int ruleCompliance, string comment)
        {
            Cleanliness = cleanliness;
            RuleCompliance = ruleCompliance;
            Comment = comment;
        }
    }
}
