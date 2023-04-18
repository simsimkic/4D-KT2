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
using System.Windows.Shapes;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for CurrentTour.xaml
    /// </summary>
    /// 


    public partial class CurrentTour : Window
    {
        public int UserId;
        public int TourId;
        public CurrentTour(int userId)
        {
            InitializeComponent();
            Loaded += ActiveTour_Loaded;

            this.UserId = userId;
        }

        private void ActiveTour_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var touristService = new TouristService(db);

                Tour currentTour = touristService.GetCurrentTour(UserId);
                CheckPoint currentCheckPoint = touristService.ShowCurrentCheckPoint(UserId);

                if(currentTour == null)
                {
                    MessageBox.Show("Tura je null");
                }
                
                TourNameLabel.Content = currentTour.Name;
                CurrentCheckPointLabel.Content = currentCheckPoint.Name;

                TourId = currentTour.Id;
            }
        }

        private void OpenRateTour(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var touristService = new TouristService(db);

                Tour currentTour = touristService.GetCurrentTour(UserId);
                CheckPoint currentCheckPoint = touristService.ShowCurrentCheckPoint(UserId);

                if(currentCheckPoint.CheckPointType == CheckPointType.EndPoint)
                {
                    RateTour newView = new RateTour(UserId, TourId);
                    newView.Show();
                }
                else
                {
                    MessageBox.Show("Current CheckPoint is not EndPount!");
                }

                
            }
        }
    }
}
