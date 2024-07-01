using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.Spreadsheet;

namespace askue3
{
    public partial class FormMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string dbconnectionString;

        public FormMain()
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

        private void ribbonControl1_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            ribbonControl1.SelectedPage = ribbonControl1.MergedRibbon.Pages[0];
        }

        // типы приборов учета
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblCounterType");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // проекты
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblProjectType");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // филиалы
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblFilial");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // типы точек учета
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblMetPointType");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // типы опроса
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblOprosType");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // места установки
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblMetPointType");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // ручная загрузка данных
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormLoadData form1 = null;
            form1 = new FormLoadData();
            form1.Text = "Загрузка данных";

            form1.Show();
        }
                
        // точки учета
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblMeteringPoint");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // показания ПУ
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            FormGridDataView form1 = null;
            form1 = new FormGridDataView("tblCounterValue");
            form1.MdiParent = this;
            splashScreenManager1.CloseWaitForm();
            form1.Show();
        }

        // карта сбора
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormMapParams formmv = new FormMapParams();

            if (formmv.ShowDialog(this) == DialogResult.OK)
            {
                splashScreenManager1.ShowWaitForm();

                SqlConnection SQLconnection = new SqlConnection(dbconnectionString);
                SQLconnection.Open();

                FormLoadData form1 = null;
                form1 = new FormLoadData();
                form1.Text = "Карта сбора";
                                
                /*form1.spreadsheetControl1.BeginUpdate();
                form1.spreadsheetControl1.Hide();
                form1.Show();*/
                //----------------------------------
                IWorkbook workbook = form1.spreadsheetControl1.Document;                
                Worksheet worksheet = workbook.Worksheets[0];
                Cell spcell = worksheet[0, 0];

                workbook.History.IsEnabled = false;
                form1.spreadsheetControl1.BeginUpdate();
                form1.spreadsheetControl1.Hide();
                form1.Show();

                DataSet1 DataSetLoad = new DataSet1();
                DataSet1TableAdapters.tblMeteringPointTableAdapter tblMeteringPointTableAdapter = new DataSet1TableAdapters.tblMeteringPointTableAdapter();
                //DataSet1TableAdapters.tblCounterValueTableAdapter tblCounterValueTableAdapter = new DataSet1TableAdapters.tblCounterValueTableAdapter();
                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);

                int row_fstat0 = 4; // номер начальной строки для вывода статистики (строка даты)
                int row0 = 1 + 13 + 3 + 1; // номер "начальной" строки при выводе карты сбора
                int col0 = 0; // номер "начального" столбца

                // выводим сетку для статистики по филиалам
                worksheet[row_fstat0 + 1, col0 + 4].SetValue("Ангарские ЭС");
                worksheet[row_fstat0 + 2, col0 + 4].SetValue("Иркутские ЭС");
                worksheet[row_fstat0 + 3, col0 + 4].SetValue("Киренские ЭС");
                worksheet[row_fstat0 + 4, col0 + 4].SetValue("Мамско-Чуйские ЭС");
                worksheet[row_fstat0 + 5, col0 + 4].SetValue("Нижнеудинские ЭС");
                worksheet[row_fstat0 + 6, col0 + 4].SetValue("Саянские ЭС");
                worksheet[row_fstat0 + 7, col0 + 4].SetValue("Тайшетские ЭС");
                worksheet[row_fstat0 + 8, col0 + 4].SetValue("Усть-Кутские ЭС");
                worksheet[row_fstat0 + 9, col0 + 4].SetValue("Усть-Ордынские ЭС");
                worksheet[row_fstat0 + 10, col0 + 4].SetValue("Черемховские ЭС");
                worksheet[row_fstat0 + 11, col0 + 4].SetValue("непривязанные");
                worksheet[row_fstat0 + 12, col0 + 4].SetValue("Всего:");

                // выводим точки учета и название проекта
                worksheet[row0, col0 + 1].SetValue("Проект");
                worksheet[row0, col0 + 2].SetValue("Филиал");
                worksheet[row0, col0 + 3].SetValue("Населенный пункт");
                worksheet[row0, col0 + 4].SetValue("ТП ОКЭ");                
                worksheet[row0, col0 + 5].SetValue("Наименование");
                worksheet[row0, col0 + 6].SetValue("Дата добавления");
                worksheet[row0, col0 + 7].SetValue("Тип данных");
                worksheet[row0, col0 + 8].SetValue("Номер ПУ");
                worksheet[row0, col0 + 9].SetValue("Код Л/С");
                worksheet[row0, col0 + 10].SetValue("Тип ПУ");
                worksheet[row0, col0 + 11].SetValue("Кт");

                /* for (int i = 0; i < 20; i++) // для тестирования уменьшил выборку!!!
                 //for (int i = 0; i < db_askueDataSetMetPoint.tblMeteringPoint.Count; i++)
                 {
                     // проект
                     spcell = worksheet[row0+1+i, col0+1];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["id_caption_prj"].ToString());

                     // филиал
                     spcell = worksheet[row0 + 1 + i, col0 + 2];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["id_caption_filial"].ToString());

                     // примечание - город
                     spcell = worksheet[row0+1+i, col0+3];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["comment_mp"].ToString());

                     // наименование точки учета
                     spcell = worksheet[row0+1+i, col0+4];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["caption_mp"].ToString());
                                
                     // номер ПУ
                     spcell = worksheet[row0 + 1 + i, col0 + 6];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["id_counter_serial"].ToString());

                     // код ОКЭ (ls2)
                     spcell = worksheet[row0 + 1 + i, col0 + 7];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["ls2"].ToString());

                     // тип ПУ
                     spcell = worksheet[row0 + 1 + i, col0 + 8];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["id_counter_type"].ToString());

                     // Кт
                     spcell = worksheet[row0 + 1 + i, col0 + 9];
                     spcell.SetValue(db_askueDataSetMetPoint.tblMeteringPoint.Rows[i]["tt_value"].ToString());
                 }*/
                //----------------------------------------

                // карта сбора
                int[] filial_values = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // кол-во точек опроса по филиалу за день - 10 филиалов
                int unknown_values = 0; // кол-во неизвестных (непривязаных точек)            

                DateTime start_date = formmv.dateEditStart.DateTime;
                DateTime end_date = formmv.dateEditEnd.DateTime;
                TimeSpan daysbetween = end_date - start_date;
                string prj_type = formmv.PrjcomboBoxEdit1.Text;
                                
                int counter_value_constall_rows = 0;     // кол-во строк с "постоянными" показаниями (все)
                int counter_value_const2_rows = 0;       // кол-во строк с "постоянными" показаниями (любая пара)
                int counter_value_narasterror_rows = 0;  // кол-во строк с нарушенным признаком нарастающих показаний
                int counter_value_nodata_rows = 0;       // кол-во ни разу не опросившихся точек учета

                // отбираем по типу проекта                        
                string fexpr_prj = "id_caption_prj = '" + prj_type + "'";
                DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr_prj);

                /*
                // формируем массив для карты сбора -------
                DataRow[] cvRows2 = db_askueDataSetCValue.tblCounterValue.Select(""); // пока без фильтра 

                List<List<string>> cvRowsArr = new List<List<string>>();                                                 

                // не так - надо CV
                int cvl = cvRows2.Length; // поставил пока для отладки - потом убрать
                int cvColCount = db_askueDataSetCValue.tblCounterValue.Columns.Count;

                for (int j = 0; j < cvl; j++)
                {
                    cvRowsArr.Add(new List<string>()); //добавление новой строки

                    for (int k = 0; k < cvColCount; k++)
                        cvRowsArr[j].Add(cvRows2[j][k].ToString());
                }*/
                //----------------------------------------

                //for (int j = 0; j < 20; j++) // для отладки
                for (int j = 0; j < mpRows.Length; j++)
                {
                    /* iter++;

                     form1.Text = "Итерация " + iter.ToString();*/

                    // "дублируем" по типам данных
                    /*for (int k = 0; k < 3; k++)
                    {*/
                    // проект
                    worksheet[row0 + 1 + j, col0 + 1].SetValue(mpRows[j]["id_caption_prj"].ToString());
                    // филиал
                    worksheet[row0 + 1 + j, col0 + 2].SetValue(mpRows[j]["id_caption_filial"].ToString());                    
                    // примечание - город
                    worksheet[row0 + 1 + j, col0 + 3].SetValue(mpRows[j]["comment_mp"].ToString());
                    // примечание - ТП ОКЭ
                    worksheet[row0 + 1 + j, col0 + 4].SetValue(mpRows[j]["comment_mp2"].ToString());
                    // наименование точки учета
                    worksheet[row0 + 1 + j, col0 + 5].SetValue(mpRows[j]["caption_mp"].ToString());

                    // дата добавления точки учета в систему
                    DateTime create_rec_date = Convert.ToDateTime(mpRows[j]["create_rec_date"].ToString());
                    worksheet[row0 + 1 + j, col0 + 6].SetValue(create_rec_date.ToShortDateString());
                    
                    // номер ПУ
                    worksheet[row0 + 1 + j, col0 + 8].SetValue(mpRows[j]["id_counter_serial"].ToString());
                    // код ОКЭ (ls2)
                    worksheet[row0 + 1 + j, col0 + 9].SetValue(mpRows[j]["ls2"].ToString());
                    // тип ПУ
                    worksheet[row0 + 1 + j, col0 + 10].SetValue(mpRows[j]["id_counter_type"].ToString());
                    // Кт
                    worksheet[row0 + 1 + j, col0 + 11].SetValue(mpRows[j]["tt_value"].ToString());
                    //------------------------------------------------

                    //string counter_type = mpRows[j]["id_counter_type"].ToString();
                    string counter_serial = mpRows[j]["id_counter_serial"].ToString();
                    string filial_caption = mpRows[j]["id_caption_filial"].ToString();

                    //-----------------------------------------------------
                    bool counter_value_constall_flag = true;
                    bool counter_value_const2_flag = false;
                    bool counter_value_narasterror_flag = false;
                    bool counter_value_nodata_flag = true;

                    for (int i = 1; i < daysbetween.Days + 2; i++)
                    {
                        splashScreenManager1.SetWaitFormDescription("Обработка (" + (j + 1).ToString() + " из " + mpRows.Length.ToString() + " (период " + i.ToString() + " из " + (daysbetween.Days + 1).ToString() + ")");
                        //form1.Text = "Карта сбора - Точка учета " + (j + 1).ToString() + " из " + mpRows.Length.ToString() + " (период " + i.ToString() + " из " + (daysbetween.Days + 1).ToString() + ")";
                        
                        // обнуляем статистику
                        for (int k = 0; k < 10; k++) filial_values[k] = 0;
                        unknown_values = 0;

                        // выводим период
                        DateTime curr_date = start_date.AddDays(i - 1);
                        spcell = worksheet[row0 + 0, col0 + 11 + i];
                        spcell.SetValue(curr_date.ToShortDateString());

                        /*for (int j = 0; j < mpRows.Length; j++)
                        {
                        
                            //}
                            //} //for (int k = 0; k < 3; k++)                                        

                        } */

                        // берем последнее показание дня из-за ошибки суточного профиля
                        // время "пробел 00:00:00" !!!!!                        

                        /*db_askueDataSetCValue.tblCounterValueRow cvRow = db_askueDataSetCValue.tblCounterValue.FindByvaluedatevaluetimeid_counter_serialid_counter_type(curr_date, " 00:00:00", counter_serial, counter_type);
                        if (cvRow != null) // показание(я) найдено
                        {
                            // показания ПУ ---------
                            worksheet[row0 + 1 + j, col0 + 5].SetValue("показания"); // убрал j*3 везде ниже по тексту
                            spcell = worksheet[row0 + 1 + j, col0 + 9 + i];
                            string counter_value = cvRow["counter_value"].ToString();
                            spcell.SetValue(counter_value.ToString());
                            //-----------------------
                            
                            // время ----------------
                            //worksheet[row0 + 2 + j * 3, col0 + 5].SetValue("время снятия");
                            //worksheet[row0 + 2 + j * 3, col0 + 9 + i].SetValue(cvRow["valuetime"].ToString());
                            //-----------------------

                            // потребление, кВт*ч ----------------
                            //worksheet[row0 + 3 + j * 3, col0 + 5].SetValue("потребление, кВт*ч");
                            //worksheet[row0 + 3 + j * 3, col0 + 9 + i].SetValue("");
                            //---------------------------------------

                            // подсчет статистики по сбору показаний
                            if (filial_caption == "Ангарские ЭС") filial_values[0]++;
                            if (filial_caption == "Иркутские ЭС") filial_values[1]++;
                            if (filial_caption == "Киренские ЭС") filial_values[2]++;
                            if (filial_caption == "Мамско-Чуйские ЭС") filial_values[3]++;
                            if (filial_caption == "Нижнеудинские ЭС") filial_values[4]++;
                            if (filial_caption == "Саянские ЭС") filial_values[5]++;
                            if (filial_caption == "Тайшетские ЭС") filial_values[6]++;
                            if (filial_caption == "Усть-Кутские ЭС") filial_values[7]++;
                            if (filial_caption == "Усть-Ордынские ЭС") filial_values[8]++;
                            if (filial_caption == "Черемховские ЭС") filial_values[9]++;
                        }
                        else
                        {*/
                        
                        // пробуем на SQL                        
                        string fexpr3 = "id_counter_serial = '" + counter_serial + "' and valuedate = '" + curr_date + "'";
                        //DataRow[] cvRows = DataSetLoad.tblCounterValue.Select(fexpr3, "valuetime DESC");

                        string queryString =
                            "SELECT valuetime, counter_value " +
                            "FROM [db_askue2].[dbo].[tblCounterValue] " +
                            "WHERE " + fexpr3 +
                            " ORDER BY valuetime DESC";
                        DataTable tableCV = new DataTable();
                        MyFUNC_SelectDataFromSQLwoutConnection(tableCV, SQLconnection, queryString);
                        //---------------------------------------------

                        //db_askueDataSetCValue.tblCounterValueRow[] cvRows111 = 

                        //cvRowsArr.FindAll(FindcvRows(counter_type,counter_serial,curr_date));

                        /*int[] fff = new int[245000];
                        for (int z = 0; z < 245000; z++) fff[z] = z;
                        for (int z = 1; z < 245000; z++) fff[z] += fff[z-1];*/

                        /*string fexpr3 = "valuedate = '" + curr_date + "'";
                        DataRow[] cvRows2 = cv_tbl2.Select(fexpr3, "valuetime DESC");                        */

                        //if (cvRows.Length > 0)
                        if (tableCV.Rows.Count > 0)
                        {
                            counter_value_nodata_flag = false; // флаг на наличие показаний в периоде

                            // показания ПУ ---------
                            worksheet[row0 + 1 + j, col0 + 7].SetValue("показания"); // ?????????????????7 почему в цикле
                            spcell = worksheet[row0 + 1 + j, col0 + 11 + i];
                            //string counter_value = cvRows[0]["counter_value"].ToString();
                            string counter_value = tableCV.Rows[0]["counter_value"].ToString();
                            spcell.SetValue(counter_value.ToString());

                            // проверка на "постоянные" показания (пара+все), нарастающий итог
                            if (i > 1)
                            {
                                spcell = worksheet[row0 + 1 + j, col0 + 11 + i - 1];
                                string counter_value_prev = spcell.Value.TextValue;
                                if (counter_value == counter_value_prev)
                                {
                                    counter_value_const2_flag = true;
                                }
                                else
                                {
                                    counter_value_constall_flag = false;
                                }
                                //---------------------------------
                                double cv = Convert.ToDouble(counter_value);
                                double cv_prev = Convert.ToDouble(counter_value_prev);
                                if (cv < cv_prev) counter_value_narasterror_flag = true;
                                else counter_value_narasterror_flag = false;
                            }

                            // подсчет статистики (ПЕРЕДЕЛАТЬ!!!) - всегда только увеличивается на +1, т.к. бежим по точкам, а внутри по периоду
                            if (filial_caption == "Ангарские ЭС") filial_values[0]++;
                            else if (filial_caption == "Иркутские ЭС") filial_values[1]++;
                            else if (filial_caption == "Киренские ЭС") filial_values[2]++;
                            else if (filial_caption == "Мамско-Чуйские ЭС") filial_values[3]++;
                            else if (filial_caption == "Нижнеудинские ЭС") filial_values[4]++;
                            else if (filial_caption == "Саянские ЭС") filial_values[5]++;
                            else if (filial_caption == "Тайшетские ЭС") filial_values[6]++;
                            else if (filial_caption == "Усть-Кутские ЭС") filial_values[7]++;
                            else if (filial_caption == "Усть-Ордынские ЭС") filial_values[8]++;
                            else if (filial_caption == "Черемховские ЭС") filial_values[9]++;
                            else unknown_values++;
                        }

                        // обновляем статистику опроса по филиалам (переделать в дальнейшем - суммы отдельно)
                        int sum_fil = 0;                        

                        for (int k = 0; k < 10; k++)
                        {
                            spcell = worksheet[row_fstat0, col0 + 11 + i];
                            spcell.SetValue(curr_date.ToShortDateString());

                            spcell = worksheet[row_fstat0 + 1 + k, col0 + 11 + i];
                            int sum_fil_date = Convert.ToInt32(spcell.Value.TextValue);
                            sum_fil_date += filial_values[k];
                            spcell.SetValue(sum_fil_date.ToString());

                            //spcell.SetValue(filial_values[k].ToString());
                            //sum_fil += filial_values[k];

                            sum_fil += sum_fil_date;
                        }

                        spcell = worksheet[row_fstat0 + 1 + 10, col0 + 11 + i];
                        int prevUnknownValues = Convert.ToInt32(spcell.Value.TextValue);
                        prevUnknownValues += unknown_values;
                        spcell.SetValue(prevUnknownValues.ToString());

                        spcell = worksheet[row_fstat0 + 11, col0 + 11 + i];
                        spcell.SetValue(prevUnknownValues.ToString());

                        sum_fil += prevUnknownValues;
                        spcell = worksheet[row_fstat0 + 12, col0 + 11 + i];
                        spcell.SetValue(sum_fil.ToString());

                        // выводим расход - ???
                        //spcell = worksheet[row0 + 1 + j, col0 + 3 + i];

                        tableCV.Dispose();
                    } // for (int i = 1; i < daysbetween.Days+2; i++)

                    //--------------------------
                    if (counter_value_constall_flag && counter_value_nodata_flag == false)
                    {
                        worksheet.Rows[row0 + 1 + j].FillColor = Color.Tomato;
                        counter_value_constall_rows++;
                    }

                    if (counter_value_const2_flag && counter_value_constall_flag == false)
                    {
                        worksheet.Rows[row0 + 1 + j].FillColor = Color.Orange;
                        counter_value_const2_rows++;
                    }

                    if (counter_value_narasterror_flag)
                    {
                        worksheet.Rows[row0 + 1 + j].FillColor = Color.Green;
                        counter_value_narasterror_rows++;
                    }

                    if (counter_value_nodata_flag)
                    {
                        worksheet.Rows[row0 + 1 + j].FillColor = Color.LightSteelBlue;
                        counter_value_nodata_rows++;
                    }

                } // for (int j = 0; j < mpRows.Length; j++)

                worksheet[0, 1].SetValue("Внимание!!! Количество точек учета с постоянными показаниями: " + counter_value_constall_rows.ToString());
                worksheet[0, 1].Font.Color = Color.Tomato;

                worksheet[1, 1].SetValue("Внимание!!! Количество точек учета с одинаковой парой показаний: " + counter_value_const2_rows.ToString());
                worksheet[1, 1].Font.Color = Color.Orange;

                worksheet[2, 1].SetValue("Внимание!!! Количество точек учета с нарушенным нарастающим итогом: " + counter_value_narasterror_rows.ToString());
                worksheet[2, 1].Font.Color = Color.Green;

                worksheet[3, 1].SetValue("Внимание!!! Количество ни разу не опросившихся точек учета: " + counter_value_nodata_rows.ToString());
                worksheet[3, 1].Font.Color = Color.LightSteelBlue;

                //----------------------------------

                /*// выводим статистику опроса по филиалам
                for (int i = 1; i < daysbetween.Days + 2; i++)
                {
                    DateTime curr_date = start_date.AddDays(i - 1);
                    spcell = worksheet[row0 + 0, col0 + 9 + i];
                    spcell.SetValue(curr_date.ToShortDateString());

                    // выводим период         
                    spcell = worksheet[1, col0 + 9 + i];
                    spcell.SetValue(curr_date.ToShortDateString());

                    int sum_fil = 0;
                    for (int k = 0; k < 10; k++)
                    {
                        spcell = worksheet[2 + k, col0 + 9 + i];
                        spcell.SetValue(filial_values[k].ToString());
                        sum_fil += filial_values[k];
                    }
                    spcell = worksheet[12, col0 + 9 + i];
                    spcell.SetValue(sum_fil.ToString());
                }*/

                worksheet.Columns.AutoFit(0, 40);

                form1.spreadsheetControl1.EndUpdate();
                SQLconnection.Close();
                splashScreenManager1.CloseWaitForm();

                form1.spreadsheetControl1.Show();

                //form1.Show();
            }
            else
            {

            }

            formmv.Dispose();
        } // карта сбора

        // выгрузка для 1С (последние показания периода)
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMapParams formmv = new FormMapParams();

            if (formmv.ShowDialog(this) == DialogResult.OK)
            {
                splashScreenManager1.ShowWaitForm();

                SqlConnection SQLconnection = new SqlConnection(dbconnectionString);
                SQLconnection.Open();

                FormLoadData form1 = null;
                form1 = new FormLoadData();
                form1.Text = "выгрузка для 1С";
                                //----------------------------------
                IWorkbook workbook = form1.spreadsheetControl1.Document;                
                Worksheet worksheet = workbook.Worksheets[0];
                Cell spcell = worksheet[0, 0];

                workbook.History.IsEnabled = false;
                form1.spreadsheetControl1.BeginUpdate();
                form1.spreadsheetControl1.Hide();
                form1.Show();

                DataSet1 DataSetLoad = new DataSet1();
                DataSet1TableAdapters.tblMeteringPointTableAdapter tblMeteringPointTableAdapter = new DataSet1TableAdapters.tblMeteringPointTableAdapter();
                //DataSet1TableAdapters.tblCounterValueTableAdapter tblCounterValueTableAdapter = new DataSet1TableAdapters.tblCounterValueTableAdapter();
                tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);
                //tblCounterValueTableAdapter.Fill(DataSetLoad.tblCounterValue);
                
                int row0 = 0; // номер "начальной" строки
                int col0 = 0; // номер "начального" столбца

                // выводим точки учета и название проекта
                worksheet[row0, col0 + 0].SetValue("Код Л/С");
                worksheet[row0, col0 + 1].SetValue("Населенный пункт");
                worksheet[row0, col0 + 2].SetValue("Наименование");
                worksheet[row0, col0 + 3].SetValue("Номер ПУ");
                worksheet[row0, col0 + 4].SetValue("Дата");
                worksheet[row0, col0 + 5].SetValue("Показание");
                worksheet[row0, col0 + 6].SetValue("Филиал");

                /*worksheet[row0, col0 + 1].SetValue("Проект");
                worksheet[row0, col0 + 2].SetValue("Филиал");
                worksheet[row0, col0 + 3].SetValue("Населенный пункт");
                worksheet[row0, col0 + 4].SetValue("Наименование");
                worksheet[row0, col0 + 5].SetValue("Тип данных");
                worksheet[row0, col0 + 6].SetValue("Номер ПУ");
                worksheet[row0, col0 + 7].SetValue("Код ОКЭ");
                worksheet[row0, col0 + 8].SetValue("Тип ПУ");
                worksheet[row0, col0 + 9].SetValue("Кт");*/

                //--------------------------------------------

                // формирование выгрузки

                DateTime start_date = formmv.dateEditStart.DateTime;
                DateTime end_date = formmv.dateEditEnd.DateTime;
                //TimeSpan daysbetween = end_date - start_date;
                string prj_type = formmv.PrjcomboBoxEdit1.Text;

                // отбираем по типу проекта                        
                string fexpr_prj = "id_caption_prj = '" + prj_type + "'";
                DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr_prj);

                //for (int j = 0; j < 50; j++) // выборка для теста
                for (int j = 0; j < mpRows.Length; j++)
                {
                    splashScreenManager1.SetWaitFormDescription("Обработка (" + (j + 1).ToString() + " из " + mpRows.Length.ToString() + ")");
                    //form1.Text = "Выгрузка для 1С - Точка учета " + (j + 1).ToString() + " из " + mpRows.Length.ToString();

                    // проект
                    //worksheet[row0 + 1 + j, col0 + 1].SetValue(mpRows[j]["id_caption_prj"].ToString());
                    // филиал
                    worksheet[row0 + 1 + j, col0 + 6].SetValue(mpRows[j]["id_caption_filial"].ToString());
                    // примечание - город
                    worksheet[row0 + 1 + j, col0 + 1].SetValue(mpRows[j]["comment_mp"].ToString());
                    // наименование точки учета
                    worksheet[row0 + 1 + j, col0 + 2].SetValue(mpRows[j]["caption_mp"].ToString());
                    // номер ПУ
                    worksheet[row0 + 1 + j, col0 + 3].SetValue(mpRows[j]["id_counter_serial"].ToString());
                    // код ОКЭ (ls2)
                    worksheet[row0 + 1 + j, col0 + 0].SetValue(mpRows[j]["ls2"].ToString());
                    /*// тип ПУ
                    worksheet[row0 + 1 + j, col0 + 8].SetValue(mpRows[j]["id_counter_type"].ToString());
                    // Кт
                    worksheet[row0 + 1 + j, col0 + 9].SetValue(mpRows[j]["tt_value"].ToString());*/
                    //------------------------------------------------

                    //string counter_type = mpRows[j]["id_counter_type"].ToString();
                    string counter_serial = mpRows[j]["id_counter_serial"].ToString();
                    string filial_caption = mpRows[j]["id_caption_filial"].ToString();

                    //-----------------------------------------------------                    
                    // ищем показание в периоде, отсортировав его по убыванию даты и времени

                    string fexpr3 = "id_counter_serial = '" + counter_serial + "' and valuedate >= '" + start_date + "' and valuedate <= '" + end_date + "'";
                    //DataRow[] cvRows = DataSetLoad.tblCounterValue.Select(fexpr3, "valuedate DESC, valuetime DESC");
                    string queryString =
                            "SELECT valuedate, valuetime, counter_value " +
                            "FROM [db_askue2].[dbo].[tblCounterValue] " +
                            "WHERE " + fexpr3 +
                            " ORDER BY valuedate DESC, valuetime DESC";
                    DataTable tableCV = new DataTable();
                    MyFUNC_SelectDataFromSQLwoutConnection(tableCV, SQLconnection, queryString);

                    if (tableCV.Rows.Count > 0)
                    {
                        // дата показания                        
                        //string datestr = cvRows[0]["valuedate"].ToString();
                        string datestr = tableCV.Rows[0]["valuedate"].ToString();
                        DateTime date1 = Convert.ToDateTime(datestr);
                        worksheet[row0 + 1 + j, col0 + 4].SetValue(date1.ToShortDateString());

                        // показание ПУ с отсеканием дробной части
                        double counter_value_float = Convert.ToDouble(tableCV.Rows[0]["counter_value"].ToString());
                        double counter_value_int = Math.Truncate(counter_value_float);
                        worksheet[row0 + 1 + j, col0 + 5].SetValue(counter_value_int.ToString());
                    }

                    tableCV.Dispose();

                } // for (int j = 0; j < mpRows.Length; j++)

                //----------------------------------

                SQLconnection.Close();

                worksheet.Columns.AutoFit(0, 40);

                form1.spreadsheetControl1.EndUpdate();
                splashScreenManager1.CloseWaitForm();

                form1.spreadsheetControl1.Show();

                //form1.Show();
            }
            else
            {

            }

            formmv.Dispose();
        } // выгрузка для 1С (последние показания периода)

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormMapGeoDraw form1 = new FormMapGeoDraw();
            form1.Text = "ЭСК Сухой ручей";
            form1.Show();
        }
    }
}
