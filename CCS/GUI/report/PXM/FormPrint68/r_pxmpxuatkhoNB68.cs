using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GUI.report.PXM.FormPrint68
{
    public partial class r_pxmpxuatkhoNB68 : DevExpress.XtraReports.UI.XtraReport
    {
        public r_pxmpxuatkhoNB68()
        {
            InitializeComponent();
            txtngayky.Text = "Ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;
        }

    }
}
