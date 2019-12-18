using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace GUI.report.PXM.FormPrint68
{
    public partial class r_pxmnhapkho68 : DevExpress.XtraReports.UI.XtraReport
    {
        public r_pxmnhapkho68()
        {
            InitializeComponent();
            txtngayky.Text = "Ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;

        }

        private void r_pxmnhapkho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
       
        }

        private void txtngay_TextChanged(object sender, EventArgs e)
        {
              
        }
    }
}
