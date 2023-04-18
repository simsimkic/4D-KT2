using InitialProject.Context;
using InitialProject.Dto;
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

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for ShowReview.xaml
    /// </summary>
    public partial class ShowReview : Window
    {
        private int loggedUserId;
        public ShowReview(int loggedUserId)
        {
            InitializeComponent();
            this.loggedUserId = loggedUserId;
        }
        public void FinishedTours_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var guideRepository = new GuideRepository(context);
                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository, guideRepository);

                List<Tour> tours = tourService.FinishedTours(loggedUserId);
                ListOfTours.ItemsSource = tours;
            }
        }

        public void ShowReview_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository);

                int tourId = int.Parse(TourId.Text);

                //List<TourReviewDto> review = tourService.ShowReview(tourId);
                //ShowReviewList.ItemsSource = review;
            }
        }

        public void ReportReview_Click(Object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext()) 
            {

            }
        }

        public void ShowCheckPoint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
