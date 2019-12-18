namespace GUI.report.PXM.FormPrint68
{
    partial class BaseReport_68
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseReport_68));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLaos = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPic_logo68 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xpPageSelector1 = new DevExpress.Xpo.XPPageSelector(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 20F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 20F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLaos,
            this.xrPic_logo68,
            this.xrCompany});
            this.ReportHeader.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.ReportHeader.HeightF = 263.2239F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseFont = false;
            this.ReportHeader.StylePriority.UseTextAlignment = false;
            this.ReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLaos
            // 
            this.xrLaos.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrLaos.LocationFloat = new DevExpress.Utils.PointFloat(402.3782F, 2.119276E-05F);
            this.xrLaos.Multiline = true;
            this.xrLaos.Name = "xrLaos";
            this.xrLaos.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 10, 0, 100F);
            this.xrLaos.SizeF = new System.Drawing.SizeF(384.6218F, 72.40204F);
            this.xrLaos.StylePriority.UseFont = false;
            this.xrLaos.StylePriority.UsePadding = false;
            this.xrLaos.StylePriority.UseTextAlignment = false;
            this.xrLaos.Text = "CỘNG HÒA DÂN CHỦ NHÂN DÂN LÀO\r\nHòa bình - Độc lập - Dân chủ - Thống nhất- Thịnh V" +
    "ượng";
            this.xrLaos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPic_logo68
            // 
            this.xrPic_logo68.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPic_logo68.ImageSource"));
            this.xrPic_logo68.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPic_logo68.Name = "xrPic_logo68";
            this.xrPic_logo68.SizeF = new System.Drawing.SizeF(77.39225F, 72.40206F);
            this.xrPic_logo68.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrCompany
            // 
            this.xrCompany.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrCompany.LocationFloat = new DevExpress.Utils.PointFloat(77.39225F, 0F);
            this.xrCompany.Multiline = true;
            this.xrCompany.Name = "xrCompany";
            this.xrCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrCompany.SizeF = new System.Drawing.SizeF(247.3644F, 72.40206F);
            this.xrCompany.StylePriority.UseFont = false;
            this.xrCompany.StylePriority.UseTextAlignment = false;
            this.xrCompany.Text = "CÔNG TY CỔ PHẦN\r\nXÂY DỰNG DỊCH VỤ & THƯƠNG MẠI 68";
            this.xrCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 0F;
            this.PageHeader.Name = "PageHeader";
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 96.88364F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo});
            this.PageFooter.HeightF = 58.10412F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo
            // 
            this.xrPageInfo.Font = new System.Drawing.Font("Arial", 11F);
            this.xrPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35.10411F);
            this.xrPageInfo.Name = "xrPageInfo";
            this.xrPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo.SizeF = new System.Drawing.SizeF(787F, 23F);
            this.xrPageInfo.StylePriority.UseFont = false;
            this.xrPageInfo.StylePriority.UseTextAlignment = false;
            this.xrPageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrPageInfo.TextFormatString = "Trang {0}/{1}";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSourceType = null;
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // BaseReport_68
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1,
            this.xpPageSelector1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.Xpo.XPPageSelector xpPageSelector1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo;
        private DevExpress.XtraReports.UI.XRLabel xrLaos;
        private DevExpress.XtraReports.UI.XRPictureBox xrPic_logo68;
        private DevExpress.XtraReports.UI.XRLabel xrCompany;
    }
}
