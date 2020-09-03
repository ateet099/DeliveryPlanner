using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliveryPlanner.Model
{
    class GoogleMapDistanceMatrixRow: IGoogleMapDistanceMatrixRow
    {
       public List<GoogleMapDistanceMatixElement> Elements { get; set; }

        public static async Task<GoogleMapResponse<GoogleMapDistanceMatrixRow>> ParseResponse(HttpResponseMessage response)
        {
            if (response is null)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw await GoogleMapResponse<GoogleMapDistanceMatrixRow>.ResponseError(response);
            }

            var content = await response.Content.ReadAsStringAsync();
            GoogleMapResponse<GoogleMapDistanceMatrixRow> result;
            try
            {
                result = ParseResponse(JsonConvert.DeserializeObject<GoogleMapResponse<GoogleMapDistanceMatrixRowT>>(content));
            }
            catch (JsonSerializationException)
            {
                throw new Exception("Error Deserializeing  google map distance matrix Response");
            }

            if (result is null)
            {
                throw await GoogleMapResponse<GoogleMapDistanceMatrixRow>.ResponseError(response);
            }

            if (result.Status == "INVALID_REQUEST")
            {
                throw new HttpRequestException(result.ErrorMessage ?? "Error");
            }

            return result;
        }

        private static GoogleMapResponse<GoogleMapDistanceMatrixRow> ParseResponse(GoogleMapResponse<GoogleMapDistanceMatrixRowT> matrixResponse)
        {
            if (matrixResponse is null)
            {
                return null;
            }

            return new GoogleMapResponse<GoogleMapDistanceMatrixRow>()
            {
                ErrorMessage = matrixResponse.ErrorMessage,
                DestinationAddresses = matrixResponse.DestinationAddresses,
                OriginAddresses = matrixResponse.OriginAddresses,
                Rows = ParseAddress(matrixResponse.Rows),
                Status = matrixResponse.Status
            };
        }

        public static List<GoogleMapDistanceMatrixRow> ParseAddress(List<GoogleMapDistanceMatrixRowT> matrixRowsT)
        {
            if (matrixRowsT is null)
            {
                return null;
            }

            var matrixRows = new List<GoogleMapDistanceMatrixRow>();
            foreach (var item in matrixRowsT)
            {
                matrixRows.Add(ParseAddress(item));
            }
            return matrixRows;
        }

        public static GoogleMapDistanceMatrixRow ParseAddress(GoogleMapDistanceMatrixRowT matrixRow)
        {
            if (matrixRow is null)
            {
                return null;
            }

            return new GoogleMapDistanceMatrixRow()
            {
                Elements = matrixRow.Elements
            };
        }
    }
}
