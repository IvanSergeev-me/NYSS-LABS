
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Nyss_lab2_parser
{
    public class Parser
    {
        
        private static List<RowDataObject> rows = new List<RowDataObject>();

       
        public string PathFile { get; set; }
        private _Application excel = new _Excel.Application();
        private Workbook wb;
        private Worksheet ws;
        public Preloader p;
        public Parser(string path)
        {
            p = new Preloader();
            p.Show();
            PathFile = path;
            try{
                int rowStart = 3;
                int colStart = 1;
                int countCols = 10;
                int countRows = 222;
                //10 x 222
                
                wb = excel.Workbooks.Open(path);
               
                ws = wb.Worksheets[1];
          
                List<string> newRow = new List<string>();
                for (int i = rowStart; i < countRows; i++)
                {
                    newRow.Clear();
                    for (int j = colStart; j < countCols; j++)
                    {
                        newRow.Add(ws.Cells[i, j].Value2.ToString());

                    }
                    rows.Add(new RowDataObject(newRow.ToArray()[0], newRow.ToArray()[1], newRow.ToArray()[2], newRow.ToArray()[3], newRow.ToArray()[4], newRow.ToArray()[5], newRow.ToArray()[6], newRow.ToArray()[7]));

                }

                OpenParsed();

                excel.Quit();
                p.Close();
            }
            catch
            {
                
            }
            
        }
        private static void  OpenParsed()
        {
            DataGridWindow win2 = new DataGridWindow();
            win2.Show();
        }
        public static List<RowDataObject> GetRows()
        { return rows; }
        private static void SetRows(ArrayList value)
        { SetRows(value); }
    }
}
