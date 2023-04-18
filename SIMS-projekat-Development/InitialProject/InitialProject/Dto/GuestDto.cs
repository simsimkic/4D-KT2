using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    internal class GuestDto
    {
        public int GuestId { get; set; }

        public int ReservationId { get; set; }
        public string Username { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public GuestDto() { }

        public GuestDto(int id, int reservationId, string username, DateTime checkInDate, DateTime checkOutDate)
        {
            GuestId = id;
            ReservationId = reservationId;
            Username = username;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

    }

}
