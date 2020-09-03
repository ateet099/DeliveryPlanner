using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    interface IGoogleMapDistanceMatrixElement
    {
        GoogleMapDistanceMatixValues Distance { get; set; }
        GoogleMapDistanceMatixValues Duration { get; set; }
    }
}
