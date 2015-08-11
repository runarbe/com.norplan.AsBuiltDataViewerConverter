using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Gdal = OSGeo.GDAL.Gdal;
using Ogr = OSGeo.OGR.Ogr;

namespace AsBuiltToGIS
{
    public static partial class GdalConfiguration
    {
        private static bool _configuredOgr;
        private static bool _configuredGdal;

        /// <summary>
        /// Function to determine which platform we're on
        /// </summary>
        private static string GetPlatform()
        {
            return "gdal";
        }


        /// <summary>
        /// Construction of Gdal/Ogr
        /// </summary>
        static GdalConfiguration()
        {
            var executingAssemblyFile = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            var executingDirectory = Path.GetDirectoryName(executingAssemblyFile);

            if (string.IsNullOrEmpty(executingDirectory))
                throw new InvalidOperationException("cannot get executing directory");


            var gdalPath = Path.Combine(executingDirectory, "gdal");
            var nativePath = Path.Combine(gdalPath, GetPlatform());

            Debug.WriteLine(nativePath);

            // Prepend native path to environment path, to ensure the
            // right libs are being used.
            var path = Environment.GetEnvironmentVariable("PATH");
            path = gdalPath + ";" + nativePath + ";" + Path.Combine(nativePath, "plugins") + ";" + Path.Combine(nativePath, "plugins-external") + ";" + path;
            Environment.SetEnvironmentVariable("PATH", path);

            Debug.WriteLine("PATH: " + path);

            // Set the additional GDAL environment variables.
            var gdalData = Path.Combine(gdalPath, "gdal-data");
            Environment.SetEnvironmentVariable("GDAL_DATA", gdalData);
            Gdal.SetConfigOption("GDAL_DATA", gdalData);

            Debug.WriteLine("GDAL_DATA: " + gdalData);

            var driverPath = Path.Combine(nativePath, "plugins");
            var extDriverPath = Path.Combine(nativePath, "plugins-external");
            Environment.SetEnvironmentVariable("GDAL_DRIVER_PATH", driverPath + ";" + extDriverPath);
            Gdal.SetConfigOption("GDAL_DRIVER_PATH", driverPath+";"+extDriverPath);

            Environment.SetEnvironmentVariable("GEOTIFF_CSV", gdalData);
            Gdal.SetConfigOption("GEOTIFF_CSV", gdalData);

            var projPath = Path.Combine(gdalPath, "proj");
            var projSharePath = Path.Combine(projPath, "share");
            Environment.SetEnvironmentVariable("PROJ_LIB", projSharePath);
            Gdal.SetConfigOption("PROJ_LIB", projSharePath);
        }

        /// <summary>
        /// Method to ensure the static constructor is being called.
        /// </summary>
        /// <remarks>Be sure to call this function before using Gdal/Ogr/Osr</remarks>
        public static void ConfigureOgr()
        {
            if (_configuredOgr) return;

            // Register drivers
                Ogr.RegisterAll();
            _configuredOgr = true;

            PrintDriversOgr();
        }

        /// <summary>
        /// Method to ensure the static constructor is being called.
        /// </summary>
        /// <remarks>Be sure to call this function before using Gdal/Ogr/Osr</remarks>
        public static void ConfigureGdal()
        {
            if (_configuredGdal) return;

            // Register drivers
            Gdal.AllRegister();
            _configuredGdal = true;

            PrintDriversGdal();
        }

        private static void PrintDriversOgr()
        {
#if DEBUG
            var num = Ogr.GetDriverCount();
            for (var i = 0; i < num; i++)
            {
                var driver = Ogr.GetDriver(i);
                Console.WriteLine(string.Format("OGR {0}: {1}", i, driver.name));
            }
#endif
        }

        private static void PrintDriversGdal()
        {
#if DEBUG
            var num = Gdal.GetDriverCount();
            for (var i = 0; i < num; i++)
            {
                var driver = Gdal.GetDriver(i);
                Console.WriteLine(string.Format("GDAL {0}: {1}-{2}", i, driver.ShortName, driver.LongName));
            }
#endif
        }
    }
}