using Norplan.Adm.AsBuiltDataConversion.Functions;
using DotSpatial.Symbology;
using System.Drawing;

namespace Norplan.Adm.AsBuiltDataConversion.FeatureTypes
{
    public static class MapSymbols
    {
        public static PolygonSymbolizer PolygonSymbol(Color pColor, Color pOutlineColor)
        {
            return new PolygonSymbolizer(pColor, pOutlineColor);
        }

        public static PointSymbolizer PointSymbol(Color pColor, int pSize)
        {
            return new PointSymbolizer(pColor, PointShape.Rectangle, pSize);
        }

        public static LineSymbolizer LineSymbol(Color pColor, int pWidth)
        {
            return new LineSymbolizer(pColor, pWidth);
        }

    }
}
