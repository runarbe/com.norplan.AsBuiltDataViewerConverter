using System;
using System.Data.Odbc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Data;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    /// <summary>
    /// Utility functions that are used by the application
    /// </summary>
    public static class Utilities
    {

        public static string[] GetLastN(this string[] pArray, int pN)
        {
            if (pArray.Length < pN)
            {
                pN = pArray.Length;
            }
            
            string[] mArray = new string[pN];


            for (int idx = pArray.Length, pLimit = (pArray.Length - pN); idx > pLimit; idx--)
            {
                mArray[pN - 1] = pArray[idx - 1];
                pN--;
            }
            return mArray;

        }

        /// <summary>
        /// Extension method for Dictionary<int, DataRow>
        /// </summary>
        /// <param name="pDictionary">Self</param>
        /// <param name="pKey">ID of road</param>
        /// <param name="pValueFldNm">Property of road</param>
        /// <returns>The value of the property as a string</returns>
        public static string GetStringOrNull(this Dictionary<int, DataRow> pDictionary, int pKey, string pValueFldNm)
        {
            if (pDictionary.Keys.Contains(pKey))
            {
                DataRow mDataRow = pDictionary[pKey];
                if (mDataRow.Table.Columns.Contains(pValueFldNm))
                {
                    return mDataRow[pValueFldNm].ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// Extension method for Dictionary<string, DataRow>
        /// </summary>
        /// <param name="pDictionary">Self</param>
        /// <param name="pKey">ID of road</param>
        /// <param name="pValueFldNm">Property of road</param>
        /// <returns>The value of the property as a string</returns>
        public static string GetStringOrNull(this Dictionary<string, DataRow> pDictionary, string pKey, string pValueFldNm)
        {
            if (pDictionary.Keys.Contains(pKey))
            {
                DataRow mDataRow = pDictionary[pKey];
                if (mDataRow.Table.Columns.Contains(pValueFldNm))
                {
                    return mDataRow[pValueFldNm].ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the ODBC driver names from the registry.
        /// </summary>
        /// <returns>a string array containing the ODBC driver names, if the registry key is present; null, otherwise.</returns>
        public static string[] GetOdbcDriverNames()
        {
            string[] odbcDriverNames = null;
            using (RegistryKey localMachineHive = Registry.LocalMachine)
            using (RegistryKey odbcDriversKey = localMachineHive.OpenSubKey(@"SOFTWARE\ODBC\ODBCINST.INI\ODBC Drivers"))
            {
                if (odbcDriversKey != null)
                {
                    odbcDriverNames = odbcDriversKey.GetValueNames();
                }
            }

            return odbcDriverNames;
        }

        /// <summary>
        /// Random number generator accessible to all classes
        /// </summary>
        public static Random Rnd = new Random();

        /// <summary>
        /// Holds the most recent 1000 generated random numbers
        /// </summary>
        public static List<int> RecentRndList = new List<int>();

        /// <summary>
        /// Constant for conversion of centimeters to pixels assuming 1 inch = 100 pixels
        /// </summary>
        public const double PixelsPerCentimeter = 100 / 2.54;

        /// <summary>
        /// Abu Dhabi in latin script
        /// </summary>
        public static string LABEL_ABUDHABI_EN = "Abu Dhabi";

        /// <summary>
        /// Abu Dhabi in arabic script
        /// </summary>
        public static string LABEL_ABUDHABI_AR = "أبو ظبي‎";

        /// <summary>
        /// Return number of pixels for the specified distance in centimeters
        /// </summary>
        /// <param name="pCm">A distance in centimeter</param>
        /// <returns>Distance in pixels</returns>
        public static int Cm(double pCm)
        {
            return (int)Math.Floor(pCm * Utilities.PixelsPerCentimeter);
        }

        /// <summary>
        /// Log a message to the debug stream
        /// </summary>
        /// <param name="pMsg"></param>
        public static void LogDebug(string pMsg)
        {
            Debug.WriteLine(pMsg);
            return;
        }

        public static byte[] StringToUtf8Bytes(string str)
        {
            if (str == null)
            {
                return null;
            }

            Encoding encoder = Encoding.UTF8;
            int strLen = str.Length;
            int nativeLength = encoder.GetMaxByteCount(strLen);
            byte[] bytes = new byte[nativeLength + 1]; // zero terminated
            encoder.GetBytes(str, 0, str.Length, bytes, 0);
            return bytes;
        }

        public static string NormalizeString(string pStr)
        {
            return Regex.Replace(pStr, "[\r\n]*", "");
        }

        public static string GetVariants(string pTerms)
        {
            var mVariants = new List<string>();
            foreach (string pTerm in pTerms.Split(' '))
            {
                if (!String.IsNullOrEmpty(pTerm) && pTerm != "Street")
                {

                    // Check for double e
                    if (pTerm.Contains("ee"))
                    {
                        mVariants.Add(pTerm.Replace("ee", "i"));
                    }

                    // Check for mFeatureIndex
                    if (pTerm.Contains("i"))
                    {
                        mVariants.Add(pTerm.Replace("i", "ee"));
                    }

                    // Add more tests here

                }
            }


            return String.Join(", ", mVariants);
        }

        public static string GetANSI(string pStr)
        {
            return pStr;
            // The below fix was used for the version of the C# bindings on
            // NuGet
            //return Encoding.GetEncoding("Windows-1252").GetString(Encoding.UTF8.GetBytes(pStr));
        }

        public static void ResetRndGenerator()
        {
            Utilities.RecentRndList.Clear();
            return;
        }

        public static int GetRndBetween(int pLow, int pHigh)
        {
            int mRnd = Utilities.Rnd.Next(pLow, pHigh);
            while (Utilities.RecentRndList.Contains(mRnd))
            {
                mRnd = Utilities.Rnd.Next(pLow, pHigh);
            };
            Utilities.RecentRndList.Add(mRnd);
            return mRnd;
        }

        public static double Greatest(double pDouble1, double pDouble2)
        {
            if (pDouble1 > pDouble2)
            {
                return pDouble1;
            }
            else
            {
                return pDouble2;
            }
        }

        public static double Smallest(double pDouble1, double pDouble2)
        {
            if (pDouble1 < pDouble2)
            {
                return pDouble1;
            }
            else
            {
                return pDouble2;
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the mFiles in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
