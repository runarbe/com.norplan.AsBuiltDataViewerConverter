using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls.Header;

namespace AsBuiltToGIS.Functions
{
    [Export(typeof(IHeaderControl))]
    public class SimpleHeaderControl : MenuBarHeaderControl, IPartImportsSatisfiedNotification
    {
        private ToolStripPanel toolStripContainer1;

        [Import("Shell", typeof(ContainerControl))]
        private ContainerControl Shell { get; set; }

        /// 

        /// Called when a part's imports have been satisfied and it is safe to use. (Shell will have a mCurrentValue)
        /// 

        public void OnImportsSatisfied()
        {
            this.toolStripContainer1 = new ToolStripPanel();
            this.toolStripContainer1.SuspendLayout();

            this.toolStripContainer1.Dock = DockStyle.Fill;
            this.toolStripContainer1.Name = "toolStripContainer1";

            // place all of the controls that were on the form originally inside of our content panel.
            while (Shell.Controls.Count > 0)
            {
                foreach (Control control in Shell.Controls)
                {
                    this.toolStripContainer1.Controls.Add(control);
                }
            }

            Shell.Controls.Add(this.toolStripContainer1);

            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();

            Initialize(toolStripContainer1, new MenuStrip());        }
    }
}
