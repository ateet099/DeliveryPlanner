using DeliveryPlanner.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliveryPlanner.Services
{
    class GeocodeService : GoogleMapBase,IGeocodeService
    {
        public GeocodeService(string apiKey) : base(apiKey)
        {
        }
        /// <summary>
        /// Gets the first coordinates from a possible list of address components.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="address">The address to search on Google Api.</param>
        /// <returns>Returns a Location where item1 is latitude and item2 is longitude. Returns null if nothing is returned from the Api.</returns>
        public async Task<GoogleMapLocationModel> GetCoordinatesFromAddress(HttpClient client, string address)
        {
            var result = await SearchAddress(client, address);
            var geoAddresses = result.Results.FirstOrDefault();
            if (geoAddresses != null)
            {
                return new GoogleMapLocationModel(
                    geoAddresses.FormattedAddress,
                    geoAddresses.Geometry?.Location.Coordinates?.Lat ?? 0,
                    geoAddresses.Geometry?.Location.Coordinates?.Lng ?? 0);
            }
            return null;
        }

        /// <summary>
        /// Search for an address and returns all results from Google Api.
        /// </summary>
        /// <param name="client">HttpClient object.</param>
        /// <param name="address">The address to search on Google Api.</param>
        /// <returns>Returns all results from Google Api as an Response<Address>.</returns>
        public async Task<GoogleMapResponse<GoogleMapAddress>> SearchAddress(HttpClient client, string address)
        {
            if (address is null)
            {
                throw new ArgumentException("Address is required'");
            }
            var uri = GetGeocodingQueryString(("address", address));
            var response = await client.GetAsync(uri);
            return await GoogleMapAddress.ParseResponse(response);
        }
    }
}
