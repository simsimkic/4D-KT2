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
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window
    {
        public TourTracking()
        {
            InitializeComponent();
        }

        private void ShowTodayTours_Click(object sender, RoutedEventArgs e)
        {
            using( var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var locationRepository = new LocationRepository(context);
                var tourService = new TourService(tourRepository, locationRepository);
                var service = new TourService(context);

                List<Tour> tours = tourService.ShowTodayTours();
                TourList.ItemsSource = tours;
            }
        }
    }
}
