using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.ComponentModel;



namespace CICS_PP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        // 用于路面图像显示区图像文件加载时，线程间同步的对象
        object lockObj = new object();
        // 用于存储路面图像区图像集合
        ObservableCollection<Uri> images = null;
        // 图像病害统计区数据源定义
        ObservableCollection<ImageDiseases> imageDiseasesList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // *************************图像病害统计区显示
            imageDiseasesList = new ObservableCollection<ImageDiseases>();

            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2011", fileName = "T003A-003+674000-003+674000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "2", severeCrack = "1", lightBlockRip = "3", severeBlockRip = "1", lightVerticalRip = "2", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2012", fileName = "T003A-005+086000-005+086000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2013", fileName = "T003A-000+286000-000+286000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "0", severeCrack = "0", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2014", fileName = "T003A-003+682000-003+682000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "0", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2015", fileName = "T003A-003+674000-003+372000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "2", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2016", fileName = "T003A-003+674000-023+374000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "0", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2017", fileName = "T003A-003+674000-005+573000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2018", fileName = "T003A-003+674000-003+634000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "0", severeCrack = "2", lightBlockRip = "0", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2019", fileName = "T003A-003+679000-005+679000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "0", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2020", fileName = "T003A-003+674000-005+684000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "0", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2021", fileName = "T003A-003+679000-003+254000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2022", fileName = "T003A-003+694000-005+694000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "1", severeCrack = "2", lightBlockRip = "1", severeBlockRip = "0", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2023", fileName = "T003A-003+674000-003+884000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2024", fileName = "T003A-003+344000-005+344000.jpg", roadType = "沥青路面", lightCrack = "1", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "1", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });
            imageDiseasesList.Add(new ImageDiseases() { serialNo = "2025", fileName = "T003A-003+904000-003+904000.jpg", roadType = "沥青路面", lightCrack = "0", NormalCrack = "1", severeCrack = "1", lightBlockRip = "1", severeBlockRip = "0", lightVerticalRip = "1", severeVerticalRip = "1", lightHorizontalRip = "1", severeHorizontalRip = "1", lightPit = "1", severePit = "1", lightLoose = "1", severeLoose = "1", lightSink = "1", severeSink = "1", lightRut = "1", severeRut = "1", lightWave = "1", severeWave = "1", weeping = "1", patch = "1" });

            imageDiseasesDataGrid.ItemsSource = imageDiseasesList;
            this.DataContext = this;

            // *************************路面图像区显示
            Double imageWidth = 0;
            Double imageHeight = 0;
            images = new ObservableCollection<Uri>();
            Binding b = new Binding
            {
                Source = images,
                IsAsync = true
            };
            imagesListBox.SetBinding(ItemsControl.ItemsSourceProperty, b);

            // 这一句很关键，开启集合的异步访问支持
            BindingOperations.EnableCollectionSynchronization(images, lockObj);

            // 异步加载数据
            Task.Run(() =>
            {
                // 代码写在 lock 块中
                lock (lockObj)
                {

                    //string path = @"E:\001_JianDu\Projects\CICS_PP\CICS_PP\Resources\roads\ground\";
                    string path = @"E:\data\Images\20170521\T003A_104901";
                    FileAccess fa = new FileAccess();
                    String[] imagesPath = FileAccess.GetImages(path, SearchOption.AllDirectories);

                    foreach (string s in imagesPath)
                    {
                        this.images.Add(new Uri(s, UriKind.Absolute));
                    }
                }
            });
            //DisplayImages(roadStackPanel, images);
        }

        /// <summary>  
        /// 显示图像  
        /// </summary>  
        /// <param name="panel">放置图像控件的容器</param>  
        /// <param name="images">要显示的图像文件集合</param>  
        /// <param name="handler">图像控件点击事件处理器</param>  
        private void DisplayImages(StackPanel panel, String[] images)
        {   // 参数检测  
   
        }

        private Grid grid;
        private Point startPoint;
        private Boolean hasDraw;
        private List<Rectangle> rectList = new List<Rectangle>();

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid = sender as Grid;
            startPoint = e.GetPosition(grid);
        }

        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point point = e.GetPosition(grid);

                if (hasDraw)
                {
                    hasDraw = false;
                    this.startPoint = point;
                }

                if(Math.Abs(point.X - startPoint.X) > 10 || Math.Abs(point.Y - startPoint.Y) > 10)
                {
                    RectangleGeometry rectGeom = new RectangleGeometry();
                    rectGeom.Rect = new Rect(startPoint.X - 5, startPoint.Y + 5, 10, 10);
                    System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
                    path.Fill = Brushes.Transparent;
                    path.Stroke = Brushes.Red;
                    path.StrokeThickness = 2;
                    path.Data = rectGeom;
                    
                    grid.Children.Add(path);

                    hasDraw = true;
                } else
                {
                    hasDraw = false;
                }
                    
            }
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }

    public sealed class UriToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = (Uri)value;
            BitmapImage bmp = new BitmapImage();
            bmp.DecodePixelHeight = 250; // 确定解码高度，宽度不同时设置
            bmp.BeginInit();
            // 延迟，必要时创建
            bmp.CreateOptions = BitmapCreateOptions.DelayCreation;
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = uri;
            bmp.EndInit(); //结束初始化
            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    

    public class ImageDiseases
    {
        // 序号
        public String serialNo { get; set; }
        // 文件名
        public String fileName { get; set; }
        // 路面类型
        public String roadType { get; set; }
        // 龟裂㎡（轻）
        public String lightCrack { get; set; }
        // 龟裂㎡（中）
        public String NormalCrack { get; set; }
        // 龟裂㎡（重）
        public String severeCrack { get; set; }
        // 块状裂缝㎡（轻）
        public String lightBlockRip { get; set; }
        // 块状裂缝（重）
        public String severeBlockRip{ get; set; }
        // 纵向裂缝m（轻）
        public String lightVerticalRip{ get; set; }
        // 纵向裂缝m（重）
        public String severeVerticalRip{ get; set; }
        // 横向裂缝m（轻）
        public String lightHorizontalRip{ get; set; }
        // 横向裂缝m（重）
        public String severeHorizontalRip{ get; set; }
        // 坑槽㎡（轻）
        public String lightPit{ get; set; }
        // 坑槽㎡（重）
        public String severePit{ get; set; }
        // 松散㎡（轻）
        public String lightLoose{ get; set; }
        // 松散㎡（重）
        public String severeLoose{ get; set; }
        // 沉陷㎡（轻）
        public String lightSink{ get; set; }
        // 沉陷㎡（重）
        public String severeSink{ get; set; }
        // 车辙m（轻重）
        public String lightRut{ get; set; }
        // 车辙m（轻重）
        public String severeRut{ get; set; }
        // 波浪拥包㎡（轻）
        public String lightWave{ get; set; }
        // 波浪拥包㎡（重）
        public String severeWave{ get; set; }
        // 泛油㎡
        public String weeping{ get; set; }
        // 修补㎡
        public String patch{ get; set; }

    }
}