using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public enum UserType
    {
        Owner,
        Guest,
        Tourist,
        Guide
    }

    public enum AccommodationType
    {
        Apartmant,
        Hut,
        House
    }

    public enum CheckPointType
    {
        StartPoint,
        InbetweenPoint,
        EndPoint
    }
}
