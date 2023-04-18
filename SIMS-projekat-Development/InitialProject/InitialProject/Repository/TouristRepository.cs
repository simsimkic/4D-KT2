using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class TouristRepository
    {

        private readonly DbContext _context;

        public TouristRepository(DbContext context)

        {
            _context = context;
        }

        //private TouristRepository() { }
        public Tourist GetById(int touristId)
        {
            return _context.Set<Tourist>().Include(t => t.Vouchers).FirstOrDefault(t => t.Id == touristId);

        }

        public Tourist GetByName(string touristName)
        {
            return _context.Set<Tourist>().Include(t => t.Vouchers).FirstOrDefault(t => t.Username == touristName);

        }

        public void AddVoucher(int touristId, Voucher voucher)
        {
            var tourist = _context.Set<Tourist>().Include(t => t.Vouchers).FirstOrDefault(t => t.Id == touristId);

            if (tourist != null)
            {
                tourist.Vouchers.Add(voucher);
                _context.SaveChanges();
            }
        }

        public void Update(Tourist tourist)
        {
            _context.Entry(tourist).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

}

