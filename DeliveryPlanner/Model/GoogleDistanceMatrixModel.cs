using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Model
{
    class GoogleDistanceMatrixModel
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Distance { get; set; }
    }
}
