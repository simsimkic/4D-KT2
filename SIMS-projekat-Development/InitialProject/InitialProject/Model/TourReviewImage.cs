using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class TourReviewImage
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public TourReviewImage(string url)
        {
            Url = url;
        }
    }
}
