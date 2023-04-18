using InitialProject.Context;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore;
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

namespace InitialProject.View.Guest
{
    /// <summary>
    /// Interaction logic for Guest.xaml
    /// </summary>
    public partial class Guest : Window
    {
        public Guest()
        {
            InitializeComponent();
        }

        private void SearchAccommodation_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                
                var accommodationRepository = new AccommodationRepository(db);
                var locationRepository = new LocationRepository(db);
                var accommodationService = new AccommodationService(accommodationRepository, locationRepository);

                string nameSearch = null;
                string countrySearch = null;
                string citySearch = null;
                int? numberOfGuestsSearch = null;
                int? reservationDaysSearch = null;

                if (!string.IsNullOrEmpty(NameSearch.Text))
                {
                    nameSearch = NameSearch.Text;
                }

                if (!string.IsNullOrEmpty(CountrySearch.Text))
                {
                    countrySearch = CountrySearch.Text;
                }

                if (!string.IsNullOrEmpty(CitySearch.Text))
                {
                    citySearch = CitySearch.Text;
                }

                if (!string.IsNullOrEmpty(NumberOfGuestsSearch.Text) && int.TryParse(NumberOfGuestsSearch.Text, out int parsedNumberOfGuests))
                {
                    numberOfGuestsSearch = parsedNumberOfGuests;
                }


                if (!string.IsNullOrEmpty(ReservationDaysSearch.Text) && int.TryParse(ReservationDaysSearch.Text, out int parsedReservationDays))
                {
                    reservationDaysSearch = parsedReservationDays;
                }
                
                var accommodations = accommodationService.Search(
                    nameSearch, 
                    TypeSearch.SelectedIndex,
                    countrySearch, citySearch,
                    numberOfGuestsSearch,
                    reservationDaysSearch);

                AccommodationsList.ItemsSource = accommodations; 
            }
        }

        private void NumberOfGuests(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void ResationDaysText(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void OpenReserveView(object sender, RoutedEventArgs e)
        {
            AccommodationLocationDto selectedItem = (AccommodationLocationDto)AccommodationsList.SelectedItem;

            Reserve newWindow = new Reserve(selectedItem);

            newWindow.Show();
        }
    }
}
