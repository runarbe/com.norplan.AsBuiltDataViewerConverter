using System;
using System.Collections.Generic;

namespace Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi
{
    public class SignRecord
    {
        public List<string> StatementBuffer = new List<string>();

        public string GetBuffer()
        {
            var mCopy = String.Join(", ", this.StatementBuffer);
            this.StatementBuffer.Clear();
            return mCopy;
        }

        public void AddToBuffer(string pQrCode,
            string pAddressUnitNumber,
            string pStreetNameEn,
            string pStreetNameAr,
            string pDistrictNameEn,
            string pDistrictNameAr,
            string pX,
            string pY,
            string pStreetNameDescEn = "",
            string pStreetNameDescAr = "",
            string pTableName = "ADRPRINCIPALADDRESS",
            string pSignType = "Address unit sign",
            string pSignTypeDesc = "")
        {

            this.StatementBuffer.Add(String.Format("('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})",
                pQrCode,
                pTableName,
                pSignTypeDesc,
                pSignType,
                pAddressUnitNumber,
                pStreetNameDescEn,
                pStreetNameEn,
                pStreetNameDescAr,
                pStreetNameAr,
                pDistrictNameEn,
                pDistrictNameAr,
                pX,
                pY));

        }

    }
}
