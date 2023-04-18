using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class VoucherService
    {
        private readonly VoucherRepository _voucherRepository;
        private MyDbContext context;
        public VoucherService(MyDbContext context)
        {
            this.context = context;
        }

        public VoucherService(VoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public List<Voucher> GetActiveVouchers()
        {
            List<Voucher> allVouchers = _voucherRepository.GetAll();
            List<Voucher> activeVouchers = new List<Voucher>();

            foreach (Voucher voucher in allVouchers)
            {
                if (voucher.Redeemed == false)
                {
                    activeVouchers.Add(voucher);
                }
            }

            return activeVouchers;
        }

        public void UseVoucher(int? voucherId, int tourId)
        {
            Voucher voucher = _voucherRepository.GetById(voucherId);
            voucher.Redeemed = true;
            voucher.UsedOnTourId = tourId;

            _voucherRepository.Update(voucher);
        }
    }
}
