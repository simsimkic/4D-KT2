using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    internal class BookInformationDto
    {
        public int TourId { get; set; }
        public bool IsAccepted { get; set; }
        public int FreePlaces { get; set; }
        public BookInformationDto() { }

        public BookInformationDto(int tourId, bool isAccepted, int freePlaces)
        {
            TourId = tourId;
            IsAccepted = isAccepted;
            FreePlaces = freePlaces;
        }
    }
}
