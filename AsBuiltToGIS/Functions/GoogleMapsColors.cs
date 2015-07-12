using System.Drawing;

namespace AsBuiltToGIS.Functions
{
    public static class GoogleMapsColors
    {
        public static Color Sea = Color.FromArgb(190, 225, 255);
        public static Color Land = Color.FromArgb(240, 237, 229);
        public static Color Building = Color.FromArgb(223, 219, 212);
        public static Color MinorRoad = Color.FromArgb(224, 219, 209);
        public static Color MajorRoad = Color.FromArgb(255, 225, 104);
        public static Color BoundaryMinor = Color.FromArgb(182, 182, 181);
        public static Color BoundaryMajor = Color.FromArgb(102, 102, 102);
    }

    public static class SignColors
    {
        public static Color AddressUnitSign = Color.FromArgb(255,0,0);
        public static Color StreetNameSign = Color.FromArgb(0,255,0);
        public static Color AddressGuideSign = Color.FromArgb(0,0,255);
    }
}
