using InitialProject.Context;
using InitialProject.Dto;
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
    /// Interaction logic for BookTour.xaml
    /// </summary>
    public partial class BookTour : Window
    {
        public int userId;
        public BookTour(int userId)
        {
            InitializeComponent();
            Loaded += ActiveVouchers_Loaded;

            this.userId = userId;
        }

        private void ActiveVouchers_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var voucherRepository = new VoucherRepository(db);
                var voucherService = new VoucherService(voucherRepository);

                List<Voucher> activeVouchers = voucherService.GetActiveVouchers();
                ActiveVouchers.ItemsSource = activeVouchers;
            }
        }

        private void BookTour_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var voucherRepository = new VoucherRepository(db);
                var voucherService = new VoucherService(voucherRepository);
                var locationRepository = new LocationRepository(db);
                var tourRepository = new TourRepository(db, locationRepository);
                var tourService = new TourService(tourRepository, locationRepository, voucherService);
                

                int? voucherId = null;

                if (!string.IsNullOrEmpty(VoucherIdBook.Text) && int.TryParse(VoucherIdBook.Text, out int parsedVoucher))
                {
                    voucherId = parsedVoucher;
                }

                BookInformationDto bookInfo = tourService.BookTour(
                    int.Parse(TourIdBook.Text),
                    int.Parse(NumberOfTouristsBook.Text),
                    voucherId
                );

                List<Voucher> activeVouchers = voucherService.GetActiveVouchers();
                ActiveVouchers.ItemsSource = activeVouchers;

                List<BookInformationDto>? list = new List<BookInformationDto>();
                list.Add(bookInfo);


                if (bookInfo.FreePlaces > 0 && bookInfo.IsAccepted == false)
                {
                    //ispisati pre
                    int freePlaces = bookInfo.FreePlaces;
                    string content = "Nema mesta. Slobodnih mesta za izabranu turu: " + freePlaces.ToString();
                    ToursWithSameLocation.ItemsSource = null;
                    FreePlacesLabel.Content = content;
                }

                if (bookInfo.IsAccepted == true)
                {
                    string content = "Tura je uspešno bukirana";
                    ToursWithSameLocation.ItemsSource = null;
                    FreePlacesLabel.Content = content;
                }


                Tour tour = tourRepository.GetById(bookInfo.TourId);
                Location location = locationRepository.GetByTourId(bookInfo.TourId);
                if (bookInfo.FreePlaces == 0 && bookInfo.IsAccepted == false)
                {
                    List<Tour> tours = tourRepository.GetByLocationId(location.Id);

                    List<TourLocationDto> tourLocationDtos = tourService.AddLocationsToTours(tours);
                    FreePlacesLabel.Content = "Nema mesta u izabranoj turi. " +
                        "Ture sa istom lokacijom:";
                    ToursWithSameLocation.ItemsSource = tourLocationDtos;
                }
            }
        }
    }
}
