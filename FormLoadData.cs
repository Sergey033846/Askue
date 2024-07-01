using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.Spreadsheet;

namespace askue3
{
    public partial class FormLoadData : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string dbconnectionString;

        public FormLoadData()
        {
            dbconnectionString = "Data Source=SERVERPFB;Initial Catalog=db_askue2;User ID=sa;Password=SqL1310198";
            InitializeComponent();
        }

        // !!!!!!!!!!!!!!!!1 добавить в модуль
        public static DataTable MyFUNC_SelectDataFromSQLwoutConnection(DataTable dataset, SqlConnection connection, string queryString)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(queryString, connection);
            adapter.Fill(dataset);

            return dataset;
        }

        private void FormLoadData_Load(object sender, EventArgs e)
        {

        }

        // выбрать вид загрузки
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormLoadTypeSelect form1 = null;
            form1 = new FormLoadTypeSelect();
            form1.Text = "Выберите вид загрузки данных";

            // загрузка названий таблиц
            form1.comboBoxEdit1.Properties.Items.Clear();

            DataSet1 DataSetLoad = new DataSet1();
            DataSet1TableAdapters.tblMeteringPointTableAdapter tblMeteringPointTableAdapter = new DataSet1TableAdapters.tblMeteringPointTableAdapter();
            DataSet1TableAdapters.tblCounterValueTableAdapter tblCounterValueTableAdapter = new DataSet1TableAdapters.tblCounterValueTableAdapter();

            for (int i = 0; i < DataSetLoad.Tables.Count; i++)
                form1.comboBoxEdit1.Properties.Items.Add(DataSetLoad.Tables[i].TableName.ToString());

            if (form1.ShowDialog(this) == DialogResult.OK)
            {
                splashScreenManager1.ShowWaitForm();

                IWorkbook workbook = this.spreadsheetControl1.Document;
                Worksheet worksheet = workbook.Worksheets[0];

                // определяем количество колонок в листе
                int maxcol = 0;
                Cell spcell = worksheet[0, 0];
                string spcellv;

                while (spcell.Value.ToString() != "")
                {
                    maxcol++;
                    spcell = worksheet[0, maxcol];
                }

                // определяем количество строк в листе (считаем макс по 2 столбцам)
                int maxrow = 0;
                int maxrow1 = 0;
                int maxrow2 = 0;
                spcell = worksheet[0, 0];
                while (spcell.Value.ToString() != "")
                {
                    maxrow1++;
                    spcell = worksheet[maxrow1, 0];
                }

                spcell = worksheet[0, 1];
                while (spcell.Value.ToString() != "")
                {
                    maxrow2++;
                    spcell = worksheet[maxrow2, 1];
                }
                if (maxrow1 < maxrow2) maxrow = maxrow2;
                else maxrow = maxrow1;
                //-------------------------------

                workbook.History.IsEnabled = false;
                this.spreadsheetControl1.Hide();
                this.spreadsheetControl1.BeginUpdate();

                string[] svalues = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

                // загружаем данные в зависимости от вида загрузки
                switch (form1.listBoxLoadType.SelectedValue.ToString())
                {
                    // универсальная полная загрузка данных в выбранную ПУСТУЮ таблицу (первая строка - названия полей в таблице БД)
                    // !!!порядок столбцов в файле должен соответствовать порядку в БД - птом исправить!!!!
                    case "Полная загрузка таблицы":
                        for (int i = 0; i < maxrow - 1; i++)
                        {
                            /*for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }*/

                            tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                            // загружаем в выбранную таблицу
                            switch (form1.comboBoxEdit1.SelectedItem.ToString())
                            {
                                case "tblMeteringPoint":
                                    spcellv = worksheet[i + 1, 0].Value.ToString(); string ls1 = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 1].Value.ToString(); string caption_mp = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 2].Value.ToString(); string id_origbd = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 3].Value.ToString(); string ls2 = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 4].Value.ToString(); double? tt_value = null; if (spcellv != "") tt_value = Convert.ToDouble(spcellv);
                                    spcellv = worksheet[i + 1, 5].Value.ToString(); double? tn_value = null; if (spcellv != "") tn_value = Convert.ToDouble(spcellv);

                                    spcellv = worksheet[i + 1, 6].Value.ToString(); DateTime? date_sbyt = null; if (spcellv != "") date_sbyt = Convert.ToDateTime(spcellv);
                                    spcellv = worksheet[i + 1, 7].Value.ToString(); string geo1coord = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 8].Value.ToString(); string geo2coord = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 9].Value.ToString(); DateTime? date_install = null; if (spcellv != "") date_install = Convert.ToDateTime(spcellv);
                                    spcellv = worksheet[i + 1, 10].Value.ToString(); string comment_mp = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 11].Value.ToString(); string id_phonenumber = (spcellv == "") ? null : spcellv;

                                    spcellv = worksheet[i + 1, 12].Value.ToString(); string id_modem_type = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 13].Value.ToString(); string id_modem_serial = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 14].Value.ToString(); string id_caption_prj = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 15].Value.ToString(); string id_caption_mptype = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 16].Value.ToString(); string id_caption_placement = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 17].Value.ToString(); string id_caption_fider = (spcellv == "") ? null : spcellv;

                                    spcellv = worksheet[i + 1, 18].Value.ToString(); string id_caption_filial = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 19].Value.ToString(); string id_caption_res = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 20].Value.ToString(); string id_caption_pc = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 21].Value.ToString(); string id_caption_tp = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 22].Value.ToString(); string id_caption_raion = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 23].Value.ToString(); string id_caption_oblast = (spcellv == "") ? null : spcellv;

                                    spcellv = worksheet[i + 1, 24].Value.ToString(); string id_caption_city = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 25].Value.ToString(); string id_caption_street = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 26].Value.ToString(); string id_caption_house = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 27].Value.ToString(); string flat = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 28].Value.ToString(); DateTime create_rec_date = Convert.ToDateTime(spcellv);
                                    spcellv = worksheet[i + 1, 29].Value.ToString(); string id_caption_opros_type = (spcellv == "") ? null : spcellv;

                                    spcellv = worksheet[i + 1, 30].Value.ToString(); string caption_korpus = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 31].Value.ToString(); string id_balance_type = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 32].Value.ToString(); string id_caption_street_type = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 33].Value.ToString(); string id_caption_city_type = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 34].Value.ToString(); string id_counter_serial = (spcellv == "") ? null : spcellv;
                                    spcellv = worksheet[i + 1, 35].Value.ToString(); string id_counter_type = (spcellv == "") ? null : spcellv;

                                    // проверяем на существование строки с ключом (номером ПУ)
                                    DataSet1.tblMeteringPointRow mprow = DataSetLoad.tblMeteringPoint.FindByid_counter_serial(id_counter_serial);
                                    if (mprow == null)
                                    {
                                        tblMeteringPointTableAdapter.Insert(ls1, caption_mp, id_origbd, ls2, tt_value, tn_value, date_sbyt, geo1coord, geo2coord, date_install, comment_mp, id_phonenumber, id_modem_type, id_modem_serial, id_caption_prj,
                                            id_caption_mptype, id_caption_placement, id_caption_fider, id_caption_filial, id_caption_res, id_caption_pc, id_caption_tp, id_caption_raion, id_caption_oblast, id_caption_city,
                                            id_caption_street, id_caption_house, flat, create_rec_date, id_caption_opros_type, caption_korpus, id_balance_type, id_caption_street_type, id_caption_city_type, id_counter_serial, id_counter_type, null);
                                        tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                    }
                                    break;
                            } // switch (form1.comboBoxEdit1.SelectedItem.ToString())
                        } // for (int i = 0; i < maxrow-1; i++)
                        break; // case "Полная загрузка таблицы":

                    case "приборы учета МКД РиМ СибЭл":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if (DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[2]) == null)
                            {
                                DateTime? date_null = null;
                                double? double_null = null;
                                string str0 = null;

                                // загрузка в mp                        
                                double? tt_value;
                                if (svalues[4] == "") tt_value = null;
                                else tt_value = Convert.ToDouble(svalues[4]);

                                string filial_value;
                                if (svalues[5] == "") filial_value = str0;
                                else filial_value = svalues[5];

                                tblMeteringPointTableAdapter.Insert(str0, svalues[1], str0, str0, tt_value, double_null, date_null, str0, str0, date_null, svalues[0], str0, str0, str0, "Ангарск МКД 2300", "МКД", str0, str0, svalues[5], str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "GSM CSD", str0, str0, str0, str0, svalues[2], svalues[3], str0);
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }
                            else
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета МКД РиМ СибЭл":

                    case "приборы учета МКД РиМ хозспособ":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if (DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[2]) == null)                            
                            {
                                DateTime? date_null = null;
                                double? double_null = null;
                                string str0 = null;
                                
                                // загрузка в mp                        
                                double? tt_value;
                                if (svalues[4] == "") tt_value = null;
                                else tt_value = Convert.ToDouble(svalues[4]);

                                string filial_value;
                                if (svalues[5] == "") filial_value = str0;
                                else filial_value = svalues[5];

                                tblMeteringPointTableAdapter.Insert(str0, svalues[1], str0, str0, tt_value, double_null, date_null, str0, str0, date_null, svalues[0], str0, str0, str0, "МКД РиМ хозспособ", "МКД", str0, str0, svalues[5], str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "GSM CSD", str0, str0, str0, str0, svalues[2], svalues[3],str0);
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }
                            else
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }
                        
                        break; // case "приборы учета МКД Рим хозспособ":

                    //------------------------------------------------------

                    case "приборы учета МКД Матрица новая":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if (DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[2]) == null)
                            {
                                DateTime? date_null = null;
                                double? double_null = null;
                                string str0 = null;

                                // загрузка в mp                        
                                double? tt_value = null;
                                if (svalues[3] == "") tt_value = null;
                                else tt_value = Convert.ToDouble(svalues[3]);

                                string filial_value = str0;
                                if (svalues[4] == "") filial_value = str0;
                                else filial_value = svalues[4];

                                string counter_type = svalues[7];
                                if (String.Equals(counter_type, "NULL") || String.IsNullOrWhiteSpace(counter_type)) counter_type = str0;

                                tblMeteringPointTableAdapter.Insert(str0, svalues[1], str0, svalues[6], tt_value, double_null, date_null, str0, str0, date_null, svalues[0], str0, str0, str0, "МКД Матрица новая", "МКД", svalues[5], str0, str0, str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "GSM GPRS", str0, str0, str0, str0, svalues[2], counter_type, str0);
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }
                            else
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета МКД Матрица новая":

                    //------------------------------------------------------

                    case "приборы учета Матрица Тайшет":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < 23; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if (DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[13]) == null)
                            {
                                DateTime? date_null = null;
                                double? double_null = null;
                                string str0 = null;

                                // загрузка в mp                        
                                double? tt_value = null;
                                if (svalues[15] == "") tt_value = null;
                                else tt_value = Convert.ToDouble(svalues[15]);

                                string filial_value = str0;
                                /*if (svalues[5] == "") filial_value = str0;
                                else filial_value = svalues[5];*/

                                string counter_type = svalues[14];
                                if (String.Equals(counter_type, "NULL") || String.IsNullOrWhiteSpace(counter_type)) counter_type = str0;

                                tblMeteringPointTableAdapter.Insert(str0, svalues[9], str0, svalues[11], tt_value, double_null, date_null, str0, str0, date_null, svalues[1], str0, str0, str0, "потребители", svalues[12], str0, str0, "Усть-Ордынские ЭС", str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "GSM CSD", str0, str0, str0, str0, svalues[13], counter_type, str0);
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }
                            else
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета Матрица Тайшет":

                    //------------------------------------------------------

                    case "приборы учета подрядчики":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }
                            
                            if (DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[2]) == null)
                            {
                                DateTime? date_null = null;
                                double? double_null = null;
                                string str0 = null;

                                // загрузка в mp                        
                                double? tt_value;
                                if (svalues[4] == "") tt_value = null;
                                else tt_value = Convert.ToDouble(svalues[4]);

                                string filial_value;
                                if (svalues[5] == "") filial_value = str0;
                                else filial_value = svalues[5];
                                                                
                                //tblMeteringPointTableAdapter.Insert(str0, svalues[1], str0, svalues[7], tt_value, double_null, date_null, str0, str0, date_null, svalues[0], str0, str0, str0, "потребители", svalues[8], str0, str0, svalues[5], str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "GSM CSD", str0, str0, str0, str0, svalues[2], svalues[3], svalues[6]);
                                tblMeteringPointTableAdapter.Insert(str0, svalues[1], str0, str0, tt_value, double_null, date_null, str0, str0, date_null, svalues[0], str0, str0, str0, str0, str0, str0, str0, svalues[5], str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "GSM CSD", str0, str0, str0, str0, svalues[2], svalues[3], str0);
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }
                            else
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета подрядчики":

                    //------------------------------------------------------

                    case "приборы учета Тулун ЧС РиМ":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if (DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[2]) == null)
                            {
                                DateTime? date_null = null;
                                double? double_null = null;
                                string str0 = null;

                                // загрузка в mp                        
                                double? tt_value;
                                if (svalues[4] == "") tt_value = null;
                                else tt_value = Convert.ToDouble(svalues[4]);

                                /*string filial_value;
                                if (svalues[5] == "") filial_value = str0;
                                else filial_value = svalues[5];*/

                                string ls2_value = str0;
                                /*if (svalues[5] == "") ls2_value = str0;
                                else ls2_value = svalues[5];*/

                                tblMeteringPointTableAdapter.Insert(str0, svalues[1], str0, str0, tt_value, double_null, date_null, str0, str0, date_null, svalues[0], str0, str0, str0, "Тулун ЧС РиМ", "частный сектор", "опора", str0, "Нижнеудинские ЭС", str0, str0, str0, str0, str0, str0, str0, str0, str0, DateTime.Now, "МКС CSD", str0, str0, str0, str0, svalues[2], svalues[3],str0);                                
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);
                                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }
                            else
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета Тулун ЧС РиМ":

                    //------------------------------------------------------

                    case "приборы учета удаление":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                        tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            // удаляем показания ПУ
                            string fexpr = "id_counter_serial = '" + svalues[0] + "'";
                            DataRow[] cvRows = DataSetLoad.tblCounterValue.Select(fexpr);                            
                            for (int k = 0; k < cvRows.Count(); k++)
                            {
                                cvRows[k].Delete();
                                /*DateTime valuedate = Convert.ToDateTime(cvRows[k]["valuedate"].ToString());
                                double counter_value = Convert.ToDouble(cvRows[k]["counter_value"].ToString());
                                string valuetime = cvRows[k]["valuetime"].ToString();
                                string id_counter_serial = cvRows[k]["id_counter_serial"].ToString();
                                tblCounterValueTableAdapter.Delete(valuedate,counter_value,valuetime,id_counter_serial);*/
                                tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);                                
                            }

                            // удаляем ПУ                                  
                            DataRow mpRow = DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[0]);
                            if (mpRow != null)
                            {
                                mpRow.Delete();
                                tblMeteringPointTableAdapter.Update(DataSetLoad.tblMeteringPoint);

                                worksheet.Rows[i].FillColor = Color.Green; // переделать в будущем!!!
                            }

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        } // for (int i = 0; i < maxrow; i++)

                        break; // case "приборы учета удаление":

                    //----------------------------------------------------------------------

                    case "unit_id РиМ хз":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            // ищем по unit_id тип и номер счетчика
                            string fexpr = "id_counter_serial = '" + svalues[1] + "' AND (id_caption_prj = 'МКД РиМ хозспособ' OR id_caption_prj = 'Тулун ЧС РиМ' OR id_caption_prj = 'подрядчики')";
                            DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);

                            if (mpRows.Length == 1) // если найдена только одна точка с таким id_counter_serial
                            {
                                mpRows[0]["id_origbd"] = svalues[0];
                                tblMeteringPointTableAdapter.Update(mpRows[0]);
                                worksheet[i, 0].FillColor = Color.Green;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "unit_id РиМ хз":

                    case "unit_id РиМ СибЭл":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            // ищем по unit_id тип и номер счетчика
                            string fexpr = "id_counter_serial = '" + svalues[1] + "' AND (id_caption_prj = 'Ангарск МКД 2300' OR id_caption_prj = 'Ангарск ТехУчет')";
                            DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);

                            if (mpRows.Length == 1) // если найдена только одна точка с таким id_counter_serial
                            {
                                mpRows[0]["id_origbd"] = svalues[0];
                                tblMeteringPointTableAdapter.Update(mpRows[0]);
                                worksheet[i, 0].FillColor = Color.Green;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "unit_id РиМ СибЭл":

                    case "unit_id РиМ МКД ЧС хз показания":
                        //tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);

                        SqlConnection SQLconnection = new SqlConnection(dbconnectionString);
                        SQLconnection.Open();
                        
                        //string[] svalues = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if ((svalues[1] != "DATE_") && (svalues[3] != "POKAZ"))
                            {
                                DateTime vdate = Convert.ToDateTime(svalues[1]);
                                double counter_value = Convert.ToDouble(svalues[3]);
                                string vtime = " " + svalues[2]; // Добавляем пробел - потом удалить

                                // ищем по unit_id и типу проекта тип и номер счетчика                        
                                string fexpr = "id_origbd = '" + svalues[0] + "' AND (id_caption_prj = 'МКД РиМ хозспособ' OR id_caption_prj = 'Тулун ЧС РиМ' OR id_caption_prj = 'подрядчики' OR id_caption_prj = 'потребители')";
                                //DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);
                                string queryStringmp =
                                        "SELECT id_counter_serial " +
                                        "FROM [db_askue2].[dbo].[tblMeteringPoint] " +
                                        "WHERE " + fexpr;
                                DataTable tableMP = new DataTable();
                                MyFUNC_SelectDataFromSQLwoutConnection(tableMP, SQLconnection, queryStringmp);

                                //if (mpRows.Length == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                if (tableMP.Rows.Count == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                {
                                    //string counter_type = mpRows[0]["id_counter_type"].ToString();
                                    
                                    //string counter_serial = mpRows[0]["id_counter_serial"].ToString();
                                    string counter_serial = tableMP.Rows[0]["id_counter_serial"].ToString();

                                    //-------------------------------------

                                    string fexpr3 = "id_counter_serial = '" + counter_serial + "' and valuedate = '" + vdate + "' and valuetime = '" + vtime + "'";
                                    string queryStringcv =
                                        "SELECT valuedate, valuetime, counter_value " +
                                        "FROM [db_askue2].[dbo].[tblCounterValue] " +
                                        "WHERE " + fexpr3; 
                                    DataTable tableCV = new DataTable();
                                    MyFUNC_SelectDataFromSQLwoutConnection(tableCV, SQLconnection, queryStringcv);

                                    //if (DataSetLoad.tblCounterValue.FindByvaluedatevaluetimeid_counter_serial(vdate, vtime, counter_serial) == null)  
                                    if (tableCV.Rows.Count == 0)                                           
                                    {                                        
                                        tblCounterValueTableAdapter.Insert(vdate, counter_value, vtime, counter_serial);
                                        //tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);
                                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue); 

                                        worksheet[i, 0].FillColor = Color.Green;  
                                    }
                                    else
                                    {
                                        worksheet[i, 0].FillColor = Color.Red; 
                                    }

                                    tableCV.Dispose();
                                }

                                tableMP.Dispose();
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");

                            //this.Text = "Загрузка данных (строка " + (i + 1).ToString() + " из " + maxrow.ToString() + ")";
                        }  // for (int i = 0; i < maxrow; i++)

                        SQLconnection.Close();

                        tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);

                        break; // case "unit_id РиМ МКД ЧС хз показания"

                    case "unit_id РиМ МКД ЧС хз показания (КЭС)":
                        //tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);

                        SQLconnection = new SqlConnection(dbconnectionString);
                        SQLconnection.Open();

                        //string[] svalues = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if ((svalues[1] != "DATE_") && (svalues[3] != "POKAZ"))
                            {
                                DateTime vdate = Convert.ToDateTime(svalues[1]);
                                double counter_value = Convert.ToDouble(svalues[3]);
                                string vtime = " " + svalues[2]; // Добавляем пробел - потом удалить

                                // ищем по unit_id и типу проекта тип и номер счетчика                        
                                string fexpr = "id_origbd = '" + svalues[0] + "' AND (id_caption_prj = 'МКД РиМ хозспособ (КЭС)')";
                                //DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);
                                string queryStringmp =
                                        "SELECT id_counter_serial " +
                                        "FROM [db_askue2].[dbo].[tblMeteringPoint] " +
                                        "WHERE " + fexpr;
                                DataTable tableMP = new DataTable();
                                MyFUNC_SelectDataFromSQLwoutConnection(tableMP, SQLconnection, queryStringmp);

                                //if (mpRows.Length == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                if (tableMP.Rows.Count == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                {
                                    //string counter_type = mpRows[0]["id_counter_type"].ToString();

                                    //string counter_serial = mpRows[0]["id_counter_serial"].ToString();
                                    string counter_serial = tableMP.Rows[0]["id_counter_serial"].ToString();

                                    //-------------------------------------

                                    string fexpr3 = "id_counter_serial = '" + counter_serial + "' and valuedate = '" + vdate + "' and valuetime = '" + vtime + "'";
                                    string queryStringcv =
                                        "SELECT valuedate, valuetime, counter_value " +
                                        "FROM [db_askue2].[dbo].[tblCounterValue] " +
                                        "WHERE " + fexpr3;
                                    DataTable tableCV = new DataTable();
                                    MyFUNC_SelectDataFromSQLwoutConnection(tableCV, SQLconnection, queryStringcv);

                                    //if (DataSetLoad.tblCounterValue.FindByvaluedatevaluetimeid_counter_serial(vdate, vtime, counter_serial) == null)  
                                    if (tableCV.Rows.Count == 0)
                                    {
                                        tblCounterValueTableAdapter.Insert(vdate, counter_value, vtime, counter_serial);
                                        //tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);
                                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue); 

                                        worksheet[i, 0].FillColor = Color.Green;
                                    }
                                    else
                                    {
                                        worksheet[i, 0].FillColor = Color.Red;
                                    }

                                    tableCV.Dispose();
                                }

                                tableMP.Dispose();
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");

                            //this.Text = "Загрузка данных (строка " + (i + 1).ToString() + " из " + maxrow.ToString() + ")";
                        }  // for (int i = 0; i < maxrow; i++)

                        SQLconnection.Close();

                        tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);

                        break; // case "unit_id РиМ МКД ЧС хз показания (КЭС)"

                    case "показания МКД Матрица новая":
                        //tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);

                        SQLconnection = new SqlConnection(dbconnectionString);
                        SQLconnection.Open();

                        //string[] svalues = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

                        for (int i = 1; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            /*if ((svalues[1] != "DATE_") && (svalues[3] != "POKAZ")) // !!!
                            {*/
                                DateTime fullDateTime = Convert.ToDateTime(svalues[1]);
                                DateTime vdate = fullDateTime.Date;
                            string counter_value_str = svalues[3];
                            
                                string vtime = " " + fullDateTime.ToShortTimeString(); // Добавляем пробел - потом удалить

                            // ищем по unit_id и типу проекта тип и номер счетчика                        
                            //string fexpr = "id_counter_serial = '" + svalues[0] + "' AND id_caption_prj = 'МКД Матрица новая'";
                            string fexpr = "id_counter_serial = '" + svalues[0] + "'";

                            //DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);
                            string queryStringmp =
                                        "SELECT id_counter_serial " +
                                        "FROM [db_askue2].[dbo].[tblMeteringPoint] " +
                                        "WHERE " + fexpr;
                                DataTable tableMP = new DataTable();
                                MyFUNC_SelectDataFromSQLwoutConnection(tableMP, SQLconnection, queryStringmp);

                                //if (mpRows.Length == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                if (tableMP.Rows.Count == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                {
                                    //string counter_type = mpRows[0]["id_counter_type"].ToString();

                                    //string counter_serial = mpRows[0]["id_counter_serial"].ToString();
                                    string counter_serial = tableMP.Rows[0]["id_counter_serial"].ToString();

                                    //-------------------------------------

                                    string fexpr3 = "id_counter_serial = '" + counter_serial + "' and valuedate = '" + vdate + "' and valuetime = '" + vtime + "'";
                                    string queryStringcv =
                                        "SELECT valuedate, valuetime, counter_value " +
                                        "FROM [db_askue2].[dbo].[tblCounterValue] " +
                                        "WHERE " + fexpr3;
                                    DataTable tableCV = new DataTable();
                                    MyFUNC_SelectDataFromSQLwoutConnection(tableCV, SQLconnection, queryStringcv);

                                    //if (DataSetLoad.tblCounterValue.FindByvaluedatevaluetimeid_counter_serial(vdate, vtime, counter_serial) == null)  
                                    if (tableCV.Rows.Count == 0 && !String.IsNullOrWhiteSpace(counter_value_str))
                                    {
                                        //double counter_value = Convert.ToDouble(svalues[3]);

                                        tblCounterValueTableAdapter.Insert(vdate, Convert.ToDouble(counter_value_str), vtime, counter_serial);
                                        //tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);
                                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue); 

                                        worksheet[i, 0].FillColor = Color.Green;
                                    }
                                    else
                                    {
                                        worksheet[i, 0].FillColor = Color.Red;
                                    }

                                    tableCV.Dispose();
                                }

                                tableMP.Dispose();
                            //}

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");

                            //this.Text = "Загрузка данных (строка " + (i + 1).ToString() + " из " + maxrow.ToString() + ")";
                        }  // for (int i = 0; i < maxrow; i++)

                        SQLconnection.Close();

                        tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);

                        break; // case "показания МКД Матрица новая"

                    case "unit_id СибЭл РиМ показания":
                        //tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                        //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);

                        SQLconnection = new SqlConnection(dbconnectionString);
                        SQLconnection.Open();

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            if ((svalues[1] != "DATE_") && (svalues[3] != "POKAZ"))
                            {
                                DateTime vdate = Convert.ToDateTime(svalues[1]);
                                double counter_value = Convert.ToDouble(svalues[3]);
                                string vtime = " " + svalues[2]; // Добавляем пробел - потом удалить

                                // ищем по unit_id и типу проекта тип и номер счетчика                                                        
                                string fexpr = "id_origbd = '" + svalues[0] + "' AND (id_caption_prj = 'Ангарск МКД 2300' OR id_caption_prj = 'Ангарск ТехУчет')";
                                //DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);
                                string queryStringmp =
                                        "SELECT id_counter_serial " +
                                        "FROM [db_askue2].[dbo].[tblMeteringPoint] " +
                                        "WHERE " + fexpr;
                                DataTable tableMP = new DataTable();
                                MyFUNC_SelectDataFromSQLwoutConnection(tableMP, SQLconnection, queryStringmp);

                                if (tableMP.Rows.Count == 1) // если найдена только одна точка с таким unit_id и типом проекта
                                {                                    
                                    string counter_serial = tableMP.Rows[0]["id_counter_serial"].ToString();

                                    string fexpr3 = "id_counter_serial = '" + counter_serial + "' and valuedate = '" + vdate + "' and valuetime = '" + vtime + "'";
                                    string queryStringcv =
                                        "SELECT valuedate, valuetime, counter_value " +
                                        "FROM [db_askue2].[dbo].[tblCounterValue] " +
                                        "WHERE " + fexpr3;
                                    DataTable tableCV = new DataTable();
                                    MyFUNC_SelectDataFromSQLwoutConnection(tableCV, SQLconnection, queryStringcv);

                                    //if (DataSetLoad.tblCounterValue.FindByvaluedatevaluetimeid_counter_serial(vdate, vtime, counter_serial) == null)
                                    if (tableCV.Rows.Count == 0)
                                    {
                                        tblCounterValueTableAdapter.Insert(vdate, counter_value, vtime, counter_serial);
                                        //tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);
                                        //tblCounterValueTableAdapter.Fill(db_askueDataSetCValue.tblCounterValue); 

                                        worksheet[i, 0].FillColor = Color.Green;  
                                    }
                                    else
                                    {
                                        worksheet[i, 0].FillColor = Color.Red; 
                                    }

                                    tableCV.Dispose();
                                }

                                tableMP.Dispose();
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        } // for (int i = 0; i < maxrow; i++)

                        SQLconnection.Close();

                        tblCounterValueTableAdapter.Update(DataSetLoad.tblCounterValue);

                        break; // case "unit_id СибЭл РиМ показания":

                    case "лицевой счет 1С":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            string counter_serial = svalues[0];                            
                            string fexpr3 = "id_counter_serial = '" + counter_serial + "'";

                            DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr3, "");

                            if (mpRows.Length != 1)
                            //DataRow mpRow = db_askueDataSetMetPoint.tblMeteringPoint.FindByid_counter_serialid_counter_type(counter_serial, counter_type);
                            //if (mpRow == null)
                            {
                                //worksheet.Rows[i].FillColor = Color.Red;
                                worksheet[i, 0].FillColor = Color.Red;
                            }
                            else
                            {
                                mpRows[0]["ls2"] = svalues[1];
                                tblMeteringPointTableAdapter.Update(mpRows[0]);
                                //worksheet.Rows[i].FillColor = Color.Green;  // метод тупит!!!
                                worksheet[i,0].FillColor = Color.Green;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "лицевой счет 1С":

                    case "приборы учета РиМ проверка НСИ":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            DataRow mpRow = DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[3]);
                            if (mpRow == null)
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }
                            else
                            {
                                // сравнение параметров точки
                                if (mpRow["comment_mp"].ToString() == svalues[0]) worksheet[i, 0].FillColor = Color.Green;
                                else worksheet[i, 0].FillColor = Color.Red;

                                if (mpRow["caption_mp"].ToString() == svalues[1]) worksheet[i, 1].FillColor = Color.Green;
                                else worksheet[i, 1].FillColor = Color.Red;

                                if (mpRow["tt_value"].ToString() == svalues[2]) worksheet[i, 2].FillColor = Color.Green;
                                else worksheet[i, 2].FillColor = Color.Red;

                                if (mpRow["id_counter_type"].ToString() == svalues[4]) worksheet[i, 4].FillColor = Color.Green;
                                else worksheet[i, 4].FillColor = Color.Red;

                                worksheet[i, 6].SetValue(mpRow["comment_mp"].ToString());
                                worksheet[i, 7].SetValue(mpRow["caption_mp"].ToString());
                                worksheet[i, 8].SetValue(mpRow["tt_value"].ToString());
                                worksheet[i, 9].SetValue(mpRow["id_counter_type"].ToString());                                
                            }

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета МКД ЧС Рим хозспособ проверка":

                    //----------------------------------------------------------------------
                    case "приборы учета РиМ перезапись НСИ":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            DataRow mpRow = DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[3]);
                            if (mpRow == null)
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }
                            else
                            {                                
                                mpRow["comment_mp"] = svalues[0];                                
                                mpRow["caption_mp"] = svalues[1];                                
                                mpRow["tt_value"] = svalues[2];                                
                                mpRow["id_counter_type"] = svalues[4];
                                tblMeteringPointTableAdapter.Update(mpRow);
                                worksheet.Rows[i].FillColor = Color.Green;
                            }

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета РиМ перезапись НСИ":

                    //----------------------------------------------------------------------

                    case "приборы учета Матрица перезапись НСИ":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            DataRow mpRow = DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[2]);
                            if (mpRow == null)
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }
                            else
                            {
                                mpRow["comment_mp"] = svalues[0];
                                mpRow["caption_mp"] = svalues[1];
                                mpRow["tt_value"] = svalues[3];
                                //mpRow["id_counter_type"] = svalues[4];
                                mpRow["id_caption_filial"] = svalues[4];

                                string id_caption_placement = String.IsNullOrWhiteSpace(svalues[5]) ? null: svalues[5];
                                mpRow["id_caption_placement"] = id_caption_placement;

                                string ls2 = String.IsNullOrWhiteSpace(svalues[6]) ? null : svalues[6];
                                mpRow["ls2"] = ls2;

                                tblMeteringPointTableAdapter.Update(mpRow);
                                worksheet.Rows[i].FillColor = Color.Green;
                            }

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "приборы учета Матрица перезапись НСИ":

                    //----------------------------------------------------------------------

                    case "номерПУ ТП":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            // ищем по unit_id тип и номер счетчика
                            string fexpr = "id_counter_serial = '" + svalues[0]+"'";
                            DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);

                            if (mpRows.Length == 1) // если найдена только одна точка с таким id_counter_serial
                            {
                                mpRows[0]["caption_mp2"] = svalues[1];
                                tblMeteringPointTableAdapter.Update(mpRows[0]);
                                worksheet[i, 0].FillColor = Color.Green;
                            }

                            splashScreenManager1.SetWaitFormDescription("Загрузка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        break; // case "номерПУ ТП":

                    case "координаты по адресу":
                        tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

                        spreadsheetControl1.Hide();

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            DataRow mpRow = DataSetLoad.tblMeteringPoint.FindByid_counter_serial(svalues[3]);
                            if (mpRow == null)
                            {
                                worksheet.Rows[i].FillColor = Color.Red;
                            }
                            else
                            {
                                // адресная строка запроса для геокодирования
                                string addr_find = svalues[0]+","+ svalues[1]+","+ svalues[2];
                                
                                //Запрос к API геокодирования Google ----------------------------------------------------------
                                string url = string.Format(
                                    "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true_or_false&language=ru",
                                    Uri.EscapeDataString(addr_find));

                                //Выполняем запрос к универсальному коду ресурса (URI).
                                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                                //Получаем ответ от интернет-ресурса.
                                System.Net.WebResponse response = request.GetResponse();

                                //Экземпляр класса System.IO.Stream
                                //для чтения данных из интернет-ресурса.
                                System.IO.Stream dataStream = response.GetResponseStream();

                                //Инициализируем новый экземпляр класса
                                //System.IO.StreamReader для указанного потока.
                                System.IO.StreamReader sreader = new System.IO.StreamReader(dataStream);

                                //Считывает поток от текущего положения до конца.           
                                string responsereader = sreader.ReadToEnd();

                                //Закрываем поток ответа.
                                response.Close();

                                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                                xmldoc.LoadXml(responsereader);

                                //Переменные широты и долготы.
                                double latitude = 0.0;
                                double longitude = 0.0;

                                if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                                {

                                    //Получение широты и долготы.
                                    System.Xml.XmlNodeList nodes = xmldoc.SelectNodes("//location");
                                                                        
                                    //Получаем широту и долготу.
                                    foreach (System.Xml.XmlNode node in nodes)
                                    {
                                        latitude = System.Xml.XmlConvert.ToDouble(node.SelectSingleNode("lat").InnerText.ToString());
                                        longitude = System.Xml.XmlConvert.ToDouble(node.SelectSingleNode("lng").InnerText.ToString());
                                    }
                                }

                                //----------------------------------

                                mpRow["geo1coord"] = latitude.ToString();
                                mpRow["geo2coord"] = longitude.ToString();
                                tblMeteringPointTableAdapter.Update(mpRow);

                                worksheet[i, 4].SetValue(latitude.ToString());
                                worksheet[i, 5].SetValue(longitude.ToString());                                                                
                            }

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        spreadsheetControl1.Show();

                        break; // case "координаты по адресу":

                    case "координаты мкс38":
                        DataSet1TableAdapters.tblCoordTestTableAdapter tblCoordTestTableAdapter = new DataSet1TableAdapters.tblCoordTestTableAdapter();
                        tblCoordTestTableAdapter.Fill(DataSetLoad.tblCoordTest);

                        spreadsheetControl1.Hide();

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < maxcol; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }
                            
                            //----------------------------------
                            tblCoordTestTableAdapter.Insert("Сухой ручей п.", svalues[7], svalues[4], svalues[5], svalues[10], svalues[11]);
                                tblCoordTestTableAdapter.Update(DataSetLoad.tblCoordTest);
                      

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        spreadsheetControl1.Show();

                        break; // case "координаты мкс38":

                    case "координаты свойства":
                        DataSet1TableAdapters.tblMapObjTableAdapter tblMapObjTableAdapter = new DataSet1TableAdapters.tblMapObjTableAdapter();
                        DataSet1TableAdapters.tblMapObjCoordsTableAdapter tblMapObjCoordsTableAdapter = new DataSet1TableAdapters.tblMapObjCoordsTableAdapter();
//                        tblMapObjTableAdapter.Fill(DataSetLoad.tblMapObj);
                        tblMapObjCoordsTableAdapter.Fill(DataSetLoad.tblMapObjCoords);

                        spreadsheetControl1.Hide();

                        for (int i = 0; i < maxrow; i++)
                        {
                            for (int j = 0; j < 20; j++)
                            {
                                spcell = worksheet[i, j];
                                svalues[j] = spcell.Value.ToString();
                            }

                            //----------------------------------
                            tblMapObjTableAdapter.Fill(DataSetLoad.tblMapObj);
                            DataRow objrow = DataSetLoad.tblMapObj.FindByidmapobj(svalues[1]);

                            if (objrow == null)
                            {
                                tblMapObjTableAdapter.Insert(svalues[1], svalues[8], svalues[9], svalues[10], svalues[11], svalues[12], svalues[13], svalues[14], svalues[15], svalues[16]);
                                tblMapObjTableAdapter.Update(DataSetLoad.tblMapObj);                                

                                tblMapObjCoordsTableAdapter.Insert(svalues[1], Convert.ToInt32(svalues[3]), svalues[4], svalues[5], svalues[7], svalues[6], svalues[0]);
                                tblMapObjCoordsTableAdapter.Update(DataSetLoad.tblMapObjCoords);

                                worksheet.Rows[i].FillColor = Color.Green;
                            }
                            else
                            {
                                tblMapObjCoordsTableAdapter.Insert(svalues[1], Convert.ToInt32(svalues[3]), svalues[4], svalues[5], svalues[7], svalues[6], svalues[0]);
                                tblMapObjCoordsTableAdapter.Update(DataSetLoad.tblMapObjCoords);

                                worksheet.Rows[i].FillColor = Color.Green;
                            }

                            //----------------------------------

                            splashScreenManager1.SetWaitFormDescription("Обработка данных (" + (i + 1).ToString() + " из " + maxrow.ToString() + ")");
                        }

                        spreadsheetControl1.Show();

                        break; // case "координаты свойства":

                } // switch (form1.listBoxLoadType.SelectedValue.ToString())             

                //-------------------------------

                this.spreadsheetControl1.EndUpdate();
                splashScreenManager1.CloseWaitForm();

                this.spreadsheetControl1.Show();
            } // if (form1.ShowDialog(this) == DialogResult.OK)
        }

        private void spreadsheetControl1_BeforeImport(object sender, SpreadsheetBeforeImportEventArgs e)
        {
            this.Text = "Загрузка данных - ";
            this.Text += e.Options.SourceUri.ToString();
        }
    }
}