using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Helper
{
        public static class GlobalContext
        {
            public static string ClientRequestId { get; set; }
            public static string UserName { get; set; }
        }
}
