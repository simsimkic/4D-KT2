using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsRated { get; set; }



        public Reservation()
        {
        }

        public Reservation(int numberOfGuests, DateTime CheckInDate, DateTime CheckOutDate )
        {
            NumberOfGuests = numberOfGuests;
            this.CheckInDate = CheckInDate;
            this.CheckOutDate = CheckOutDate;
            IsRated = false;
        }


    }
}
