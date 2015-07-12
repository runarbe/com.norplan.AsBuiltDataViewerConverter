using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS.Functions
{
    public enum RetValStatus{
        success = 1,
        failure = -1
    }

    public class RetVal
    {
        RetValStatus status = RetValStatus.success;
        String msg = "";

        public RetVal(RetValStatus pStatus, Object pObj, String pMsg = null)
        {
            this.status = pStatus;
            if (pMsg == null) {
                this.msg = pMsg;
            }
            return;
        }
    }
}
