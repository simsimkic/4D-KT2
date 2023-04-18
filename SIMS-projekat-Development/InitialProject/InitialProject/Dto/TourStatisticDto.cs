using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    internal class TourStatisticDto
    {
        public int TouristsUnder18 { get; set; }
        public int TouristsBetween18and50 { get; set; }
        public int TouristsOver50 { get; set; }
        public double TouristsWithVoucherPercentage { get; set; }
        public double TouristsWithoutVoucherPercentage { get; set; }

        public TourStatisticDto(int touristsUnder18, int touristsBetween18and50, int touristsOver50, double touristsWithVoucherPercentage, double touristsWithoutVoucherPercentage)
        {
            TouristsUnder18 = touristsUnder18;
            TouristsBetween18and50 = touristsBetween18and50;
            TouristsOver50 = touristsOver50;
            TouristsWithVoucherPercentage = touristsWithVoucherPercentage;
            TouristsWithoutVoucherPercentage = touristsWithoutVoucherPercentage;
        }
    }
}
