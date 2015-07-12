using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using DotSpatial.Controls.Docking;

namespace AsBuiltToGIS.Extensibility
{
    //[Export(typeof(IDockManager))]
    public class SimpleDockManager : IDockManager
    {

        event EventHandler<DockablePanelEventArgs> IDockManager.ActivePanelChanged
        {
            add { }
            remove { }
        }

        event EventHandler<DockablePanelEventArgs> IDockManager.PanelHidden
        {
            add { }
            remove { }
        }

        void IDockManager.ShowPanel(string pStr)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        void IDockManager.Add(DockablePanel panel)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void IDockManager.HidePanel(string key)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<DockablePanelEventArgs> IDockManager.PanelAdded
        {
            add { }
            remove { }
        }
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<DockablePanelEventArgs> IDockManager.PanelClosed
        {
            add { }
            remove { }
        }
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<DockablePanelEventArgs> IDockManager.PanelRemoved
        {
            add { }
            remove { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void IDockManager.Remove(string key)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        void IDockManager.ResetLayout()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void IDockManager.SelectPanel(string key)
        {
        }

    }
}
