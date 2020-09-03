using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    class GoogleMapGeometry
    {
        [JsonProperty("location")]
        public GoogleMapLocationModel Location { get; set; }
    }
}
