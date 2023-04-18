using InitialProject.Context;
using InitialProject.Dto;
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
    /// Interaction logic for UnratedGuestsWindow.xaml
    /// </summary>
    public partial class UnratedGuestsWindow : Window
    {
        public UnratedGuestsWindow()
        {
            InitializeComponent();

            using (var db = new MyDbContext())
            {
                var guestRepository = new GuestRepository(db);
                var guestService = new GuestService(guestRepository);
                // Get a collection of GuestDto instances from the database or elsewhere
                List<GuestDto> guests = guestService.getUnratedGuests();
                //GuestDto tmp = new GuestDto(1,3,"luzer",DateTime.Now,DateTime.Now);
                //guests.Add(tmp);
                // Set the ItemsSource property of the GuestListView to the collection of guests
                GuestListView.ItemsSource = guests;
            }
        }

    }
}
