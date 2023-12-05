using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Xml;
using System.Drawing;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using Image = iText.Layout.Element.Image;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Colors;
using System.Text.RegularExpressions;
using Rectangle = iText.Kernel.Geom.Rectangle;
using iText.Layout.Borders;
using System.Text;
using System.Drawing.Imaging;
using iText.Kernel.Exceptions;
using _2023._08_Uni_Scheduler.Domain.Entities;
using ClosedXML.Excel;
using System.Data;


namespace _2023._08_Uni_Scheduler.Configuration
{
    public class Exportation
    {
        public static string toExcelWithPath(DataGridView dataGridView, string filename)
        {

            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename + "_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".xlsx");

            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                Microsoft.Office.Interop.Excel._Worksheet excelWorksheet = excelWorkbook.Sheets[1];

                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    excelWorksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {                    
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        string cellValue = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        if (cellValue != null && cellValue.StartsWith("R$"))
                        {
                            cellValue = cellValue.Replace("R$", "").Trim();
                            if (double.TryParse(cellValue, out double numericValue))
                            {
                                excelWorksheet.Cells[i + 2, j + 1] = numericValue;
                            }
                        }
                        else
                        {
                            excelWorksheet.Cells[i + 2, j + 1] = cellValue;
                        }
                    }

                }

                Microsoft.Office.Interop.Excel.Range cellRange = excelWorksheet.Range["A1", excelWorksheet.Cells[dataGridView.Rows.Count + 1, dataGridView.Columns.Count]];
                Microsoft.Office.Interop.Excel.ListObject table = excelWorksheet.ListObjects.AddEx(Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange, cellRange);

                table.ShowTotals = true;

                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    bool isCurrencyColumn = false;

                    // Verifica se a coluna possui o símbolo "R$" no início de qualquer célula
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        string cellValue = dataGridView.Rows[j].Cells[i].Value?.ToString();
                        if (cellValue != null && cellValue.StartsWith("R$"))
                        {
                            isCurrencyColumn = true;
                            break;
                        }
                    }

                    if (isCurrencyColumn)
                    {
                        Microsoft.Office.Interop.Excel.Range columnRange = excelWorksheet.Range[excelWorksheet.Cells[2, i + 1], excelWorksheet.Cells[dataGridView.Rows.Count + 1, i + 1]];
                        columnRange.NumberFormat = "$ #,##0.00";

                        table.ListColumns[i + 1].TotalsCalculation = Microsoft.Office.Interop.Excel.XlTotalsCalculation.xlTotalsCalculationSum;

                        // Define o formato de número das células de total para moeda
                        table.ListColumns[i + 1].Range.Cells[dataGridView.Rows.Count + 2].NumberFormat = "R$ #,##0.00";
                    }

                }


                excelWorkbook.SaveAs(path);
                excelWorkbook.Close();
                excelApp.Quit();
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                //CustomMessage.Sucess();
            }

            return path;
        }
        public static string toXmlWithPath(DataGridView dataGridView, string filename)
        {
            try
            {
                // Obtém o caminho para a pasta temp e adiciona o nome do arquivo
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename + ".xml");

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t",
                    NewLineChars = Environment.NewLine,
                    NewLineHandling = NewLineHandling.Replace
                };

                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Rows");

                    foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
                    {
                        if (!dataGridViewRow.IsNewRow) // Ignore the new row used for adding data
                        {
                            writer.WriteStartElement("Row");

                            foreach (DataGridViewCell cell in dataGridViewRow.Cells)
                            {
                                writer.WriteStartElement(dataGridView.Columns[cell.ColumnIndex].Name.replaceSpecialCharacters().Replace(" ", string.Empty));
                                if (cell.Value != null)
                                {
                                    writer.WriteString(cell.Value?.ToString());
                                }
                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();
                        }
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                return path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static string toJsonWithPath(DataGridView dataGridView, string filename)
        {
            // Obtém o caminho para a pasta temp e adiciona o nome do arquivo
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename + ".json");

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataGridViewCell cell in dataGridViewRow.Cells)
                {
                    row[dataGridView.Columns[cell.ColumnIndex].Name] = cell.Value;
                }
                rows.Add(row);
            }

            string json = JsonConvert.SerializeObject(rows, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, json);            
            return path;
        }

        public static string toExcelWithPath(DataTable dataTable, string filename)
        {
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename + "_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".xlsx");

            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = null;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelWorkbook = excelApp.Workbooks.Add();
                Microsoft.Office.Interop.Excel._Worksheet excelWorksheet = excelWorkbook.Sheets[1];

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    excelWorksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        var cellValue = dataTable.Rows[i][j]?.ToString();
                        if (cellValue != null && cellValue.StartsWith("R$"))
                        {
                            cellValue = cellValue.Replace("R$", "").Trim();
                            if (double.TryParse(cellValue, out double numericValue))
                            {
                                excelWorksheet.Cells[i + 2, j + 1] = numericValue;
                            }
                        }
                        else
                        {
                            excelWorksheet.Cells[i + 2, j + 1] = cellValue;
                        }
                    }
                }

                Microsoft.Office.Interop.Excel.Range cellRange = excelWorksheet.Range["A1", excelWorksheet.Cells[dataTable.Rows.Count + 1, dataTable.Columns.Count]];
                Microsoft.Office.Interop.Excel.ListObject table = excelWorksheet.ListObjects.AddEx(Microsoft.Office.Interop.Excel.XlListObjectSourceType.xlSrcRange, cellRange);
                table.ShowTotals = true;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    bool isCurrencyColumn = false;

                    // Verifica se a coluna possui o símbolo "R$" no início de qualquer célula
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        string cellData = dataTable.Rows[j][i]?.ToString();
                        if (cellData != null && cellData.StartsWith("R$"))
                        {
                            isCurrencyColumn = true;
                            break;
                        }
                    }

                    if (isCurrencyColumn)
                    {
                        Microsoft.Office.Interop.Excel.Range columnRange = excelWorksheet.Range[excelWorksheet.Cells[2, i + 1], excelWorksheet.Cells[dataTable.Rows.Count + 1, i + 1]];
                        columnRange.NumberFormat = "$ #,##0.00";

                        table.ListColumns[i + 1].TotalsCalculation = Microsoft.Office.Interop.Excel.XlTotalsCalculation.xlTotalsCalculationSum;

                        // Define o formato de número das células de total para moeda
                        table.ListColumns[i + 1].Range.Cells[dataTable.Rows.Count + 2].NumberFormat = "R$ #,##0.00";
                    }
                }

                excelWorkbook.SaveAs(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excelWorkbook?.Close();
                excelApp?.Quit();

                //Libere os recursos COM. 
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }

            return path;
        }
        public static string toXmlWithPath(DataTable dataTable, string filename)
        {
            try
            {                
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename + ".xml");

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t",
                    NewLineChars = Environment.NewLine,
                    NewLineHandling = NewLineHandling.Replace
                };                    
                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Rows");


                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        writer.WriteStartElement("Row");

                        foreach (DataColumn column in dataTable.Columns)
                        {
                            writer.WriteStartElement(column.ColumnName.replaceSpecialCharacters().Replace(" ", string.Empty));
                            var cellValue = dataRow[column];
                            if (cellValue != null)
                            {
                                writer.WriteString(cellValue.ToString());
                            }
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                return path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }            
        }
        public static string toJsonWithPath(DataTable dataTable, string filename)
        {
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename + ".json");

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    row[column.ColumnName] = dataRow[column];
                }
                rows.Add(row);
            }

            string json = JsonConvert.SerializeObject(rows, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, json);
            return path;
        }


        public static byte[] toByteExcelFromArchives(List<Archive> dataTables)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (var dt in dataTables)
                {
                    wb.Worksheets.Add(dt.data, dt.description);
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] toByteExcelFromArchive(Archive archive)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(archive.data, archive.description);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
