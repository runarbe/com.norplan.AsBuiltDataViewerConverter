using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AsBuiltToGIS.DataTypes;
using AsBuiltToGIS.FeatureTypes;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using OSGeo.OGR;
using BruTile.Cache;
using BruTile.Web;
using System.Data.Odbc;
using System.Diagnostics;
using DotSpatial.Topology.Utilities;
using DotSpatial.Topology;
using System.Globalization;
using OSGeo.OSR;

namespace AsBuiltToGIS.Functions
{
    public static partial class ExtFunctions
    {
        /// <summary>
        /// The name of the special namefield that can be added to exported OGR mFiles
        /// </summary>
        public static string TitleFieldName = "NameFld";

        /// <summary>
        /// Returns the mapped name of a field
        /// </summary>
        /// <param name="pFieldName">Input field name</param>
        /// <param name="pFieldMap">A map of source and target field names</param>
        /// <returns></returns>
        public static string MapFieldName(string pFieldName, Dictionary<string, string> pFieldMap = null)
        {
            return (pFieldMap != null && pFieldMap.Keys.Contains(pFieldName)) ? pFieldMap[pFieldName] : pFieldName;
        }

        /// <summary>
        /// Get the selected layer from the map or null if no layer is selected
        /// </summary>
        /// <param name="pMap">DotSpatial Map object</param>
        /// <returns>A layer object or null if none selected</returns>
        public static IMapLayer GetSelectedLayer(Map pMap)
        {
            return (pMap.Layers.SelectedLayer != null) ? pMap.Layers.SelectedLayer : null;
        }

        /// <summary>
        /// If a feature layer is seleted, return it, otherwise null
        /// </summary>
        /// <param name="pMap">DotSpatial Map object</param>
        /// <returns>Feature layer or null</returns>
        public static IFeatureLayer GetSelectedFeatureLayer(Map pMap)
        {
            var mLayer = ExtFunctions.GetSelectedLayer(pMap);
            return (mLayer is IFeatureLayer) ? (IFeatureLayer)mLayer : null;
        }

        /// <summary>
        /// If a point layer is selected, return it, otherwise null
        /// </summary>
        /// <param name="pMap">DotSpatial Map object</param>
        /// <returns>Point layer or null</returns>
        public static IPointLayer GetSelectedPointLayer(Map pMap)
        {
            var mLayer = ExtFunctions.GetSelectedLayer(pMap);
            return (mLayer is IPointLayer) ? (IPointLayer)mLayer : null;
        }

        /// <summary>
        /// If an address unit sign layer is selected return it, otherwise null.
        /// </summary>
        /// <param name="pMap">DotSpatial Map object</param>
        /// <returns>Address unit sign layer or null</returns>
        public static IPointLayer GetSelectedAddressUnitLayer(Map pMap)
        {
            var mLayer = ExtFunctions.GetSelectedPointLayer(pMap);
            return (mLayer != null && mLayer.LegendText == LayerNames.AddressUnitSigns) ? mLayer : null;
        }

        /// <summary>
        /// Mapping of C# data types to OGR data types
        /// </summary>
        /// <param name="pType">Any C# type</param>
        /// <returns>Corresponding OGR type</returns>
        public static OSGeo.OGR.FieldType DataTypeToOgrFieldType(Object pType)
        {
            if (pType is string)
            {
                return OSGeo.OGR.FieldType.OFTString;
            }
            else if (pType is int)
            {
                return OSGeo.OGR.FieldType.OFTInteger;
            }
            else if (pType is double || pType is decimal || pType is float)
            {
                return OSGeo.OGR.FieldType.OFTReal;
            }
            else if (pType is DateTime)
            {
                return OSGeo.OGR.FieldType.OFTDateTime;
            }
            else
            {
                return OSGeo.OGR.FieldType.OFTString;
            }
        }

        /// <summary>
        /// Mapping of DotSpatial topology types to WKB geometry types as used by OGR
        /// </summary>
        /// <param name="pFeatureType">A DotSpatial feature type</param>
        /// <returns>A WKB geometry type as defined by OGR</returns>
        public static wkbGeometryType DSGeomTypeToOgrGeomType(DotSpatial.Topology.FeatureType pFeatureType)
        {
            switch (pFeatureType)
            {
                case DotSpatial.Topology.FeatureType.Point:
                    return wkbGeometryType.wkbPoint;
                case DotSpatial.Topology.FeatureType.Line:
                    return wkbGeometryType.wkbLineString;
                case DotSpatial.Topology.FeatureType.Polygon:
                    return wkbGeometryType.wkbPolygon;
                default:
                    return wkbGeometryType.wkbNone;
            }
        }


        public static LabelCategory GetLabelCategory(
            string pTitle,
            string pLabelExpression,
            LabelSymbolizer pLabelSymbolizer)
        {
            return new LabelCategory()
            {
                Expression = pLabelExpression,
                Symbolizer = pLabelSymbolizer,
                Name = pTitle
            };

        }

        public static LabelSymbolizer GetLabelSymbolizer(
            string pFontFamily,
            Color pColor,
            int pFontSize)
        {
            return new LabelSymbolizer()
            {
                FontFamily = pFontFamily,
                FontColor = pColor,
                FontSize = pFontSize,
                Orientation = ContentAlignment.MiddleRight,
                PartsLabelingMethod = PartLabelingMethod.LabelLargestPart,
                OffsetX = 5
            };
        }

        /// <summary>
        /// Make a sub-set of a specific number of random elements from the selected
        /// feature layer in the map legend.
        /// </summary>
        /// <param name="pSampleSize">Number of elements to return</param>
        public static FeatureSet CreateRandomSelection(IFeatureLayer mLayer, int pSampleSize)
        {
            if (mLayer == null)
            {
                Utilities.LogDebug("The specified layer is null");
                return null;
            }

            var mRndFeatureSet = new FeatureSet();
            IFeatureSet mFeatureSet = mLayer.DataSet;

            mRndFeatureSet.DataTable = mFeatureSet.DataTable.Clone();

            int mFeatureSetSize = mFeatureSet.Features.Count;
            Utilities.ResetRndGenerator();
            for (int i = 0; i < pSampleSize; i++)
            {
                IFeature mOldFeature = mFeatureSet.Features[Utilities.GetRndBetween(0, mFeatureSetSize)];
                IFeature mNewFeature = mRndFeatureSet.AddFeature(mOldFeature.Copy());
                mNewFeature.CopyAttributes(mOldFeature);
            }
            return mRndFeatureSet;
        }

        public static void ParseSelectedDatabase(Map pMap, ProjectionInfo pProjection)
        {

            if (frmMain.dbx != null)
            {
                var mGroup = new MapGroup();
                mGroup.LegendText = frmMain.dbx.dbBasename;

                var mAddressUnitFeatures = new AddressUnitFeature(frmMain.dbx);
                mAddressUnitFeatures.PopulateFromTable();
                var mAddressUnitLayer = ExtFunctions.GetFeatureLayer(mGroup.Layers, mAddressUnitFeatures, LayerNames.AddressUnitSigns, MapSymbols.PointSymbol(SignColors.AddressUnitSign, 3), pProjection);
                mAddressUnitLayer.Reproject(pMap.Projection);
                ExtFunctions.AddLabelsForFeatureLayer(mAddressUnitLayer, LayerNames.AddressUnitSigns, "[ADDRESSUNITNR] ([ROADID])", SignColors.AddressUnitSign, "Arial", 6, true);

                var mStreetNameSignFeatures = new StreetNameSignFeature();
                mStreetNameSignFeatures.PopulateFromTable(frmMain.dbx);
                var mStreetSignLayer = ExtFunctions.GetFeatureLayer(mGroup.Layers, mStreetNameSignFeatures, LayerNames.StreetNameSigns, MapSymbols.PointSymbol(SignColors.StreetNameSign, 6), pProjection);
                mStreetSignLayer.Reproject(pMap.Projection);

                var mAddressGuideSignFeatures = new AddressGuideSignFeature();
                mAddressGuideSignFeatures.PopulateFromTable(frmMain.dbx);
                var mAddressGuideSignLayer = ExtFunctions.GetFeatureLayer(mGroup.Layers, mAddressGuideSignFeatures, LayerNames.AddressGuideSigns, MapSymbols.PointSymbol(SignColors.AddressGuideSign, 4), pProjection);
                mAddressGuideSignLayer.Reproject(pMap.Projection);
                pMap.Layers.Add(mGroup);

                mStreetSignLayer.SelectionEnabled = false;
                mAddressGuideSignLayer.SelectionEnabled = false;
                mAddressUnitLayer.SelectionEnabled = false;

                frmMain.dbx.Close();

                Extent mExtent = new Extent();
                ExtFunctions.CombineExtents(ref mExtent, mStreetSignLayer.Extent);
                ExtFunctions.CombineExtents(ref mExtent, mAddressGuideSignLayer.Extent);
                ExtFunctions.CombineExtents(ref mExtent, mAddressUnitLayer.Extent);

                if (mExtent.Width != 0 && mExtent.Height != 0)
                {
                    pMap.ViewExtents = mExtent;
                    pMap.Invalidate();
                }
            }
        }


        public static void ToggleSatelliteLayer(frmMain pFrm, bool pRemove = false)
        {
            if (pFrm.satellite == null)
            {
                pFrm.satellite = BruTileLayer.CreateBingAerialLayer();
            }

            if (pFrm.theMap.Layers.Contains(pFrm.satellite))
            {
                pFrm.theMap.Layers.Remove(pFrm.satellite);
            }
            else if (pRemove)
            {
                //Do nothing
                ;
            }
            else
            {
                pFrm.theMap.Layers.Insert(0, pFrm.satellite);
            }

        }

        /// <summary>
        /// Returns an OGR SpatialReference object for the specified EPSG Code
        /// </summary>
        /// <param name="pEPSGCode">EPSG code</param>
        /// <returns>OGR SpatialReference object</returns>
        public static SpatialReference GetSpatialReferenceByEPSG(int pEPSGCode)
        {
            SpatialReference mSR = new SpatialReference(null);
            mSR.ImportFromEPSG(pEPSGCode);
            return mSR;
        }

        /// <summary>
        /// Returns a projection info object for the specified EPSG code
        /// </summary>
        /// <param name="pEPSGCode">EPSG code</param>
        /// <returns>DotSpatial ProjectionInfo object</returns>
        public static ProjectionInfo GetProjByEPSG(int pEPSGCode)
        {
            var mProjInfo = ProjectionInfo.FromEpsgCode(pEPSGCode);
            mProjInfo.EpsgCode = pEPSGCode;
            return mProjInfo;
        }

        /// <summary>
        /// Converts a DotSpatial ProjectionInfo object to an OGR SpatialReference object
        /// </summary>
        /// <param name="pProjection">DotSpatial ProjectionInfo</param>
        /// <returns>An OGR SpatialReference</returns>
        public static SpatialReference DSProjection2OGRSpatialReference(ProjectionInfo pProjection)
        {
            Debug.WriteLine(pProjection.ToProj4String());
            SpatialReference mSpatialReference = new SpatialReference(null);
            mSpatialReference.ImportFromProj4(pProjection.ToProj4String());
            return mSpatialReference;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void AddBaseLayers(Map pMap)
        {
            var mBasemap = new MapGroup();
            mBasemap.LegendText = "Base Map";

            var mLandFilename = Application.StartupPath + "\\GisData\\land.shp";
            PolygonLayer mLandLayer = (PolygonLayer)mBasemap.Layers.Add(mLandFilename);
            mLandLayer.Symbolizer = new PolygonSymbolizer(GoogleMapsColors.Land);
            mLandLayer.LegendText = "Abu Dhabi Emirate";
            mLandLayer.Projection = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N;
            mLandLayer.Reproject(pMap.Projection);

            var mRoadsFilename = Application.StartupPath + "\\GisData\\roadssub.shp";
            LineLayer mRoadsLayer = (LineLayer)mBasemap.Layers.Add(mRoadsFilename);
            mRoadsLayer.Symbolizer = new LineSymbolizer(GoogleMapsColors.MajorRoad, 2);
            mRoadsLayer.LegendText = "Approved Roads";
            mRoadsLayer.Projection = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N;
            mRoadsLayer.Reproject(pMap.Projection);

            var ms = new ShapefileDataProvider();
            var ms2 = ms.Open(Application.StartupPath + "\\GisData\\roadssub.shp");

            pMap.Layers.Add(mBasemap);
            pMap.Legend.RefreshNodes();

            mLandLayer.SelectionEnabled = false;
            mRoadsLayer.SelectionEnabled = false;

            ExtFunctions.AddLabelsForFeatureLayer(mRoadsLayer, "Road IDs", "[ADRROADID]", Color.Blue, "Arial", 6, true);
            pMap.ViewExtents = mRoadsLayer.Extent;
            pMap.Refresh();
        }

        /// <summary>
        /// Add map labels for a feature layer
        /// </summary>
        /// <param name="pFLyr">The feature layer to add labels to</param>
        /// <param name="pCategoryTitle">The title of the label category (not visible)</param>
        /// <param name="pLabelExpression">The label expression, mFeatureIndex.e. [fieldname]</param>
        /// <param name="pFontFamily">The font family</param>
        /// <param name="pFontSize"></param>
        /// <param name="pFontColor"></param>
        /// <param name="pDynVisWidth"></param>
        /// <param name="pUseDynVis"></param>
        /// <param name="pDynVisMode"></param>
        public static void AddLabelsForFeatureLayer(
            IFeatureLayer pFeatureLayer,
            string pCategoryTitle,
            string pLabelExpression,
            Color pFontColor = default(Color),
            string pFontFamily = "Arial",
            int pFontSize = 7,
            bool pUseDynVis = false,
            int pDynVisWidth = 2000,
            DynamicVisibilityMode pDynVisMode = DynamicVisibilityMode.ZoomedIn)
        {
            Color pColor = pFontColor.IsEmpty ? Color.Black : pFontColor;

            var mSymbolizer = GetLabelSymbolizer(pFontFamily, pFontColor, pFontSize);
            var mLabelCategory = GetLabelCategory(pCategoryTitle, pLabelExpression, mSymbolizer);

            MapLabelLayer mMapLabelLayer = new MapLabelLayer(pFeatureLayer);
            pFeatureLayer.LabelLayer = mMapLabelLayer;
            pFeatureLayer.ShowLabels = true;
            pFeatureLayer.LabelLayer.Symbology.Categories.Clear();
            pFeatureLayer.LabelLayer.Symbology.Categories.Add(mLabelCategory);
            pFeatureLayer.LabelLayer.UseDynamicVisibility = pUseDynVis;
            pFeatureLayer.LabelLayer.DynamicVisibilityMode = pDynVisMode;
            pFeatureLayer.LabelLayer.DynamicVisibilityWidth = pDynVisWidth;
            pFeatureLayer.LabelLayer.CreateLabels();
            return;
        }

        public static Extent CombineExtents(ref Extent pOriginalExtent, Extent pAdditionalExtent)
        {
            pOriginalExtent.MinX = Utilities.Smallest(pOriginalExtent.MinX, pAdditionalExtent.MinX);
            pOriginalExtent.MinY = Utilities.Smallest(pOriginalExtent.MinY, pAdditionalExtent.MinY);
            pOriginalExtent.MaxX = Utilities.Greatest(pOriginalExtent.MaxX, pAdditionalExtent.MaxX);
            pOriginalExtent.MaxY = Utilities.Greatest(pOriginalExtent.MaxY, pAdditionalExtent.MaxY);
            return pOriginalExtent;
        }

        public static IFeatureLayer GetFeatureLayer(
            IMapLayerCollection pMapLayerCollection,
            FeatureSet pFeatureSet,
            string pName,
            Object pSymbolizer,
            ProjectionInfo pProjection)
        {

            IFeatureLayer mLayer;

            if (pFeatureSet.FeatureType == DotSpatial.Topology.FeatureType.Point)
            {
                mLayer = (PointLayer)pMapLayerCollection.Add(pFeatureSet);
                ((PointLayer)mLayer).Symbolizer = (PointSymbolizer)pSymbolizer;
                ((PointLayer)mLayer).LegendText = pName;
            }
            else if (pFeatureSet.FeatureType == DotSpatial.Topology.FeatureType.Line)
            {
                mLayer = (LineLayer)pMapLayerCollection.Add(pFeatureSet);
                ((LineLayer)mLayer).Symbolizer = (LineSymbolizer)pSymbolizer;
                ((LineLayer)mLayer).LegendText = pName;
            }
            else if (pFeatureSet.FeatureType == DotSpatial.Topology.FeatureType.Polygon)
            {
                mLayer = (PolygonLayer)pMapLayerCollection.Add(pFeatureSet);
                ((PolygonLayer)mLayer).Symbolizer = (PolygonSymbolizer)pSymbolizer;
                ((PolygonLayer)mLayer).LegendText = pName;
            }
            else
            {
                mLayer = null;
            }

            mLayer.Projection = pProjection;

            return mLayer;
        }

        public static Object CopyLayerSymbolizer(ILayer pLayer)
        {
            if (pLayer.GetType() == typeof(MapLineLayer))
            {
                return ((MapLineLayer)pLayer).Symbolizer;
            }
            else if (pLayer.GetType() == typeof(MapPointLayer))
            {
                return ((MapPointLayer)pLayer).Symbolizer;
            }
            else
            {
                return null;
            }

        }

        public static bool ExportSelectedDatabaseToGML(string pOutputFilename)
        {
            // Get database, return if no database selected
            if (frmMain.dbx == null)
            {
                Utilities.LogDebug("Please select a database first...");
                return false;
            }

            // Setup driver
            OSGeo.OGR.Driver drv = Ogr.GetDriverByName("GML");
            if (drv == null)
            {
                Utilities.LogDebug("Could not load driver");
                return false;
            }

            // Create a datasource
            DataSource ds = drv.CreateDataSource(pOutputFilename, null);
            if (ds == null)
            {
                Utilities.LogDebug("Could not create datasource");
                return false;
            }

            // Create a layer
            OSGeo.OGR.Layer l = ds.CreateLayer("AddressUnits", null, wkbGeometryType.wkbPoint, null);
            if (l == null)
            {
                Utilities.LogDebug("Failed to create GML file: AddressUnits");
                return false;
            }

            // Create a class to hold address unit data
            AddressUnit mAddressUnits = new AddressUnit();

            // Add fields to shapefile
            foreach (System.Reflection.FieldInfo mFld in mAddressUnits.GetType().GetFields())
            {
                var val = (DatFld)mFld.GetValue(mAddressUnits);
                var mNewField = new FieldDefn(mFld.Name, val.type);
                if (val.type == FieldType.OFTString)
                {
                    mNewField.SetWidth(val.length);
                }
                if (Ogr.OGRERR_NONE != l.CreateField(mNewField, 1))
                {
                    Utilities.LogDebug("Failed to add field: " + mFld.Name);
                }
            }

            int ctr = 0;
            double mX, mY;

            var mAdmAdrFeature = new AddressUnitFeature(frmMain.dbx);

            DataTable mTable = mAdmAdrFeature.GetTable(sqlStatements.selectAddressUnitsSQL);

            // Add features
            foreach (DataRow mRow in mTable.Rows)
            {
                if (Double.TryParse(mRow["loc_x"].ToString(), out mX) && Double.TryParse(mRow["loc_y"].ToString(), out mY))
                {

                    var mFeature = new OSGeo.OGR.Feature(l.GetLayerDefn());
                    var mPoint = new OSGeo.OGR.Geometry(wkbGeometryType.wkbPoint);

                    mPoint.SetPoint(0, mX, mY, 0);
                    mFeature.SetFID(ctr);
                    mFeature.SetGeometry(mPoint);
                    mFeature.SetField("ADDRESSUNITID", int.Parse(mRow["id"].ToString()));
                    mFeature.SetField("ROADID", int.Parse(mRow["road_id"].ToString()));
                    mFeature.SetField("ADDRESSUNITNR", int.Parse(mRow["addressUnitNumber"].ToString()));
                    mFeature.SetField("ROADNAME_EN", Utilities.GetANSI(mRow["NAMEENGLISH"].ToString()));
                    mFeature.SetField("ROADNAME_AR", Utilities.GetANSI(mRow["NAMEARABIC"].ToString()));
                    mFeature.SetField("DISTRICT_EN", Utilities.GetANSI(mRow["DISTRICT_EN"].ToString()));
                    mFeature.SetField("DISTRICT_AR", Utilities.GetANSI(mRow["DISTRICT_AR"].ToString()));
                    mFeature.SetField("MUNICIPALITY_EN", Utilities.GetANSI(Utilities.LABEL_ABUDHABI_EN));
                    mFeature.SetField("MUNICIPALITY_AR", Utilities.GetANSI(Utilities.LABEL_ABUDHABI_AR));
                    mFeature.SetField("QR_CODE", Utilities.GetANSI(mAdmAdrFeature.GetQRCode(mRow, mTable)));
                    l.CreateFeature(mFeature);

                }
                else
                {
                    Utilities.LogDebug("Error");
                }

                ctr++;
            }

            l.Dispose();
            ds.Dispose();
            drv.Dispose();
            return true;

        }

        /// <summary>
        /// Extracts an X,Y coordinate pair from a data row provided the correct values are present
        /// </summary>
        /// <param name="mRow">A data row</param>
        /// <returns>An array of two double precision coordinate values on success or null on missing data</returns>
        public static double[] GetXYFromRow(DataRow mRow)
        {
            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;
            double[] mXY = new double[2];

            if (mRow.Table.Columns.Contains("loc_x") && mRow.Table.Columns.Contains("loc_y"))
            {
                Double.TryParse(mRow["loc_x"].ToString(), NumberStyles.Float, mNumFormat, out mXY[0]);
                Double.TryParse(mRow["loc_y"].ToString(), NumberStyles.Float, mNumFormat, out mXY[1]);
            }
            else if (mRow.Table.Columns.Contains("X-coordinate") && mRow.Table.Columns.Contains("Y-coordinate"))
            {
                Double.TryParse(mRow["X-coordinate"].ToString(), NumberStyles.Float, mNumFormat, out mXY[0]);
                Double.TryParse(mRow["Y-coordinate"].ToString(), NumberStyles.Float, mNumFormat, out mXY[1]);
            }

            if (mXY[0] == 0.0D || mXY[1] == 0.0D)
            {
                return null;
            }
            else
            {
                return mXY;
            }
        }

        public static object OutputDistricts(string pMdbFile)
        {
            var mDriver = Ogr.GetDriverByName("PGeo");
            if (mDriver == null) return null;

            Debug.WriteLine("Loaded driver");
            var mSource = mDriver.Open(pMdbFile, 0);
            if (mSource == null) return null;

            Debug.WriteLine("Loaded datasource");
            var mLayer = mSource.GetLayerByName("ADRDISTRICT");

            //Debug.WriteLine("Set attribute filter");
            if (mLayer == null) return null;
            Debug.WriteLine("Loaded layer");
            var i = 0;
            OSGeo.OGR.Feature mFeature;
            Debug.WriteLine("First pass - get feature");
            var mCount = mLayer.GetFeatureCount(1);
            for (var j = 0; j < mCount; j++)
            {
                mFeature = mLayer.GetFeature(j);
                string mWkt;
                string mAbbrev = mFeature.GetFieldAsString("ABBREVIATION");
                OSGeo.OGR.Geometry mGeom = mFeature.GetGeometryRef();
                mGeom.ExportToWkt(out mWkt);
                if (String.IsNullOrEmpty(mWkt))
                {
                    Debug.WriteLine("Error with geometry");
                }
                else
                {
                    Debug.WriteLine(mAbbrev);
                }

            }
            Debug.WriteLine("Second pass - get next");

            while (null != (mFeature = mLayer.GetNextFeature()))
            {
                i++;
                string mWkt;
                string mAbbrev = mFeature.GetFieldAsString("ABBREVIATION");
                OSGeo.OGR.Geometry mGeom = mFeature.GetGeometryRef();
                mGeom.ExportToWkt(out mWkt);
                if (String.IsNullOrEmpty(mWkt))
                {
                    Debug.WriteLine("Error with geometry");
                }
                else
                {
                    Debug.WriteLine(mAbbrev);
                }
            }
            Debug.WriteLine("Processed {0} features", i);
            return true;
        }

        /// <summary>
        /// Exports the selected database to a filegeodatabase
        /// </summary>
        /// <param name="pOnlyApproved"></param>
        /// <returns></returns>
        public static FeatureSet GetDistrictsFeatureSetFromAdmAdrMdb(ref ToolStripProgressBar pPgBar, string pMdbFile, int pOnlyApproved = 0)
        {
            // Setup SQL string to select all or only some streets
            string mSql;
            if (pOnlyApproved == 1)
            {
                mSql = "SELECT ABBREVIATION, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, APPROVED FROM ADRDISTRICT WHERE APPROVED = 'Y'";
            }
            else
            {
                mSql = "SELECT ABBREVIATION, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, APPROVED FROM ADRDISTRICT";
            }

            // Setup a dictionary to hold road names
            var mDistrictNames = new Dictionary<string, DataRow>();

            // Connect to database and load names
            var mOdbcConn = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + pMdbFile + ";Uid=Admin;Pwd=;");
            var mDataAdapter = new OdbcDataAdapter(mSql, mOdbcConn);
            var mDataTable = new DataTable();
            mDataAdapter.Fill(mDataTable);
            int ctr1 = 0;
            int total1 = mDataTable.Rows.Count;

            pPgBar.Value = ctr1;
            foreach (DataRow mRow in mDataTable.Rows)
            {
                mDistrictNames.Add(mRow["ABBREVIATION"].ToString(), mRow);
                ctr1++;
                if (ctr1 % 100 == 0)
                {
                    double mFraction1 = (((double)ctr1 / (double)total1) * 100);
                    pPgBar.ProgressBar.Value = (int)(Math.Round(mFraction1));
                    Application.DoEvents();
                }
            }
            Debug.WriteLine("Done loading district names");
            Debug.WriteLine(ctr1);
            mOdbcConn.Close();
            mDataAdapter = null;
            mOdbcConn = null;

            // Connect to database using OGR to load geometries
            var mDriver = Ogr.GetDriverByName("PGeo");
            if (mDriver == null) return null;

            Debug.WriteLine("Loaded driver");
            var mSource = mDriver.Open(pMdbFile, 0);
            if (mSource == null) return null;

            Debug.WriteLine("Loaded datasource");
            var mLayer = mSource.GetLayerByName("ADRDISTRICT");

            //Debug.WriteLine("Set attribute filter");
            if (mLayer == null) return null;
            Debug.WriteLine("Loaded layer");

            // Create simplified roads
            var mSimplifiedRoadsFeatureSet = new District();

            int mTotal2 = mLayer.GetFeatureCount(1);
            Debug.WriteLine("Number of features" + mTotal2);

            pPgBar.Value = 0;
            OSGeo.OGR.Feature mFeature;

            int mFeatureIndex = 0;
            while (null != (mFeature = mLayer.GetNextFeature()))
            //for (int mFeatureIndex = 0; mFeatureIndex < mTotal2; mFeatureIndex++)
            {


                if (mFeature == null)
                {
                    Debug.WriteLine("Feature #{0} is null", mFeatureIndex);
                }
                else
                {
                    OSGeo.OGR.Geometry mGeometry = mFeature.GetGeometryRef();
                    if (mGeometry != null)
                    {
                        mGeometry.FlattenTo2D();
                        byte[] mWkb = new Byte[mGeometry.WkbSize()];
                        string mWkt = "";
                        mGeometry.ExportToWkb(mWkb);
                        mGeometry.ExportToWkt(out mWkt);
                        //Debug.WriteLine(mWkt);
                        //Debug.WriteLine(mFeatureIndex);
                        IMultiPolygon mPolygon;

                        string mDistrictAbbreviation = mFeature.GetFieldAsString("ABBREVIATION");

                        if (!String.IsNullOrEmpty(mDistrictAbbreviation))
                        {

                            var mWkbReader = new WkbReader();
                            IGeometry mGeom = mWkbReader.Read(mWkb);
                            if (mGeom.GetType() == typeof(DotSpatial.Topology.MultiPolygon))
                            {
                                var mPolygons = new List<Polygon>();
                                mPolygons.Add(mGeom as Polygon);

                                var mPolygonsN = new List<Polygon>();
                                foreach (var mPart in mPolygons)
                                {
                                    if (mPart != null)
                                    {
                                        mPolygonsN.Add(mPart);
                                    }
                                }
                                mPolygon = new DotSpatial.Topology.MultiPolygon(mPolygonsN.ToArray<Polygon>());
                            }
                            else
                            {
                                mPolygon = mGeom as IMultiPolygon;
                            }

                            if (mDistrictNames.ContainsKey(mDistrictAbbreviation))
                            {
                                var mDistrictName = mDistrictNames[mDistrictAbbreviation];

                                var mDistrictId = mDistrictName["ABBREVIATION"].ToString();
                                var mNameArabic = mDistrictName["NAMEARABIC"].ToString();
                                var mNameLatin = mDistrictName["NAMEENGLISH"].ToString();
                                var mNamePopularArabic = mDistrictName["NAMEPOPULARARABIC"].ToString();
                                var mNamePopularLatin = mDistrictName["NAMEPOPULARENGLISH"].ToString();
                                var mApproved = mDistrictName["APPROVED"].ToString();
                                mSimplifiedRoadsFeatureSet.AddNewRow(mGeom, mDistrictAbbreviation, mNameArabic, mNameLatin, mNamePopularArabic, mNamePopularLatin, mApproved);
                            }
                            else
                            {
                                Debug.WriteLine("No matching abbreviation");
                            }
                        }
                        else
                        {
                            Debug.WriteLine("No abbreviation");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No geometry");
                    }

                }

                if (mFeatureIndex % (mTotal2 / 20) == 0)
                {
                    double mFraction = (double)mFeatureIndex / (double)mTotal2;
                    int mProgress = (int)Math.Round(mFraction * 100);
                    pPgBar.ProgressBar.Value = mProgress;
                    Application.DoEvents();
                }
                mFeatureIndex++;
            }

            //mSimplifiedRoadsFeatureSet.Projection = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N;
            mSimplifiedRoadsFeatureSet.UpdateExtent();
            Debug.WriteLine("Done creating simplified roads...");
            return mSimplifiedRoadsFeatureSet;
        }


        /// <summary>
        /// Exports the selected database to a filegeodatabase
        /// </summary>
        /// <param name="pOnlyApproved"></param>
        /// <returns></returns>
        public static FeatureSet GetRoadFeatureSetFromAdmAdrMdb(ref ToolStripProgressBar pPgBar, string pMdbFile, int pOnlyApproved = 0)
        {
            // Setup SQL string to select all or only some streets
            string mSql;
            if (pOnlyApproved == 1)
            {
                mSql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD WHERE APPROVED = 1";
            }
            else
            {
                mSql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD";
            }

            // Setup a dictionary to hold road names
            var mRoadNames = new Dictionary<int, DataRow>();

            // Connect to database and load names
            var mOdbcConn = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + pMdbFile + ";Uid=Admin;Pwd=;");
            var mDataAdapter = new OdbcDataAdapter(mSql, mOdbcConn);
            var mDataTable = new DataTable();
            mDataAdapter.Fill(mDataTable);
            int ctr1 = 0;
            int total1 = mDataTable.Rows.Count;

            pPgBar.Value = ctr1;
            foreach (DataRow mRow in mDataTable.Rows)
            {
                int mRoadId;
                if (int.TryParse(mRow["ADRROADID"].ToString(), out mRoadId))
                {
                    mRoadNames.Add(mRoadId, mRow);
                }
                ctr1++;
                if (ctr1 % 100 == 0)
                {
                    double mFraction1 = (((double)ctr1 / (double)total1) * 100);
                    pPgBar.ProgressBar.Value = (int)(Math.Round(mFraction1));
                    Application.DoEvents();
                }
            }
            Debug.WriteLine("Done loading road names");
            mOdbcConn.Close();
            mDataAdapter = null;
            mOdbcConn = null;

            // Connect to database using OGR to load geometries
            var mDriver = Ogr.GetDriverByName("PGeo");
            if (mDriver == null) return null;

            Debug.WriteLine("Loaded driver");
            var mSource = mDriver.Open(pMdbFile, 0);
            if (mSource == null) return null;

            Debug.WriteLine("Loaded datasource");
            var mLayer = mSource.GetLayerByName("ADRROADSEGMENT");
            //mLayer.SetAttributeFilter("ADRROADID in (" + string.Join(",", mDistrictNames.Keys) + ")");

            //Debug.WriteLine("Set attribute filter");
            if (mLayer == null) return null;
            Debug.WriteLine("Loaded layer");

            // Create simplified roads
            var mSimplifiedRoadsFeatureSet = new SimplifiedRoads();

            int mTotal2 = mLayer.GetFeatureCount(0);
            pPgBar.Value = 0;
            for (int mCtr2 = 0; mCtr2 < mTotal2; mCtr2++)
            {
                OSGeo.OGR.Feature mFeature = mLayer.GetFeature(mCtr2);
                if (mFeature != null)
                {
                    OSGeo.OGR.Geometry mGeometry = mFeature.GetGeometryRef();
                    if (mGeometry != null)
                    {
                        mGeometry.FlattenTo2D();
                        byte[] mWkb = new Byte[mGeometry.WkbSize()];
                        mGeometry.ExportToWkb(mWkb);

                        //string mWkt = "";
                        //mGeometry.ExportToWkt(out mWkt);
                        //Debug.WriteLine(mWkt);

                        IMultiLineString mMultiLine;

                        int mRoadId = -1;
                        int.TryParse(mFeature.GetFieldAsString("ADRROADID"), out mRoadId);

                        if (mRoadId != -1)
                        {

                            var mWkbReader = new WkbReader();
                            IGeometry mGeom = mWkbReader.Read(mWkb);
                            if (mGeom.GetType() == typeof(DotSpatial.Topology.LineString))
                            {
                                var mLineStrings = new List<LineString>();
                                mLineStrings.Add(mGeom as LineString);
                                mMultiLine = new DotSpatial.Topology.MultiLineString(mLineStrings);
                            }
                            else
                            {
                                mMultiLine = mGeom as IMultiLineString;
                            }

                            //Debug.WriteLine(mPolygon.GetType().ToString());

                            if (mRoadNames.ContainsKey(mRoadId))
                            {
                                var mRoadName = mRoadNames[mRoadId];
                                int mDistrictId, mRoadClass, mApproved;

                                int.TryParse(mRoadName["ADRDISTRICTID"].ToString(), out mDistrictId);
                                var mNameArabic = mRoadName["NAMEARABIC"].ToString();
                                var mNameLatin = mRoadName["NAMEENGLISH"].ToString();
                                var mNamePopularArabic = mRoadName["NAMEPOPULARARABIC"].ToString();
                                var mNamePopularLatin = mRoadName["NAMEPOPULARENGLISH"].ToString();
                                int.TryParse(mRoadName["ROADTYPE"].ToString(), out mRoadClass);
                                int.TryParse(mRoadName["APPROVED"].ToString(), out mApproved);
                                mSimplifiedRoadsFeatureSet.AddNewRow(mGeom, mDistrictId, mRoadId, mNameArabic, mNameLatin, mNamePopularArabic, mNamePopularLatin, mRoadClass, mApproved);
                            }
                        }
                    }

                }

                if (mCtr2 % (mTotal2 / 20) == 0)
                {
                    double mFraction = (double)mCtr2 / (double)mTotal2;
                    int mProgress = (int)Math.Round(mFraction * 100);
                    pPgBar.ProgressBar.Value = mProgress;
                    Application.DoEvents();
                }

            }

            //mSimplifiedRoadsFeatureSet.Projection = KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N;
            mSimplifiedRoadsFeatureSet.UpdateExtent();
            Debug.WriteLine("Done creating simplified roads...");
            return mSimplifiedRoadsFeatureSet;
        }

        public static void AnalyzeRoadBoundingBoxes(string pMdbFile, Action<Object, bool> aLog, int pOnlyApproved = 0)
        {
            // Setup SQL string to select all or only some streets
            string mSql;
            if (pOnlyApproved == 1)
            {
                mSql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD WHERE APPROVED = 1 ORDER BY ADRROADID";
            }
            else
            {
                mSql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD ORDER BY ADRROADID";
            }

            // Setup a dictionary to hold road names
            var mRoads = new Dictionary<int, DataRow>();

            // Connect to database and load names
            var mOdbcConn = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + pMdbFile + ";Uid=Admin;Pwd=;");
            var mDataAdapter = new OdbcDataAdapter(mSql, mOdbcConn);
            var mDataTable = new DataTable();
            mDataAdapter.Fill(mDataTable);
            int ctr1 = 0;
            int total1 = mDataTable.Rows.Count;

            foreach (DataRow mRow in mDataTable.Rows)
            {
                int mRoadId;
                if (int.TryParse(mRow["ADRROADID"].ToString(), out mRoadId))
                {
                    mRoads.Add(mRoadId, mRow);
                }
                ctr1++;
                if (ctr1 % 100 == 0)
                {
                    double mFraction1 = (((double)ctr1 / (double)total1) * 100);
                    Application.DoEvents();
                }
            }
            Debug.WriteLine("Done loading road names");
            mOdbcConn.Close();
            mDataAdapter = null;
            mOdbcConn = null;

            // Connect to database using OGR to load geometries
            var mDriver = Ogr.GetDriverByName("PGeo");
            if (mDriver == null) return;

            Debug.WriteLine("Loaded driver");
            var mSource = mDriver.Open(pMdbFile, 0);
            if (mSource == null) return;

            Debug.WriteLine("Loaded datasource");
            var mLayer = mSource.GetLayerByName("ADRROADSEGMENT");
            //mLayer.SetAttributeFilter("ADRROADID in (" + string.Join(",", mDistrictNames.Keys) + ")");

            //Debug.WriteLine("Set attribute filter");
            if (mLayer == null) return;
            Debug.WriteLine("Loaded layer");

            // Create simplified roads
            var mSimplifiedRoadsFeatureSet = new SimplifiedRoads();

            int mTotal2 = mLayer.GetFeatureCount(0);
            foreach (var mRoadID in mRoads)
            {
                mLayer.SetAttributeFilter("ADRROADID = " + mRoadID.Key);

                OSGeo.OGR.Feature mFeature;
                int mNumShapes = 0;
                double mLength = 0;
                double mLeft = 0;
                double mBottom = 0;
                double mTop = 0;
                double mRight = 0;
                int mDuplicates = 0;
                List<string> mGeoms = new List<string>();
                while (null != (mFeature = mLayer.GetNextFeature()))
                {
                    OSGeo.OGR.Geometry mGeom = mFeature.GetGeometryRef();
                    
                    if (mGeom != null)
                    {
                        string wkt;
                        mGeom.ExportToWkt(out wkt);
                        OSGeo.OGR.Envelope mExtent = new OSGeo.OGR.Envelope();
                        mGeom.GetEnvelope(mExtent);
                        if (mTop == 0 || mExtent.MaxY > mTop)
                        {
                            mTop = mExtent.MaxY;
                        }
                        if (mBottom == 0 || mExtent.MinY < mBottom)
                        {
                            mBottom = mExtent.MinY;
                        }
                        if (mLeft == 0 || mExtent.MinX < mLeft)
                        {
                            mLeft = mExtent.MinX;
                        }
                        if (mRight == 0 || mExtent.MaxX > mRight)
                        {
                            mRight = mExtent.MaxX;
                        }

                        if (!mGeoms.Contains(wkt))
                        {
                            mLength = mLength + mGeom.Length();
                            mGeoms.Add(wkt);
                        }
                        else
                        {
                            mDuplicates++;
                        }

                    }

                    mNumShapes++;
                }

                var mA = mRight - mLeft;
                var mB = mTop - mBottom;
                var mC = Math.Sqrt(mA * mA + mB * mB);
                if (mLength > (2.2 * mC) || mDuplicates > 0 )
                {
                    aLog(String.Format("r: {0}, s: {1}, l: {2}, d: {3}, bb: {4}",
                        mRoadID.Key,
                        mNumShapes,
                        Math.Round(mLength, 1),
                        mDuplicates,
                        Math.Round(mC, 1)),
                        true);
                }
            }
        }

    }
}
