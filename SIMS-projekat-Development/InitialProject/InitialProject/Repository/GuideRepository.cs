using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class GuideRepository
    {
        private readonly DbContext _context;

        public GuideRepository(DbContext context)
        {
            _context = context;
        }

        public List<Guide> GetAll()
        {
            return _context.Set<User>().OfType<Guide>().ToList();
        }
        public Guide GetById(int id)
        {
            List<Guide> guides = GetAll();
            Guide guide = new Guide();

            foreach (Guide g in guides)
            {
                if (g.Id == id)
                {
                    guide = g;
                }
            }
            return guide;
        }

    }
}
