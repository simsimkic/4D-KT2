using InitialProject.Context;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for TourStatistic.xaml
    /// </summary>
    public partial class TourStatistic : Window
    {
        private int loggedUserId;
        public TourStatistic(int loggedUserId)
        {
            InitializeComponent();
            this.loggedUserId = loggedUserId;
        }

        public void FinishedTours_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository);

                List<Tour> tours = tourService.FinishedTours();
                var guideRepository = new GuideRepository(context);
                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository, guideRepository);

                List<Tour> tours = tourService.FinishedTours(loggedUserId);
                ListOfTours.ItemsSource = tours;
            }


        }

        public void ShowStatistic_Click(object sender, RoutedEventArgs e) 
        {
            using (var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository);

                int tourId = Int32.Parse(TourIdStatistic.Text);

                TourStatisticDto tourStatisticDto = tourService.TourStatistic(tourId);

                TextBlock touristsUnder18 = (TextBlock)FindName("TouristsUnder18");
                touristsUnder18.Text = tourStatisticDto.TouristsUnder18.ToString();

                TextBlock touristsBetween18And50 = (TextBlock)FindName("TouristsBetween18And50");
                touristsBetween18And50.Text = tourStatisticDto.TouristsBetween18and50.ToString();

                TextBlock touristsOver50 = (TextBlock)FindName("TouristsOver50");
                touristsOver50.Text = tourStatisticDto.TouristsOver50.ToString();

                TextBlock touristsWithVoucherPercentage = (TextBlock)FindName("TouristsWithVoucher");
                touristsWithVoucherPercentage.Text = tourStatisticDto.TouristsWithVoucherPercentage.ToString();
                
                TextBlock touristsWithoutVoucherPercentage = (TextBlock)FindName("TouristsWithoutVoucher");
                touristsWithoutVoucherPercentage.Text = tourStatisticDto.TouristsWithVoucherPercentage.ToString();


            }
        }
        public void MostVisitedTour_Click(object sender, RoutedEventArgs e)
        { 
            using(var context = new MyDbContext())
            {
                
                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository);

                List<Tour> tours = tourService.MostVisited();
                ListOfTours.ItemsSource = tours;
                

            }
        }

        public void MostVisitedTourByYear_Click(object sender, RoutedEventArgs e) 
        {
            using (var context = new MyDbContext())
            {

                var tourRepository = new TourRepository(context);
                var tourService = new TourService(tourRepository);

                var year = int.Parse(YearStatistic.Text);

                List<Tour> tours = tourService.MostVisitedByYear(year);
                ListOfTours.ItemsSource = tours;
                string content = string.Empty;

                var tourinfo = tourService.MostVisitedByYear(year);
                if (tourinfo == null)
                {
                    content = "Nema ture u toj godini!";
                }
                
                FreePlacesLabel.Content = content;

            }

        }


    }
}
