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
using Microsoft.Win32;

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
        private static string errorMessage = "База не обнаружена. Попробуйте сначала загрузить новую базу.";

        public MainWindow()
        {
            InitializeComponent();
            CheckBaseExists();
        }
        //Проверка наличия базы данных при входе
        private void CheckBaseExists()
        {

            if (!File.Exists(PATH_TO_SAVE))
            {
                MessageBoxButton answer = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show("Файла базы данных не обнаружено.", " Выполнить загрузку?", answer);
                if(result == MessageBoxResult.Yes)
                {
                    DownloadBase();
                    this.Close();
                }
                else{setErrorMessage(errorMessage);}
            }
            else
            {
                MessageBoxButton answer = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show("Базы данных обнаружена.", "Использовать существующую базу данных?", answer);
                if (result == MessageBoxResult.Yes)
                {
                    new Parser(PATH_TO_FILE, false);
                    this.Close();
                }
            }
        }
       
        private void DownloadBase()
        {
            WebClient myWebClient = new WebClient();

            myWebClient.DownloadFile(URL_TO_BASE, PATH_TO_SAVE);

            if (File.Exists(PATH_TO_SAVE)) setErrorMessage("Локальная база данных успешно загружена.");
            else setErrorMessage("Не удалось загрузить базу данных из Сети.");
            new Parser(PATH_TO_FILE, false);
            
        }
        //Кнопка
        private void CreateLocalBase(object sender, RoutedEventArgs e)
        {
            if (File.Exists(PATH_TO_SAVE))
            {
                setErrorMessage("База данных уже существует. Воспользуйтесь существующей.");
            }
            else
            {
                DownloadBase();

                this.Close();
            }
            
        }
        //Кнопка
        private void ParseBase (object sender, RoutedEventArgs e)
        {
            
            Parse(false);
           
            this.Close();
        }
        //Кнопка
        private void LookMinifised(object sender, RoutedEventArgs e)
        {
            Parse(true);
           
            this.Close();
        }
        private void Parse(bool isMinified)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx|Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {  
                new Parser(openFileDialog.FileName, isMinified); 
            }

        }
        private void setErrorMessage(string message)
        {
            label_Debug.Content = message;
        }

    }
}
