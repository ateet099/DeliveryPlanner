using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Model
{
    class GoogleMapAddress : IGoogleMapAddress
    {
        public string PlaceId { get; set; }
        public string FormattedAddress { get; set; }
        public GoogleMapGeometry Geometry { get; set; }
        public static async Task<GoogleMapResponse<GoogleMapAddress>> ParseResponse(HttpResponseMessage response)
        {
            if (response is null)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw await GoogleMapResponse<GoogleMapAddressT>.ResponseError(response);
            }

            var content = await response.Content.ReadAsStringAsync();

            GoogleMapResponse<GoogleMapAddress> result;
            try
            {
                result = ParseResponse(JsonConvert.DeserializeObject<GoogleMapResponse<GoogleMapAddressT>>(content));               
            }
            catch (JsonSerializationException)
            {
                throw new Exception("Error Deserializeing  google map Response");
            }

            if (result is null)
            {
                throw await GoogleMapResponse<GoogleMapAddressT>.ResponseError(response);
            }

            if (result.Status == "INVALID_REQUEST")
            {
                throw new HttpRequestException(result.ErrorMessage ?? "Error");
            }

            return result;
        }

        private static GoogleMapResponse<GoogleMapAddress> ParseResponse(GoogleMapResponse<GoogleMapAddressT> address)
        {
            if (address is null)
            {
                return null;
            }
            
            return new GoogleMapResponse<GoogleMapAddress>()
            {
                ErrorMessage = address.ErrorMessage,
                Result = ParseAddress(address.Result),
                Results = address.Results?.Select(ParseAddress).ToList(),
                Status = address.Status
            };
        }
        public static GoogleMapAddress ParseAddress(GoogleMapAddressT address)
        {
            if (address is null)
            {
                return null;
            }

            return new GoogleMapAddress()
            {
                PlaceId = address.PlaceId,
                FormattedAddress = address.FormattedAddress,
                Geometry = address.Geometry
            };
        }
    }
}
