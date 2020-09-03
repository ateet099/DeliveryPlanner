using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Services
{
    class GoogleMap: GoogleMapBase,IGoogleMap
    {
        private readonly GeocodeService _googleGeocode;
        private readonly DistanceMatrixService _googleDistanceMatrix;

        public GoogleMap(string apiKey) : base(apiKey)
        {
            _googleGeocode = new GeocodeService(apiKey);
            _googleDistanceMatrix = new DistanceMatrixService(apiKey);
        }

        /// <summary>
        /// Get Latitude and Longitud from given Address.
        /// </summary>
        /// <param name="client">The HttpClient object.</param>
        /// <param name="address">The address to search on Google Api.</param>
        public async Task<GoogleMapLocationModel> GetLocationByAddress(HttpClient client, string address)
        {
            var location = await _googleGeocode.GetCoordinatesFromAddress(client, address);           
            return location;
        }


        /// <summary>
        /// Gets the Google Map distance matrix for guven points of origin and destinations.
        /// </summary>
        /// <param name="client">The HttpClient object.</param>
        /// <param name="address">The address to search on Google Api.</param>
        public async Task<GoogleMapResponse<GoogleMapDistanceMatrixRow>> GetAllDistanceResults(HttpClient client, List<string> origins, List<string> destinations)
        {
            var location = await _googleDistanceMatrix.GetDistanceMatrix(client, origins,destinations);
            return location;
        }

    }
}
