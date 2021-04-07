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
        public static string PATH_TO_FILE = Path.GetFullPath(PATH_TO_SAVE);

        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void CreateLocalBase(object sender, RoutedEventArgs e)
        {
            
            WebClient myWebClient = new WebClient();
       
            myWebClient.DownloadFile(URL_TO_BASE, PATH_TO_SAVE);
     
            if (File.Exists(PATH_TO_SAVE)) label_Debug.Content = "Локальная база данных успешно загружена.";
            else label_Debug.Content = "Не удалось загрузить базу данных из Сети.";

            new Parser(PATH_TO_FILE, false);
            this.Close();


        }
        private void ParseBase (object sender, RoutedEventArgs e)
        {
            if (!File.Exists(PATH_TO_SAVE)) label_Debug.Content = "База не обнаружена. Попробуйте сначала загрузить новую базу.";
            else
            {
                new Parser(PATH_TO_FILE, false);

                this.Close();
            }
            
        }
        private void LookMinifised(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(PATH_TO_SAVE)) label_Debug.Content = "База не обнаружена. Попробуйте сначала загрузить новую базу.";
            else
            {
                new Parser(PATH_TO_FILE, true);

                this.Close();
            }
        }


    }
}
