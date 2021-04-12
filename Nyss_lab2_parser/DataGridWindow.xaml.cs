using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
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
        public static bool IsOpened { get; set; } = false;

        private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
            "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
        "Нарушение целостности", "Нарушение доступности"};
        public DataGridWindow()
        {
            InitializeComponent();
            IsOpened = true;
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
                            $"{((RowDataObject)item).Source}{delimiter}{((RowDataObject)item).Object}{delimiter}{((RowDataObject)item).Confidentiality}{delimiter}{((RowDataObject)item).Integrity}{delimiter}{((RowDataObject)item).Access}";
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

    
        private void UpdateBase(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RowDataObject> distinctData;
                List<RowDataObject> oldData = data;
           
                
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(MainWindow.URL_TO_BASE, MainWindow.PATH_TO_SAVE);
                new Parser(MainWindow.PATH_TO_FILE, false);
                List<RowDataObject> newData = Parser.GetRows();
            
                
                distinctData = GetDistinctData(oldData, newData);
                data = newData;
                pageData = data.GetRange(0, 30);
                tableData.Items.Refresh();
                MessageBoxButton answer = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show($"Обновление прошло успешно. Всего строк изменено: {distinctData.Count} Хотите увидеть отчёт об обновлении?", " Посмотреть отчет?", answer);
                if (result == MessageBoxResult.Yes)
                {
                    UpdateReport updateReport = new UpdateReport();
                    updateReport.SetData(distinctData);
                    updateReport.Show();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            
        }
      
        private List<RowDataObject> GetDistinctData(List<RowDataObject> oldList ,List<RowDataObject> newList)
        {
            //string id, string name, string description, string source, string @object, string сonfidentiality, string integrity, string access
            List<RowDataObject> distinctData = new List<RowDataObject>();
            string itIsNew = "Этот ряд совершенно новый: ";
            string notChanged = "Нет изменений: ";
           
            if (oldList.Count > newList.Count)
            {
                MessageBox.Show(oldList.Count.ToString() + " aaa " + newList.Count.ToString());
                for (int i = 0; i < newList.Count; i++)
                {

                    bool isRowChanged = false;
                    string rowId = notChanged + oldList[i].Id;
                    string rowName = notChanged + oldList[i].Name;
                    string rowDesc = notChanged + oldList[i].Description;
                    string rowSrc = notChanged + oldList[i].Source;
                    string rowObj = notChanged + oldList[i].Object;
                    string rowConf = notChanged + oldList[i].Confidentiality;
                    string rowInt = notChanged + oldList[i].Integrity;
                    string rowAccess = notChanged + oldList[i].Access;
                    RowDataObject row = new RowDataObject(rowId, rowName, rowDesc, rowSrc, rowObj, rowConf, rowInt,rowAccess);

                    if (oldList[i].Id != newList[i].Id)
                    {
                        row.Id = $"Было: {oldList[i].Id} - Стало: {newList[i].Id}";
                        isRowChanged = true;
                    }

                    if (oldList[i].Name != newList[i].Name)
                    {
                        row.Name = $"Было: {oldList[i].Name} - Стало: {newList[i].Name}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Description != newList[i].Description)
                    {
                        row.Description = $"Было: {oldList[i].Description} - Стало: {newList[i].Description}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Source != newList[i].Source)
                    {
                        row.Source = $"Было: {oldList[i].Source} - Стало: {newList[i].Source}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Object != newList[i].Object)
                    {
                        row.Object = $"Было: {oldList[i].Object} - Стало: {newList[i].Object}";
                        isRowChanged = true;
                    }

                    if (oldList[i].Confidentiality != newList[i].Confidentiality)
                    {
                        row.Confidentiality = $"Было: {oldList[i].Confidentiality} - Стало: {newList[i].Confidentiality}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Integrity != newList[i].Integrity)
                    {
                        row.Integrity = $"Было: {oldList[i].Integrity} - Стало: {newList[i].Integrity}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Access != newList[i].Access)
                    {
                        row.Access = $"Было: {oldList[i].Access} - Стало: {newList[i].Access}";
                        isRowChanged = true;
                    }
                    if (isRowChanged)
                    {
                        distinctData.Add(row);
                    }
                }
            }
            else
            {
                for (int i = 0; i < oldList.Count; i++)
                {

                    bool isRowChanged = false;
                    string rowId = notChanged + oldList[i].Id;
                    string rowName = notChanged + oldList[i].Name;
                    string rowDesc = notChanged + oldList[i].Description;
                    string rowSrc = notChanged + oldList[i].Source;
                    string rowObj = notChanged + oldList[i].Object;
                    string rowConf = notChanged + oldList[i].Confidentiality;
                    string rowInt = notChanged + oldList[i].Integrity;
                    string rowAccess = notChanged + oldList[i].Access;
                    RowDataObject row = new RowDataObject(rowId, rowName, rowDesc, rowSrc, rowObj, rowConf, rowInt, rowAccess);

                    if (oldList[i].Id != newList[i].Id)
                    {
                        row.Id = $"Было: {oldList[i].Id} - Стало: {newList[i].Id}";
                        isRowChanged = true;
                    }

                    if (oldList[i].Name != newList[i].Name)
                    {
                        row.Name = $"Было: {oldList[i].Name} - Стало: {newList[i].Name}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Description != newList[i].Description)
                    {
                        row.Description = $"Было: {oldList[i].Description} - Стало: {newList[i].Description}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Source != newList[i].Source)
                    {
                        row.Source = $"Было: {oldList[i].Source} - Стало: {newList[i].Source}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Object != newList[i].Object)
                    {
                        row.Object = $"Было: {oldList[i].Object} - Стало: {newList[i].Object}";
                        isRowChanged = true;
                    }

                    if (oldList[i].Confidentiality != newList[i].Confidentiality)
                    {
                        row.Confidentiality = $"Было: {oldList[i].Confidentiality} - Стало: {newList[i].Confidentiality}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Integrity != newList[i].Integrity)
                    {
                        row.Integrity = $"Было: {oldList[i].Integrity} - Стало: {newList[i].Integrity}";
                        isRowChanged = true;
                    }
                    if (oldList[i].Access != newList[i].Access)
                    {
                        row.Access = $"Было: {oldList[i].Access} - Стало: {newList[i].Access}";
                        isRowChanged = true;
                    }
                    if (isRowChanged)
                    {
                        distinctData.Add(row);
                    }

                }
                if (newList.Count > oldList.Count)
                {
                    for (int i = oldList.Count; i < newList.Count; i++)
                    {
                        
                        RowDataObject row = new RowDataObject(itIsNew + newList[i].Id, newList[i].Name, newList[i].Description, newList[i].Source, newList[i].Object, newList[i].Confidentiality, newList[i].Integrity, newList[i].Access);
                        distinctData.Add(row);
                    }
                }
            }
            
            return distinctData;
        }
    }
   
}
