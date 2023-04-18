using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.View.Owner
{
    public partial class RatingWindow : Window
    {
        public RatingWindow()
        {
            InitializeComponent();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                int cleanliness = (int)cleanlinessSlider.Value;
                int ruleCompliance = (int)ruleComplianceSlider.Value;
                string comment = commentTextBox.Text;
                int guestId = int.Parse(guestIDTextBox.Text);
                int reservationId = int.Parse(reservationIDTextBox.Text);


                
                var reservationRepository = new ReservationRepository(db);
                reservationRepository.MarkReservation(reservationId);

                GuestRating guestRating = new GuestRating(cleanliness, ruleCompliance, comment);
                var guestRepository = new GuestRepository(db);
                var guestService = new GuestService(guestRepository);
                guestService.GuestRatingInput(cleanliness, ruleCompliance, comment, guestId);
                
            }
        }
    }
}
