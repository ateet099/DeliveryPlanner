using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    [JsonObject]
    class GoogleMapDistanceMatixValues
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
