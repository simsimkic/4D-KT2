using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Service
{
    internal class TourReviewService
    {
        private readonly TourReviewRepository _tourReviewRepository;
        private MyDbContext context;
        public TourReviewService(MyDbContext context)
        {
            this.context = context;
        }

        public TourReviewService(TourReviewRepository tourReviewRepository)
        {
            _tourReviewRepository = tourReviewRepository;
        }

        public void RateTour(int tourId, int touristId, int guideKnoweladge, int guideLanguage, int tourInterest, string comment, List<TourReviewImage> images)
        {
            TourReview tourReview = new TourReview(tourId, touristId, guideKnoweladge, guideLanguage, tourInterest, comment);
            tourReview.Images = images;
            _tourReviewRepository.Add(tourReview);
        }
    }
}
