using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

using GMap.NET;
using GMap.NET.WindowsForms;

namespace askue3
{
    public partial class FormMapGeoDraw : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        // класс "пользовательский приямоугольный маркер"
        class GMapMarkerImage : GMapMarker
        {
            private Image image;
            public Image Image
            {
                get
                {
                    return image;
                }
                set
                {
                    image = value;
                    if (image != null)
                    {
                        this.Size = new Size(image.Width,image.Height);
                    }
                }
            }

            public GMapMarkerImage(GMap.NET.PointLatLng p,Image image) : base(p)
            {
                Size = new System.Drawing.Size(image.Width,image.Height);
                Offset = new System.Drawing.Point(-Size.Width / 2,-Size.Height / 2);
                this.image = image;
            }

            public override void OnRender(Graphics g)
            {
                if (image != null)
                {
                    Rectangle rect =  new Rectangle(LocalPosition.X,LocalPosition.Y,Size.Width,Size.Height);
                    g.DrawImage(image, rect);
                }
            }
        }
                

        //-------------------------------
        public int GetOverlayIDByName(GMapControl gmc,string name)
        {
            for (int i = 0; i < gmc.Overlays.Count; i++)
            {
                if (gmc.Overlays[i].Id == name)
                {
                    return i;
                }                
            }
            return -1;
        }

        //---------------------------
        public FormMapGeoDraw()
        {
            InitializeComponent();
        }

        private void FormMapGeoDraw_Load(object sender, EventArgs e)
        {
            //Настройки для компонента GMap.
            gMapControl1.Bearing = 0;

            //CanDragMap - Если параметр установлен в True,
            //пользователь может перетаскивать карту
            ///с помощью правой кнопки мыши.
            gMapControl1.CanDragMap = true;

            //Указываем, что перетаскивание карты осуществляется
            //с использованием левой клавишей мыши.
            //По умолчанию - правая.
            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.GrayScaleMode = true;

            //MarkersEnabled - Если параметр установлен в True,
            //любые маркеры, заданные вручную будет показаны.
            //Если нет, они не появятся.
            gMapControl1.MarkersEnabled = true;

            //Указываем значение максимального приближения.
            gMapControl1.MaxZoom = 36;// 18;

            //Указываем значение минимального приближения.
            gMapControl1.MinZoom = 2;

            //Устанавливаем центр приближения/удаления
            //курсор мыши.
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            //Отказываемся от негативного режима.
            gMapControl1.NegativeMode = false;

            //Разрешаем полигоны.
            gMapControl1.PolygonsEnabled = true;

            //Разрешаем маршруты
            gMapControl1.RoutesEnabled = true;

            //Скрываем внешнюю сетку карты
            //с заголовками.
            gMapControl1.ShowTileGridLines = false;

            //Указываем, что при загрузке карты будет использоваться
            //2х кратное приближение.
            gMapControl1.Zoom = 2;

            //Указываем что будем использовать карты Yandex.
            /*gMapControl1.MapProvider =
                GMap.NET.MapProviders.GMapProviders.YandexMap;*/
            //gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.YandexSatelliteMap;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.BingSatelliteMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMap.NET.MapProviders.GMapProvider.WebProxy = System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            // рисуем панель навигации (настроек)----

            // панель выбора "подложки"
            listBoxControl1.Items.Clear();
            foreach (GMap.NET.MapProviders.GMapProvider GMapProvider in GMap.NET.MapProviders.GMapProviders.List)
            {
                listBoxControl1.Items.Add(GMapProvider.ToString());
            }
            //--------------------------------------- 

            // панель выбора "отображаемых слоев"
            this.checkedListBoxControl1.Items.Clear();

            // корявенько!!!!
            DataSet1 DataSetLoad = new DataSet1();
            DataSet1TableAdapters.tblMapObjTableAdapter tblMapObjTableAdapter = new DataSet1TableAdapters.tblMapObjTableAdapter();            
            tblMapObjTableAdapter.Fill(DataSetLoad.tblMapObj);

            DataView view = new DataView(DataSetLoad.tblMapObj);
            DataTable distinctValues = view.ToTable(true, "layer");

            //string fexpr = "DISTINCT layer";
            //string fexpr = "";            
            //DataRow[] layerRows = DataSetLoad.tblMapObj.Select()

            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {
                checkedListBoxControl1.Items.Add(distinctValues.Rows[i]["layer"].ToString());
            }

            /*foreach (DataRow layerRow in layerRows)
            {
                checkedListBoxControl1.Items.Add(layerRow["layer"]).ToString();
            }*/
            //--------------------------------------- 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Запрос к API геокодирования Google.
            string url = string.Format(
                "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true_or_false&language=ru",
                Uri.EscapeDataString(textBox1.Text));

            //Выполняем запрос к универсальному коду ресурса (URI).
            System.Net.HttpWebRequest request =
                (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            //Получаем ответ от интернет-ресурса.
            System.Net.WebResponse response =
                request.GetResponse();

            //Экземпляр класса System.IO.Stream
            //для чтения данных из интернет-ресурса.
            System.IO.Stream dataStream =
                response.GetResponseStream();

            //Инициализируем новый экземпляр класса
            //System.IO.StreamReader для указанного потока.
            System.IO.StreamReader sreader =
                new System.IO.StreamReader(dataStream);

            //Считывает поток от текущего положения до конца.           
            string responsereader = sreader.ReadToEnd();

            //Закрываем поток ответа.
            response.Close();

            System.Xml.XmlDocument xmldoc =
                new System.Xml.XmlDocument();

            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {

                //Получение широты и долготы.
                System.Xml.XmlNodeList nodes =
                    xmldoc.SelectNodes("//location");

                //Переменные широты и долготы.
                double latitude = 0.0;
                double longitude = 0.0;

                //Получаем широту и долготу.
                foreach (System.Xml.XmlNode node in nodes)
                {
                    latitude =
                       System.Xml.XmlConvert.ToDouble(node.SelectSingleNode("lat").InnerText.ToString());
                    longitude =
                       System.Xml.XmlConvert.ToDouble(node.SelectSingleNode("lng").InnerText.ToString());
                }

                //Варианты получения информации о найденном объекте.
                //Вариант 1.
                string formatted_address =
                   xmldoc.SelectNodes("//formatted_address").Item(0).InnerText.ToString();

                //Вариант 2.
                //Массив, элементы которого содержат подстроки данного экземпляра, разделенные
                //одним или более знаками из separator.
                string[] words = formatted_address.Split(',');
                string dataMarker = string.Empty;
                foreach (string word in words)
                {
                    dataMarker += word + ";" + Environment.NewLine;
                }

                //Вариант 3.
                //string[] words = formatted_address.Split(',');               
                ////Дом.
                //string house = words[1].Trim();
                ////Улица.
                //string Street = words[0].Trim();
                ////Город.
                //string City = words[2].Trim();
                ////Область.
                //string Region = words[3].Trim();               
                ////Страна.
                //string Country = words[4].Trim();
                ////Почтовый индекс.
                //string PostalCode = words[5].Trim();

                //Вариант 4
                ////Дом.
                //string house = xmldoc.SelectNodes("//address_component").Item(0).SelectNodes("short_name").Item(0).InnerText.ToString();
                ////Улица.
                //string Street = xmldoc.SelectNodes("//address_component").Item(1).SelectNodes("short_name").Item(0).InnerText.ToString();
                ////Область.
                //string Region = xmldoc.SelectNodes("//address_component").Item(2).SelectNodes("short_name").Item(0).InnerText.ToString();
                ////Город.
                //string City = xmldoc.SelectNodes("//address_component").Item(3).SelectNodes("short_name").Item(0).InnerText.ToString();
                ////Страна.
                //string Country = xmldoc.SelectNodes("//address_component").Item(6).SelectNodes("long_name").Item(0).InnerText.ToString();
                ////Почтовый индекс.
                //string PostalCode = xmldoc.SelectNodes("//address_component").Item(7).SelectNodes("short_name").Item(0).InnerText.ToString();

                //Создаем новый список маркеров, с указанием компонента
                //в котором они будут использоваться и названием списка.
                GMap.NET.WindowsForms.GMapOverlay markersOverlay =
                    new GMap.NET.WindowsForms.GMapOverlay("marker");
                //new GMap.NET.WindowsForms.GMapOverlay(gMapControl1, "marker");

                //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат.
                GMap.NET.WindowsForms.Markers.GMarkerGoogle markerG =
                    new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(latitude, longitude),
                    GMap.NET.WindowsForms.Markers.GMarkerGoogleType.green);
                //new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new GMap.NET.PointLatLng(latitude, longitude)));

                /*GMap.NET.WindowsForms.Markers.GMarkerGoogleType markerG =
                    new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(
                    new GMap.NET.PointLatLng(latitude, longitude));*/
                markerG.ToolTip =
                    new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);

                //Указываем, что подсказку маркера, необходимо отображать всегда.
                markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Always;

                //Текст подсказки маркера.
                //Для Варианта 1,2.
                markerG.ToolTipText = dataMarker;

                //Для Варианта 3,4.
                //markerG.ToolTipText =
                //   "Почтовый ин.: "+PostalCode + Environment.NewLine+
                //   "Страна: " + Country + Environment.NewLine +
                //   "Город: " + City + Environment.NewLine +
                //   "Область: " + Region + Environment.NewLine +
                //   "Улица: " + Street + Environment.NewLine +
                //   "Номер дома: " + house + Environment.NewLine;

                //Добавляем маркеры в список маркеров.
                markersOverlay.Markers.Add(markerG);

                //Очищаем список маркеров компонента.
                gMapControl1.Overlays.Clear();

                //Добавляем в компонент, список маркеров.
                gMapControl1.Overlays.Add(markersOverlay);

                //Устанавливаем позицию карты.                
                gMapControl1.Position = new GMap.NET.PointLatLng(latitude, longitude);

                //Указываем, что при загрузке карты будет использоваться
                //17ти кратное приближение.
                gMapControl1.Zoom = 17;

                //Обновляем карту.
                gMapControl1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet1 DataSetLoad = new DataSet1();
            DataSet1TableAdapters.tblMeteringPointTableAdapter tblMeteringPointTableAdapter = new DataSet1TableAdapters.tblMeteringPointTableAdapter();
            tblMeteringPointTableAdapter.Fill(DataSetLoad.tblMeteringPoint);

            string fexpr = "geo1coord IS NOT NULL";
            DataRow[] mpRows = DataSetLoad.tblMeteringPoint.Select(fexpr);

            textBox2.Text = mpRows.Count().ToString();

            //-------------------------
            //Создаем новый список маркеров, с указанием компонента
            //в котором они будут использоваться и названием списка.
            GMap.NET.WindowsForms.GMapOverlay markersOverlay =
                new GMap.NET.WindowsForms.GMapOverlay("marker");
            //new GMap.NET.WindowsForms.GMapOverlay(gMapControl1, "marker");
            
            //Для Варианта 3,4.
            //markerG.ToolTipText =
            //   "Почтовый ин.: "+PostalCode + Environment.NewLine+
            //   "Страна: " + Country + Environment.NewLine +
            //   "Город: " + City + Environment.NewLine +
            //   "Область: " + Region + Environment.NewLine +
            //   "Улица: " + Street + Environment.NewLine +
            //   "Номер дома: " + house + Environment.NewLine;

            double latitude = 0.0;
            double longitude = 0.0;

            for (int i = 0; i < mpRows.Count(); i++)
            {
                latitude = Convert.ToDouble(mpRows[i]["geo1coord"]);
                longitude = Convert.ToDouble(mpRows[i]["geo2coord"]);

                GMap.NET.WindowsForms.Markers.GMarkerGoogle markerG =
                    new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(latitude, longitude), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.green);                

                markerG.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);

                //Указываем, что подсказку маркера, необходимо отображать всегда.
                markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Always;

                //Текст подсказки маркера.
                //Для Варианта 1,2.
                markerG.ToolTipText = mpRows[i]["caption_mp"].ToString();

                markersOverlay.Markers.Add(markerG);
            }

            //Очищаем список маркеров компонента.
            gMapControl1.Overlays.Clear();

            //Добавляем в компонент, список маркеров.
            gMapControl1.Overlays.Add(markersOverlay);

            //Устанавливаем позицию карты.
            gMapControl1.Position = new GMap.NET.PointLatLng(latitude, longitude);

            //Указываем, что при загрузке карты будет использоваться
            //17ти кратное приближение.
            gMapControl1.Zoom = 17;

            //Обновляем карту.
            gMapControl1.Refresh();
            //---------------------------------------------
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet1 DataSetLoad = new DataSet1();
            DataSet1TableAdapters.tblCoordTestTableAdapter tblCoordTestTableAdapter = new DataSet1TableAdapters.tblCoordTestTableAdapter();
            tblCoordTestTableAdapter.Fill(DataSetLoad.tblCoordTest);

            //string fexpr = "geo1coord IS NOT NULL";
            //string fexpr = "";
            DataRow[] mpRows = DataSetLoad.tblCoordTest.Select();

            textBox2.Text = mpRows.Count().ToString();

            //-------------------------
            //Создаем новый список маркеров, с указанием компонента
            //в котором они будут использоваться и названием списка.
            //GMap.NET.WindowsForms.GMapOverlay markersOverlay = new GMap.NET.WindowsForms.GMapOverlay("marker");
            GMap.NET.WindowsForms.GMapOverlay markersOverlay1 = new GMap.NET.WindowsForms.GMapOverlay("oporaOKE");
            GMap.NET.WindowsForms.GMapOverlay markersOverlay2 = new GMap.NET.WindowsForms.GMapOverlay("oporaNOTOKE");
            GMap.NET.WindowsForms.GMapOverlay markersOverlay3 = new GMap.NET.WindowsForms.GMapOverlay("OZ");
            GMap.NET.WindowsForms.GMapOverlay polyOverlay = new GMap.NET.WindowsForms.GMapOverlay("poligons");

            // для рисования площадного объекта
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            /*points.Add(new GMap.NET.PointLatLng(-25.969562, 32.585789));
            points.Add(new GMap.NET.PointLatLng(-25.966205, 32.588171));
            points.Add(new GMap.NET.PointLatLng(-25.968134, 32.591647));
            points.Add(new GMap.NET.PointLatLng(-25.971684, 32.589759));*/
            /*GMap.NET.WindowsForms.GMapPolygon polygon = new GMap.NET.WindowsForms.GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);*/

            //Для Варианта 3,4.
            //markerG.ToolTipText =
            //   "Почтовый ин.: "+PostalCode + Environment.NewLine+
            //   "Страна: " + Country + Environment.NewLine +
            //   "Город: " + City + Environment.NewLine +
            //   "Область: " + Region + Environment.NewLine +
            //   "Улица: " + Street + Environment.NewLine +
            //   "Номер дома: " + house + Environment.NewLine;

            //Маркер для простой отметки на карте
            Bitmap bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\marker.png") as Bitmap;            

            double latitude = 0.0;
            double longitude = 0.0;

            for (int i = 0; i < mpRows.Count(); i++)
            {
                latitude = Convert.ToDouble(mpRows[i]["coordalt"]);
                longitude = Convert.ToDouble(mpRows[i]["coordlong"]);

                GMap.NET.WindowsForms.Markers.GMarkerGoogleType marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.orange_small;
                if (mpRows[i]["layer"].ToString() == "опоры ОКЭ") marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.orange_small;
                if (mpRows[i]["layer"].ToString() == "опоры иного собственника") marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_small;
                if (mpRows[i]["layer"].ToString() == "охранная зона")
                {
                    marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_small;
                    points.Add(new GMap.NET.PointLatLng(latitude,longitude));
                }

                /*if (mpRows[i]["layer"].ToString() != "охранная зона")
                {*/
                /*GMap.NET.WindowsForms.Markers.GMarkerGoogle markerG =
                new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(latitude, longitude), marker_type);*/

                GMapMarkerImage markerG =
                    new GMapMarkerImage(new GMap.NET.PointLatLng(latitude, longitude),bitmap_marker);

//                markerG.IsMouseOver()

                markerG.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);

                //Указываем, что подсказку маркера, необходимо отображать всегда.
                //markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Always;
                markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.OnMouseOver;

                //Текст подсказки маркера.
                //Для Варианта 1,2.
                markerG.ToolTipText = mpRows[i]["x"].ToString() + "," + mpRows[i]["y"];
                //}

                if (mpRows[i]["layer"].ToString() == "опоры ОКЭ") markersOverlay1.Markers.Add(markerG);
                if (mpRows[i]["layer"].ToString() == "опоры иного собственника") markersOverlay2.Markers.Add(markerG);
                if (mpRows[i]["layer"].ToString() == "охранная зона")
                {
                    //markersOverlay3.Markers.Add(markerG);                    
                }
            } // for (int i = 0; i < mpRows.Count(); i++)

            GMap.NET.WindowsForms.GMapPolygon polygon = new GMap.NET.WindowsForms.GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);
           
            //Очищаем список маркеров компонента.
            gMapControl1.Overlays.Clear();

            //Добавляем в компонент, список маркеров.
            gMapControl1.Overlays.Add(markersOverlay1);
            gMapControl1.Overlays.Add(markersOverlay2);
            //gMapControl1.Overlays.Add(markersOverlay3);
            gMapControl1.Overlays.Add(polyOverlay);

            //Устанавливаем позицию карты.
            gMapControl1.Position = new GMap.NET.PointLatLng(latitude, longitude);

            //Указываем, что при загрузке карты будет использоваться
            //17ти кратное приближение.
            gMapControl1.Zoom = 17;

            //Обновляем карту.
            gMapControl1.Refresh();
            //---------------------------------------------
        }

        //выбор провайдера("подложки")
        private void listBoxControl1_MouseClick(object sender, MouseEventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.List[this.listBoxControl1.SelectedIndex];
            gMapControl1.ReloadMap();
            gMapControl1.Refresh();
        }

        // нажатие на маркер
        private void gMapControl1_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            this.memoEdit1.Text = item.ToolTipText;
        }

        // объектное отображение (31.01.2016)
        private void button4_Click(object sender, EventArgs e)
        {
            DataSet1 DataSetLoad = new DataSet1();
            DataSet1TableAdapters.tblMapObjTableAdapter tblMapObjTableAdapter = new DataSet1TableAdapters.tblMapObjTableAdapter();
            DataSet1TableAdapters.tblMapObjCoordsTableAdapter tblMapObjCoordsTableAdapter = new DataSet1TableAdapters.tblMapObjCoordsTableAdapter();
            tblMapObjTableAdapter.Fill(DataSetLoad.tblMapObj);
            tblMapObjCoordsTableAdapter.Fill(DataSetLoad.tblMapObjCoords);

            //string fexpr = "geo1coord IS NOT NULL";
            //string fexpr = "";
            DataRow[] objRows = DataSetLoad.tblMapObj.Select();
            //DataRow[] objCoordsRows = DataSetLoad.tblMapObjCoords.Select();

            //textBox2.Text = mpRows.Count().ToString();

            //-------------------------
            GMap.NET.WindowsForms.GMapOverlay markersOverlay_oporaOKE = new GMap.NET.WindowsForms.GMapOverlay("oporaOKE");
            GMap.NET.WindowsForms.GMapOverlay markersOverlay_oporaNOTOKE = new GMap.NET.WindowsForms.GMapOverlay("oporaNOTOKE");
            GMap.NET.WindowsForms.GMapOverlay markersOverlay3 = new GMap.NET.WindowsForms.GMapOverlay("OZ2");
            GMap.NET.WindowsForms.GMapOverlay polyOverlay_OZ = new GMap.NET.WindowsForms.GMapOverlay("OZ");
            GMap.NET.WindowsForms.GMapOverlay polyOverlay_TPOKE = new GMap.NET.WindowsForms.GMapOverlay("tpOKE");

            /*GMap.NET.WindowsForms.GMapOverlay routesOverlay = new GMap.NET.WindowsForms.GMapOverlay("routes");
            GMap.NET.WindowsForms.GMapOverlay polyOverlay_routes = new GMap.NET.WindowsForms.GMapOverlay("routespol");*/

            // для рисования площадного объекта
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            //List<GMap.NET.PointLatLng> routespoints = new List<GMap.NET.PointLatLng>();

            //Для Варианта 3,4.
            //markerG.ToolTipText =
            //   "Почтовый ин.: "+PostalCode + Environment.NewLine+
            //   "Страна: " + Country + Environment.NewLine +
            //   "Город: " + City + Environment.NewLine +
            //   "Область: " + Region + Environment.NewLine +
            //   "Улица: " + Street + Environment.NewLine +
            //   "Номер дома: " + house + Environment.NewLine;

            //Маркер для простой отметки на карте
            //Bitmap bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\marker.png") as Bitmap;

            double latitude = 0.0;
            double longitude = 0.0;

            gMapControl1.Overlays.Clear();

            for (int i = 0; i < objRows.Count(); i++)
            {
                points.Clear();

                string coordexpr = "idmapobj = '"+objRows[i]["idmapobj"].ToString()+"'";
                string coordsort = "idpointinobj DESC";                              
                DataRow[] objCoordsRows = DataSetLoad.tblMapObjCoords.Select(coordexpr,coordsort);

                for (int j = 0; j < objCoordsRows.Count(); j++)
                {
                    latitude = Convert.ToDouble(objCoordsRows[j]["coordalt"]);
                    longitude = Convert.ToDouble(objCoordsRows[j]["coordlong"]);
                                        
                    /*if (objRows[i]["layer"].ToString() == "подстанция ОКЭ")
                    {
                        //marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_small;
                        points.Add(new GMap.NET.PointLatLng(latitude, longitude));
                    }
                    if (objRows[i]["layer"].ToString() == "охранная зона")
                    {
                        //marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_small;
                        points.Add(new GMap.NET.PointLatLng(latitude, longitude));
                    }*/
                    if (objRows[i]["prop1"].ToString() == "площадной") points.Add(new GMap.NET.PointLatLng(latitude, longitude));

                } // for (int j = 0; j < objCoordsRows.Count(); j++)

                Color layercolor = Color.Red;
                if (objRows[i]["layer"].ToString() == "подстанция ОКЭ") layercolor = Color.Orange;
                if (objRows[i]["layer"].ToString() == "охранная зона") layercolor = Color.Red;
                
                switch (objRows[i]["prop1"].ToString())
                {
                    /*case "линейный":
                        GMapRoute r = new GMapRoute(points, "route"+i.ToString());
                        r.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        r.Stroke.Color = Color.Lime;                        
                        r.Tag = objRows[i]["prop1"].ToString() + Environment.NewLine +
                            objRows[i]["prop2"].ToString() + Environment.NewLine +
                            objRows[i]["prop3"].ToString() + Environment.NewLine +
                            objRows[i]["prop4"].ToString() + Environment.NewLine +
                            objRows[i]["prop5"].ToString() + Environment.NewLine +
                            objRows[i]["prop6"].ToString() + Environment.NewLine +
                            objRows[i]["prop7"].ToString() + Environment.NewLine +
                            objRows[i]["prop8"].ToString();

                        r.IsHitTestVisible = true;

                        if (objRows[i]["layer"].ToString() == "подстанция ОКЭ") polyOverlay_TPOKE.Polygons.Add(polygon);
                        if (objRows[i]["layer"].ToString() == "охранная зона") polyOverlay_OZ.Polygons.Add(polygon);
                        break;*/

                    case "площадной":
                        GMap.NET.WindowsForms.GMapPolygon polygon = new GMap.NET.WindowsForms.GMapPolygon(points, "polygon" + i.ToString());
                        polygon.Fill = new SolidBrush(Color.FromArgb(50, layercolor));
                        polygon.Stroke = new Pen(layercolor, 1);
                        polygon.Tag = objRows[i]["prop1"].ToString() + Environment.NewLine +
                            objRows[i]["prop2"].ToString() + Environment.NewLine +
                            objRows[i]["prop3"].ToString() + Environment.NewLine +
                            objRows[i]["prop4"].ToString() + Environment.NewLine +
                            objRows[i]["prop5"].ToString() + Environment.NewLine +
                            objRows[i]["prop6"].ToString() + Environment.NewLine +
                            objRows[i]["prop7"].ToString() + Environment.NewLine +
                            objRows[i]["prop8"].ToString();

                        polygon.IsHitTestVisible = true;

                        if (objRows[i]["layer"].ToString() == "подстанция ОКЭ") polyOverlay_TPOKE.Polygons.Add(polygon);
                        if (objRows[i]["layer"].ToString() == "охранная зона") polyOverlay_OZ.Polygons.Add(polygon);
                        break;

                    case "точечный":
                        Bitmap bitmap_marker = null;
                        if (objRows[i]["layer"].ToString() == "опора ОКЭ") bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\orange.png") as Bitmap;
                        if (objRows[i]["layer"].ToString() == "опора иного собственника") bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\blue.png") as Bitmap;
                        GMapMarkerImage markerG = new GMapMarkerImage(new GMap.NET.PointLatLng(latitude, longitude), bitmap_marker);
                        markerG.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);
                        markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.OnMouseOver;
                        //markerG.ToolTipText = (i + 1).ToString();
                        markerG.ToolTipText = objRows[i]["prop1"].ToString() + Environment.NewLine +
                            objRows[i]["prop2"].ToString() + Environment.NewLine +
                            objRows[i]["prop3"].ToString() + Environment.NewLine +
                            objRows[i]["prop4"].ToString() + Environment.NewLine +
                            objRows[i]["prop5"].ToString() + Environment.NewLine +
                            objRows[i]["prop6"].ToString() + Environment.NewLine +
                            objRows[i]["prop7"].ToString() + Environment.NewLine +
                            objRows[i]["prop8"].ToString();

                        if (objRows[i]["layer"].ToString() == "опора ОКЭ") markersOverlay_oporaOKE.Markers.Add(markerG);
                        if (objRows[i]["layer"].ToString() == "опора иного собственника") markersOverlay_oporaNOTOKE.Markers.Add(markerG);

                        //routespoints.Add(new GMap.NET.PointLatLng(latitude, longitude));
                        break;
                }
                
            } // for (int i = 0; i < objRows.Count(); i++)

            /*GMapRoute r = new GMapRoute(routespoints, "myroute");
            r.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            r.Stroke.Color = Color.Lime;
            routesOverlay.Routes.Add(r);*/

            /*GMap.NET.WindowsForms.GMapPolygon polygon22 = new GMap.NET.WindowsForms.GMapPolygon(routespoints, "polygon22");
            polygon22.Fill = new SolidBrush(Color.FromArgb(50, Color.Lime));
            polygon22.Stroke = new Pen(Color.Lime, 1);
            polyOverlay_routes.Polygons.Add(polygon22);*/

            //Добавляем в компонент, список маркеров.
            gMapControl1.Overlays.Add(markersOverlay_oporaOKE);
            gMapControl1.Overlays.Add(markersOverlay_oporaNOTOKE);
            //gMapControl1.Overlays.Add(markersOverlay3);
            gMapControl1.Overlays.Add(polyOverlay_TPOKE);
            gMapControl1.Overlays.Add(polyOverlay_OZ);
            //gMapControl1.Overlays.Add(routesOverlay);
           // gMapControl1.Overlays.Add(polyOverlay_routes);

            //Устанавливаем позицию карты.
            gMapControl1.Position = new GMap.NET.PointLatLng(51.64467630922288, 103.75751321528403);

            //Указываем, что при загрузке карты будет использоваться
            //17ти кратное приближение.
            gMapControl1.Zoom = 17;

            //Обновляем карту.
            gMapControl1.Refresh();
            //---------------------------------------------
        }

        // нажатие на полигон
        private void gMapControl1_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            this.memoEdit1.Text = item.Tag.ToString();            
        }

        // новое отображение (01.02.2016)
        private void button5_Click(object sender, EventArgs e)
        {
            DataSet1 DataSetLoad = new DataSet1();
            DataSet1TableAdapters.tblMapObjTableAdapter tblMapObjTableAdapter = new DataSet1TableAdapters.tblMapObjTableAdapter();
            DataSet1TableAdapters.tblMapObjCoordsTableAdapter tblMapObjCoordsTableAdapter = new DataSet1TableAdapters.tblMapObjCoordsTableAdapter();
            tblMapObjTableAdapter.Fill(DataSetLoad.tblMapObj);
            tblMapObjCoordsTableAdapter.Fill(DataSetLoad.tblMapObjCoords);

            //string fexpr = "geo1coord IS NOT NULL";
            //string fexpr = "";
            DataRow[] objRows = DataSetLoad.tblMapObj.Select();
            //DataRow[] objCoordsRows = DataSetLoad.tblMapObjCoords.Select();

            //textBox2.Text = mpRows.Count().ToString();

            //-------------------------
            
            //DataView view = new DataView(DataSetLoad.tblMapObj);
            //DataTable layerValues = view.ToTable(true, "layer", "prop1");

            GMapOverlay polyOverlay = null;
            //GMapOverlay markersOverlay = null;
            gMapControl1.Overlays.Clear();

            /*for (int i = 0; i < layerValues.Rows.Count; i++)
            {
                if (layerValues.Rows[i]["prop1"].ToString() == "площадной")
                {
                    polyOverlay = new GMap.NET.WindowsForms.GMapOverlay(layerValues.Rows[i]["layer"].ToString());
                    polyOverlay.IsVisibile = false;
                    gMapControl1.Overlays.Add(polyOverlay);
                }
                if (layerValues.Rows[i]["prop1"].ToString() == "точечный")
                {                    
                    markersOverlay = new GMap.NET.WindowsForms.GMapOverlay(layerValues.Rows[i]["layer"].ToString());
                    markersOverlay.IsVisibile = false;
                    gMapControl1.Overlays.Add(markersOverlay);
                }
            }*/

            string layerfilter = "";
            for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
            {
                layerfilter = "layer = '"+checkedListBoxControl1.Items[i].ToString()+"'";
                DataRow[] objRows2 = DataSetLoad.tblMapObj.Select(layerfilter);

                polyOverlay = new GMapOverlay(checkedListBoxControl1.Items[i].ToString());
                polyOverlay.IsVisibile = false;
                // polyOverlay.Id = checkedListBoxControl1.Items[i].ToString();
                gMapControl1.Overlays.Add(polyOverlay);

                /*if (objRows2[0]["prop1"].ToString() == "площадной")
                {
                    polyOverlay = new GMapOverlay(checkedListBoxControl1.Items[i].ToString());
                    polyOverlay.IsVisibile = false;
                   // polyOverlay.Id = checkedListBoxControl1.Items[i].ToString();
                    gMapControl1.Overlays.Add(polyOverlay);
                }
                if (objRows2[0]["prop1"].ToString() == "точечный")
                {
                    markersOverlay = new GMapOverlay(checkedListBoxControl1.Items[i].ToString());
                    markersOverlay.IsVisibile = false;
                    //markersOverlay.Id = checkedListBoxControl1.Items[i].ToString();
                    gMapControl1.Overlays.Add(markersOverlay);
                }*/
            }

            // точечный
            //GMap.NET.WindowsForms.GMapOverlay markersOverlay = new GMap.NET.WindowsForms.GMapOverlay("markers");            

            // площадной
            //GMap.NET.WindowsForms.GMapOverlay polyOverlay = new GMap.NET.WindowsForms.GMapOverlay("polygons");
            //GMap.NET.WindowsForms.GMapOverlay polyOverlay_TPOKE = new GMap.NET.WindowsForms.GMapOverlay("tpOKE");

            /*GMap.NET.WindowsForms.GMapOverlay routesOverlay = new GMap.NET.WindowsForms.GMapOverlay("routes");
            GMap.NET.WindowsForms.GMapOverlay polyOverlay_routes = new GMap.NET.WindowsForms.GMapOverlay("routespol");*/

            // для рисования площадного объекта
            //List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            //List<GMap.NET.PointLatLng> routespoints = new List<GMap.NET.PointLatLng>();

            //Маркер для простой отметки на карте
            //Bitmap bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\marker.png") as Bitmap;

            double latitude = 0.0;
            double longitude = 0.0;
            
            for (int i = 0; i < objRows.Count(); i++)
            {
                List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();

                string coordexpr = "idmapobj = '" + objRows[i]["idmapobj"].ToString() + "'";
                string coordsort = "idpointinobj DESC";
                DataRow[] objCoordsRows = DataSetLoad.tblMapObjCoords.Select(coordexpr, coordsort);

                string layer = objRows[i]["layer"].ToString();

                // отрабатываем по координатам объекта
                for (int j = 0; j < objCoordsRows.Count(); j++)
                {
                    latitude = Convert.ToDouble(objCoordsRows[j]["coordalt"]);
                    longitude = Convert.ToDouble(objCoordsRows[j]["coordlong"]);

                    if (objRows[i]["prop1"].ToString() == "площадной") points.Add(new GMap.NET.PointLatLng(latitude, longitude));
                    if (objRows[i]["prop1"].ToString() == "линейный") points.Add(new GMap.NET.PointLatLng(latitude, longitude));

                } // for (int j = 0; j < objCoordsRows.Count(); j++)

                Color layercolor = Color.Red;
                string layercapt = objRows[i]["layer"].ToString();
                int layerid = GetOverlayIDByName(gMapControl1, layercapt);

                switch (objRows[i]["prop1"].ToString())
                {
                    case "линейный":
                        layercolor = Color.Lime;

                        GMapRoute r = new GMapRoute(points, "route"+i.ToString());
                        r.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        r.Stroke.Color = layercolor;                        
                        r.Tag = objRows[i]["prop1"].ToString() + Environment.NewLine +
                            objRows[i]["prop2"].ToString() + Environment.NewLine +
                            objRows[i]["prop3"].ToString() + Environment.NewLine +
                            objRows[i]["prop4"].ToString() + Environment.NewLine +
                            objRows[i]["prop5"].ToString() + Environment.NewLine +
                            objRows[i]["prop6"].ToString() + Environment.NewLine +
                            objRows[i]["prop7"].ToString() + Environment.NewLine +
                            objRows[i]["prop8"].ToString();

                        r.IsHitTestVisible = true;
                        
                        gMapControl1.Overlays[layerid].Routes.Add(r);

                        //polyOverlay.Polygons.Add(polygon);                        
                        break;

                    case "площадной":
                        layercolor = Color.Red;

                        GMap.NET.WindowsForms.GMapPolygon polygon = new GMap.NET.WindowsForms.GMapPolygon(points, "polygon" + i.ToString());
                        polygon.Fill = new SolidBrush(Color.FromArgb(50, layercolor));
                        polygon.Stroke = new Pen(layercolor, 1);
                        polygon.Tag = objRows[i]["prop1"].ToString() + Environment.NewLine +
                            objRows[i]["prop2"].ToString() + Environment.NewLine +
                            objRows[i]["prop3"].ToString() + Environment.NewLine +
                            objRows[i]["prop4"].ToString() + Environment.NewLine +
                            objRows[i]["prop5"].ToString() + Environment.NewLine +
                            objRows[i]["prop6"].ToString() + Environment.NewLine +
                            objRows[i]["prop7"].ToString() + Environment.NewLine +
                            objRows[i]["prop8"].ToString();

                        polygon.IsHitTestVisible = true;

                        gMapControl1.Overlays[layerid].Polygons.Add(polygon);

                        //polyOverlay.Polygons.Add(polygon);                        
                        break;

                    case "точечный":
                        Bitmap bitmap_marker = null;
                        bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\orange.png") as Bitmap;
                        /*if (objRows[i]["layer"].ToString() == "опора ОКЭ") bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\orange.png") as Bitmap;
                        if (objRows[i]["layer"].ToString() == "опора иного собственника") bitmap_marker = Bitmap.FromFile(Application.StartupPath + @"\markers\\blue.png") as Bitmap;*/
                        GMapMarkerImage markerG = new GMapMarkerImage(new GMap.NET.PointLatLng(latitude, longitude), bitmap_marker);
                        markerG.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);
                        markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.OnMouseOver;
                        //markerG.ToolTipText = (i + 1).ToString();
                        markerG.ToolTipText = objRows[i]["prop1"].ToString() + Environment.NewLine +
                            objRows[i]["prop2"].ToString() + Environment.NewLine +
                            objRows[i]["prop3"].ToString() + Environment.NewLine +
                            objRows[i]["prop4"].ToString() + Environment.NewLine +
                            objRows[i]["prop5"].ToString() + Environment.NewLine +
                            objRows[i]["prop6"].ToString() + Environment.NewLine +
                            objRows[i]["prop7"].ToString() + Environment.NewLine +
                            objRows[i]["prop8"].ToString();

                        gMapControl1.Overlays[layerid].Markers.Add(markerG);
                        
                        //markersOverlay.Markers.Add(markerG);
                        /*if (objRows[i]["layer"].ToString() == "опора ОКЭ") markersOverlay_oporaOKE.Markers.Add(markerG);
                        if (objRows[i]["layer"].ToString() == "опора иного собственника") markersOverlay_oporaNOTOKE.Markers.Add(markerG);*/

                        //routespoints.Add(new GMap.NET.PointLatLng(latitude, longitude));
                        break;

                    case "подпись":
                        GMap.NET.WindowsForms.Markers.GMarkerGoogleType marker_type = GMap.NET.WindowsForms.Markers.GMarkerGoogleType.orange_small;
                        GMap.NET.WindowsForms.Markers.GMarkerGoogle markerT = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(latitude, longitude), marker_type);
                        markerT.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(markerT);
                        markerT.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Always;
                        //markerG.ToolTipText = (i + 1).ToString();
                        markerT.ToolTipText = objRows[i]["prop2"].ToString();

                        gMapControl1.Overlays[layerid].Markers.Add(markerT);

                        //markersOverlay.Markers.Add(markerG);
                        /*if (objRows[i]["layer"].ToString() == "опора ОКЭ") markersOverlay_oporaOKE.Markers.Add(markerG);
                        if (objRows[i]["layer"].ToString() == "опора иного собственника") markersOverlay_oporaNOTOKE.Markers.Add(markerG);*/

                        //routespoints.Add(new GMap.NET.PointLatLng(latitude, longitude));
                        break;
                }

            } // for (int i = 0; i < objRows.Count(); i++)

            /*GMapRoute r = new GMapRoute(routespoints, "myroute");
            r.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            r.Stroke.Color = Color.Lime;
            routesOverlay.Routes.Add(r);*/

            /*GMap.NET.WindowsForms.GMapPolygon polygon22 = new GMap.NET.WindowsForms.GMapPolygon(routespoints, "polygon22");
            polygon22.Fill = new SolidBrush(Color.FromArgb(50, Color.Lime));
            polygon22.Stroke = new Pen(Color.Lime, 1);
            polyOverlay_routes.Polygons.Add(polygon22);*/
                        
            //gMapControl1.Overlays.Add(routesOverlay);
            // gMapControl1.Overlays.Add(polyOverlay_routes);

 //           gMapControl1.Overlays.Add(markersOverlay);
   //         gMapControl1.Overlays.Add(polyOverlay);

            //Устанавливаем позицию карты.
            gMapControl1.Position = new GMap.NET.PointLatLng(51.64467630922288, 103.75751321528403);

            //Указываем, что при загрузке карты будет использоваться
            //17ти кратное приближение.
            gMapControl1.Zoom = 17;

            //Обновляем карту.
            gMapControl1.Refresh();
            //---------------------------------------------
        }

        // check layers
        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked) gMapControl1.Overlays[e.Index].IsVisibile = true;
            else gMapControl1.Overlays[e.Index].IsVisibile = false;

            gMapControl1.Refresh();
        }

        private void gMapControl1_OnRouteClick(GMapRoute item, MouseEventArgs e)
        {
            this.memoEdit1.Text = item.Tag.ToString();
        }
    }
}