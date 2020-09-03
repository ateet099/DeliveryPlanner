using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    interface IGoogleMapAddress
    {
        string PlaceId { get; set; }
        string FormattedAddress { get; set; }
        GoogleMapGeometry Geometry { get; set; }
    }
}
