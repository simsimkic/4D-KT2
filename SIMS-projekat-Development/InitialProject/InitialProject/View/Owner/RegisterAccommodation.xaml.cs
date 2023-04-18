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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for RegisterAccommodation.xaml
    /// </summary>
    public partial class RegisterAccommodation : Window
    {
        public RegisterAccommodation()
        {
            InitializeComponent();
        }

        
        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                string name = txtName.Text;
                string country = txtCountry.Text;
                string city = txtCity.Text;
                int accommodationType = AccommodationType.SelectedIndex;


                int maxGuests;
                if (!int.TryParse(txtMaxGuests.Text, out maxGuests))
                {
                    MessageBox.Show("Maximum guests must be a valid integer.");
                    return;
                }

                int minReservationPeriod;
                if (!int.TryParse(txtMinReservationPeriod.Text, out minReservationPeriod))
                {
                    MessageBox.Show("Minimum reservation period must be a valid integer.");
                    return;
                }

                int cancelPeriod;
                if (!int.TryParse(txtCancelPeriod.Text, out cancelPeriod))
                {
                    MessageBox.Show("Cancel period must be a valid integer.");
                    return;
                }

                var accommodationRepository = new AccommodationRepository(db);
                var locationRepository = new LocationRepository(db);
                var ownerRepository = new OwnerRepository(db);
                var accommodationService = new AccommodationService(accommodationRepository, locationRepository, ownerRepository);
                accommodationService.RegisterAccommodation(country, city, name, accommodationType, maxGuests, minReservationPeriod, cancelPeriod); 

            }
        }
    }
}
