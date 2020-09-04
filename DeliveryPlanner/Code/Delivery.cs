using DeliveryPlanner.Constants;
using DeliveryPlanner.Model;
using DeliveryPlanner.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPlanner.Code
{
    class Delivery
    {
        private readonly HttpClient _client;
        private GoogleMap _googleMap;
        private int MaxResponseContentBufferSize { get; } = 256000;

        public Delivery()
        {
            _client = new HttpClient { MaxResponseContentBufferSize = MaxResponseContentBufferSize };
            _googleMap = new GoogleMap(Environment.GetEnvironmentVariable("GoogleMapKey"));
        }       

        public LambdaResponseModel CalculateFastestDelivery(LambdaRequestModel request)
        {
            //Google Map Distance matrix request
            var distanceMatrixResponse = GetDistanceMatrix(request);
            var storeCustomerMatrix = distanceMatrixResponse[0];
            var fleetStoreMatrix = distanceMatrixResponse[1];
            var storeToCustomer = GetFastestOriginDestinationFromMatrix(storeCustomerMatrix);
            var fleetToStore = GetFastestOriginDestinationFromMatrix(fleetStoreMatrix, storeToCustomer);

            //Fastest storeToCustomer
            var fleetAddress = fleetToStore.FirstOrDefault().Origin;
            var storeAddress = fleetToStore.FirstOrDefault().Destination;
            var customerAddress = storeToCustomer.FirstOrDefault().Destination;

            //GeoCode Address to get Latitude and Longitude 
            var GeoCodeAddresses = Geocode(fleetAddress, storeAddress, customerAddress);             

            var totalDistance = GetTotalDistanceBetweenFleetToCustomer(fleetToStore, storeToCustomer);
            var timetaken = GetTotalTravelTimeBetweenFleetToCustomer(totalDistance);

            var queryResponse = new LambdaResponseModel
            {
                Depot = new GoogleMapLocationModel(GeoCodeAddresses[0].Address, GeoCodeAddresses[0].Coordinates.Lat, GeoCodeAddresses[0].Coordinates.Lng),
                Store = new GoogleMapLocationModel(GeoCodeAddresses[1].Address, GeoCodeAddresses[1].Coordinates.Lat, GeoCodeAddresses[1].Coordinates.Lng),
                Customer = new GoogleMapLocationModel(GeoCodeAddresses[2].Address, GeoCodeAddresses[2].Coordinates.Lat, GeoCodeAddresses[2].Coordinates.Lng),
                Time = timetaken,
                Distance = totalDistance
            };
            return queryResponse;
        }

        private GoogleMapResponse<GoogleMapDistanceMatrixRow>[] GetDistanceMatrix(LambdaRequestModel request)
        {
            return Task.WhenAll(
               _googleMap.GetAllDistanceResults(_client, new List<string>(LocationConstants.StoreAddress), new List<string>() { request.Address }),
               _googleMap.GetAllDistanceResults(_client, new List<string>(LocationConstants.FleetDepotsAddress), new List<string>(LocationConstants.StoreAddress))
           ).Result;
        }

        private GoogleMapLocationModel[] Geocode(string fleetAddress, string storeAddress, string customerAddress)
        {
            return Task.WhenAll(
                _googleMap.GetLocationByAddress(_client, fleetAddress),
                _googleMap.GetLocationByAddress(_client, storeAddress),
                _googleMap.GetLocationByAddress(_client, customerAddress)
           ).Result;
        }

        private double GetTotalDistanceBetweenFleetToCustomer(List<GoogleDistanceMatrixModel> fleetToStore, List<GoogleDistanceMatrixModel> storeToCustomer)
        {
            return fleetToStore.FirstOrDefault().Distance + storeToCustomer.FirstOrDefault().Distance;
        }

        private string GetTotalTravelTimeBetweenFleetToCustomer(double totalDistance)
        {
            var time = (totalDistance / 1000) / DroneConstants.speed;
            TimeSpan span = TimeSpan.FromHours(time);
            return span.ToString("mm\\:ss");//To minute second format
        }

        private List<GoogleDistanceMatrixModel> GetFastestOriginDestinationFromMatrix(
                GoogleMapResponse<GoogleMapDistanceMatrixRow> matrixResponse,
                List<GoogleDistanceMatrixModel> waypointMatrix = null)
        {
            int rowIndex = 0;
            int colIndex = 0;
            double minDistance;
            List<string> destinationAddress = waypointMatrix != null? waypointMatrix.Select(o => o.Origin).ToList():null;
            List<GoogleDistanceMatrixModel> matrix = new List<GoogleDistanceMatrixModel>();

            minDistance = -1;
            foreach (var row in matrixResponse.Rows)
            {
                colIndex = 0;
                foreach (var element in row.Elements)
                {
                    if (destinationAddress  != null && !destinationAddress.Contains(matrixResponse.DestinationAddresses[colIndex])){
                        colIndex += 1;
                        continue;
                    }

                    if (minDistance == -1 || minDistance >= element.Distance.Value)
                    {                        
                        if (minDistance != element.Distance.Value)
                        {
                            matrix.Clear();
                        }
                        minDistance = element.Distance.Value;
                        matrix.Add(new GoogleDistanceMatrixModel
                        {
                            RowIndex = rowIndex,
                            ColumnIndex = colIndex,
                            Distance = element.Distance.Value,
                            Origin = matrixResponse.OriginAddresses[rowIndex],
                            Destination = matrixResponse.DestinationAddresses[colIndex]
                        });
                    }
                    colIndex += 1;
                }
                rowIndex += 1;
            }
            return matrix;
        }

    }
}
