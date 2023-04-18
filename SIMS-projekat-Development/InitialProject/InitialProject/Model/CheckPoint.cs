using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class CheckPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CheckPointType CheckPointType { get; set; }
        public bool Active { get; set; }
        public List<Tourist> PresentTourists { get; set; }

        public CheckPoint() 
        { 
        }

        public CheckPoint(string name, CheckPointType checkPointType)
        {
            Name = name;
            CheckPointType = checkPointType;
            Active = false;
        }

        public CheckPoint(string name, CheckPointType checkPointType, bool active, List<Tourist> presentTourists) : this(name, checkPointType)
        {
            Active = active;
            PresentTourists = presentTourists;
        }
    }
}
