using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class TourReviewRepository
    {
        private readonly DbContext _dbContext;
        public TourReviewRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TourReview tourReview)
        {
            _dbContext.Set<TourReview>().Add(tourReview);
            _dbContext.SaveChanges();
        }
    }
}
