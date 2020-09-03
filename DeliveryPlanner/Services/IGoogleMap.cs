using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Services
{
    interface IGoogleMap
    {
        /// <summary>
        /// Get Latitude and Longitud from given Address.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="address">address</param>
        /// <param name="returnFields">Address Latitude and Longitude</param>
        /// <returns></returns>
        Task<GoogleMapLocationModel> GetLocationByAddress(HttpClient client, string address);
    }
}
