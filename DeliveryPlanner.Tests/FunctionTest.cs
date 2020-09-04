using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using DeliveryPlanner;
using Newtonsoft.Json;
using DeliveryPlanner.Model;
using DeliveryPlanner.Controller;

namespace DeliveryPlanner.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
            Environment.SetEnvironmentVariable("GoogleMapKey", "AIzaSyDOysssk3TVGLj3vjOZk-aM1RZ8RlXMxg4");
        }

        [Fact]
        public void TetGetMethod()
        {
            TestLambdaContext context;
            LambdaRequestModel request;
            APIGatewayProxyResponse response;
            
            var depotAddress = "Metro-Straße 12, 40235 Düsseldorf, Germany";
            var storeAddress = "Bilker Allee 128, 40217 Düsseldorf, Germany";
            var customerAddress = "Friedrichstraße 133, 40217 Düsseldorf, Germany";
            Functions functions = new Functions();
            request = new LambdaRequestModel() {
                Address = customerAddress,
                ClientRequestId ="123",
                UserName="ateet@"
            };
            context = new TestLambdaContext();
            response = functions.FunctionHandler(request, context);

            Assert.Equal(200, response.StatusCode);
            var result = JsonConvert.DeserializeObject<LambdaResponseModel>(response.Body);
            Assert.Equal(depotAddress, result.Depot.Address);
            Assert.Equal(storeAddress, result.Store.Address);
            Assert.Equal(customerAddress, result.Customer.Address);
        }
    }
}
