using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Services
{
    class DistanceMatrixService: GoogleMapBase,IDistanceMatrixService
    {
        public DistanceMatrixService(string apiKey) : base(apiKey)
        {
        }
        /// <summary>
        /// Gets The Google Map API distance matrix for given origins and destinations.
        /// </summary>
        /// <param name="client">The HttpClient object.</param>
        /// <param name="origins">List of origin address</param>
        /// <param name="destinations">List of destination address</param>
        /// <returns>Returns Google Map API distance matrix for given origins and destinations. Returns null if nothing is returned from the Api.</returns>
        public async Task<GoogleMapResponse<GoogleMapDistanceMatrixRow>> GetDistanceMatrix(HttpClient client, List<string> origins, List<string> destinations)
        {
            var result = await DistanceMatrixRequest(client, origins, destinations);
            return result;
        }

        /// <summary>
        /// Gets The Google Map API distance matrix for given origins and destinations.
        /// </summary>
        /// <param name="origins">List of origin address</param>
        /// <param name="destinations">List of destination address</param>
        /// <returns>Returns Google Map API distance matrix for given origins and destinations. Returns null if nothing is returned from the Api.</returns>
        public async Task<GoogleMapResponse<GoogleMapDistanceMatrixRow>> DistanceMatrixRequest(HttpClient client, List<string> origins, List<string> destinations)
        {
            if (origins is null || destinations is null)
            {
                throw new ArgumentException("origins and destinations are required'");
            }
            var uri = GetDistanceMatrixQueryString(
                    ("origins", string.Join("|", origins)),
                    ("destinations", string.Join("|", destinations))
                );
            var response = await client.GetAsync(uri);
            return await GoogleMapDistanceMatrixRow.ParseResponse(response);
        }
    }
}
