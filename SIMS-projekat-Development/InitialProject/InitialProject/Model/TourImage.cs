using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class TourImage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }

        public TourImage()
        {
        }

        public TourImage(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
