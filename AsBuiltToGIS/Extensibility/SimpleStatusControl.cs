using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using DotSpatial.Controls.Header;

namespace AsBuiltToGIS.Extensibility
{
    //[Export(typeof(IStatusControl))]
    class SimpleStatusControl : IStatusControl
    {

        /// <summary>
        /// add status panel 
        /// </summary>
        /// <param name="panel"></param>
        void IStatusControl.Add(StatusPanel panel)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// remove status panel
        /// </summary>
        /// <param name="panel"></param>
        void IStatusControl.Remove(StatusPanel panel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Put progres 
        /// </summary>
        /// <param name="key">key panel</param>
        /// <param name="percent">percent progress</param>
        /// <param name="message">message app</param>
        void DotSpatial.Data.IProgressHandler.Progress(string key, int percent, string message)
        {
        }

    }
}
