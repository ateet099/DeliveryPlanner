using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using DeliveryPlanner.Model;
using DeliveryPlanner.Constants;
using Autofac;
using DeliveryPlanner.Services;
using DeliveryPlanner.Helper;
using System.Diagnostics;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DeliveryPlanner.Controller
{
       public class Functions
        {
            private IViewService _viewService;
            private readonly Lazy<ILifetimeScope> _lifetimeScope = new Lazy<ILifetimeScope>(Bootstrapper.CreateContainer());

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
        }


        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The API Gateway response.</returns>
        public APIGatewayProxyResponse FunctionHandler(LambdaRequestModel request, ILambdaContext context)
        {
            APIGatewayProxyResponse response;
            var timer = Stopwatch.StartNew();
            Logger.LogInfo(timer.Elapsed.TotalMilliseconds, "REQUEST STARTED");

            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} is null");
            }

            GlobalContext.UserName = request.UserName;
            GlobalContext.ClientRequestId = request.ClientRequestId;

            using (var innerScope = _lifetimeScope.Value.BeginLifetimeScope())
            {
                _viewService = innerScope.Resolve<IViewService>();
            }

            try
            {
                var result = _viewService.GetFastestDelivery(request);
                response = LambdaResponse(result);
            }
            catch (Exception e)
            {
                Logger.LogError( 0, $"Request failed -  {e.Message}", e.StackTrace);
                response = LambdaResponse(null);
            }

            timer.Stop();
            Logger.LogInfo(timer.Elapsed.TotalMilliseconds, "REQUEST END");
            return response;
        }

        APIGatewayProxyResponse LambdaResponse(LambdaResponseModel result)
        {
            int statusCode = (result != null) ?
                (int)HttpStatusCode.OK :
                (int)HttpStatusCode.InternalServerError;

            string body = (result != null) ?
                JsonConvert.SerializeObject(result) : string.Empty;

            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
            return response;
        }
    }
}
