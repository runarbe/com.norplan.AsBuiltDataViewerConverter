using Norplan.Adm.AsBuiltDataConversion.Functions;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.TypeExtensions;

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

        private string OgrDataSourceCopyBaseName;

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

                OgrDataSourceCopyBaseName = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
                var tmpShapefileName = Path.GetTempPath() + OgrDataSourceCopyBaseName + ".shp";
                Log(tmpShapefileName);
                OgrDataSourceCopy = Ogr.GetDriverByName("ESRI Shapefile").CreateDataSource(tmpShapefileName, null);
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
                var roadIdsToBeDeleted = new List<int>();

                var roadTable = AccessDatabase.Query("SELECT ADRROADID FROM ADM_ADRROAD ORDER BY ADRROADID ASC");

                var roadCounter = 0;

                var numberOfRoads = roadTable.Rows.Count;

                foreach (DataRow roadRow in roadTable.Rows)
                {
                    int currentRoadId = roadRow["ADRROADID"].AsInteger();

                    OgrLayer.ResetReading();
                    OgrLayer.SetAttributeFilter("ADRROADID=" + currentRoadId);

                    using (Feature roadSegment = OgrLayer.GetNextFeature())
                    {
                        if (roadSegment == null)
                        {
                            roadIdsToBeDeleted.Add(currentRoadId);
                        }
                    }

                    roadCounter++;

                    if (roadCounter % 100 == 0 || roadCounter == numberOfRoads)
                    {
                        Log("Processed " +  roadCounter + " roads", true);
                    }

                }

                int numberOfRoadsToBeDeleted = roadIdsToBeDeleted.Count();
                int deletedRoads = 0;
                foreach (int roadIdToDelete in roadIdsToBeDeleted)
                {
                    deletedRoads++;
                    var sql = "DELETE * FROM ADM_ADRROAD WHERE ADRROADID = " + roadIdToDelete;
                    
                    int affectedRoads = AccessDatabase.Execute(sql);
                    
                    if (affectedRoads > -1)
                    {
                        Log(roadIdToDelete.ToString(), true);
                    }
                    else
                    {
                        //Log(sql, true);
                    }

                    if (deletedRoads % 100 == 0 || deletedRoads == numberOfRoadsToBeDeleted)
                    {
                        Log("Deleted " + deletedRoads + " roads...", true);
                    }

                }

                LogFunction("Deleted " + roadIdsToBeDeleted.Count() + " road entries without corresponding geometries", true);

            }
        }

        public void BlankNamesWhereNotApproved()
        {
            if (IsValid)
            {
                int affectedRows = AccessDatabase.Execute("UPDATE ADM_ADRROAD SET NAMEENGLISH = '', NAMEARABIC='', NAMEPOPULARENGLISH='', NAMEPOPULARARABIC='' WHERE APPROVED IS NULL OR APPROVED = 0");
                LogFunction("Blanked names of roads that are not yet approved", true);
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

                foreach (var fileToDelete in Directory.GetFiles(Path.GetTempPath(), OgrDataSourceCopyBaseName + ".*"))
                {
                    LogFunction("Deleting file: " + fileToDelete + "...", true);
                    try
                    {
                        File.Delete(fileToDelete);
                    }
                    catch (Exception)
                    {
                        LogFunction("Error: Unable to delete file", true);
                    }
                }

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
