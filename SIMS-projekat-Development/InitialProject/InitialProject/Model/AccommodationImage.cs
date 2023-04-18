using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public AccommodationImage()
        {
        }

        public AccommodationImage(string name, string url)
        {
            Name = name;
            Url = url;
        }


    }
}
