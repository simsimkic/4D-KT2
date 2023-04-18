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

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for MakeTour.xaml
    /// </summary>
    public partial class MakeTour : Window
    {
        public MakeTour()
        {
            InitializeComponent();
        }

        private void MakeTourButton_Click(object sender, RoutedEventArgs e)
        {
            using( var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var locationRepository = new LocationRepository(context);
                var tourService = new TourService(tourRepository, locationRepository);
                var service = new TourService(context);

                DateTime StartingDate = (DateTime)StartingDatePicker.SelectedDate;

                var tour = new Tour {Name = NameAdd.Text, Description = DescriptionAdd.Text, Language = LanguageAdd.Text, MaxTourists = int.Parse(MaxTouristsAdd.Text), DurationInHours = int.Parse(DurationInHoursAdd.Text), StartingDate = StartingDate };

                var location = new Location { Country = CountryAdd.Text, City = CityAdd.Text };
                var tourImages = new List<TourImage> {
                    new TourImage { Url = ImageAdd.Text, Name = NameAdd.Text },
                    new TourImage { Url = ImageAdd.Text, Name = NameAdd.Text}
                    };
                var checkPoints = new List<CheckPoint> {
                    new CheckPoint { Name = CheckPointAdd.Text },
                    new CheckPoint { Name = CheckPointAdd.Text }
                    };
                tourService.MakeTour(tour, location, tourImages, checkPoints);
                
            }
        }
    }
    
}
