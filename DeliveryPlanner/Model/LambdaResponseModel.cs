using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    public class LambdaResponseModel
    {
        public GoogleMapLocationModel Customer { get; set; }
        public GoogleMapLocationModel Depot { get; set; }
        public GoogleMapLocationModel Store { get; set; }      
        public string Time { get; set; }  //HH:MM:SS Format
        public double Distance { get; set; } //Km
    }
}
