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
        private void CheckBaseExists()
        {

            if (!File.Exists(PATH_TO_SAVE))
            {
                MessageBoxButton answer = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show("Файла базы данных не обнаружено.", " Выполнить загрузку?", answer);
                if(result == MessageBoxResult.Yes)
                {
                    DownloadBase();
                }
                else
                {
                    setErrorMessage(errorMessage);
                }
            }
            else
            {
                MessageBoxButton answer = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show("Базы данных обнаружена.", " Начать загрузку?", answer);
                if (result == MessageBoxResult.Yes)
                {
                    new Parser(PATH_TO_FILE, false);

                    this.Close();
                }
            }
        }
        private void CreateLocalBase(object sender, RoutedEventArgs e)
        {
            DownloadBase();
        }
        private void DownloadBase()
        {
            WebClient myWebClient = new WebClient();

            myWebClient.DownloadFile(URL_TO_BASE, PATH_TO_SAVE);

            if (File.Exists(PATH_TO_SAVE)) setErrorMessage("Локальная база данных успешно загружена.");
            else setErrorMessage("Не удалось загрузить базу данных из Сети.");

            new Parser(PATH_TO_FILE, false);
            this.Close();
        }
        private void ParseBase (object sender, RoutedEventArgs e)
        {
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
			if(openFileDialog.ShowDialog() == true)
				txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            */
            /*
            if (!File.Exists(PATH_TO_SAVE)) setErrorMessage(errorMessage);
             else
             {
                 new Parser(PATH_TO_FILE, false);

                 this.Close();
             }
            */
            Parse(false);


        }
        private void LookMinifised(object sender, RoutedEventArgs e)
        {
            Parse(true);
        }
        private void Parse(bool isMinified)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx|Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                
                new Parser(openFileDialog.FileName, isMinified);

                this.Close();
            }

        }
        private void setErrorMessage(string message)
        {
            label_Debug.Content = message;
        }

    }
}
