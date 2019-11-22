﻿using System;
using System.Drawing;
using System.Linq;
using BUS;
using DAL;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using CellValueChangedEventArgs = DevExpress.XtraTreeList.CellValueChangedEventArgs;

namespace GUI
{
    public partial class FrmPhanQuyen : XtraForm
    {
        private KetNoiDBDataContext db = new KetNoiDBDataContext();
        t_todatatable _tTodatatable = new t_todatatable();
        public FrmPhanQuyen()
        {
            InitializeComponent();

            gridControl1.DataSource = db.accounts;

            treeList1.ExpandAll();
            var skin = GridSkins.GetSkin(treeList1.LookAndFeel);
            skin.Properties[GridSkins.OptShowTreeLine] = true;
        }

        private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                e.Appearance.Options.UseTextOptions = true;
            }
        }

        private void NapChucNangNguoiDung()
        {
            db = new KetNoiDBDataContext();
            // lay user
            var user = gridView1.GetFocusedRow() as account;
            if (user == null) return;

            // lay danh sach quyen của user
            var l = db.PhanQuyen2s.Where(t => t.TaiKhoan == user.id);

            var q = from c in db.ChucNangs
                select new ObjPhanQuyen
                {
                    MaChucNang = c.MaChucNang,
                    TenChucNang = c.TenChucNang,
                    ChucNangCha = c.ChucNangCha,
                    Xem = LayQuyen(0, l, c),
                    Them = LayQuyen(1, l, c),
                    Sua = LayQuyen(2, l, c),
                    Xoa = LayQuyen(3, l, c)
                };

            treeList1.DataSource = _tTodatatable.addlst(q.ToList());
            treeList1.ExpandAll();
        }

        private bool LayQuyen(int index, IQueryable<PhanQuyen2> l, ChucNang c)
        {
            var b = false;
            if (index == 0)
            {
                var find = l.FirstOrDefault(q => q.ChucNang == c.MaChucNang);
                if (find == null) return false;

                return find.Xem == null ? false : Convert.ToBoolean(find.Xem);
            }
            if (index == 1)
            {
                var find = l.FirstOrDefault(q => q.ChucNang == c.MaChucNang);
                if (find == null) return false;

                return find.Them == null ? false : Convert.ToBoolean(find.Them);
            }
            if (index == 2)
            {
                var find = l.FirstOrDefault(q => q.ChucNang == c.MaChucNang);
                if (find == null) return false;

                return find.Sua == null ? false : Convert.ToBoolean(find.Sua);
            }
            if (index == 3)
            {
                var find = l.FirstOrDefault(q => q.ChucNang == c.MaChucNang);
                if (find == null) return false;

                return find.Xoa == null ? false : Convert.ToBoolean(find.Xoa);
            }
            return b;
        }

        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            NapChucNangNguoiDung();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // xu ly phan quyen
            var q = Biencucbo.QuyenDangChon;
            if (q == null) return;

            // vi du thoi
            gridControl1.Enabled = treeList1.Enabled = Convert.ToBoolean(q.Sua);
        }

        private void SetCheckedChildNodes(TreeListNode node, TreeListColumn col, bool check)
        {
            var allowShow = (bool) node.GetValue(colXem);
            var allowAddNew = (bool) node.GetValue(colThem);
            var allowEdit = (bool) node.GetValue(colSua);
            var allowDelete = (bool) node.GetValue(colXoa);


            // viet o day
            var user = gridView1.GetFocusedRow() as account;

            var obj = treeList1.GetDataRecordByNode(node) as ObjPhanQuyen;
            var find = db.PhanQuyen2s.FirstOrDefault(q => q.ChucNang == obj.MaChucNang && q.TaiKhoan == user.id);
            if (find == null)
            {
                find = new PhanQuyen2();
                find.TaiKhoan = user.id;
                find.ChucNang = obj.MaChucNang;
                find.Xem = allowShow;
                find.Them = allowAddNew;
                find.Sua = allowEdit;
                find.Xoa = allowDelete;

                db.PhanQuyen2s.InsertOnSubmit(find);
                db.SubmitChanges();
            }
            else
            {
                find.Xem = allowShow;
                find.Them = allowAddNew;
                find.Sua = allowEdit;
                find.Xoa = allowDelete;
                db.SubmitChanges();
            }

            for (var i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i][col] = check;
                SetCheckedChildNodes(node.Nodes[i], col, check);
            }
        }

        private void SetCheckedParentNodes(TreeListNode node, TreeListColumn col, bool check)
        {
            if (node.ParentNode != null)
            {
                var b = false;
                bool state;
                for (var i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (bool) node.ParentNode.Nodes[i][col];
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                var bb = b ? false : check;
                node.ParentNode[col] = bb;

                var allowShow = (bool) node.ParentNode.GetValue(colXem);
                var allowAddNew = (bool) node.ParentNode.GetValue(colThem);
                var allowEdit = (bool) node.ParentNode.GetValue(colSua);
                var allowDelete = (bool) node.ParentNode.GetValue(colXoa);


                // viet o day
                var user = gridView1.GetFocusedRow() as account;

                var obj = treeList1.GetDataRecordByNode(node.ParentNode) as ObjPhanQuyen;
                var find = db.PhanQuyen2s.FirstOrDefault(q => q.ChucNang == obj.MaChucNang && q.TaiKhoan == user.id);
                if (find == null)
                {
                    find = new PhanQuyen2();
                    find.TaiKhoan = user.id;
                    find.ChucNang = obj.MaChucNang;
                    find.Xem = allowShow;
                    find.Them = allowAddNew;
                    find.Sua = allowEdit;
                    find.Xoa = allowDelete;

                    db.PhanQuyen2s.InsertOnSubmit(find);
                    db.SubmitChanges();
                }
                else
                {
                    find.Xem = allowShow;
                    find.Them = allowAddNew;
                    find.Sua = allowEdit;
                    find.Xoa = allowDelete;
                    db.SubmitChanges();
                }
                SetCheckedParentNodes(node.ParentNode, col, check);
            }
        }

        private void treeList1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            var user = gridView1.GetFocusedRow() as account;
            if (user == null) return;

            e.Node.SetValue(e.Column, e.Value);

            var obj = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as ObjPhanQuyen;
            var find = db.PhanQuyen2s.FirstOrDefault(q => q.ChucNang == obj.MaChucNang && q.TaiKhoan == user.id);
            if (find == null)
            {
                find = new PhanQuyen2();
                find.TaiKhoan = user.id;
                find.ChucNang = obj.MaChucNang;
                find.Xem = obj.Xem;
                find.Them = obj.Them;
                find.Sua = obj.Sua;
                find.Xoa = obj.Xoa;

                db.PhanQuyen2s.InsertOnSubmit(find);
                db.SubmitChanges();
            }
            else
            {
                find.Xem = obj.Xem;
                find.Them = obj.Them;
                find.Sua = obj.Sua;
                find.Xoa = obj.Xoa;
                db.SubmitChanges();
            }

            SetCheckedChildNodes(e.Node, e.Column, (bool) e.Value);
            SetCheckedParentNodes(e.Node, e.Column, (bool) e.Value);
        }
    }
}