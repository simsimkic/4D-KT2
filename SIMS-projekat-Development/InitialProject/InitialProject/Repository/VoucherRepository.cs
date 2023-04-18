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
    internal class VoucherRepository
    {
        private readonly DbContext _dbContext;

        public VoucherRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Voucher> GetAll()
        {
            return _dbContext.Set<Voucher>().ToList();
        }

        public Voucher GetById(int? id) 
        {
            List<Voucher> vouchers = GetAll();
            Voucher voucher = new Voucher();

            foreach (Voucher v in vouchers)
            {
                if (v.Id == id)
                {
                    voucher = v;
                }
            }
            return voucher;
        }

        public void Update(Voucher voucher)
        {
            _dbContext.Set<Voucher>().Update(voucher);
            _dbContext.SaveChanges();
        }
    }
}
