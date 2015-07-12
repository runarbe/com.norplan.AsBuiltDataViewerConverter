using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS.DataTypes
{
    /// <summary>
    /// A generic return value class used by all functions in the library
    /// </summary>
    public class ReturnValue
    {
        public bool Success = true;
        public List<string> Messages = new List<string>();
        public Object Value = null;

        public ReturnValue(bool pSuccess = true)
        {
            this.Success = pSuccess;
        }

        public void SetValue(Object pValue) {
            this.Value = pValue;
        }

        public void AddMessage(string pMessage, bool pIsError = false) {
            if (!pIsError) {
                this.Success = false;
            }
            this.Messages.Add(pMessage);
        }

    }
}
