using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Model
{
    [JsonObject]
    class GoogleMapDistanceMatixElement : IGoogleMapDistanceMatrixElement
    {
        [JsonProperty("distance")]
        public GoogleMapDistanceMatixValues Distance { get; set; }

        [JsonProperty("duration")]
        public GoogleMapDistanceMatixValues Duration { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}