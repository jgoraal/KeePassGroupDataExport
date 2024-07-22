using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace KeePassGroupDataExport
{
    /// <summary>
    /// Klasa odpowiedzialna za tworzenie pliku Excel z danymi komputerów.
    /// </summary>
    internal class ExcelFileDataExporter
    {
        private string Path { get; set; }
        private int DataRows { get; set; }
        private int DataCols { get; set; }
        private List<string> ExportKeys { get; set; }

        public ExcelFileDataExporter(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Tworzy plik Excel z danymi komputerów.
        /// </summary>
        public void CreateExcelFile(List<ComputerData> computersData)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataRows = computersData.Count;
            DataCols = ComputerData.ExportKeys.Count;
            ExportKeys = ComputerData.ExportKeys.ToList();

            MessageCreator.CreateWarningMessage(string.Join(", ", ExportKeys));

            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Zestawienie sprzętu");

                InsertKeysToSheet(sheet);

                InsertDataToSheet(sheet, computersData);

                sheet.Cells["A:XFD"].AutoFitColumns();

                var excelFile = new FileInfo(Path);
                package.SaveAs(excelFile);
            }
        }

        /// <summary>
        /// Wstawia klucze do arkusza Excel.
        /// </summary>
        private void InsertKeysToSheet(ExcelWorksheet sheet)
        {
            for (var col = 1; col < DataCols + 2; col++)
            {
                var cell = sheet.Cells[1, col];
                cell.Value = col == 1 ? "L.p." : ExportKeys[col - 2];

                var cellStyle = cell.Style;
                cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                cellStyle.Font.Bold = true;
                cellStyle.Font.Name = "Arial";
                cellStyle.Fill.BackgroundColor.SetColor(Color.LightGreen);
                cellStyle.Font.Color.SetColor(Color.Black);
                cellStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cellStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
                cellStyle.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            }

            // Ustawienie wartości w pierwszej kolumnie (L.P)
            for (var i = 2; i <= DataRows + 1; i++)
            {
                var cell = sheet.Cells[$"A{i}"];
                cell.Value = $"{i - 1}";
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.Font.Name = "Arial";
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            }
        }

        /// <summary>
        /// Wstawia dane do arkusza Excel.
        /// </summary>
        private void InsertDataToSheet(ExcelWorksheet sheet, List<ComputerData> data)
        {
            for (int i = 0; i < DataRows; i++)
            {
                for (int j = 0; j < DataCols; j++)
                {
                    var cell = sheet.Cells[i + 2, j + 2];
                    cell.Value = data[i].ExportOrderData[ExportKeys[j]];
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin,Color.Black);
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
            }
        }
    }
}
