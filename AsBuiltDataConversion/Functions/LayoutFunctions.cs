using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using DotSpatial.Controls;
using DotSpatial.Symbology;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class LayoutFunctions
    {

        public static void AddRectangle(LayoutControl pLayout, int pWidth, int pHeight, int pLeft, int pTop, bool pFill = true, string pColorName = "White")
        {
            LayoutRectangle mRect = new LayoutRectangle();
            mRect.Name = "mRect";
            mRect.Size = new Size(pWidth, pHeight);
            mRect.Location = new Point(pLeft, pTop);
            mRect.Background = SymbologyFunctions.GetPolygonSymbolizer(Color.FromName(pColorName));
            pLayout.AddToLayout(mRect);
        }

        public static void LayoutPageSize(Map pMap, LayoutControl pLayout, string pMapTitle, string pPageSizeName = "A3", double pWidth = 42, double pHeight = 29.7)
        {
            var mG = pLayout.CreateGraphics();

            // Calculate position parameters
            int pPageWidth = Utilities.Cm(pWidth);
            int pPageHeight = Utilities.Cm(pHeight);

            //int pPageWidth = pLayout.PrinterSettings.DefaultPageSettings.PaperSize.Width;
            //int pPageHeight = pLayout.PrinterSettings.DefaultPageSettings.PaperSize.Height;

            int pOuterMargin = Utilities.Cm(1);
            int pInternalMargin = Utilities.Cm(0.25);
            int pTitleHeight = Utilities.Cm(1);
            int pSubTitleHeight = Utilities.Cm(0.5);
            int pTitleAreaHeight = pTitleHeight + pInternalMargin + pSubTitleHeight + pInternalMargin;
            int pLegendWidth = Utilities.Cm(5);
            int pMapRectWidth = ((pPageWidth - (2 * pOuterMargin)) - pLegendWidth) - pInternalMargin;
            int pMapRectHeight = (pPageHeight - (2 * pOuterMargin)) - pTitleAreaHeight;
            int pMapWidth = pMapRectWidth - (pInternalMargin * 2);
            int pMapHeight = pMapRectHeight - (pInternalMargin * 2);
            int pScaleBarHeight = Utilities.Cm(0.75);
            int pScaleBarWidth = Utilities.Cm(5);
            int pNArrowWidth = Utilities.Cm(1);
            int pNArrowHeight = Utilities.Cm(1);

            // Set fonts
            var mFont1 = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Millimeter);
            var mFont2 = new Font("Arial", 4, FontStyle.Italic, GraphicsUnit.Millimeter);
            var mFont3 = new Font("Arial", 3, FontStyle.Regular, GraphicsUnit.Millimeter);

            // Set page size
            var mPaperSize = new PaperSize();
            mPaperSize.PaperName = pPageSizeName;
            mPaperSize.Width = pPageWidth;
            mPaperSize.Height = pPageHeight;
            pLayout.PrinterSettings.DefaultPageSettings.PaperSize = mPaperSize;

            // Set printer margin to 1 cm
            pLayout.PrinterSettings.DefaultPageSettings.Margins = new Margins(pOuterMargin, pOuterMargin, pOuterMargin, pOuterMargin);

            // Add map to layout
            var mMap = new LayoutMap(pMap);
            //mMap.MapControl = pMap;
            mMap.Size = new Size(pPageWidth, pPageHeight);
            mMap.Location = new Point(0, 0);
            pLayout.AddToLayout(mMap);

            // Add map mask

            // Add top margin
            AddRectangle(pLayout, pPageWidth, pOuterMargin + pTitleAreaHeight, 0, 0);

            // Add right margin
            AddRectangle(pLayout, pOuterMargin + pInternalMargin + pLegendWidth, pPageHeight, ((pPageWidth-pOuterMargin) - pLegendWidth) - pInternalMargin, 0);

            // Add bottom margin
            AddRectangle(pLayout, pPageWidth, pOuterMargin, 0, pPageHeight-pOuterMargin);

            // Add left margin
            AddRectangle(pLayout, pOuterMargin, pPageHeight, 0, 0);

            // Add title element to layout
            var mMapTitleElement = new LayoutText();
            mMapTitleElement.Name = "Title";
            mMapTitleElement.Size = new Size(pMapRectWidth, pTitleHeight);
            mMapTitleElement.Location = new Point(pOuterMargin, pOuterMargin);
            mMapTitleElement.Text = pMapTitle;
            mMapTitleElement.Font = mFont1;
            pLayout.AddToLayout(mMapTitleElement);

            // Add subtitle element to layout
            var mSubTitleElement = new LayoutText();
            mSubTitleElement.Name = "Subtitle";
            mSubTitleElement.Size = new Size(pMapRectWidth, pSubTitleHeight);
            mSubTitleElement.Location = new Point(pOuterMargin, pOuterMargin + pTitleHeight + pInternalMargin);
            mSubTitleElement.Text = "New Addressing System for Abu Dhabi Municipality";
            mSubTitleElement.Font = mFont2;
            pLayout.AddToLayout(mSubTitleElement);

            // Add map border rectangle to map
            var mRect = new LayoutRectangle();
            mRect.Size = new Size(pMapRectWidth, pMapRectHeight);
            mRect.Location = new Point(pOuterMargin, pOuterMargin + pTitleAreaHeight);
            pLayout.AddToLayout(mRect);

            // Add legend element to layout
            var mLegend = new LayoutLegend();
            mLegend.Name = "Legend";
            mLegend.LayoutControl = pLayout;
            mLegend.Map = mMap;
            mLegend.Size = new Size(pLegendWidth, pMapRectHeight);
            mLegend.Location = new Point(pPageWidth - pOuterMargin - pLegendWidth, pOuterMargin + pTitleAreaHeight);
            mLegend.Font = mFont3;
            pLayout.AddToLayout(mLegend);

            // Add scalebar element to layout
            var mScaleBar = new LayoutScaleBar();
            mScaleBar.Name = "Scale Bar";
            mScaleBar.Map = mMap;
            mScaleBar.Unit = ScaleBarUnit.Meters;
            mScaleBar.UnitText = "m";
            mScaleBar.Size = new Size(pScaleBarWidth, pScaleBarWidth);
            mScaleBar.Location = new Point(pOuterMargin + pInternalMargin + pInternalMargin, ((((pPageHeight - pOuterMargin) - pInternalMargin) - pInternalMargin) - pInternalMargin) - pScaleBarHeight);
            mScaleBar.Font = mFont3;
            mScaleBar.BreakBeforeZero = false;
            mScaleBar.NumberOfBreaks = 2;
            pLayout.AddToLayout(mScaleBar);

            // Add north arrow to layout
            var mNorthArrow = new LayoutNorthArrow();
            mNorthArrow.Name = "North Arrow";
            mNorthArrow.NorthArrowStyle = NorthArrowStyle.ArrowN;
            mNorthArrow.Size = new Size(Utilities.Cm(0.5), Utilities.Cm(1));
            mNorthArrow.Location = new Point(Utilities.Cm(1.5), Utilities.Cm(3.5));
            pLayout.AddToLayout(mNorthArrow);

            mG.DrawLine(new Pen(Color.Black), new Point(0, 0), new Point(500, 500));

        }

    }
}
