using InitialProject.Context;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for Reserve.xaml
    /// </summary>
    /// 

    public partial class Reserve : Window
    {
        public AccommodationLocationDto accommodationLocationDto;
        private int accommodationId;
        public Reserve(AccommodationLocationDto selectedItem)
        {
            InitializeComponent();
            accommodationId = selectedItem.Id;
            DataContext = selectedItem;
        }

        private void ResationDaysText(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void ShowDateClick(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var accommodationRepository = new AccommodationRepository(db);
                var reservationRepository = new ReservationRepository(db);

                var reservationService = new ReserationService(reservationRepository, accommodationRepository);

                DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
                DateTime endDate = (DateTime)EndDatePicker.SelectedDate;

                
                Accommodation accommodation = accommodationRepository.FindById(accommodationId);
                
                // Check if reservation days less then minimal reservation period allowed
                if (int.Parse(ReserveDays.Text) == null || int.Parse(ReserveDays.Text) < accommodation.MinimalReservationPeriod )
                {
                    MessageBox.Show("Please set a bigger reservation period");
                } 
                else 
                {
                    var availabeDates = reservationService.GetAvailableDates(accommodationId, startDate, endDate, int.Parse(ReserveDays.Text));

                    // If there are no dates it will try again with later dates
                    while (availabeDates == null)
                    {
                        startDate = endDate;
                        endDate = startDate.AddDays(int.Parse(ReserveDays.Text)*5);
                        availabeDates = reservationService.GetAvailableDates(accommodationId, startDate, endDate, int.Parse(ReserveDays.Text));
                    }

                    AvailableDates.ItemsSource = availabeDates;
                }
            }
        }

        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var accommodationRepository = new AccommodationRepository(db);
                var reservationRepository = new ReservationRepository(db);

                var reservationService = new ReserationService(reservationRepository, accommodationRepository);

                //Popup insert numberOfGuests
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter a number of guests", "Number of Guests");
                int numberOfGuests;

                if (AvailableDates.SelectedItem == null)
                {
                    MessageBox.Show("Please select a date");
                }
                else
                {
                    ReservationDto selectedItem = (ReservationDto)AvailableDates.SelectedItem;
                    Accommodation accommodation = accommodationRepository.FindById(accommodationId);

                    if (int.TryParse(input, out numberOfGuests))
                    {
                        if (numberOfGuests > accommodation.MaxNumberOfGuests)
                        {
                            MessageBox.Show("Please enter a smaller number of guests");
                        }
                        else
                        {
                            reservationRepository.AddReservation(selectedItem.StartDate, selectedItem.EndDate, accommodation, numberOfGuests);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please write a number");
                    }
                } 
            }
        }
    }
}