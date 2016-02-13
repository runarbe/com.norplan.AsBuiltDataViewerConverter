using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public enum RetValStatus{
        success = 1,
        failure = -1
    }

    public class RetVal
    {
        public RetValStatus status = RetValStatus.success;
        String msg = "";

        public RetVal(RetValStatus pStatus, Object pObj, String pMsg = null)
        {
            this.status = pStatus;
            if (pMsg == null) {
                this.msg = pMsg;
            }
            return;
        }

        public RetVal Failure()
        {
            this.status = RetValStatus.failure;
            return this;
        }

        public RetVal Success()
        {
            this.status = RetValStatus.success;
            return this;
        }

    }
}
