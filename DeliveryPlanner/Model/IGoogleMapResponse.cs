using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    interface IGoogleMapResponse<T>
    {
        List<T> Results { get; set; }
        T Result { get; set; }
        public List<string> DestinationAddresses { get; set; }
        public List<string> OriginAddresses { get; set; }
        public List<T> Rows { get; set; }
        string Status { get; set; }
        string ErrorMessage { get; set; }
    }
}
