using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Nyss_lab2_parser
{
    /// <summary>
    /// Логика взаимодействия для DataGridWindow.xaml
    /// </summary>
    public partial class DataGridWindow : Window
    {
        private static int firstIndex = 0;
        private static int lastIndex = 30;
        public static string delimiter = "#";
        private static List<RowDataObject> data = Parser.GetRows();
        private static List<RowDataObject> pageData = data.GetRange(firstIndex, 30);
        

        private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
            "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
        "Нарушение целостности", "Нарушение доступности"};
        public DataGridWindow()
        {
            InitializeComponent();
           
            this.tableData.ItemsSource = pageData;

        }

        private void SaveBase(object sender, RoutedEventArgs e)
        {
            ItemCollection items = this.tableData.Items;
           



            SaveFileDialog saveFileDialog = new SaveFileDialog();
            System.IO.FileStream fs;
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
            
                fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                if (fs != null)
                {
                    string data = "";
                    byte[] bytes = Encoding.UTF8.GetBytes(data);


                    foreach (var item in headers)
                    {

                        data = $"{item}{delimiter}";
                        bytes = Encoding.UTF8.GetBytes(data);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    foreach (var item in items)
                    {

                        data = $"\n{((RowDataObject)item).Id}{delimiter}{((RowDataObject)item).Name}{delimiter}{((RowDataObject)item).Description}{delimiter}" +
                            $"{((RowDataObject)item).Source}{delimiter}{((RowDataObject)item).Object}{delimiter}{((RowDataObject)item).Сonfidentiality}{delimiter}{((RowDataObject)item).Integrity}{delimiter}{((RowDataObject)item).Access}";
                        bytes = Encoding.UTF8.GetBytes(data);
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    fs.Close();

                    MessageBox.Show("Saved");
                }
                



            }
                
        }

        private void GoFirstPage(object sender, RoutedEventArgs e)
        {
            lastIndex = 30;
            firstIndex = 0;
            SetPage(firstIndex, lastIndex);
        }

        private void GoPrevPage(object sender, RoutedEventArgs e)
        {
            lastIndex = firstIndex;
            firstIndex -= 30;
            if (firstIndex < 0)
            {
                firstIndex = 0;
                lastIndex =  30;
                SetPage(firstIndex, 30);
            }
            else
            {
                SetPage(firstIndex, 30);
            }
        }

        private void GoNextPage(object sender, RoutedEventArgs e)
        {
            firstIndex = lastIndex;
            lastIndex += 30;
          
            if (lastIndex > data.Count)
            {
               
                lastIndex = data.Count;
                firstIndex = data.Count - 30;
                SetPage(firstIndex, 30);
             
            }
            else if (lastIndex <= data.Count && lastIndex > 0)
            {
                
                SetPage(firstIndex, 30);
            }
            
            
        }

        private void GoLastPage(object sender, RoutedEventArgs e)
        {
            firstIndex = data.Count - 30;
            lastIndex = data.Count;
            SetPage(firstIndex, 30);
        }
        private void SetPage(int firstIndex, int step)
        {
            pageData = data.GetRange(firstIndex, step);
            this.tableData.ItemsSource = pageData;
            this.tableData.Items.Refresh();
        }
    }
   
}
