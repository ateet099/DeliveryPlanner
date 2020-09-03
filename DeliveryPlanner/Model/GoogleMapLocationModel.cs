using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    public class GoogleMapLocationModel
    {
        public string Address { get; set; }
        public CoordinatesModel Coordinates { get; set; }
        public GoogleMapLocationModel(string address, double lat, double lng)
        {
            Address = address;
            Coordinates = new CoordinatesModel(lat, lng);
        }
    }
}
