using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class Owner : User
    {
        public List<Accommodation> Accommodations { get; set; }

        public Owner() {
            Accommodations = new List<Accommodation>();
        }
        public Owner(string username, string password, UserType userType) : base(username, password, userType)
        {
            Accommodations = new List<Accommodation>();
        }

        public Owner( List<Accommodation> accommodations)
        {
            Accommodations = accommodations;
        }
    }
}
