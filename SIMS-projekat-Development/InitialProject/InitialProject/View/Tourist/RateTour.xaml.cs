using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for RateTour.xaml
    /// </summary>
    public partial class RateTour : Window
    {
        public int userId;

        public int tourId;
        public RateTour(int userId, int tourId)
        {
            InitializeComponent();
            Loaded += FinishedTours_Loaded;

            this.userId = userId;
            this.tourId = tourId;
        }

        private void FinishedTours_Loaded(object sender, RoutedEventArgs e)
        {
            // Call the function you want to run here
            using (var db = new MyDbContext())
            {
                var tourRepository = new TourRepository(db);
                var locationRepository = new LocationRepository(db);
                var tourService = new TourService(tourRepository, locationRepository);

                List<Tour> finishedTours = tourService.FinishedTours();
                FinishedTourList.ItemsSource = finishedTours;
            }
        }

        private List<TourReviewImage> generateImages()
        {
            List <TourReviewImage> images = new List < TourReviewImage >();

            string urlString = ImageUrlsRaiting.Text;
            List<string> urlList = urlString.Split(',').Select(url => url.Trim()).ToList();

            foreach (var url in urlList)
            {
                TourReviewImage image = new TourReviewImage(url);
                images.Add(image);
            }

            return images;
        }

        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var tourRepository = new TourRepository(db);
                var locationRepository = new LocationRepository(db);
                var tourService = new TourService(tourRepository, locationRepository);
                var tourReviewRepository = new TourReviewRepository(db);
                var tourReviewService = new TourReviewService(tourReviewRepository);


                List<TourReviewImage> images = generateImages();

                tourReviewService.RateTour(
                    tourId,
                    userId,
                    int.Parse(GouideKnoweladgeRaiting.Text),
                    int.Parse(GuideLagnuageRaiting.Text),
                    int.Parse(TourInterestRaiting.Text),
                    CommentRaiting.Text,
                    images
                );
            }
        }
    }
}
