
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

        private static List<RowDataObject> rows;

       
        public string PathFile { get; set; }
        private _Application excel = new _Excel.Application();
        private Workbook wb;
        private Worksheet ws;
        private static int rowStart = 3;
        private static int colStart = 1;
        private static int countCols = 10;
        private static int countMinifiedCols = 3;
        private static int countRows = 10;
        private bool isMinified = false;
        public Preloader p;
        public Parser(string path, bool isMinified)
        {
            p = new Preloader();
            p.Show();
            rows = new List<RowDataObject>();
            PathFile = path;
            this.isMinified = isMinified;
            try
            {
               
                //10 x 222
                
                wb = excel.Workbooks.Open(path);
               
                ws = wb.Worksheets[1];
                countRows = ws.Rows.CurrentRegion.EntireRow.Count;
                countCols = ws.Rows.CurrentRegion.EntireColumn.Count;
                string fileExtention;
                fileExtention = PathFile.Split('.').Last();
                if (fileExtention == "xlsx" || fileExtention == "xls")
                {
                    if (!isMinified) ParseNormal();
                    else ParseMinified();
                }
                else if (fileExtention == "txt" || fileExtention == "csv")
                {
                    ParseCsv();
                }

                if (!DataGridWindow.IsOpened) OpenParsed();
                excel.Quit();
                p.Close();
            }
            catch
            {
                
            }
            
        }
        private void ParseNormal()
        {
            List<string> newRow = new List<string>();
            for (int i = rowStart; i <= countRows; i++)
            {
                newRow.Clear();
                for (int j = colStart; j < countCols; j++)
                {
                    if (ws.Cells[i, j].Value2 == null) MessageBox.Show("Aboba");
                    newRow.Add(ws.Cells[i, j].Value2.ToString());

                }
                
                string confidentiality = ConvertBinarToAnswer( newRow.ToArray()[5]);
                string integrity  = ConvertBinarToAnswer(newRow.ToArray()[6]);
                string access = ConvertBinarToAnswer(newRow.ToArray()[7]);
       
                rows.Add(new RowDataObject(newRow.ToArray()[0], newRow.ToArray()[1], newRow.ToArray()[2], newRow.ToArray()[3], newRow.ToArray()[4], confidentiality, integrity, access));

            }
        }
        private void ParseMinified()
        {
            List<string> newRow = new List<string>();
            for (int i = rowStart; i <= countRows; i++)
            {
                newRow.Clear();
                for (int j = colStart; j < countMinifiedCols; j++)
                {
                    newRow.Add(ws.Cells[i, j].Value2.ToString());

                }
                rows.Add(new RowDataObject("УБИ."+newRow.ToArray()[0], newRow.ToArray()[1], null,null,null,null, null,null));

            }
        }
        //In developing
        public void ParseCsv()
        {
            MessageBox.Show(PathFile);
            List<string> newRow = new List<string>();
            using (StreamReader sr = new StreamReader(PathFile))
            {
                if (sr.ReadLine() != null)
                {
                    MessageBox.Show(sr.ReadLine());
                }
                else
                {
                    MessageBox.Show("a");
                    // MessageBox.Show(sr.ReadLine());
                   
                }
                sr.Close();
            }

            MessageBox.Show(PathFile);
        }
        private string ConvertBinarToAnswer(string binar)
        {
            if (binar.All(char.IsDigit))
            {
                if (binar == "1") return "Да";
                else if (binar == "0") return "Нет";
                else return "NaN";
            }
            else return "NaN";
        }
       private static void OpenParsed()
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
