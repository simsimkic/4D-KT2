using InitialProject.Context;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for SearchTourist.xaml
    /// </summary>
    public partial class SearchTourist : Window
    {
        public int UserId;
        public SearchTourist(int userId)
        {
            InitializeComponent();
            this.UserId = userId;
        }

        private void SearchTours_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var tourRepository = new TourRepository(db);
                var locationRepository = new LocationRepository(db);
                var tourService = new TourService(tourRepository, locationRepository);


                string countrySearch = null;
                string citySearch = null;
                string languageSearch = null;
                int? numberOfTourists = null;
                int? durationInHours = null;

                if (!string.IsNullOrEmpty(CountrySearch.Text))
                {
                    countrySearch = CountrySearch.Text;
                }

                if (!string.IsNullOrEmpty(CitySearch.Text))
                {
                    citySearch = CitySearch.Text;
                }

                if (!string.IsNullOrEmpty(LanguageSearch.Text))
                {
                    languageSearch = LanguageSearch.Text;
                }

                if (!string.IsNullOrEmpty(NumberOfTouristsSearch.Text) && int.TryParse(NumberOfTouristsSearch.Text, out int parsedTourists))
                {
                    numberOfTourists = parsedTourists;
                }

                if (!string.IsNullOrEmpty(DurationInHoursSearch.Text) && int.TryParse(DurationInHoursSearch.Text, out int parsedDuration))
                {
                    durationInHours = parsedDuration;
                }
                
                
                var tours = tourService.Search(
                    countrySearch,
                    citySearch,
                    languageSearch,
                    numberOfTourists,
                    durationInHours
                );
                


                TourList.ItemsSource = tours;
            }
        }

        private void OpenBookTour(object sender, RoutedEventArgs e)
        {
            BookTour newView = new BookTour(this.UserId);

            newView.Show();
        }

        private void OpenCurrentTour(object sender, RoutedEventArgs e)
        {
            CurrentTour newView = new CurrentTour(this.UserId);

            newView.Show();
        }
    }
}
