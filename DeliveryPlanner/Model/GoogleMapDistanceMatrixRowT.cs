using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    [JsonObject]
    class GoogleMapDistanceMatrixRowT: IGoogleMapDistanceMatrixRow
    {
        [JsonProperty("elements")]
        public List<GoogleMapDistanceMatixElement> Elements { get; set; }
    }
}
