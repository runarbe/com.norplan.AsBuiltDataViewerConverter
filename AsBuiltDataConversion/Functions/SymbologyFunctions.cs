using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DotSpatial.Symbology;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class SymbologyFunctions
    {
        public static PolygonSymbolizer GetPolygonSymbolizer(Color pColor) {

            return new PolygonSymbolizer(pColor, pColor);

        }
    }
}
