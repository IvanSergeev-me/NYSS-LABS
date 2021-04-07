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
        private List<RowDataObject> data = Parser.GetRows();
        //private static ObservableCollection<RowDataObject> list = new ObservableCollection<RowDataObject>();
        private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
            "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
        "Нарушение целостности", "Нарушение доступности"};
        public DataGridWindow()
        {
            InitializeComponent();
            //SynchArrays();
            
            this.tableData.ItemsSource = data;
           // MessageBox.Show(tableData.Columns.Count.ToString());
            //tableData.Columns.Count;

        }

        private void SaveBase(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Saved");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //if (saveFileDialog.ShowDialog() == true)
                //File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
        }
        /*private void SynchArrays(){foreach(var item in data){list.Add(item);}}*/
    }
   
}
