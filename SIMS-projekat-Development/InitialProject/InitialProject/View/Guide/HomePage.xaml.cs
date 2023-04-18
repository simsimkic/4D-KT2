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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private int loggedUserId;
        public HomePage(int loggedUserId)
        {
            InitializeComponent();
            this.loggedUserId = loggedUserId;
        }

        public void MakeTourButton_Click(object sender, RoutedEventArgs e)
        {
            MakeTour makeTour = new MakeTour();
            makeTour.Show();
            this.Close();
        }

        public void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            CancelTour cancelTour = new CancelTour();
            cancelTour.Show();
            this.Close();
        }
        public void TourStatistic_Click(object sender, RoutedEventArgs e)
        {
            TourStatistic tourStatistic = new TourStatistic(loggedUserId);
            tourStatistic.Show();
            this.Close();
        }
        public void TourTracking_Click(object sender, RoutedEventArgs e)
        {
            TourTracking tourTracking = new TourTracking();
            tourTracking.Show();
            this.Close();
        }
        public void ShowReview_Click(object sender, RoutedEventArgs e)
        {
            ShowReview showReview = new ShowReview(loggedUserId);
            showReview.Show();
            this.Close();
        }
    }
}
