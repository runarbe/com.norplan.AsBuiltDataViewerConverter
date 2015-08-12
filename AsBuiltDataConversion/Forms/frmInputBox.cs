using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Norplan.Adm.AsBuiltDataConversion.Functions;

namespace Norplan.Adm.AsBuiltDataConversion.Forms
{
    public partial class frmInputBox : Form
    {
        public frmInputBox()
        {
            InitializeComponent();
        }

        public frmInputBox(string pTitle, string pCaption, object pDefaultValue)
        {
            InitializeComponent();

            this.lblInputCaption.Text = pCaption;
            this.Text = pTitle;
            this.tbInput.Text = pDefaultValue.ToString();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public int? GetAsInteger()
        {
            int i;
            return (int.TryParse(this.tbInput.Text, out i)) ? (int?)i : null;
        }

        public int? GetAsPercent()
        {
            var mStr = tbInput.Text.Replace(@"%", "").Trim();
            int i;
            if (int.TryParse(mStr, out i))
            {
                return (i <= 100) ? (int?)i : null;
            }
            else
            {
                Utilities.LogDebug("Could not parse value to percentage");
                return null;
            }
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

    }
}
