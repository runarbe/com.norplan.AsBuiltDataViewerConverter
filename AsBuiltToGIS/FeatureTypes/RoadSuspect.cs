using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS
{
    class RoadSuspect
    {
        public int RoadId { get; set; }
        public int NumberOfSegments { get; set; }
        public int NumberOfDuplicates { get; set; }
        public double DiagonalLength { get; set; }
        public double TotalSegmentLength { get; set; }
        public double SegmentDiagonalFactor { get; set; }
        public int SuspectRank { get; set; }

        public RoadSuspect(int pRoadId, int pNumberOfSegments, int pNumberOfDuplicates, double pDiagonalLength, double pTotalSegmentLength)
        {
            this.RoadId = pRoadId;
            this.NumberOfSegments = pNumberOfSegments;
            this.NumberOfDuplicates = pNumberOfDuplicates;
            this.DiagonalLength = pDiagonalLength;
            this.TotalSegmentLength = pTotalSegmentLength;
            this.SegmentDiagonalFactor = Math.Round(pTotalSegmentLength / pDiagonalLength, 1);

            if (double.IsNaN(this.SegmentDiagonalFactor )) {
                this.SuspectRank = 4;
            }
            else if (this.SegmentDiagonalFactor <= 2.2)
            {
                this.SuspectRank = 0;
            }
            else if (this.SegmentDiagonalFactor <= 2.2)
            {
                this.SuspectRank = 1;
            }
            else if (this.SegmentDiagonalFactor <= 4)
            {
                this.SuspectRank = 2;
            }
            else if (this.SegmentDiagonalFactor <= 10)
            {
                this.SuspectRank = 3;
            }
            else if (this.SegmentDiagonalFactor > 10)
            {
                this.SuspectRank = 4;
            }

        }
    }
}
