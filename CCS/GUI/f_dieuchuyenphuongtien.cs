﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ControlLocalizer;
using DAL;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GUI.Properties;
using Lotus;

namespace GUI
{
    public partial class f_dieuchuyenphuongtien : Form
    {
        public static string tenpt, dv_ht, tendv_ht, dt_ht, tendt_ht, tendv_dc, tendt_dc;
        private readonly KetNoiDBDataContext db = new KetNoiDBDataContext();
        private t_doituong dt = new t_doituong();
        private readonly t_lichsu_phuongtien ls = new t_lichsu_phuongtien();
        private readonly t_phuongtien pt = new t_phuongtien();

        public f_dieuchuyenphuongtien()
        {
            InitializeComponent();
            txtphuongtien.Properties.DataSource = new KetNoiDBDataContext().phuongtiens;
            txtdv_dc.Properties.DataSource = new KetNoiDBDataContext().congtrinhs;
            txtdt_dc.Properties.DataSource = new KetNoiDBDataContext().nhanviens;
        }


        private void f_dieuchuyenphuongtien_Load(object sender, EventArgs e)
        {
            LanguageHelper.Translate(this);

            Text = LanguageHelper.TranslateMsgString("." + Name + "_title", "Điều Chuyển Phương Tiện");

            changeFont.Translate(this);

            if (Biencucbo.dcpt == 1)
            {
                txtphuongtien.Enabled = false;
                txtphuongtien.ReadOnly = true;
                txtdv_ht.ReadOnly = true;
                txtdt_ht.ReadOnly = true;

                var Lst = (from a in db.phuongtiens
                    join b in db.nhanviens
                        on a.madt equals b.id into k
                    join c in db.congtrinhs
                        on a.madv equals c.id into l
                    from k1 in k.DefaultIfEmpty()
                    from l1 in l.DefaultIfEmpty()
                    select new
                    {
                        a.id,
                        a.ten,
                        a.madv,
                        tendv = l1.tencongtrinh,
                        madt = k1.id,
                        tendt = k1.ten
                    }).Single(t => t.id == Biencucbo.ma);

                txtphuongtien.DataBindings.Clear();

                txtphuongtien.Text = Lst.id;
                lblpt.Text = Lst.ten;
                txtdv_ht.Text = Lst.madv;
                lblcongtrinh.Text = Lst.tendv;
                txtdt_ht.Text = Lst.madt;
                lbldt.Text = Lst.tendt;
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtphuongtien.Text == "" || lblpt.Text == "" || txtthoigian.Text == "")
                {
                    MsgBox.ShowWarningDialog("Thông tin chưa đầy đủ - Vui lòng kiểm tra lại!");
                }
                else
                {
                    if (txtdv_dc.Text == "" && txtdt_dc.Text == "")
                    {
                        MsgBox.ShowWarningDialog("Thông tin chưa đầy đủ - Vui lòng kiểm tra lại!");
                    }
                    else
                    {
                        if (txtdv_dc.Text == "")
                        {
                            txtdv_dc.Text = txtdv_ht.Text;
                            lblcongtrinhdc.Text = lblcongtrinh.Text;
                        }
                        if (txtdt_dc.Text == "")
                        {
                            if (txtdt_ht.Text != "")
                            {
                                txtdt_dc.Text = txtdt_ht.Text;
                                lbldtdc.Text = lbldt.Text;
                            }
                            else
                            {
                                txtdt_dc.Text = "";
                                lbldtdc.Text = "";
                            }
                        }

                        pt.sua_dieuchuyen(txtphuongtien.Text, txtdv_dc.Text, txtdt_dc.Text);

                        var lst = (from a in db.lichsu_phuongtiens where a.id == txtphuongtien.Text select a).ToList();
                        if (lst.Count == 0)
                        {
                            ls.moi_lsdieuchuyen(txtphuongtien.Text, lblpt.Text, txtdv_ht.Text, lblcongtrinh.Text,
                                txtdv_dc.Text, lblcongtrinhdc.Text, txtdt_ht.Text, lbldt.Text, txtdt_dc.Text,
                                lbldtdc.Text, txtthoigian.DateTime, Biencucbo.donvi);
                        }
                        else
                        {
                            ls.sua_lsdieuchuyen(txtphuongtien.Text, lblpt.Text, txtdv_ht.Text, lblcongtrinh.Text,
                                txtdv_dc.Text, lblcongtrinhdc.Text, txtdt_ht.Text, lbldt.Text, txtdt_dc.Text,
                                lbldtdc.Text, txtthoigian.DateTime, Biencucbo.donvi);
                        }

                        Close();
                    }
                }
            }
            catch
            {
            }
        }

        private void txtdt_dc_Popup(object sender, EventArgs e)
        {
            var popupControl = sender as IPopupControl;
            var button = new SimpleButton
            {
                Image = Resources.icons8_Add_File_16,
                Text = "Add",
                BorderStyle = BorderStyles.NoBorder
            };

            button.Click += button2_Click;

            button.Location = new Point(5, popupControl.PopupWindow.Height - button.Height - 5);
            popupControl.PopupWindow.Controls.Add(button);
            button.BringToFront();
        }


        public void button2_Click(object sender, EventArgs e)
        {
            var frm = new f_nhanvienlaixe();
            frm.ShowDialog();
            txtdt_dc.Properties.DataSource = new KetNoiDBDataContext().nhanviens;
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtdt_dc_EditValueChanged(object sender, EventArgs e)
        {
            var list = (from a in db.nhanviens
                where a.id == txtdt_dc.Text
                select a).Single(t => t.id == txtdt_dc.Text);


            lbldtdc.Text = list.ten;
        }

        private void txtdv_dc_EditValueChanged(object sender, EventArgs e)
        {
            var list = (from a in db.congtrinhs select a).Single(t => t.id == txtdv_dc.Text);
            lblcongtrinhdc.Text = list.tencongtrinh;
        }

        private void txtphuongtien_EditValueChanged(object sender, EventArgs e)
        {
            var lst = (from a in db.phuongtiens
                join b in db.nhanviens
                    on a.madt equals b.id into k
                join c in db.congtrinhs
                    on a.madv equals c.id into l
                from k1 in k.DefaultIfEmpty()
                from l1 in l.DefaultIfEmpty()
                select new
                {
                    a.id,
                    a.ten,
                    a.madv,
                    tendv = l1.tencongtrinh,
                    madt = k1.id,
                    tendt = k1.ten
                }).Single(t => t.id == txtphuongtien.Text);

            tenpt = lst.ten;
            dv_ht = lst.madv;
            tendv_ht = lst.tendv;
            dt_ht = lst.madt;
            tendt_ht = lst.tendt;

            lblpt.Text = tenpt;
            txtdv_ht.Text = dv_ht;
            txtdt_ht.Text = dt_ht;
            lblcongtrinh.Text = tendv_ht;
            lbldt.Text = tendt_ht;
        }
    }
}