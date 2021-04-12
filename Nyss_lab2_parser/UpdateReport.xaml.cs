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
using System.Windows.Shapes;

namespace Nyss_lab2_parser
{
    /// <summary>
    /// Логика взаимодействия для UpdateReport.xaml
    /// </summary>
    public partial class UpdateReport : Window
    {
        public UpdateReport()
        {
            InitializeComponent();
        }
        public void SetData(List<RowDataObject> list)
        {
            this.tableData.ItemsSource = list;
            this.tableData.Items.Refresh();
            
        }
    }
}
