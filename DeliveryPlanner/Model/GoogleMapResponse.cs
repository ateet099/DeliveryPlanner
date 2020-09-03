using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Model
{
    [JsonObject]
    public class GoogleMapResponse<T> : IGoogleMapResponse<T>
    {
        [JsonProperty("results")]
        public List<T> Results { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
               
        [JsonProperty("destination_addresses")]
        public List<string> DestinationAddresses { get; set; }

        [JsonProperty("origin_addresses")]
        public List<string> OriginAddresses { get; set; }
        
        [JsonProperty("rows")]
        public List<T> Rows { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        public static async Task<HttpRequestException> ResponseError(HttpResponseMessage response)
        {
            return new HttpRequestException($"Error parsing Response<{nameof(T)}>. Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
        }
    }
}
