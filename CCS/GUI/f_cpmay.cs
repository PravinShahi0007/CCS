﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ControlLocalizer;
using DAL;
using DevExpress.Utils.Win;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using GUI.Properties;
using Lotus;
using DevExpress.Data;
using GUI.theodoitt;

namespace GUI
{
    public partial class f_cpmay : Form
    {
        private KetNoiDBDataContext db = new KetNoiDBDataContext();
        private readonly t_cpmay pchi = new t_cpmay();
        private string _mact = "";
        private bool _duyet = false;
        //form
        public f_cpmay()
        {
            InitializeComponent();

            rsearchtiente1.DataSource = new KetNoiDBDataContext().tientes;
            btnmuccp.DataSource = new KetNoiDBDataContext().muccps;
            btncongviec1.DataSource = new KetNoiDBDataContext().congviecs;
            txtiddt.Properties.DataSource = new KetNoiDBDataContext().doituongs;
            btnPhuongTien.DataSource = new KetNoiDBDataContext().phuongtiens;
            txtloaichi.Properties.DataSource = new KetNoiDBDataContext().loaicpms;

            var lst = (from a in db.vanbandens
                       join d in db.donvis on a.iddv equals d.id
                       //where a.iddv == Biencucbo.donvi
                       select new
                       {
                           a.id,
                           a.ngaynhan,
                           a.sovb,
                           a.noidung
                       }).ToList();
            slulink.Properties.DataSource = _tTodatatable.addlst(lst.ToList());
            txtlinkgoc.Properties.DataSource = _tTodatatable.addlst(lst.ToList());
        }


        //load
        private void layttnhanvien(string txt)
        {
            try
            {
                var lst = (from a in db.accounts select a).Single(t => t.id == txt);
                lbltennv.Text = lst.name;
                txtphongban.Text = lst.phongban;
            }
            catch (Exception ex)
            {
                lbltennv.Text = "";
                txtphongban.Text = "";
            }
        }
        public void load()
        {
            db = new KetNoiDBDataContext();
            Biencucbo.hdpc = 2;
            txt1.Enabled = false;

            btnLuu.Enabled = false;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnin.Enabled = true;
            btnreload.Enabled = false;

            txtdv.ReadOnly = true;
            txtid.ReadOnly = true;
            txtdiachi.ReadOnly = true;
            txtidnv.ReadOnly = true;
            txtphongban.ReadOnly = true;

            // Enable
            txtghichu.ReadOnly = true;
            txtngaynhap.ReadOnly = true;
            txtiddt.ReadOnly = true;
            slulink.ReadOnly = true;
            txtlinkgoc.ReadOnly = true;


            txtloaichi.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;

            try
            {
                if (Biencucbo.xembc)
                {
                    var lst1 =
                        (from b in db.cpmays where b.idct == _mact select b).Single(t => t.id == Biencucbo.ma);

                    if (lst1 == null) return;

                    gcchitiet.DataSource = lst1.cpmaycts;

                    txtid.Text = lst1.id;
                    laychuyentien();
                    txtidnv.Text = lst1.idnv;
                    layttnhanvien(lst1.idnv);
                    txtdv.Text = lst1.iddv;
                    txtngaynhap.DateTime = DateTime.Parse(lst1.ngaychi.ToString());
                    txtiddt.Text = lst1.iddt;
                    slulink.Text = lst1.link;
                    txtlinkgoc.Text = lst1.linkgoc;

                    try
                    {
                        var lst2 = (from a in db.doituongs select a).Single(t => t.id == txtiddt.Text);
                        lbltendt.Text = lst2.ten;
                        txtdiachi.Text = lst2.diachi;
                    }
                    catch (Exception)
                    {
                    }
                    txtghichu.Text = lst1.ghichu;
                    txt1.Text = lst1.so.ToString();
                    txtloaichi.Text = lst1.loaichi;
                    try
                    {
                        var lst2 = (from a in db.loaicpms select a).Single(t => t.id == txtloaichi.Text);
                        lblloaichi.Text = lst2.loaicpm1;
                    }
                    catch
                    {
                    }
                    Biencucbo.xembc = false;
                    duyethd();
                }
                else
                {
                    var lst =
                    (from a in db.cpmays where a.iddv == Biencucbo.donvi && a.idct == _mact select a.so).Max();
                    var lst1 =
                        (from b in db.cpmays where b.iddv == Biencucbo.donvi && b.idct == _mact select b)
                            .FirstOrDefault(t => t.so == lst);
                    if (lst1 == null) return;

                    gcchitiet.DataSource = lst1.cpmaycts;

                    txtid.Text = lst1.id;
                    laychuyentien();
                    txtidnv.Text = lst1.idnv;
                    layttnhanvien(lst1.idnv);
                    txtdv.Text = lst1.iddv;
                    txtngaynhap.DateTime = DateTime.Parse(lst1.ngaychi.ToString());
                    txtiddt.Text = lst1.iddt;
                    slulink.Text = lst1.link;
                    txtlinkgoc.Text = lst1.linkgoc;

                    try
                    {
                        var lst2 = (from a in db.doituongs select a).Single(t => t.id == txtiddt.Text);
                        lbltendt.Text = lst2.ten;
                        txtdiachi.Text = lst2.diachi;
                    }
                    catch (Exception)
                    {
                    }
                    txtghichu.Text = lst1.ghichu;
                    txt1.Text = lst1.so.ToString();
                    txtloaichi.Text = lst1.loaichi;
                    try
                    {
                        var lst2 = (from a in db.loaicpms select a).Single(t => t.id == txtloaichi.Text);
                        lblloaichi.Text = lst2.loaicpm1;
                    }
                    catch
                    {
                    }
                    duyethd();
                }
            }
            catch
            {
            }
        }

        private string LayMaTim(donvi d)
        {
            var s = "." + d.id + "." + d.iddv + ".";
            var find = db.donvis.FirstOrDefault(t => t.id == d.iddv);
            if (find != null)
            {
                var iddv = find.iddv;
                if (d.id != find.iddv)
                {
                    if (!s.Contains(iddv))
                        s += iddv + ".";
                }
                while (iddv != find.id)
                {
                    if (!s.Contains(find.id))
                        s += find.id + ".";
                    find = db.donvis.FirstOrDefault(t => t.id == find.iddv);
                }
            }
            return s;
        }

        //phân quyền
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            var q = Biencucbo.QuyenDangChon;
            if (q == null) return;

            if ((bool)q.Them)
            {
                btnnew.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnnew.Visibility = BarItemVisibility.Never;
            }
            if ((bool)q.Sua)
            {
                btnsua.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnsua.Visibility = BarItemVisibility.Never;
            }
            if ((bool)q.Xoa)
            {
                btnxoa.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnxoa.Visibility = BarItemVisibility.Never;
            }
            if ((bool)q.chuyentien)
            {
                btnchuyentien.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnchuyentien.Visibility = BarItemVisibility.Never;
            }
            if ((bool)q.duyet)
            {
                _duyet = true;
            }
            else
            {
                _duyet = false;
            }
        }



        // Mở
        t_todatatable _tTodatatable = new t_todatatable();

        private void mo()
        {
            try
            {
                gridView1.OptionsView.ShowAutoFilterRow = true;
                gridView1.ClearColumnsFilter();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            Biencucbo.ltlc = 1;
            Biencucbo.mamo = _mact;
            var frm = new f_dscpmay();
            frm.ShowDialog();
            if (Biencucbo.getID == 1)
            {
                //load pnhap
                try
                {
                    var lst = (from pn in db.cpmays select new { a = pn }).FirstOrDefault(x => x.a.id == Biencucbo.ma);
                    if (lst == null) return;

                    txtid.Text = lst.a.id;
                    txtidnv.Text = lst.a.idnv;
                    layttnhanvien(lst.a.idnv);
                    txtdv.Text = lst.a.iddv;
                    txtngaynhap.DateTime = DateTime.Parse(lst.a.ngaychi.ToString());
                    txtiddt.Text = lst.a.iddt;
                    slulink.Text = lst.a.link;
                    txtlinkgoc.Text = lst.a.linkgoc;

                    txtloaichi.Text = lst.a.loaichi;
                    try
                    {
                        var lst2 = (from a in db.loaicpms select a).Single(t => t.id == txtloaichi.Text);
                        lblloaichi.Text = lst2.loaicpm1;
                    }
                    catch
                    {
                    }
                    txtghichu.Text = lst.a.ghichu;
                    txt1.Text = lst.a.so.ToString();

                    gcchitiet.DataSource = lst.a.cpmaycts;
                    laychuyentien();
                    duyethd();

                    //btn
                    btnnew.Enabled = true;
                    btnsua.Enabled = true;
                    btnLuu.Enabled = false;
                    btnmo.Enabled = true;
                    btnxoa.Enabled = true;
                    btnin.Enabled = true;
                    btnreload.Enabled = false;
                }
                catch
                {
                }
            }
        }
        private void btnmo_ItemClick(object sender, ItemClickEventArgs e)
        {
            mo();
        }

        //Thêm
        private void them()
        {
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.ClearColumnsFilter();

            Biencucbo.hdpc = 0;
            txtid.DataBindings.Clear();
            txtid.Text = "YYYYY";

            gcchitiet.DataSource = new KetNoiDBDataContext().View_cpmays;
            laychuyentien();
            for (var i = 0; i <= gridView1.RowCount - 1; i++)
            {
                gridView1.DeleteRow(i);
            }
            gridView1.AddNewRow();

            txtdv.Text = Biencucbo.donvi;
            txtngaynhap.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            txtphongban.Text = Biencucbo.phongban;
            txtidnv.Text = Biencucbo.idnv.Trim();
            lbltennv.Text = Biencucbo.ten;
            txtngaynhap.Focus();
            txtiddt.Text = "";
            slulink.Text = "";
            txtlinkgoc.Text = "";

            lbltendt.Text = "";

            txtghichu.Text = "";
            txtloaichi.Text = "";
            lblloaichi.Text = "";

            //btn
            btnnew.Enabled = false;
            btnmo.Enabled = false;
            btnLuu.Enabled = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnin.Enabled = false;
            btnreload.Enabled = false;

            //enabled
            txtghichu.ReadOnly = false;
            txtngaynhap.ReadOnly = false;
            txtiddt.ReadOnly = false;
            slulink.ReadOnly = false;
            txtlinkgoc.ReadOnly = false;

            txtloaichi.ReadOnly = false;
            gridView1.OptionsBehavior.Editable = true;
        }
        private void btnnew_ItemClick(object sender, ItemClickEventArgs e)
        {
            them();
        }


        //Lưu
        public void luu()
        {
            var hs = new t_history();
            var td = new t_tudong();
            gridView1.PostEditor();
            gridView1.UpdateCurrentRow();
            if (txtngaynhap.Text == "" || txtiddt.Text == "" || txtloaichi.Text == "" || txtloaichi.Text == "")
            {
                MsgBox.ShowWarningDialog("Thông tin chưa đầy đủ - Vui lòng kiểm tra lại!");
            }
            else
            {
                if (Biencucbo.hdpc == 0)
                {
                    db = new KetNoiDBDataContext();
                    try
                    {
                        var check = "CPM" + Biencucbo.donvi.Trim();
                        var lst1 = (from s in db.tudongs where s.maphieu == check select new { s.so }).ToList();

                        if (lst1.Count == 0)
                        {
                            int so;

                            so = 2;
                            td.themtudong(check, so);
                            txtid.Text = check + "_000001";
                            txt1.Text = "1";
                        }
                        else
                        {
                            int k;
                            txt1.DataBindings.Clear();
                            txt1.DataBindings.Add("text", lst1, "so");
                            k = 0;
                            k = Convert.ToInt32(txt1.Text);
                            var so0 = "";
                            if (k < 10)
                            {
                                so0 = "00000";
                            }
                            else if (k >= 10 & k < 100)
                            {
                                so0 = "0000";
                            }
                            else if (k >= 100 & k < 1000)
                            {
                                so0 = "000";
                            }
                            else if (k >= 1000 & k < 10000)
                            {
                                so0 = "00";
                            }
                            else if (k >= 10000 & k < 100000)
                            {
                                so0 = "0";
                            }
                            else if (k >= 100000)
                            {
                                so0 = "";
                            }
                            txtid.Text = check + "_" + so0 + k;

                            k = k + 1;

                            td.suatudong(check, k);
                        }
                        pchi.moiphieu(txtid.Text, txtngaynhap.DateTime, txtiddt.Text, txtdv.Text, txtidnv.Text,
                            txtghichu.Text, Convert.ToInt32(txt1.Text), txtloaichi.Text, _mact, slulink.Text,
                            "KIP", txtlinkgoc.Text);

                        try
                        {
                            gridView1.ClearSorting();
                            for (var i = 0; i <= gridView1.RowCount - 1; i++)
                            {
                                gridView1.SetRowCellValue(i, "idchi", txtid.Text);
                                gridView1.SetRowCellValue(i, "id", txtid.Text + i);
                                gridView1.SetRowCellValue(i, "stt", i.ToString());
                                pchi.moict(gridView1.GetRowCellValue(i, "diengiai").ToString(),
                                    gridView1.GetRowCellValue(i, "idcv").ToString(),
                                    gridView1.GetRowCellValue(i, "idmuccp").ToString(),
                                    gridView1.GetRowCellValue(i, "idchi").ToString(),
                                    gridView1.GetRowCellValue(i, "id").ToString(),
                                    double.Parse(gridView1.GetRowCellValue(i, "nguyente").ToString()),
                                    gridView1.GetRowCellValue(i, "idpt").ToString(),
                                    double.Parse(gridView1.GetRowCellValue(i, "dongia").ToString()),
                                    double.Parse(gridView1.GetRowCellValue(i, "sotien").ToString()),
                                    gridView1.GetRowCellValue(i, "donvi").ToString(),
                                    int.Parse(gridView1.GetRowCellValue(i, "stt").ToString()),
                                    double.Parse(gridView1.GetRowCellValue(i, "catgiam").ToString()),
                                    gridView1.GetRowCellValue(i, "lydocg").ToString());
                            }
                            gridView1.Columns["stt"].SortOrder = ColumnSortOrder.Ascending;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        //btn
                        btnmo.Enabled = true;
                        btnnew.Enabled = true;
                        btnLuu.Enabled = false;
                        btnsua.Enabled = true;
                        btnxoa.Enabled = true;
                        btnin.Enabled = true;
                        btnreload.Enabled = false;

                        //enabled
                        txtghichu.ReadOnly = true;
                        txtngaynhap.ReadOnly = true;
                        txtiddt.ReadOnly = true;
                        slulink.ReadOnly = true;
                        txtlinkgoc.ReadOnly = true;

                        txtloaichi.ReadOnly = true;

                        gridView1.OptionsBehavior.Editable = false;
                        Biencucbo.hdpc = 2;

                        // History 
                        hs.add(txtid.Text, "Thêm mới chứng từ");
                        gridView1.OptionsView.ShowAutoFilterRow = true;
                        gridView1.ClearColumnsFilter();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        pchi.suaphieu(txtid.Text, DateTime.Parse(txtngaynhap.Text), txtiddt.Text, txtghichu.Text,
                            int.Parse(txt1.Text), txtloaichi.Text, slulink.Text, "KIP", txtlinkgoc.Text);
                        //sua ct
                        LuuPhieu();
                        //btn
                        btnmo.Enabled = true;
                        btnnew.Enabled = true;
                        btnLuu.Enabled = false;
                        btnsua.Enabled = true;
                        btnxoa.Enabled = true;
                        btnin.Enabled = true;
                        btnreload.Enabled = false;

                        //enabled
                        txtghichu.ReadOnly = true;
                        txtngaynhap.ReadOnly = true;
                        txtiddt.ReadOnly = true;
                        slulink.ReadOnly = true;
                        txtlinkgoc.ReadOnly = true;
                        txtloaichi.ReadOnly = true;
                        gridView1.OptionsBehavior.Editable = false;
                        Biencucbo.hdpc = 2;


                        hs.add(txtid.Text, "Sửa chứng từ");
                        gridView1.OptionsView.ShowAutoFilterRow = true;
                        gridView1.ClearColumnsFilter();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void btnLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            luu();
        }

        private bool LuuPhieu()
        {
            // kiem tra truoc khi luu
            layoutControl1.Validate();
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            try
            {
                db.cpmaycts.Context.SubmitChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }


            return true;
        }

        //Sửa
        private void sua()
        {
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.ClearColumnsFilter();

            //if (Biencucbo.idnv != txtidnv.Text)
            //{
            //    XtraMessageBox.Show("  quyền chỉnh sửa phiếu này", "THÔNG BÁO");
            //    return;
            //}
            if (txtid.Text == "")
            {
                return;
            }

            //load 
            try
            {
                var lst = (from pn in db.cpmays select pn).FirstOrDefault(x => x.id == txtid.Text);
                if (lst == null) return;
                gcchitiet.DataSource = lst.cpmaycts;
                laychuyentien();
                //enabled
                txtghichu.ReadOnly = false;
                txtngaynhap.ReadOnly = false;
                txtiddt.ReadOnly = false;
                slulink.ReadOnly = false;
                txtlinkgoc.ReadOnly = false;
                txtloaichi.ReadOnly = false;
                gridView1.OptionsBehavior.Editable = true;

                Biencucbo.hdpc = 1;

                // btn
                btnsua.Enabled = false;
                btnLuu.Enabled = true;
                btnmo.Enabled = false;
                btnnew.Enabled = false;
                btnxoa.Enabled = false;
                btnin.Enabled = false;
                btnreload.Enabled = true;
            }
            catch
            {
            }
        }
        private void btnsua_ItemClick(object sender, ItemClickEventArgs e)
        {
            var lst3 = (from dt in new KetNoiDBDataContext().duyeths where dt.id == txtid.Text select dt).ToList();
            if (lst3.Count() != 0)
            {
                try
                {
                    if (lst3.Single().T == true)
                    {
                        XtraMessageBox.Show("Phiếu này đã được duyệt không thể sửa");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            sua();
        }

        //Xóa
        private void xoa()
        {
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.ClearColumnsFilter();
            //if (Biencucbo.idnv != txtidnv.Text)
            //{
            //    XtraMessageBox.Show("Bạn không có quyền xóa phiếu này", "THÔNG BÁO");
            //    return;
            //}
            if (txtid.Text == "")
            {
                return;
            }
            if (
                XtraMessageBox.Show("Bạn có chắc chắn muốn xóa Phiếu " + txtid.Text + " không?", "THÔNG BÁO",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var hs = new t_history();
                hs.add(txtid.Text, "Xóa chứng từ");

                try
                {
                    for (var i = gridView1.DataRowCount - 1; i >= 0; i--)
                    {
                        pchi.xoact(gridView1.GetRowCellValue(i, "id").ToString());
                        gridView1.DeleteRow(i);
                    }
                    pchi.xoapphieu(txtid.Text);

                    //btn
                    btnmo.Enabled = true;
                    btnnew.Enabled = true;
                    btnLuu.Enabled = false;
                    btnsua.Enabled = true;
                    btnxoa.Enabled = true;
                    btnin.Enabled = true;
                    btnreload.Enabled = false;

                    //enabled
                    txtghichu.ReadOnly = true;
                    txtngaynhap.ReadOnly = true;
                    txtiddt.ReadOnly = true;
                    slulink.ReadOnly = true;
                    txtlinkgoc.ReadOnly = true;
                    txtloaichi.ReadOnly = true;
                    gridView1.OptionsBehavior.Editable = false;

                    txtdv.Text = "";
                    txtid.Text = "";
                    txtidnv.Text = "";
                    txtdv.Text = "";
                    txtngaynhap.Text = "";
                    txtiddt.Text = "";
                    slulink.Text = "";
                    txtlinkgoc.Text = "";
                    txtghichu.Text = "";
                    txt1.Text = "";

                    lbltendt.Text = "";
                    lbltennv.Text = "";
                    txtloaichi.Text = "";
                    lblloaichi.Text = "";
                }
                catch
                {
                }
            }
        }
        private void btnxoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            var lst3 = (from dt in new KetNoiDBDataContext().duyeths where dt.id == txtid.Text select dt).ToList();
            if (lst3.Count() != 0)
            {
                XtraMessageBox.Show("Phiếu này đã được duyệt không thể xóa");
                return;
            }
            xoa();
        }

        //In
        private void btnin_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        //reload
        private void reload()
        {
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.ClearColumnsFilter();
            if (Biencucbo.hdpc == 1)
            {


                var lst =
                    (from pn in new KetNoiDBDataContext().cpmays where pn.idct == _mact select pn).FirstOrDefault(
                        x => x.id == txtid.Text);

                if (lst == null) return;
                txtidnv.Text = lst.idnv;
                layttnhanvien(lst.idnv);
                txtdv.Text = lst.iddv;
                txtngaynhap.DateTime = DateTime.Parse(lst.ngaychi.ToString());
                txtiddt.Text = lst.iddt;
                slulink.Text = lst.link;
                txtlinkgoc.Text = lst.linkgoc;
                txtghichu.Text = lst.ghichu;
                txt1.Text = lst.so.ToString();

                gcchitiet.DataSource = lst.cpmaycts;
                laychuyentien();
                //readonly
                txtghichu.ReadOnly = true;
                txtngaynhap.ReadOnly = true;
                txtloaichi.ReadOnly = true;
                txtiddt.ReadOnly = true;
                slulink.ReadOnly = true;
                txtlinkgoc.ReadOnly = true;
                //gcchitiet.Enabled = false;

                //btn
                btnnew.Enabled = true;
                btnsua.Enabled = true;
                btnLuu.Enabled = false;
                btnmo.Enabled = true;
                btnxoa.Enabled = true;
                btnin.Enabled = true;
                btnreload.Enabled = false;

                gridView1.OptionsBehavior.Editable = false;
            }

            else if (Biencucbo.hdpc == 0)
            {
                load();
                btnnew.Enabled = true;
                btnsua.Enabled = true;
                btnLuu.Enabled = false;
                btnmo.Enabled = true;
                btnxoa.Enabled = true;
                btnin.Enabled = true;
                btnreload.Enabled = false;

                gridView1.OptionsBehavior.Editable = false;
            }
            Biencucbo.hdpc = 2;
        }
        private void btnload_ItemClick(object sender, ItemClickEventArgs e)
        {
            reload();
        }


        // thay đổi
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            gridView1.PostEditor();
            if (e.Column.FieldName == "nguyente" || e.Column.FieldName == "dongia" || e.Column.FieldName == "catgiam")
            {
                try
                {  
                    gridView1.SetFocusedRowCellValue("sotien",
                        (double.Parse(gridView1.GetFocusedRowCellValue("nguyente").ToString()) *
                        double.Parse(gridView1.GetFocusedRowCellValue("dongia").ToString())) -
                        double.Parse(gridView1.GetFocusedRowCellValue("catgiam").ToString()));

                    gridView1.PostEditor();
                    gridView1.UpdateCurrentRow();
                    laychuyentien();
                }
                catch (Exception ex)
                {
                }
            }
            //else if (e.Column.FieldName == "dongia")
            //{
            //    try
            //    {
            //        gridView1.SetFocusedRowCellValue("sotien",
            //           (double.Parse(gridView1.GetFocusedRowCellValue("nguyente").ToString()) *
            //           double.Parse(gridView1.GetFocusedRowCellValue("dongia").ToString())) -
            //           double.Parse(gridView1.GetFocusedRowCellValue("catgiam").ToString()));
            //        gridView1.PostEditor();
            //        gridView1.UpdateCurrentRow();
            //        laychuyentien();
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
        }

        //Phím Tắt
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
        }


        private void laychuyentien()
        {
            try
            {
                var lst = (from a in new KetNoiDBDataContext().theodoitt_cpms where a.idcpm == txtid.Text select a);
                if (lst.Count() > 0)
                {
                    txttiendachuyen.Text = lst.Sum(t => t.sotienchuyen).ToString();
                    txttienconlai.Text =
                        (double.Parse(colsotien.SummaryItem.SummaryValue.ToString()) - lst.Sum(t => t.sotienchuyen))
                            .ToString();
                }
                else
                {
                    txttiendachuyen.Text = "0";
                    txttienconlai.Text = (double.Parse(colsotien.SummaryItem.SummaryValue.ToString())).ToString();
                }
            }
            catch
            {
            }
        }
        //đối tượng
        private void txtiddt_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lst = (from a in db.doituongs select a).Single(t => t.id == txtiddt.Text);
                lbltendt.Text = lst.ten;
                txtdiachi.Text = lst.diachi;
            }
            catch (Exception)
            {
            }
        }


        //Dòng mới
        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if (Biencucbo.hdpc == 1)
            {
                var ct = gridView1.GetFocusedRow() as cpmayct;
                if (ct == null) return;

                int i = 0, k = 0;
                string a;

                try
                {
                    k = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.DataRowCount - 1, "stt").ToString());
                    k = k + 1;
                }
                catch (Exception ex)
                {

                }
                a = txtid.Text + k;

                for (i = 0; i <= gridView1.DataRowCount - 1;)
                {
                    if (a == gridView1.GetRowCellValue(i, "id").ToString())
                    {
                        k = k + 1;
                        a = txtid.Text + k;
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                }

                ct.idchi = txtid.Text;
                ct.diengiai = "";
                ct.idmuccp = "";
                ct.idcv = "";
                ct.sotien = 0;
                ct.id = a;
                ct.stt = Convert.ToInt32(ct.id.Substring(ct.idchi.Length, ct.id.Length - ct.idchi.Length));
                ct.idpt = "";
                //ct.tiente = "KIP";
                ct.dongia = 1;
                ct.catgiam = 0;
                ct.lydocg = "";
                ct.nguyente = 0;
                ct.donvi = "";
            }

            else
            {
                gridView1.SetFocusedRowCellValue("diengiai", "");
                gridView1.SetFocusedRowCellValue("idmuccp", "");
                gridView1.SetFocusedRowCellValue("idcv", "");
                gridView1.SetFocusedRowCellValue("nguyente", 0);
                gridView1.SetFocusedRowCellValue("dongia", 1);
                gridView1.SetFocusedRowCellValue("catgiam", 0);
                gridView1.SetFocusedRowCellValue("lydocg", "");
                gridView1.SetFocusedRowCellValue("idpt", "");
                gridView1.SetFocusedRowCellValue("donvi", "");
            }
        }

        //Xóa dòng
        private void print()
        {
            try
            {
                var lst = from a in db.r_cpmays
                          join b in db.accounts on a.idnv equals b.id
                          join c in db.congtrinhs on a.idct equals c.id
                          join d in db.loaicpms on a.loaichi equals d.id
                          join l in db.phuongtiens on a.idpt equals l.id into k
                          from f in k.DefaultIfEmpty()
                          where a.id == txtid.Text
                          select new
                          {
                              a.id,
                              a.iddv,
                              a.ngaychi,
                              b.name,
                              tencongtrinh = "Công trình: " + a.idct + " - " + c.tencongtrinh,
                              loainhap = a.loaichi + "-" + d.loaicpm1,
                              ten = a.tendt,
                              a.catgiam,
                              a.lydocg,
                              a.ghichu,
                              a.diengiai,
                              tiente = a.donvi,
                              soluong = a.nguyente,
                              a.dongia,
                              thanhtien = a.sotien,
                              phuongtien = f.ten
                          };
                var xtra = new r_cpmay();
                xtra.DataSource = _tTodatatable.addlst(lst.ToList());
                xtra.ShowPreviewDialog();
            }
            catch
            {
            }
        }
        private void btnin_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            print();
        }

        //đóng form
        private void f_pnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Biencucbo.hdpc != 2)
            {
                var a =
                    MsgBox.ShowYesNoCancelDialog(
                        "Phiếu này chưa được lưu - Bạn có muốn lưu Phiếu này trước khi thoát không?");
                if (a == DialogResult.Yes)
                {
                    luu();
                }
                else if (a == DialogResult.Cancel) e.Cancel = true;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gridView1.IsGroupRow(e.RowHandle)) //Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) //Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; //Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); //Số thứ tự tăng dần
                    }
                    var _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    //Lấy kích thước của vùng hiển thị Text
                    var _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gridView1); }));
                    //Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", e.RowHandle * -1); //Nhân -1 để đánh lại số thứ tự tăng dần
                var _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gridView1); }));
            }
        }

        private bool cal(int _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        private void btnmuccp_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();
        }

        private void rsearchtiente1_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    gridView1.PostEditor();
            //    var lst = (from a in db.tientes select a).Single(t => t.tiente1 == gridView1.GetFocusedRowCellValue("tiente").ToString());
            //    if (lst == null) return;
            //    gridView1.SetFocusedRowCellValue("tygia", lst.tygia);
            //    //gridView1.SetFocusedRowCellValue("sotien", int.Parse(gridView1.GetFocusedRowCellValue("nguyente").ToString()) * int.Parse(txttygia.Text));
            //    //gridView1.SetFocusedRowCellValue("sotien", double.Parse(gridView1.GetFocusedRowCellValue("nguyente").ToString()) * double.Parse(gridView1.GetFocusedRowCellValue("tygia").ToString()));
            //}
            //catch
            //{
            //}
        }

        private void f_pchi_Load(object sender, EventArgs e)
        {
            //LanguageHelper.Translate(this);
            WindowState = FormWindowState.Maximized;
            LanguageHelper.Translate(barManager1);
            Text = "Chi Phí Máy - Công Trình: " +
                   (from a in db.congtrinhs select a).Single(t => t.id == Biencucbo.mact).tencongtrinh;
            _mact = Biencucbo.mact;
            changeFont.Translate(this);
            changeFont.Translate(barManager1);

            load();
            Biencucbo.hdpc = 2;
        }

        private void txttiente_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    var lst = (from a in db.tientes select a).FirstOrDefault(t => t.tiente1 == txttiente.Text);
            //    txttygia.Text = lst.tygia.ToString();

            //    for (int i = 0; i <= gridView1.RowCount - 1; i++)
            //    {
            //        try
            //        {
            //            gridView1.SetRowCellValue(i, "sotien", double.Parse(gridView1.GetRowCellValue(i, "nguyente").ToString()) * double.Parse(txttygia.Text));
            //        }
            //        catch
            //        {


            //        }
            //    }

            //}
            //catch
            //{

            //}
        }


        private void btnPhuongTien_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();
        }

        private void txtloaichi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lst = (from a in db.loaicpms select a).Single(t => t.id == txtloaichi.Text);
                lblloaichi.Text = lst.loaicpm1;
            }
            catch
            {
            }
        }

        private void txtiddt_Popup(object sender, EventArgs e)
        {
            var popupControl = sender as IPopupControl;
            var button = new SimpleButton
            {
                Image = Resources.icons8_Add_File_16,
                Text = "Edit",
                BorderStyle = BorderStyles.NoBorder
            };

            button.Click += button_Click;

            button.Location = new Point(5, popupControl.PopupWindow.Height - button.Height - 5);
            popupControl.PopupWindow.Controls.Add(button);
            button.BringToFront();
        }

        public void button_Click(object sender, EventArgs e)
        {
            var frm = new f_doituong();
            frm.ShowDialog();
            txtiddt.Properties.DataSource = new KetNoiDBDataContext().doituongs;
        }

        public void buttonlcpm_Click(object sender, EventArgs e)
        {
            var frm = new f_themlcpm();
            frm.ShowDialog();
            txtloaichi.Properties.DataSource = new KetNoiDBDataContext().loaicpms;
        }

        private void txtloaichi_Popup(object sender, EventArgs e)
        {
            var popupControl = sender as IPopupControl;
            var button = new SimpleButton
            {
                Image = Resources.icons8_Add_File_16,
                Text = "Add",
                BorderStyle = BorderStyles.NoBorder
            };

            button.Click += buttonlcpm_Click;

            button.Location = new Point(5, popupControl.PopupWindow.Height - button.Height - 5);
            popupControl.PopupWindow.Controls.Add(button);
            button.BringToFront();
        }

        public void buttontiente_Click(object sender, EventArgs e)
        {
            //f_tiente frm = new f_tiente();
            //frm.ShowDialog();
            //txttiente.Properties.DataSource = new DAL.KetNoiDBDataContext().tientes;
        }

        private void txttiente_Popup(object sender, EventArgs e)
        {
            //IPopupControl popupControl = sender as IPopupControl;
            //SimpleButton button = new SimpleButton()
            //{
            //    Image = Resources.icons8_Add_File_16,
            //    Text = "Add",
            //    BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            //};

            //button.Click += new EventHandler(buttontiente_Click);

            //button.Location = new Point(5, popupControl.PopupWindow.Height - button.Height - 5);
            //popupControl.PopupWindow.Controls.Add(button);
            //button.BringToFront();
        }

        private string layttnd(string id)
        {
            try
            {
                var lst = (from a in new KetNoiDBDataContext().accounts select a).Single(t => t.id == id);
                return lst.name;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public void duyethd()
        {
            db = new KetNoiDBDataContext();
            var lst3 = (from dt in new KetNoiDBDataContext().duyeths where dt.id == txtid.Text select dt).ToList();
            if (lst3.Count() != 0)
            {
                var lst4 = (from a in new KetNoiDBDataContext().duyeths select a).First(t => t.id == txtid.Text);
                if (lst4.T == true)
                {
                    btnduyet.Glyph = Resources.folder_full_accept_icon;
                    btnnoidung.Caption = "Người duyệt: " + lst4.iduser + "-" + layttnd(lst4.iduser) + ". Ghi Chú: " + lst4.ghichu;

                }
                else if (lst4.T == false)
                {
                    btnduyet.Glyph = Resources.folder_full_delete_icon;
                    btnnoidung.Caption = "Người duyệt: " + lst4.iduser + "-" + layttnd(lst4.iduser) + ". Ghi Chú: " + lst4.ghichu;
                }
            }
            else
            {
                btnduyet.Glyph = Resources.folder_full_icon;
                btnnoidung.Caption = "Chưa duyệt";
            }
        }

        private void btnduyet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Biencucbo.hdpc == 2)
            {
                if (_duyet)
                {
                    var hs = new t_history();
                    if (Biencucbo.hdpc == 2)
                    {
                        Biencucbo.idduyet = txtid.Text;
                        Biencucbo.loaiduyet = "Duyệt Chi Phí Máy";
                        var frm = new f_duyeths();
                        frm.ShowDialog();
                        duyethd();
                        hs.add(txtid.Text, "Duyệt Chi Phí Máy");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa được cấp quyền duyệt hồ sơ này");
                }
            }
        }

        private void f_cpmay_Activated(object sender, EventArgs e)
        {
            //if (Biencucbo.hdpc == 2)
            //{
            //    gridView1.OptionsView.ShowAutoFilterRow = true;
            //}
            //else
            //{
            //    gridView1.OptionsView.ShowAutoFilterRow = false;
            //    gridView1.ClearColumnsFilter();
            //}
        }

        private void f_cpmay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.O)
                {
                    if (btnmo.Enabled)
                        mo();
                }
                else if (e.KeyCode == Keys.N)
                {
                    if (btnthemmoi.Enabled)
                        them();
                }
                else if (e.KeyCode == Keys.S)
                {
                    if (btnLuu.Enabled)
                        luu();
                }
                else if (e.KeyCode == Keys.U)
                {
                    if (btnsua.Enabled)
                        sua();
                }
                else if (e.KeyCode == Keys.D)
                {
                    if (btnxoa.Enabled)
                        xoa();
                }
                else if (e.KeyCode == Keys.P)
                {
                    if (btnin.Enabled)
                        print();
                }
                else if (Biencucbo.hdpc != 2)
                {

                    if (e.KeyCode == Keys.Delete)
                    {
                        if (Biencucbo.hdpc == 1)
                        {
                            try
                            {
                                var ct =
                                    (from c in db.cpmaycts select c).Single(
                                        x => x.id == gridView1.GetFocusedRowCellValue("id").ToString());
                                db.cpmaycts.DeleteOnSubmit(ct);
                            }
                            catch
                            {
                            }

                        }
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    }
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (btnload.Enabled)
                    reload();
            }
            if (Biencucbo.hdpc != 2)
            {
                if (e.KeyCode == Keys.Insert)
                {
                    gridView1.AddNewRow();
                }

            }
        }

        private void btnimport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (txtid.Text == string.Empty || gridView1.DataRowCount > 0)
                {
                    XtraMessageBox.Show("Phiếu này không thể import, vui lòng liên hệ Admin");
                    return;

                }
                else if (btnLuu.Enabled == true)
                {
                    XtraMessageBox.Show("Vui lòng lưu phiếu trước khi import");
                    return;

                }
                else
                {
                    Biencucbo.ma = "cpmayct";
                    f_import frm = new f_import();
                    frm.ShowDialog();
                    var lst = (from a in new KetNoiDBDataContext().cpmays select a).Single(t => t.id == txtid.Text);
                    gcchitiet.DataSource = lst.cpmaycts;

                    laychuyentien();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnchuyentien_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Biencucbo.hdpc == 2 && txtid.Text != string.Empty)
            {
                Biencucbo.ma = txtid.Text;
                theodoitt.f_theodoitt_cpm frm = new f_theodoitt_cpm();
                frm.ShowDialog();
                laychuyentien();
            }
        }

        private void layttlbllink(string id)
        {
            try
            {
                var lst = (from a in new KetNoiDBDataContext().vanbandens select a).Single(t => t.id == id);
                lbllink.Text = lst.noidung;
            }
            catch (Exception ex)
            {
                lbllink.Text = "";
            }
        }
        private void slulink_EditValueChanged(object sender, EventArgs e)
        {
            layttlbllink(slulink.Text);
        }

        private void layttlbllinkgoc(string id)
        {
            try
            {
                var lst = (from a in new KetNoiDBDataContext().vanbandens select a).Single(t => t.id == id);
                lbllinkgoc.Text = lst.noidung;
            }
            catch (Exception ex)
            {
                lbllinkgoc.Text = "";
            }
        }
        private void txtlinkgoc_EditValueChanged(object sender, EventArgs e)
        {
            layttlbllinkgoc(txtlinkgoc.Text);
        }
    }
}