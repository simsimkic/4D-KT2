using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    class CheckPointRepository
    {

        private readonly DbContext _dbContext;

        public CheckPointRepository(DbContext context)
        {
            _dbContext = context;
        }

        public List<CheckPoint> GetAll()
        {
            return _dbContext.Set<CheckPoint>()
                .Include(cp => cp.PresentTourists)
                .ToList();
        }
    }
}
