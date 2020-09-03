using DeliveryPlanner.Code;
using DeliveryPlanner.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Services
{
    class ViewService : IViewService
    {
        public LambdaResponseModel GetFastestDelivery(LambdaRequestModel request)
        {
            var delivery = new Delivery();
            return delivery.CalculateFastestDelivery(request);
        }
    }
}