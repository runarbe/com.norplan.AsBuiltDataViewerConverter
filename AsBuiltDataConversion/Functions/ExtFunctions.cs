﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using OSGeo.OGR;
using System.Data.Odbc;
using System.Diagnostics;
using DotSpatial.Topology.Utilities;
using DotSpatial.Topology;
using System.Globalization;
using OSGeo.OSR;
using System.IO;
using CsvHelper.Excel;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static partial class ExtFunctions
    {
        /// <summary>
        /// The name of the special namefield that can be added to exported OGR mFiles
        /// </summary>
        public static string TitleFieldName = "NameFld";

        /// <summary>
        /// Returns the mapped name of fieldAttributes field
        /// </summary>
        /// <param name="pFieldName">Input field name</param>
        /// <param name="pFieldMap">A map of source and target field names</param>
        /// <returns></returns>
        public static string MapFieldName(string pFieldName, Dictionary<string, string> pFieldMap = null)
        {
            return (pFieldMap != null && pFieldMap.Keys.Contains(pFieldName)) ? pFieldMap[pFieldName] : pFieldName;
        }

        public static List<String> GetDistrictByPoint(this IMapLayer pLyr, double x, double y)
        {
            var mNameEN = "";
            var mNameAR = "";

            if (pLyr.LegendText == "Districts")
            {
                var mPoint = new DotSpatial.Topology.Point(x, y);

                var tLyr = (IFeatureLayer)pLyr;

                foreach (DotSpatial.Data.Feature mF in tLyr.DataSet.Features)
                {
                    if (mF.Contains(mPoint))
                    {
                        mNameEN = mF.DataRow["NAMELATIN"].ToString();
                        mNameAR = mF.DataRow["NAMEARABIC"].ToString();
                        break;
                    }
                }
            }
            return new List<String>() { mNameEN, mNameAR };
        }

        /// <summary>
        /// Get layer currently loaded in map by its name (as shown in the legend)
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pLayerName"></param>
        /// <returns></returns>
        public static IMapLayer GetLayerByName(Map pMap, String pLayerName)
        {
            foreach (IMapLayer mLyr in pMap.Layers)
            {
                if (mLyr.LegendText == pLayerName)
                {
                    return mLyr;
                }
            }
            return null;
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
        /// If fieldAttributes feature layer is seleted, return it, otherwise null
        /// </summary>
        /// <param name="pMap">DotSpatial Map object</param>
        /// <returns>Feature layer or null</returns>
        public static IFeatureLayer GetSelectedFeatureLayer(Map pMap)
        {
            var mLayer = ExtFunctions.GetSelectedLayer(pMap);
            return (mLayer is IFeatureLayer) ? (IFeatureLayer)mLayer : null;
        }

        /// <summary>
        /// If fieldAttributes point layer is selected, return it, otherwise null
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
        public static OSGeo.OGR.FieldType DataTypeToOgrFieldType(Type pType)
        {
            if (pType == typeof(string))
            {
                return OSGeo.OGR.FieldType.OFTString;
            }
            else if (pType == typeof(int))
            {
                return OSGeo.OGR.FieldType.OFTInteger;
            }
            else if (pType == typeof(double) || pType == typeof(decimal) || pType == typeof(float))
            {
                return OSGeo.OGR.FieldType.OFTReal;
            }
            else if (pType == typeof(DateTime))
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
                OffsetX = 5,
                FontStyle = System.Drawing.FontStyle.Bold,
                HaloColor = Color.Black,
                HaloEnabled = true,
            };
        }

        /// <summary>
        /// Make fieldAttributes sub-set of fieldAttributes specific number of random elements from the selected
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
                mGroup.LegendText = frmMain.dbx.DbBaseName;

                Debug.WriteLine("Got here");

                var mAddressUnitFeatures = new AddressUnitFeature(frmMain.dbx);
                mAddressUnitFeatures.PopulateFromTable();
                var mAddressUnitLayer = ExtFunctions.GetFeatureLayer(mGroup.Layers, mAddressUnitFeatures, LayerNames.AddressUnitSigns, MapSymbols.PointSymbol(SignColors.AddressUnitSign, 3), pProjection);
                mAddressUnitLayer.Reproject(pMap.Projection);

                ExtFunctions.AddLabelsForFeatureLayer(mAddressUnitLayer, LayerNames.AddressUnitSigns, "[ADDRESSUNITNR] ([ROADID])", Color.White, "Arial", 10, true);

                var mStreetNameSignFeatures = new StreetNameSignFeature(frmMain.dbx);
                mStreetNameSignFeatures.PopulateFromTable();
                var mStreetSignLayer = ExtFunctions.GetFeatureLayer(mGroup.Layers, mStreetNameSignFeatures, LayerNames.StreetNameSigns, MapSymbols.PointSymbol(SignColors.StreetNameSign, 6), pProjection);
                mStreetSignLayer.Reproject(pMap.Projection);

                var mAddressGuideSignFeatures = new AddressGuideSignFeature(frmMain.dbx);
                mAddressGuideSignFeatures.PopulateFromTable();
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
                pFrm.satellite = BruTileLayer.CreateOsmLayer();
                //pFrm.satellite = BruTileLayer.CreateBingAerialLayer();
                pFrm.satellite.LegendText = "OSM Tile Layer";
            }

            ILayer mLandLayer = null;

            foreach (var mGroup in pFrm.theMap.GetAllLayers())
            {
                if (mGroup.LegendText == "Abu Dhabi Emirate")
                {
                    mLandLayer = mGroup;
                    break;
                }
            }

            if (pFrm.theMap.Layers.Contains(pFrm.satellite) || pRemove)
            {
                pFrm.theMap.Layers.Remove(pFrm.satellite);
                if (mLandLayer != null)
                {
                    mLandLayer.IsVisible = true;
                }
            }
            else
            {
                pFrm.theMap.Layers.Insert(0, pFrm.satellite);
                pFrm.satellite.LegendText = "Bing (satellite)";
                if (mLandLayer != null)
                {
                    mLandLayer.IsVisible = false;
                }
            }

        }

        /// <summary>
        /// Returns an OGR SpatialReference object for the specified EPSG Code
        /// </summary>
        /// <param name="pEPSGCode">EPSG code</param>
        /// <returns>OGR SpatialReference object - if EPSG code is invalidm\</returns>
        public static SpatialReference GetSpatialReferenceByEPSG(int pEPSGCode)
        {
            if (pEPSGCode == 0)
            {
                pEPSGCode = 4326;
            }
            SpatialReference mSR = new SpatialReference(null);
            mSR.ImportFromEPSG(pEPSGCode);
            return mSR;
        }

        /// <summary>
        /// Returns fieldAttributes projection info object for the specified EPSG code
        /// </summary>
        /// <param name="pEPSGCode">EPSG code</param>
        /// <returns>DotSpatial ProjectionInfo object</returns>
        public static ProjectionInfo GetProjByEPSG(int pEPSGCode)
        {
            if (pEPSGCode == 0)
            {
                pEPSGCode = 4326;
            }
            var mProjInfo = ProjectionInfo.FromEpsgCode(pEPSGCode);
            mProjInfo.Authority = "EPSG";
            mProjInfo.AuthorityCode = pEPSGCode;
            return mProjInfo;
        }

        /// <summary>
        /// Converts fieldAttributes DotSpatial ProjectionInfo object to an OGR SpatialReference object
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

            ExtFunctions.AddLabelsForFeatureLayer(mRoadsLayer, "Road IDs", "[ADRROADID]", Color.LightBlue, "Arial", 10, true);
            pMap.ViewExtents = mRoadsLayer.Extent;
            pMap.Refresh();
        }

        /// <summary>
        /// Add map labels for fieldAttributes feature layer
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
            int pFontSize = 12,
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

        public static bool ExportSelectedDatabaseToGML(Action<string, bool> aLog, string pOutputFilename)
        {
            // Get database, return if no database selected
            if (frmMain.dbx == null)
            {
                aLog("Please select a database first...", true);
                return false;
            }

            // Setup driver
            OSGeo.OGR.Driver drv = Ogr.GetDriverByName("GML");
            if (drv == null)
            {
                aLog("Could not load driver", true);
                return false;
            }

            // Create fieldAttributes datasource
            DataSource ds = drv.CreateDataSource(pOutputFilename, null);
            if (ds == null)
            {
                aLog("Could not create datasource", true);
                return false;
            }

            // Create fieldAttributes layer
            OSGeo.OGR.Layer l = ds.CreateLayer("AddressUnits", null, wkbGeometryType.wkbPoint, null);
            if (l == null)
            {
                aLog("Failed to create GML file: AddressUnits", true);
                return false;
            }

            // Create fieldAttributes class to hold address unit data
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
                    aLog("Failed to add field: " + mFld.Name, true);
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
        /// Extracts an X,Y coordinate pair from fieldAttributes data row provided the correct values are present
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
        /// Exports the selected database to fieldAttributes filegeodatabase
        /// </summary>
        /// <param name="pOnlyApproved"></param>
        /// <returns></returns>
        public static FeatureSet GetDistrictsFeatureSetFromAdmAdrMdb(ref ToolStripProgressBar pPgBar, string pMdbFile, int pOnlyApproved = 0)
        {
            // Setup SQL string to select all or only some streets
            string mSql;
            if (pOnlyApproved == 1)
            {
                mSql = "SELECT OBJECTID, ABBREVIATION, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, APPROVED FROM ADRDISTRICT WHERE APPROVED = 'Y'";
            }
            else
            {
                mSql = "SELECT OBJECTID, ABBREVIATION, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, APPROVED FROM ADRDISTRICT";
            }

            // Setup fieldAttributes dictionary to hold road names
            var mDistrictNames = new Dictionary<int, DataRow>();

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
                if (mDistrictNames.Keys.Contains(mRow["OBJECTID"].ToInt()))
                {
                    Utilities.LogDebug("Key " + mRow["OBJECTID"].ToInt() + " already exists in districts");
                }
                mDistrictNames.Add(mRow["OBJECTID"].ToInt(), mRow);
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
            var mDistrict = new District();

            int mTotal2 = mLayer.GetFeatureCount(1);
            Debug.WriteLine("Number of features" + mTotal2);

            pPgBar.Value = 0;
            OSGeo.OGR.Feature mFeature;

            int mFeatureIndex = 0;
            while (null != (mFeature = mLayer.GetNextFeature()))
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

                        IMultiPolygon mPolygon;

                        int mDistrictObjectId = mFeature.GetFieldAsInteger("OBJECTID");

                        if (mDistrictObjectId > 0)
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

                            if (mDistrictNames.ContainsKey(mDistrictObjectId))
                            {
                                var mDistrictName = mDistrictNames[mDistrictObjectId];


                                var mDistrictAbbreviation = mDistrictName["ABBREVIATION"].ToString();
                                var mNameArabic = mDistrictName["NAMEARABIC"].ToString();
                                var mNameLatin = mDistrictName["NAMEENGLISH"].ToString();
                                var mNamePopularArabic = mDistrictName["NAMEPOPULARARABIC"].ToString();
                                var mNamePopularLatin = mDistrictName["NAMEPOPULARENGLISH"].ToString();
                                var mApproved = mDistrictName["APPROVED"].ToString();
                                mDistrict.AddNewRow(mGeom, mDistrictObjectId, mDistrictAbbreviation, mNameArabic, mNameLatin, mNamePopularArabic, mNamePopularLatin, mApproved);
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

            mDistrict.UpdateExtent();
            Debug.WriteLine("Done creating districts...");
            return mDistrict;
        }

        /// <summary>
        /// Exports the selected database to fieldAttributes filegeodatabase
        /// </summary>
        /// <param name="progressBar">A progress bar object</param>
        /// <param name="logFunction">A log function</param>
        /// <param name="mdbFile">The outputShapefileName of an addressing database</param>
        /// <param name="approvedOnly">1 to include only approved, 0 to include all</param>
        /// <returns>Feature set</returns>
        public static FeatureSet GetRoadFeatureSetFromAdmAdrMdb(ref ToolStripProgressBar progressBar, Action<string, bool> logFunction, string mdbFile, int approvedOnly = 0)
        {
            // Setup SQL string to select all or only some streets
            string sqlStatement;
            if (approvedOnly == 1)
            {
                sqlStatement = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD WHERE APPROVED = 1";
            }
            else
            {
                sqlStatement = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD";
            }

            logFunction(sqlStatement, true);

            // Setup fieldAttributes dictionary to hold road names
            var roadNames = new Dictionary<int, DataRow>();

            // Connect to database and load names
            using (var database = new Database(mdbFile))
            {
                var dataTable = database.Query(sqlStatement);
                progressBar.Value = 0;
                foreach (DataRow mRow in dataTable.Rows)
                {
                    int mRoadId;
                    if (int.TryParse(mRow["ADRROADID"].ToString(), out mRoadId))
                    {
                        roadNames.Add(mRoadId, mRow);
                    }
                }

                // Connect to database using OGR to load geometries
                var ogrDriver = Ogr.GetDriverByName("PGeo");

                if (ogrDriver == null) return null;
                logFunction("Loaded driver", true);

                var ogrDataSource = ogrDriver.Open(mdbFile, 0);
                if (ogrDataSource == null) return null;
                logFunction("Loaded datasource", true);

                var roadSegmentLayer = ogrDataSource.GetLayerByName("ADRROADSEGMENT");
                if (roadSegmentLayer == null)
                {
                    logFunction("Could not load layer", true);
                    return null;
                }
                logFunction("Loaded layer", true);

                // Create simplified roads
                var simplifiedRoadsFeatureSet = new SimplifiedRoads();

                int numberOfRoads = roadNames.Count();
                progressBar.Value = 0;
                var roadCounter = 0;
                var segmentCounter = 0;

                foreach (var roadName in roadNames)
                {
                    roadCounter++;

                    int roadIdentifier = roadName.Key;
                    DataRow roadData = roadName.Value;

                    roadSegmentLayer.ResetReading();
                    roadSegmentLayer.SetAttributeFilter("ADRROADID=" + roadIdentifier);

                    OSGeo.OGR.Feature roadSegment;
                    var mWkbReader = new WkbReader();

                    while (null != (roadSegment = roadSegmentLayer.GetNextFeature()))
                    {
                        segmentCounter++;

                        OSGeo.OGR.Geometry mGeometry = roadSegment.GetGeometryRef();

                        if (mGeometry != null)
                        {
                            mGeometry.FlattenTo2D();

                            byte[] mWkb = new Byte[mGeometry.WkbSize()];

                            mGeometry.ExportToWkb(mWkb);

                            IMultiLineString multiLine;

                            IGeometry roadGeometry = mWkbReader.Read(mWkb);

                            if (roadGeometry.GetType() == typeof(DotSpatial.Topology.LineString))
                            {
                                var mLineStrings = new List<LineString>();
                                mLineStrings.Add(roadGeometry as LineString);
                                multiLine = new DotSpatial.Topology.MultiLineString(mLineStrings);
                            }
                            else
                            {
                                multiLine = roadGeometry as IMultiLineString;
                            }

                            int districtIdentifier, roadClass, roadApproved;
                            int.TryParse(roadData["ADRDISTRICTID"].ToString(), out districtIdentifier);
                            var nameArabic = roadData["NAMEARABIC"].ToString();
                            var nameLatin = roadData["NAMEENGLISH"].ToString();
                            var namePopularArabic = roadData["NAMEPOPULARARABIC"].ToString();
                            var namePopularLatin = roadData["NAMEPOPULARENGLISH"].ToString();
                            int.TryParse(roadData["ROADTYPE"].ToString(), out roadClass);
                            int.TryParse(roadData["APPROVED"].ToString(), out roadApproved);

                            simplifiedRoadsFeatureSet.AddNewRow(
                                roadGeometry,
                                districtIdentifier,
                                roadIdentifier,
                                nameArabic,
                                nameLatin,
                                namePopularArabic,
                                namePopularLatin,
                                roadClass,
                                roadApproved);
                        }
                        else
                        {
                            logFunction("No geometry", true);
                        }

                        if (roadCounter % 100 == 0 || roadCounter == numberOfRoads)
                        {
                            progressBar.ProgressBar.Value = (int)Math.Round((double)(roadCounter * 100 / numberOfRoads));
                            Application.DoEvents();
                        }

                    }

                }

                simplifiedRoadsFeatureSet.UpdateExtent();
                logFunction("Done creating featureset", true);
                return simplifiedRoadsFeatureSet;

            }

        }

        public static void AnalyzeRoadBoundingBoxes(string pMdbFile, string pLogFile, Action<Object, bool> aLog, int pOnlyApproved = 0)
        {
            var roadSuspects = new List<RoadSuspect>();
            // Setup SQL string to select all or only some streets
            string sql;
            if (pOnlyApproved == 1)
            {
                sql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD WHERE APPROVED = 1 ORDER BY ADRROADID";
            }
            else
            {
                sql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD ORDER BY ADRROADID";
            }

            // Setup fieldAttributes dictionary to hold road names
            var mRoads = new Dictionary<int, DataRow>();

            // Connect to database and load names
            var mOdbcConn = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + pMdbFile + ";Uid=Admin;Pwd=;");
            var mDataAdapter = new OdbcDataAdapter(sql, mOdbcConn);
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

                var mBoxWidth = mRight - mLeft;
                var mBoxHeight = mTop - mBottom;
                var mBoxDiagonal = Math.Sqrt(mBoxWidth * mBoxWidth + mBoxHeight * mBoxHeight);

                roadSuspects.Add(new RoadSuspect(mRoadID.Key,
                    mNumShapes,
                    mDuplicates,
                        Math.Round(mBoxDiagonal, 1),
                        Math.Round(mLength, 1)));


                if (mLength > (2.2 * mBoxDiagonal) || mDuplicates > 0)
                {
                    aLog(String.Format("r: {0}, s: {1}, l: {2}, d: {3}, bb: {4}",
                        mRoadID.Key,
                        mNumShapes,
                        Math.Round(mLength, 1),
                        mDuplicates,
                        Math.Round(mBoxDiagonal, 1)),
                        true);
                }

            }

            using (var mCsvWriter = new CsvHelper.CsvWriter(new ExcelSerializer(pLogFile)))
            {
                mCsvWriter.WriteRecords(roadSuspects);
                aLog("Wrote output to " + pLogFile, true);
                aLog("Operation completed...", true);
            }
        }

    }
}
