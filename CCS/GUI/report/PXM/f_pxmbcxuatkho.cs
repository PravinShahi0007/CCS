﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DAL;
using DevExpress.Data;
using DevExpress.Utils.Zip.Internal;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSpreadsheet.Model;

namespace GUI.report.dk_report
{

    public partial class f_pxmbcxuatkho : GUI.frmdkreport
    {
        KetNoiDBDataContext dbData = new KetNoiDBDataContext();
        private bool checkct;

        public f_pxmbcxuatkho()
        {
            InitializeComponent();

        }
        protected override void load()
        {
            rdgloai.SelectedIndex = 0;
            txtdanhmuc.Properties.Items.Add("Công Trình");
            txtdanhmuc.Properties.Items.Add("Đối Tượng");
            txtdanhmuc.Properties.Items.Add("Vật Tư");
            txtdanhmuc.Properties.Items.Add("Phương Tiện");

        }

        protected override void loaddata()
        {
            gd1.DataSource = dbData.Laydkreport(txtdanhmuc.Text, Biencucbo.idnv, Name, Biencucbo.hostname);
            switch (rdgloai.SelectedIndex)
            {
                case 0:
                    gd2.DataSource = dbData.LayDSdkreport_phuongtien(Biencucbo.idnv, Name, Biencucbo.hostname);
                    break;
                case 2:
                    gd2.DataSource = dbData.LayDSdkreport_unctnhapxuat(Biencucbo.idnv, Name, Biencucbo.hostname);
                    break;
                case 1:
                    gd2.DataSource = dbData.LayDSdkreport_ctnhapxuat(Biencucbo.idnv, Name, Biencucbo.hostname);
                    break;
            }
        }

        private bool layinfo(string tungay, string denngay, bool tg)
        {
            if (tg)
                Biencucbo.ngaybc = "";
            else
                Biencucbo.ngaybc = "Từ ngày " + tungay + " Đến ngày " + denngay;
            Biencucbo.info = "";
            bool checkct = false;
            string loai = "";
            gv2.Columns["loai"].SortOrder = ColumnSortOrder.Ascending;

            for (int i = 0; i < gv2.DataRowCount; i++)
            {
                if (gv2.GetRowCellValue(i, "loai").ToString() == "Công Trình")
                {
                    checkct = true;
                }
                if (loai != gv2.GetRowCellValue(i, "loai").ToString())
                {
                    if (Biencucbo.info == "")
                    {
                        Biencucbo.info = gv2.GetRowCellValue(i, "loai") + ": " + gv2.GetRowCellValue(i, "name");
                    }
                    else
                    {
                        Biencucbo.info = Biencucbo.info + "\n" + gv2.GetRowCellValue(i, "loai") + ": " + gv2.GetRowCellValue(i, "name");
                    }
                }
                else
                {
                    Biencucbo.info = Biencucbo.info + ", " + gv2.GetRowCellValue(i, "name");
                }
                loai = gv2.GetRowCellValue(i, "loai").ToString();
            }

            if (Biencucbo.info == "")
                Biencucbo.info = "Tất cả";
            return checkct;
        }

        private void inbc<T>(bool tg)
        {
            if (layinfo(DateTime.Parse(tungay.EditValue.ToString()).ToShortDateString(), DateTime.Parse(denngay.EditValue.ToString()).ToShortDateString(), tg) == false)
            {
                XtraMessageBox.Show("Cần phải chọn Kho xuất/Công trình để xem báo cáo", "THÔNG BÁO");

                return;
            }

            try
            {
                var rp = Activator.CreateInstance<T>() as XtraReport;

                switch (rdgloai.SelectedIndex)
                {
                    case 0:
                        rp.DataSource =
                            dbData.InBCXuatKhoDung(Biencucbo.idnv, Name, Biencucbo.hostname,
                            DateTime.Parse(tungay.EditValue.ToString()),
                            DateTime.Parse(denngay.EditValue.ToString()), tg);

                        break;
                    case 1:
                        rp.DataSource =
                            dbData.InBCXuatKhoNB(Biencucbo.idnv, Name, Biencucbo.hostname,
                                DateTime.Parse(tungay.EditValue.ToString()),
                                DateTime.Parse(denngay.EditValue.ToString()), tg);

                        break;
                    case 2:
                        rp.DataSource =
                            dbData.InBCXuatKhoALL(Biencucbo.idnv, Name, Biencucbo.hostname,
                                DateTime.Parse(tungay.EditValue.ToString()),
                                DateTime.Parse(denngay.EditValue.ToString()), tg);

                        break;
                }
                if (tg)
                    Biencucbo.ngay = "";
                rp.ShowPreview();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        protected override void search()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            if (tgsmode.IsOn == false)
            {
                if (tgsloai.IsOn)
                {

                    //inbc<r_BCxuatkhopx_CT>(false);
                    inbc<GUI.report.PXM.BCXuatKho_new.r_BCxuatkhopx_CT>(false);
                }
                else
                {
                    //inbc<PXM.r_BCXuatKho_CT>(false);
                    inbc<GUI.report.PXM.BCXuatKho_new.r_BCXuatKho_CT>(false);
                }
            }
            else
            {
                //inbc<PXM.r_BCXuatkho_TH>(false);
                inbc<GUI.report.PXM.BCXuatKho_new.r_BCXuatkho_TH>(false);
            }

            SplashScreenManager.CloseForm();

        }

        protected override void searchall()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            if (tgsmode.IsOn == false)
            {
                if (tgsloai.IsOn)
                {

                    inbc<GUI.report.PXM.BCXuatKho_new.r_BCxuatkhopx_CT>(true);
                }
                else
                {
                    inbc<GUI.report.PXM.BCXuatKho_new.r_BCXuatKho_CT>(true);
                }
            }
            else
            {
                inbc<GUI.report.PXM.BCXuatKho_new.r_BCXuatkho_TH>(true);
            }

            SplashScreenManager.CloseForm();
        }

        protected override void search68()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            if (tgsmode.IsOn == false)
            {
                if (tgsloai.IsOn)
                {

                    //inbc<r_BCxuatkhopx_CT>(false);
                    inbc<GUI.report.PXM.FormPrint68.r_BCxuatkhopx_CT68>(false);
                }
                else
                {
                    //inbc<PXM.r_BCXuatKho_CT>(false);
                    inbc<GUI.report.PXM.FormPrint68.r_BCXuatKho_CT68>(false);
                }
            }
            else
            {
                //inbc<PXM.r_BCXuatkho_TH>(false);
                inbc<GUI.report.PXM.FormPrint68.r_BCXuatkho_TH68>(false);
            }

            SplashScreenManager.CloseForm();

        }

        protected override void searchall68()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            if (tgsmode.IsOn == false)
            {
                if (tgsloai.IsOn)
                {

                    inbc<GUI.report.PXM.FormPrint68.r_BCxuatkhopx_CT68>(true);
                }
                else
                {
                    inbc<GUI.report.PXM.FormPrint68.r_BCXuatKho_CT68>(true);
                }
            }
            else
            {
                inbc<GUI.report.PXM.FormPrint68.r_BCXuatkho_TH68>(true);
            }

            SplashScreenManager.CloseForm();
        }


        private void loaixuat()
        {
            switch (rdgloai.SelectedIndex)
            {
                case 0:
                    txtdanhmuc.Properties.Items.Remove("Phương Tiện");
                    txtdanhmuc.Properties.Items.Add("Phương Tiện");
                    txtdanhmuc.Properties.Items.Remove("Kho Nhập Nội Bộ");
                    if (txtdanhmuc.Text == "Kho Nhập Nội Bộ")
                        txtdanhmuc.Text = "Công Trình";
                    loaddata();
                    break;
                case 1:
                    txtdanhmuc.Properties.Items.Remove("Kho Nhập Nội Bộ");
                    txtdanhmuc.Properties.Items.Add("Kho Nhập Nội Bộ");
                    txtdanhmuc.Properties.Items.Remove("Phương Tiện");
                    if (txtdanhmuc.Text == "Phương Tiện")
                        txtdanhmuc.Text = "Công Trình";
                    loaddata();
                    break;
                case 2:
                    loaddata();
                    txtdanhmuc.Properties.Items.Remove("Kho Nhập Nội Bộ");
                    txtdanhmuc.Properties.Items.Remove("Phương Tiện");
                    if (txtdanhmuc.Text == "Kho Nhập Nội Bộ" || txtdanhmuc.Text == "Phương Tiện")
                        txtdanhmuc.Text = "Công Trình";
                    break;
            }

        }
        private void rdgloai_SelectedIndexChanged(object sender, EventArgs e)
        {

            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            loaixuat();
            SplashScreenManager.CloseForm();
        }

        private void tgsmode_EditValueChanged(object sender, EventArgs e)
        {
            if (tgsmode.IsOn)
            {
                ltgsloai.Visibility = LayoutVisibility.Never;
            }
            else
            {
                ltgsloai.Visibility = LayoutVisibility.Always;
            }
        }

        private void f_pxmbcxuatkho_Load(object sender, EventArgs e)
        {

        }
    }
}