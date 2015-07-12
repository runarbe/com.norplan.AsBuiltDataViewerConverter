using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace AsBuiltToGIS.Forms
{
    public partial class frmQrCodeGen : Form
    {
        public frmQrCodeGen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbContentForEncoding.Text != "")
            {
                var barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Height = 400,
                        Width = 400,
                        Margin = 1

                    }
                };
                var bitmap = barcodeWriter.Write(tbContentForEncoding.Text);
                pbQrCode.Image = bitmap;
                System.Windows.Forms.Clipboard.SetImage(bitmap);
                lblQrStatus.Text = "The QR-code for " + tbContentForEncoding.Text + " has been copied to the clipboard";
            }
            else
            {
                lblQrStatus.Text = "You must enter some text to be encoded";
            }
        }

    }
}
