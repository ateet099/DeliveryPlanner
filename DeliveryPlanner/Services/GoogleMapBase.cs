using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace DeliveryPlanner.Services
{
    class GoogleMapBase
    {
        public static string ApiKey { get; set; }
        public static string DistanceMatrixUrl { get; set; } = "https://maps.googleapis.com/maps/api/distancematrix/";
        public static string GeocodingUrl { get; set; } = "https://maps.googleapis.com/maps/api/geocode/";
        
        public GoogleMapBase(string apiKey)
        {
            ApiKey = apiKey;
        }

        protected static string GetDistanceMatrixQueryString(params (string, string)[] keyValueTuples)
        {
            var queryString = GetQueryString(keyValueTuples);
            return $"{DistanceMatrixUrl}json?{queryString}";
        }

        protected static string GetGeocodingQueryString(params (string, string)[] keyValueTuples)
        {
            var queryString = GetQueryString(keyValueTuples);
            return $"{GeocodingUrl}json?{queryString}";
        }
        private static NameValueCollection GetQueryString(params (string, string)[] keyValueTuples)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(String.Empty);

            foreach (var keyValueTuple in keyValueTuples.Where(keyValueTuple => !String.IsNullOrEmpty(keyValueTuple.Item2)))
            {
                queryString[keyValueTuple.Item1] = keyValueTuple.Item2;
            }

            queryString["key"] = ApiKey;
            return queryString;
        }
    }
}
