using System;
using System.Collections.Generic;   
using System.Data;
using System.Linq;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using DotSpatial.Data;
using DotSpatial.Symbology;
using OSGeo.OGR;
using OSGeo.OSR;
using DotSpatial.Projections;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using System.Windows.Forms;
using DotSpatial.Controls;
using System.IO;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static partial class ExtFunctions
    {

        public static void ExportMultipleToOGR(frmMain pFrm, string[] pInputFilenames, string pOutputFilename)
        {
            pFrm.Log("Starting to process selected files...");

            foreach (var mDbFilename in pInputFilenames)
            {
                var mDb = new Database(mDbFilename);
                var mAddressUnitFeatures = new AddressUnitFeature(mDb);
                var mStreetNameFeatures = new StreetNameSignFeature(mDb);
                var mAddressGuideFeatures = new AddressGuideSignFeature(mDb);

                mAddressUnitFeatures.PopulateFromTable();
                mStreetNameFeatures.PopulateFromTable();
                mAddressGuideFeatures.PopulateFromTable();

                var mGroup = new MapGroup();
                mGroup.LegendText = mDb.DbBaseName;
                IFeatureLayer mAUNSLayer = (IFeatureLayer)ExtFunctions.GetFeatureLayer(
                    mGroup.Layers, mAddressUnitFeatures,
                    LayerNames.AddressUnitSigns,
                    MapSymbols.PointSymbol(SignColors.AddressUnitSign, 3),
                    ExtFunctions.GetProjByEPSG(32640)
                    );
                ExtFunctions.ExportFeatureLayerToOGR(
                    pDrvNm: "FileGDB",
                    pFLyr: mAUNSLayer,
                    pOPFn: pOutputFilename,
                    pSrcProj: ExtFunctions.GetProjByEPSG(32640),
                    pTgtProj: ExtFunctions.GetProjByEPSG(32640),
                    pLCOpts: new List<string>() { "FEATURE_DATASET=Simplified" },
                    pAppend: true
                    );
                IFeatureLayer mSNSLayer = (IFeatureLayer)ExtFunctions.GetFeatureLayer(
                    mGroup.Layers,
                    mStreetNameFeatures,
                    LayerNames.StreetNameSigns,
                    MapSymbols.PointSymbol(SignColors.StreetNameSign, 3),
                    ExtFunctions.GetProjByEPSG(32640)
                    );
                ExtFunctions.ExportFeatureLayerToOGR(
                    pDrvNm: "FileGDB",
                    pFLyr: mSNSLayer,
                    pOPFn: pOutputFilename,
                    pSrcProj: ExtFunctions.GetProjByEPSG(32640),
                    pTgtProj: ExtFunctions.GetProjByEPSG(32640),
                    pLCOpts: new List<string>() { "FEATURE_DATASET=Simplified" },
                    pAppend: true
                    );
                IFeatureLayer mAGSLayer = (IFeatureLayer)ExtFunctions.GetFeatureLayer(
                    mGroup.Layers,
                    mAddressGuideFeatures,
                    LayerNames.AddressGuideSigns,
                    MapSymbols.PointSymbol(SignColors.AddressGuideSign, 3),
                    ExtFunctions.GetProjByEPSG(32640)
                    );
                ExtFunctions.ExportFeatureLayerToOGR(
                    pDrvNm: "FileGDB",
                    pFLyr: mAGSLayer,
                    pOPFn: pOutputFilename,
                    pSrcProj: ExtFunctions.GetProjByEPSG(32640),
                    pTgtProj: ExtFunctions.GetProjByEPSG(32640),
                    pLCOpts: new List<string>() { "FEATURE_DATASET=Simplified" },
                    pAppend: true
                    );
                pFrm.Log("Completed parsing: " + mDbFilename);
                Application.DoEvents();
            }

            pFrm.Log("Wrote output to file: " + pOutputFilename);

        }

        /// <summary>
        /// Outputs the specified DotSpatial IFeatureLayer to an OGR layer
        /// </summary>
        /// <param name="pDrvNm">A name like KML, ESRI SHapefile or FileGDB etc.</param>
        /// <param name="pFLyr">A layer that implements the IFeatureLayer interface</param>
        /// <param name="pOPFn">The filename of the output file</param>
        /// <param name="pSrcProj">The SRS of the source dataset</param>
        /// <param name="pTgtProj">The SRS of the output dataset</param>
        /// <param name="pHasTitle">A boolean flag that determines whether to create a special title field</param>
        /// <param name="pTitleFieldNames">A comma separated list of field names to be used in the special title field</param>
        /// <param name="pTitleFormat">The C# String.Format format of the special title field</param>
        /// <param name="pLCOpts">A string array of layer creation options in the form of OPTION=VALUE entries</param>
        /// <param name="pDSCOpts">A string of data source creation options in the form of OPTION=VALUE entries</param>
        /// <param name="pFieldMap">A dictionary of source and target field names to be translated on the source
        /// the title format field names and the special title field name all observe this field mapping.</param>
        /// <param name="pOnlyInFieldMap">If true, only writes fields that are included in the field-map</param>
        /// <returns>True on success, false on error</returns>
        /// <remarks>
        /// Presently implements special functions for KML that perhaps should be kept separate.
        /// </remarks>
        public static ReturnValue ExportFeatureLayerToOGR(string pDrvNm, IFeatureLayer pFLyr, string pOPFn, ProjectionInfo pSrcProj, ProjectionInfo pTgtProj, bool pHasTitle = false, string pTitleFieldNames = "", string pTitleFormat = "", List<string> pLCOpts = null, List<string> pDSCOpts = null, Dictionary<string, string> pFieldMap = null, bool pOnlyInFieldMap = false, bool pAppend = false)
        {

            var mReturnValue = new ReturnValue(true);

            //Check if data source and layer creation options are null and if so, create an empty list object for them
            pDSCOpts = (pDSCOpts != null) ? pDSCOpts : new List<string>();
            pLCOpts = (pLCOpts != null) ? pLCOpts : new List<string>();

            // May or may not be used, declared in any case;
            Dictionary<string, string> mTitleFieldValues = null;

            // Determine whether transformation is required
            bool mTransformRequired = (pSrcProj != pTgtProj) ? true : false;

            // Read the target SRS
            SpatialReference mTgtSRS = ExtFunctions.GetSpatialReferenceByEPSG(pTgtProj.AuthorityCode);

            // If transformation is needed, create a shared transformation object for the export
            if (mTransformRequired)
            {
                pFLyr.Projection = pSrcProj;
                pFLyr.Reproject(pTgtProj);
            }

            // Setup driver
            OSGeo.OGR.Driver drv = Ogr.GetDriverByName(pDrvNm);
            if (drv == null)
            {
                mReturnValue.AddMessage("Could not load driver", true);
                return mReturnValue;
            }

            // Special handling of KML mFiles
            // Add special title name as datasource creation option
            if (pHasTitle && pDrvNm == "KML")
            {
                pDSCOpts.Add("NameField=" + ExtFunctions.TitleFieldName);
            }

            // Create a datasource
            DataSource ds = null;
            try
            {
                if (pAppend && (Directory.Exists(pOPFn) || File.Exists(pOPFn)))
                {
                    ds = drv.Open(pOPFn, 1);
                }
                else
                {
                    ds = drv.CreateDataSource(pOPFn, pDSCOpts.ToArray());
                }
                if (ds == null)
                {
                    mReturnValue.AddMessage("Could not create/open datasource");
                    return mReturnValue;
                }
            }
            catch (Exception ex)
            {
                mReturnValue.AddMessage(ex.Message);
                return mReturnValue;
            }

            // Find the geometry type of the source layer to determine output type
            wkbGeometryType mGeomType = DSGeomTypeToOgrGeomType(pFLyr.DataSet.FeatureType);
            if (mGeomType == wkbGeometryType.wkbNone)
            {
                mReturnValue.AddMessage("Could not parse geometry type of input layer");
                return mReturnValue;
            }

            // Create the new layer
            OSGeo.OGR.Layer l;
            if (pAppend && ds.HasLayer(pFLyr.LegendText.Replace(" ", "_")))
            {
                l = ds.GetLayerByName(pFLyr.LegendText.Replace(" ", "_"));
            }
            else
            {
                l = ds.CreateLayer(pFLyr.LegendText, mTgtSRS, mGeomType, pLCOpts.ToArray());
            }
            if (l == null)
            {
                mReturnValue.AddMessage("Failed to create file: ");
                return mReturnValue;
            }

            // Create a list to hold the names of fields
            var mDataSourceFieldNames = new List<string>();

            // Loop through all the fields
            foreach (DataColumn mDataColumn in pFLyr.DataSet.GetColumns())
            {
                var mColType = DataTypeToOgrFieldType(mDataColumn.DataType);
                var mNewField = new FieldDefn(MapFieldName(mDataColumn.ColumnName, pFieldMap), mColType);

                if (mColType == FieldType.OFTString)
                {
                    mNewField.SetWidth(mDataColumn.MaxLength);
                }


                if (pOnlyInFieldMap == false || (pOnlyInFieldMap && pFieldMap.Keys.Contains(mDataColumn.ColumnName)))
                {
                    if (!pAppend || !l.HasField(mNewField.GetName()))
                    {
                        if (Ogr.OGRERR_NONE != l.CreateField(mNewField, 1))
                        {
                            mReturnValue.AddMessage("Failed to add field: " + mDataColumn.ColumnName + " => " + MapFieldName(mDataColumn.ColumnName, pFieldMap));
                            return mReturnValue;
                        }
                    }
                }

                mDataSourceFieldNames.Add(mNewField.GetName());

            }

            // Add special title field, if the necessary information is present
            if (pHasTitle == true &&
                !string.IsNullOrEmpty(pTitleFieldNames) &&
                !string.IsNullOrEmpty(pTitleFormat) &&
                !mDataSourceFieldNames.Contains(ExtFunctions.TitleFieldName))
            {
                mTitleFieldValues = new Dictionary<string, string>();

                var mTitleField = new FieldDefn(MapFieldName(TitleFieldName, pFieldMap), FieldType.OFTString);
                mTitleField.SetWidth(254);

                foreach (var mTitleFieldName in pTitleFieldNames.Split(','))
                {
                    mTitleFieldValues.Add(mTitleFieldName, "");
                }

                l.CreateField(mTitleField, 1);

            }

            // For each of the row index mFeatureIndex the source featureset
            for (int i = 0; i < pFLyr.DataSet.NumRows(); i++)
            {
                // Read the source feature
                IFeature mSrcFeature = pFLyr.DataSet.GetFeature(i);

                // Read and set the geometry on the new feature
                byte[] mSrcGeom = mSrcFeature.ToBinary();
                var mFeature = new OSGeo.OGR.Feature(l.GetLayerDefn());
                var mGeom = OSGeo.OGR.Geometry.CreateFromWkb(mSrcGeom);
                mFeature.SetGeometry(mGeom);

                // Set feature id
                mFeature.SetFID(i);

                // Handle attributes
                for (int j = 0; j < mDataSourceFieldNames.Count(); j++)
                {
                    var mCurrentValue = mSrcFeature.DataRow[j];

                    if (pOnlyInFieldMap == false || (pOnlyInFieldMap && pFieldMap.Values.Contains(mDataSourceFieldNames[j])))
                    {
                        if (mCurrentValue.GetType() == typeof(string))
                        {
                            mFeature.SetField(mDataSourceFieldNames[j], Utilities.GetANSI((string)mCurrentValue));
                        }
                        else if (mCurrentValue.GetType() == typeof(int))
                        {
                            mFeature.SetField(mDataSourceFieldNames[j], (int)mCurrentValue);
                        }
                        else if (mCurrentValue.GetType() == typeof(double))
                        {
                            mFeature.SetField(mDataSourceFieldNames[j], (double)mCurrentValue);
                        }

                    }


                    if (pHasTitle && mTitleFieldValues.Keys.Contains(mDataSourceFieldNames[j]))
                    {
                        mTitleFieldValues[mDataSourceFieldNames[j]] = mCurrentValue.ToString();
                    }

                }

                // If title field is to be created, use field values collected above to construct a new string format
                // to go into the title-field
                if (pHasTitle == true)
                {
                    var mTitleValue = string.Format(pTitleFormat, mTitleFieldValues.Values.ToArray<string>());
                    mFeature.SetField(MapFieldName(TitleFieldName, pFieldMap), Utilities.GetANSI(mTitleValue));
                }

                // Create the feature
                l.CreateFeature(mFeature);
            }

            if (mTransformRequired)
            {
                pFLyr.Reproject(pSrcProj);
            }
            // Dispose and cleanup
            l.Dispose();
            ds.Dispose();
            drv.Dispose();

            return mReturnValue;
        }

    }
}
