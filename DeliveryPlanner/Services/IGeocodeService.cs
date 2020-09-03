using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Services
{
    interface IGeocodeService
    {
        /// <summary>
        /// Gets the first coordinates from a possible list of address components.
        /// </summary>
        /// <param name="client">The HttpClient object.</param>
        /// <param name="address">The address to search on Google Api.</param>
        /// <returns>Returns a Location where item1 is latitude and item2 is longitude. Returns null if nothing is returned from the Api.</returns>
        Task<GoogleMapLocationModel> GetCoordinatesFromAddress(HttpClient client, string address);
    }
}
