using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Services
{
    interface IViewService
    {
        LambdaResponseModel GetFastestDelivery(LambdaRequestModel request);
    }
}
