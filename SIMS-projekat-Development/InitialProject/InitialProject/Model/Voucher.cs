using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class Voucher
    {
        public int Id { get; set; }
        public DateTime StartingTime { get; set; }
        public bool Redeemed { get; set; }

        public int? UsedOnTourId { get; set; }

        public Voucher() { }

        public Voucher( DateTime startingTime, bool redeemed )
        {
            StartingTime = startingTime;
            Redeemed = redeemed;
        }
    }
}
