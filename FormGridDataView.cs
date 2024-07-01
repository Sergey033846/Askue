using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

namespace askue3
{
    public partial class FormGridDataView : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        // имя таблицы, отображаемой в форме
        public string ds_tblName;        

        public FormGridDataView(string tblName)
        {
            InitializeComponent();

            ds_tblName = tblName;
            gridView1.Columns.Clear();
                        
            switch (tblName)
            {
                case "tblCounterType":
                    gridControl1.DataSource = tblCounterTypeBindingSource;
                    this.Text = "Типы приборов учета";
                    break;
                case "tblProjectType":                    
                    gridControl1.DataSource = tblProjectTypeBindingSource;                    
                    this.Text = "Проекты";
                    break;
                case "tblFilial":
                    gridControl1.DataSource = tblFilialBindingSource;
                    this.Text = "Филиалы";
                    break;
                case "tblOprosType":
                    gridControl1.DataSource = tblOprosTypeBindingSource;
                    this.Text = "Типы опроса";
                    break;
                case "tblPlacementType":
                    gridControl1.DataSource = tblPlacementTypeBindingSource;
                    this.Text = "Места установки";
                    break;
                case "tblMetPointType":
                    gridControl1.DataSource = tblMetPointTypeBindingSource;
                    this.Text = "Типы точек учета";
                    break;
                case "tblMeteringPoint":
                    gridControl1.DataSource = tblMeteringPointBindingSource;
                    this.Text = "Точки учета";
                    break;
                case "tblCounterValue":
                    gridControl1.DataSource = tblCounterValueBindingSource;
                    this.Text = "Показания приборов учета";
                    break;
            }            
        }

        // пользовательский метод загрузки (обновления) данных
        private void LoadData()
        {
            this.tblMeteringPointTableAdapter.Fill(this.dataSet1.tblMeteringPoint); // необходимо для отображения дочерних гридов "+"

            switch (ds_tblName)
            {
                case "tblCounterType":
                    // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.tblCounterType". При необходимости она может быть перемещена или удалена.
                    this.tblCounterTypeTableAdapter.Fill(this.dataSet1.tblCounterType);
                    break;
                case "tblProjectType":
                    this.tblProjectTypeTableAdapter.Fill(this.dataSet1.tblProjectType);
                    break;
                case "tblFilial":
                    this.tblFilialTableAdapter.Fill(this.dataSet1.tblFilial);
                    break;
                case "tblOprosType":
                    this.tblOprosTypeTableAdapter.Fill(this.dataSet1.tblOprosType);
                    break;
                case "tblPlacementType":
                    this.tblPlacementTypeTableAdapter.Fill(this.dataSet1.tblPlacementType);
                    break;
                case "tblMetPointType":
                    this.tblMetPointTypeTableAdapter.Fill(this.dataSet1.tblMetPointType);
                    break;
                /*case "tblMeteringPoint":
                    this.tblMeteringPointTableAdapter.Fill(this.dataSet1.tblMeteringPoint);
                    break;*/
                case "tblCounterValue":
                    this.tblCounterValueTableAdapter.Fill(this.dataSet1.tblCounterValue);
                    break;
            }
        }

        // пользовательский метод сохранения данных в БД
        private void UpdateData()
        {
            switch (ds_tblName)
            {
                case "tblCounterType":                    
                    this.tblCounterTypeTableAdapter.Update(this.dataSet1.tblCounterType);
                    break;
                case "tblProjectType":
                    this.tblProjectTypeTableAdapter.Update(this.dataSet1.tblProjectType);
                    break;
                case "tblFilial":
                    this.tblFilialTableAdapter.Update(this.dataSet1.tblFilial);
                    break;
                case "tblOprosType":
                    this.tblOprosTypeTableAdapter.Update(this.dataSet1.tblOprosType);
                    break;
                case "tblPlacementType":
                    this.tblPlacementTypeTableAdapter.Update(this.dataSet1.tblPlacementType);
                    break;
                case "tblMetPointType":
                    this.tblMetPointTypeTableAdapter.Update(this.dataSet1.tblMetPointType);
                    break;
                case "tblMeteringPoint":
                    this.tblMeteringPointTableAdapter.Update(this.dataSet1.tblMeteringPoint);
                    break;
                case "tblCounterValue":
                    this.tblCounterValueTableAdapter.Update(this.dataSet1.tblCounterValue);
                    break;
            }
        }

        private void FormGridDataView_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.tblMeteringPoint". При необходимости она может быть перемещена или удалена.
            //this.tblMeteringPointTableAdapter.Fill(this.dataSet1.tblMeteringPoint);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.tblMeteringPoint". При необходимости она может быть перемещена или удалена.
            //this.tblMeteringPointTableAdapter.Fill(this.dataSet1.tblMeteringPoint);
            LoadData();
        }

        // сохранить
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            try
            {
                UpdateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            splashScreenManager1.CloseWaitForm();

            AlertInfo info = new AlertInfo("Уведомление", "Данные успешно сохранены");
            alertControl1.Show(this, info);
        }

        // экспорт в excel
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.saveFileDialog1.Title = "Сохранить в Excel";
            this.saveFileDialog1.Filter = "xlsx (*.xlsx)|*.xlsx";
            this.saveFileDialog1.FileName = this.Text;
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.gridControl1.ExportToXlsx(this.saveFileDialog1.FileName);                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // экспорт в csv
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.saveFileDialog1.Title = "Сохранить в CSV";
            this.saveFileDialog1.Filter = "csv (*.csv)|*.csv";
            this.saveFileDialog1.FileName = this.Text;
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.gridControl1.ExportToCsv(this.saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // обновить
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            LoadData();
            splashScreenManager1.CloseWaitForm();            
        }

        // очистить столбец с присвоением NULL-значений
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColumnSelect form1 = new FormColumnSelect();

            form1.Text = "Очистка столбца таблицы (для точек учета)";

            form1.comboBoxEdit1.Properties.Items.Clear();
            form1.comboBoxEdit2.Properties.Items.Clear();
            form1.comboBoxEdit3.Properties.Items.Clear();

            // заполняем поля таблицы
            for (int i = 0; i < this.dataSet1.Tables[ds_tblName].Columns.Count; i++)
                form1.comboBoxEdit1.Properties.Items.Add(this.dataSet1.Tables[ds_tblName].Columns[i].Caption.ToString());

            // выбираем тип проекта
            tblProjectTypeTableAdapter.Fill(dataSet1.tblProjectType);
            for (int i = 0; i < this.dataSet1.tblProjectType.Rows.Count; i++)
                form1.comboBoxEdit2.Properties.Items.Add(this.dataSet1.tblProjectType.Rows[i]["id_caption_prj"].ToString());

            // выбираем филиал
            tblFilialTableAdapter.Fill(dataSet1.tblFilial);
            for (int i = 0; i < this.dataSet1.tblFilial.Rows.Count; i++)
                form1.comboBoxEdit3.Properties.Items.Add(this.dataSet1.tblFilial.Rows[i]["id_caption_filial"].ToString());            

            if (form1.ShowDialog(this) == DialogResult.OK)
            {
                string str0 = null;

                /*// пока без филиала
                string fexpr = "id_caption_prj = '" + form1.comboBoxEdit2.Text + "'";*/
                string fexpr = "id_caption_prj = '" + form1.comboBoxEdit2.Text + "'" + "and id_caption_filial = '" + form1.comboBoxEdit3.Text + "'";
                DataRow[] mpRows = dataSet1.Tables[ds_tblName].Select(fexpr);                
                
                for (int i = 0; i < mpRows.Length; i++)
                {                    
                    mpRows[i][form1.comboBoxEdit1.Text] = str0;
                }

                // попробовать сделать универсальный
                switch (ds_tblName)
                {
                    case "tblMeteringPoint":
                        this.tblMeteringPointTableAdapter.Update(this.dataSet1.tblMeteringPoint);
                        this.tblMeteringPointTableAdapter.Fill(this.dataSet1.tblMeteringPoint);
                        break;
                }
                                                                
                this.gridControl1.Refresh();
            }
            else
            {

            }

            form1.Dispose();
        }
    }
}