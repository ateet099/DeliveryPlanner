using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace DeliveryPlanner.Constants
{
    public static class LocationConstants
    {
        public static string[] FleetDepotsAddress =>
            new[] {
                "Metrostrasse 12, 40235 Düsseldorf",
                "Ludenberger Str. 1, 40629 Düsseldorf"
            };

        public static string[] StoreAddress =>
            new[] {
                "Willstätterstraße 24, 40549 Düsseldorf",
                "Bilker Allee 128, 40217 Düsseldorf",
                "Hammer Landstraße 113, 41460 Neuss",
                "Gladbacher Str. 471, 41460 Neuss",
                "Lise-Meitner-Straße 1, 40878 Ratingen"
            };
    }
}
