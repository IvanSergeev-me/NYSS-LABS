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
        public static string delimiter = "#";
        private List<RowDataObject> data = Parser.GetRows();
        private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
            "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
        "Нарушение целостности", "Нарушение доступности"};
        public DataGridWindow()
        {
            InitializeComponent();
            
            this.tableData.ItemsSource = data;

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
      
    }
   
}
