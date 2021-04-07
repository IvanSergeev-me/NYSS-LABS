using System;
using System.Collections;
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
    /// Логика взаимодействия для DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
       /* private List<string[]> data = Parser.GetRows();
        private string[] headers = new string[] { "Идентификатор угрозы" , "Наименование угрозы" ,
            "Описание угрозы" , "Источник угрозы" , "Объект воздействия угрозы" ,"Нарушение конфиденциальности",
        "Нарушение целостности", "Нарушение доступности"};
        public DataWindow()
        {
            InitializeComponent();
            data_Table.RowGroups.Add(new TableRowGroup());
            data_Table.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = data_Table.RowGroups[0].Rows[0];
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 14;
            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            for (int i = 0; i < headers.Length; i++)
            {
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(headers[i]))));

            }
            SetData();
        }
        private void SetData()
        {
            foreach(var row in data)
            {
              
                int indexOfRow = data.IndexOf(row) + 1;
                //тут либо парсить до длины хедера и нналл значения заменять на ""
                //либо сдвигать налл элементы
                data_Table.RowGroups[0].Rows.Add(new TableRow());
                TableRow currentRow = data_Table.RowGroups[0].Rows[indexOfRow];
                currentRow.Background = Brushes.WhiteSmoke;
                currentRow.FontSize = 12;
                currentRow.FontWeight = System.Windows.FontWeights.Bold;
                for (int i = 0; i < row.Length; i++)
                {

                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(row[i]))));
                }
               
            }
        }*/
    }
}
