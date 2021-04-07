using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<DataObject> data = Parser.GetRows();
        private static ObservableCollection<DataObject> list = new ObservableCollection<DataObject>();
        private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
            "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
        "Нарушение целостности", "Нарушение доступности"};
        public DataGridWindow()
        {
            InitializeComponent();
            SynchArrays();
            foreach (var item in list)
            {
                MessageBox.Show(item.ToString());
            }
           
            this.tableData.ItemsSource = list;

        }
        private void SynchArrays()
        {
            foreach(var item in data)
            {
                list.Add(item);
            }
        }
    }
   
}
