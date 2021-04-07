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
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Nyss_lab2_parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public static string URL_TO_BASE = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        public static string PATH_TO_SAVE = "../../localbase.xlsx" ;
        public static string PATH_TO_FILE = @"C:\Users\inovo\source\repos\Nyss_lab2_parser\Nyss_lab2_parser\localbase.xlsx";

        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void CreateLocalBase(object sender, RoutedEventArgs e)
        {
            
            WebClient myWebClient = new WebClient();
       
            myWebClient.DownloadFile(URL_TO_BASE, PATH_TO_SAVE);
     
            if (File.Exists("../../localbase.xlsx")) label_Debug.Content = "Локальная база данных успешно загружена.";
            else label_Debug.Content = "Не удалось загрузить базу данных из Сети.";

            new Parser(PATH_TO_FILE);
            this.Close();


        }
        private void ParseBase (object sender, RoutedEventArgs e)
        {
            if (!File.Exists("../../localbase.xlsx")) label_Debug.Content = "База не обнаружена. Попробуйте сначала загрузить новую базу.";
            else
            {
                new Parser(PATH_TO_FILE);

                this.Close();
            }
            
        }
       
    }
}
