namespace askue3
{
    partial class FormGridDataView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGridDataView));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tblMeteringPointBindingSource = new System.Windows.Forms.BindingSource();
            this.dataSet1 = new askue3.DataSet1();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colls1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcaption_mp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colid_origbd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colls2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltt_value = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltn_value = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldate_sbyt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tblCounterValueBindingSource = new System.Windows.Forms.BindingSource();
            this.tblOprosTypeBindingSource = new System.Windows.Forms.BindingSource();
            this.tblProjectTypeBindingSource = new System.Windows.Forms.BindingSource();
            this.tblCounterTypeBindingSource = new System.Windows.Forms.BindingSource();
            this.tblCounterTypeTableAdapter = new askue3.DataSet1TableAdapters.tblCounterTypeTableAdapter();
            this.tblProjectTypeTableAdapter = new askue3.DataSet1TableAdapters.tblProjectTypeTableAdapter();
            this.tblMetPointTypeBindingSource = new System.Windows.Forms.BindingSource();
            this.tblMetPointTypeTableAdapter = new askue3.DataSet1TableAdapters.tblMetPointTypeTableAdapter();
            this.tblFilialBindingSource = new System.Windows.Forms.BindingSource();
            this.tblFilialTableAdapter = new askue3.DataSet1TableAdapters.tblFilialTableAdapter();
            this.tblPlacementTypeBindingSource = new System.Windows.Forms.BindingSource();
            this.tblPlacementTypeTableAdapter = new askue3.DataSet1TableAdapters.tblPlacementTypeTableAdapter();
            this.tblOprosTypeTableAdapter = new askue3.DataSet1TableAdapters.tblOprosTypeTableAdapter();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::askue3.WaitForm1), true, true);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl();
            this.tblCounterValueTableAdapter = new askue3.DataSet1TableAdapters.tblCounterValueTableAdapter();
            this.tblMeteringPointTableAdapter = new askue3.DataSet1TableAdapters.tblMeteringPointTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMeteringPointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCounterValueBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOprosTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblProjectTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCounterTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMetPointTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFilialBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPlacementTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 5;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.Size = new System.Drawing.Size(714, 143);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Обновить";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Сохранить";
            this.barButtonItem2.Id = 3;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Excel";
            this.barButtonItem3.Id = 1;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "CSV";
            this.barButtonItem4.Id = 2;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Очистить столбец";
            this.barButtonItem5.Id = 4;
            this.barButtonItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.barButtonItem5.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.LargeImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Справочник";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem5);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Операции";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem3);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem4);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Экспорт";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 143);
            this.gridControl1.LookAndFeel.SkinName = "Office 2013 Dark Gray";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(714, 273);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // tblMeteringPointBindingSource
            // 
            this.tblMeteringPointBindingSource.DataMember = "tblMeteringPoint";
            this.tblMeteringPointBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colls1,
            this.colcaption_mp,
            this.colid_origbd,
            this.colls2,
            this.coltt_value,
            this.coltn_value,
            this.coldate_sbyt});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridView1.OptionsEditForm.EditFormColumnCount = 2;
            this.gridView1.OptionsEditForm.FormCaptionFormat = "Введите значения полей";
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 500;
            this.gridView1.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // colls1
            // 
            this.colls1.FieldName = "ls1";
            this.colls1.Name = "colls1";
            this.colls1.Visible = true;
            this.colls1.VisibleIndex = 0;
            // 
            // colcaption_mp
            // 
            this.colcaption_mp.FieldName = "caption_mp";
            this.colcaption_mp.Name = "colcaption_mp";
            this.colcaption_mp.Visible = true;
            this.colcaption_mp.VisibleIndex = 1;
            // 
            // colid_origbd
            // 
            this.colid_origbd.FieldName = "id_origbd";
            this.colid_origbd.Name = "colid_origbd";
            this.colid_origbd.Visible = true;
            this.colid_origbd.VisibleIndex = 2;
            // 
            // colls2
            // 
            this.colls2.FieldName = "ls2";
            this.colls2.Name = "colls2";
            this.colls2.Visible = true;
            this.colls2.VisibleIndex = 3;
            // 
            // coltt_value
            // 
            this.coltt_value.FieldName = "tt_value";
            this.coltt_value.Name = "coltt_value";
            this.coltt_value.Visible = true;
            this.coltt_value.VisibleIndex = 4;
            // 
            // coltn_value
            // 
            this.coltn_value.FieldName = "tn_value";
            this.coltn_value.Name = "coltn_value";
            this.coltn_value.Visible = true;
            this.coltn_value.VisibleIndex = 5;
            // 
            // coldate_sbyt
            // 
            this.coldate_sbyt.FieldName = "date_sbyt";
            this.coldate_sbyt.Name = "coldate_sbyt";
            this.coldate_sbyt.Visible = true;
            this.coldate_sbyt.VisibleIndex = 6;
            // 
            // tblCounterValueBindingSource
            // 
            this.tblCounterValueBindingSource.DataMember = "tblCounterValue";
            this.tblCounterValueBindingSource.DataSource = this.dataSet1;
            // 
            // tblOprosTypeBindingSource
            // 
            this.tblOprosTypeBindingSource.DataMember = "tblOprosType";
            this.tblOprosTypeBindingSource.DataSource = this.dataSet1;
            // 
            // tblProjectTypeBindingSource
            // 
            this.tblProjectTypeBindingSource.DataMember = "tblProjectType";
            this.tblProjectTypeBindingSource.DataSource = this.dataSet1;
            // 
            // tblCounterTypeBindingSource
            // 
            this.tblCounterTypeBindingSource.DataMember = "tblCounterType";
            this.tblCounterTypeBindingSource.DataSource = this.dataSet1;
            // 
            // tblCounterTypeTableAdapter
            // 
            this.tblCounterTypeTableAdapter.ClearBeforeFill = true;
            // 
            // tblProjectTypeTableAdapter
            // 
            this.tblProjectTypeTableAdapter.ClearBeforeFill = true;
            // 
            // tblMetPointTypeBindingSource
            // 
            this.tblMetPointTypeBindingSource.DataMember = "tblMetPointType";
            this.tblMetPointTypeBindingSource.DataSource = this.dataSet1;
            // 
            // tblMetPointTypeTableAdapter
            // 
            this.tblMetPointTypeTableAdapter.ClearBeforeFill = true;
            // 
            // tblFilialBindingSource
            // 
            this.tblFilialBindingSource.DataMember = "tblFilial";
            this.tblFilialBindingSource.DataSource = this.dataSet1;
            // 
            // tblFilialTableAdapter
            // 
            this.tblFilialTableAdapter.ClearBeforeFill = true;
            // 
            // tblPlacementTypeBindingSource
            // 
            this.tblPlacementTypeBindingSource.DataMember = "tblPlacementType";
            this.tblPlacementTypeBindingSource.DataSource = this.dataSet1;
            // 
            // tblPlacementTypeTableAdapter
            // 
            this.tblPlacementTypeTableAdapter.ClearBeforeFill = true;
            // 
            // tblOprosTypeTableAdapter
            // 
            this.tblOprosTypeTableAdapter.ClearBeforeFill = true;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // alertControl1
            // 
            this.alertControl1.LookAndFeel.SkinName = "Black";
            this.alertControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // tblCounterValueTableAdapter
            // 
            this.tblCounterValueTableAdapter.ClearBeforeFill = true;
            // 
            // tblMeteringPointTableAdapter
            // 
            this.tblMeteringPointTableAdapter.ClearBeforeFill = true;
            // 
            // FormGridDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 416);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "FormGridDataView";
            this.Ribbon = this.ribbonControl1;
            this.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Visible;
            this.Text = "Форма вывода информации";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormGridDataView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMeteringPointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCounterValueBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOprosTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblProjectTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCounterTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMetPointTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblFilialBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPlacementTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource tblCounterTypeBindingSource;
        private DataSet1TableAdapters.tblCounterTypeTableAdapter tblCounterTypeTableAdapter;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private System.Windows.Forms.BindingSource tblProjectTypeBindingSource;
        private DataSet1TableAdapters.tblProjectTypeTableAdapter tblProjectTypeTableAdapter;
        private System.Windows.Forms.BindingSource tblMetPointTypeBindingSource;
        private DataSet1TableAdapters.tblMetPointTypeTableAdapter tblMetPointTypeTableAdapter;
        private System.Windows.Forms.BindingSource tblFilialBindingSource;
        private DataSet1TableAdapters.tblFilialTableAdapter tblFilialTableAdapter;
        private System.Windows.Forms.BindingSource tblPlacementTypeBindingSource;
        private DataSet1TableAdapters.tblPlacementTypeTableAdapter tblPlacementTypeTableAdapter;
        private System.Windows.Forms.BindingSource tblOprosTypeBindingSource;
        private DataSet1TableAdapters.tblOprosTypeTableAdapter tblOprosTypeTableAdapter;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private System.Windows.Forms.BindingSource tblCounterValueBindingSource;
        private DataSet1TableAdapters.tblCounterValueTableAdapter tblCounterValueTableAdapter;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private System.Windows.Forms.BindingSource tblMeteringPointBindingSource;
        private DataSet1TableAdapters.tblMeteringPointTableAdapter tblMeteringPointTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colls1;
        private DevExpress.XtraGrid.Columns.GridColumn colcaption_mp;
        private DevExpress.XtraGrid.Columns.GridColumn colid_origbd;
        private DevExpress.XtraGrid.Columns.GridColumn colls2;
        private DevExpress.XtraGrid.Columns.GridColumn coltt_value;
        private DevExpress.XtraGrid.Columns.GridColumn coltn_value;
        private DevExpress.XtraGrid.Columns.GridColumn coldate_sbyt;
    }
}