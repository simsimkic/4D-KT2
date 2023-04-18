using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class TouristService
    {

        private readonly TourRepository _tourRepository;
        private readonly TouristRepository _touristRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private MyDbContext context;
        public TouristService(MyDbContext context)
        {
            this.context = context;
            this._tourRepository = new TourRepository(context);
            this._touristRepository = new TouristRepository(context);

        }

        public void AssignVoucher(int touristId)
        {
            Tourist tourist = _touristRepository.GetById(touristId);

            Voucher voucher = new Voucher(DateTime.Now, false);
            _touristRepository.AddVoucher(touristId, voucher);
            _touristRepository.Update(tourist);

        }

        public Tour GetCurrentTour(int touristId)
        {
            Tour tour = _tourRepository.GetByTouristId(touristId);


            if (tour.HasStarted)
            {
                return tour;
            }

            return null;
        }

        public CheckPoint ShowCurrentCheckPoint(int tourId)
        {
            Tour tour = _tourRepository.GetByTouristId(tourId);

            List<CheckPoint> checkPoints = tour.CheckPoints;

            CheckPoint checkPoint = new CheckPoint();

            foreach(CheckPoint cp in checkPoints)
            {
                if (cp.Active)
                {
                    checkPoint = cp;
                }
            }

            return checkPoint;
            
        }
        public List<CheckPoint> GetCheckPointsForTourist(string name)
        {
            Tourist tourist = _touristRepository.GetByName(name);
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            foreach (CheckPoint checkpoint in _checkPointRepository.GetAll())
            {
                if (checkpoint.PresentTourists.Contains(tourist))
                {
                    checkPoints.Add(checkpoint);
                }
            }
            return checkPoints;
        }
    }
}
