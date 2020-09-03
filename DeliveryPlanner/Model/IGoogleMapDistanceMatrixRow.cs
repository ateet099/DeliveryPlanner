using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    interface IGoogleMapDistanceMatrixRow
    {
        List<GoogleMapDistanceMatixElement> Elements { get; set; }
    }
}
