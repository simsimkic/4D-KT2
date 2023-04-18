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
    /// Interaction logic for CancelTour.xaml
    /// </summary>
    public partial class CancelTour : Window
    {
        public CancelTour()
        {
            InitializeComponent();
        }

        public void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var touristRepository = new TouristRepository(context);
                var tourService = new TourService(context);


                List<Tour> tours = tourRepository.GetAll();
                ListOfTours.ItemsSource = tours;
            }


        }

        public void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var tourRepository = new TourRepository(context);
                var touristRepository = new TouristRepository(context);
                var tourService = new TourService(tourRepository, touristRepository);
                //var service = new TourService(context);


                var tourId = int.Parse(TourIdCancel.Text);
                string content = "";

                bool CancelTourInfo = tourService.CancelTour(tourId);
                if (CancelTourInfo == false) 
                {
                    content = "Ne mozete otkazati turu koja krece za manje od 48h!";
                }
                else
                {
                    content = "Uspesno otkazana tura!";
                }

                FreePlacesLabel.Content = content;
            }
        }

    }
}
