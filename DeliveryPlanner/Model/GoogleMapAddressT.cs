using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    [JsonObject]
    class GoogleMapAddressT: IGoogleMapAddress
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public GoogleMapGeometry Geometry { get; set; }
        [JsonIgnore]
        public List<string> StringTypes { get; set; }
    }
}
