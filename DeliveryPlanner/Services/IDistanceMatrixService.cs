using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Services
{
    interface IDistanceMatrixService
    {
        /// <summary>
        /// Gets The Google Map API distance matrix for given origins and destinations.
        /// </summary>
        /// <param name="client">The HttpClient object.</param>
        /// <param name="origins">List of origin address</param>
        /// <param name="destinations">List of destination address</param>
        /// <returns>Returns Google Map API distance matrix for given origins and destinations. Returns null if nothing is returned from the Api.</returns>
        Task<GoogleMapResponse<GoogleMapDistanceMatrixRow>> GetDistanceMatrix(HttpClient client, List<string> origins, List<string> destinations);
    }
}
