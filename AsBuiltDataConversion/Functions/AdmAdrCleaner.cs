using Norplan.Adm.AsBuiltDataConversion.Functions;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion
{
    class AdmAdrCleaner : IDisposable
    {
        private Action<string, bool> LogFunction;

        private Layer OgrLayer;

        private Database AccessDatabase;

        private bool IsValid;

        private Driver OgrDriver;

        private DataSource OgrDataSource;

        private DataSource OgrDataSourceCopy;

        private Layer OgrLayerCopy;

        public AdmAdrCleaner(string fileName, Action<string, bool> logFunction = null)
        {
            IsValid = false;

            LogFunction = logFunction;

            try
            {
                // Open datasource
                OgrDriver = Ogr.GetDriverByName("PGeo");

                OgrDataSource = OgrDriver.Open(fileName, 1);
                if (OgrDataSource == null)
                {
                    throw new Exception("Could not open datasource");
                }

                OgrLayer = OgrDataSource.GetLayerByName("ADRROADSEGMENT");
                if (OgrLayer == null)
                {
                    throw new Exception("No layer by the name of 'ADRROADSEGMENT' present in selected database");
                }

                var temporaryShapefileName = Path.GetTempPath() + "/" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".shp";
                Log(temporaryShapefileName);
                OgrDataSourceCopy = Ogr.GetDriverByName("ESRI Shapefile").CreateDataSource(temporaryShapefileName, null);
                OgrLayerCopy = OgrDataSourceCopy.CopyLayer(OgrLayer, "Road", null);
                if (OgrLayerCopy == null)
                {
                    throw new Exception("Could not create copy");
                }

                // Open database
                AccessDatabase = new Database(fileName);
                Log("Opened database successfully");
                IsValid = true;
            }
            catch (Exception ex)
            {
                Log("Unable to open database");
                Log(ex.Message);
                IsValid = false;
            }
        }

        public void Log(string logMessage, bool doEvents = true)
        {
            if (LogFunction != null)
            {
                LogFunction(logMessage, doEvents);
            }
            else
            {
                Debug.WriteLine(logMessage);
            }
        }


        public void RemoveDuplicates()
        {
            if (IsValid)
            {

            }
        }

        public void RemoveOverlappingGeometries()
        {
            if (IsValid)
            {

            }
        }

        public void RemoveNullGeometries()
        {
            if (IsValid)
            {

            }
        }

        public void BlankNamesWhereNotApproved()
        {
            if (IsValid)
            {

            }
        }


        public void Dispose()
        {
            if (OgrLayer != null)
            {
                OgrLayer.Dispose();
            }

            if (OgrLayerCopy != null)
            {
                OgrLayerCopy.Dispose();
            }

            if (OgrDataSource != null)
            {
                OgrDataSource.Dispose();
            }

            if (OgrDataSourceCopy != null)
            {
                OgrDataSourceCopy.Dispose();
            }

            if (OgrDriver != null)
            {
                OgrDriver.Dispose();
            }

            if (AccessDatabase != null)
            {
                AccessDatabase.Close();
            }

        }
    }
}
