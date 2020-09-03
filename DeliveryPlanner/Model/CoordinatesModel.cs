using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    public class CoordinatesModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public CoordinatesModel(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }
}
