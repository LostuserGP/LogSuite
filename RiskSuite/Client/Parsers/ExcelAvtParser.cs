using LogSuite.Client.Services;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Parsers
{
    public class ExcelAvtParser
    {
        private ToastService _toastService { get; set; }
        private IBrowserFile _file;
        private ExcelWorksheet sheet;
        private string filename;
        private DateTime DateReport;
        private IEnumerable<GisDTO> _gisList;
        private List<ReviewValueInputDTO> valueList = new List<ReviewValueInputDTO>();
        private FileTypeSettingDTO _settings;

        public ExcelAvtParser(IBrowserFile file, IEnumerable<GisDTO> gisList, FileTypeSettingDTO settings, ToastService toastService)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _file = file;
            _gisList = gisList;
            _settings = settings;
            _toastService = toastService;
        }

        public async Task<List<ReviewValueInputDTO>> GetResult()
        {
            await Parse();
            return valueList;
        }

        private async Task Parse()
        {
            var excelPackage = new ExcelPackage();
            var stream = _file.OpenReadStream(10000000);
            await excelPackage.LoadAsync(stream);
            sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
            filename = _file.Name;
            int endRow = sheet.Dimension.End.Row;
            int endCol = sheet.Dimension.End.Column;
            var dateReport = StringParser.GetFirstDateFromString(filename);
            if (dateReport == null)
            {
                _toastService.ShowToast("В результате парсинга файла " + filename + " не удалось установить дату", Helpers.ToastLevel.Error);
                return;
            }
            DateReport = dateReport.Value;
            DateTime StartMonth = new DateTime(DateReport.Year, DateReport.Month, 1);
            //берем Турецкий поток
            var gisTurkey = _gisList.Where(x => x.Names.Where(n => n.Name.ToLower().Equals("турецкий поток")).Any()).FirstOrDefault();
            if (gisTurkey == null)
            {
                _toastService.ShowToast("В БД не обнаружен Турецкий поток", Helpers.ToastLevel.Error);
                return;
            }
            var gisBlue = _gisList.Where(x => x.Names.Where(n => n.Name.ToLower().Equals("голубой поток")).Any()).FirstOrDefault();
            if (gisBlue == null)
            {
                _toastService.ShowToast("В БД не обнаружен Голубой поток", Helpers.ToastLevel.Error);
                return;
            }

            var entry = AvtFindDateEntry(sheet);
            int startRow = entry[0];
            int startCol = entry[1];
            for (int row = 1; row <= startRow; row++)
            {
                for (int col = startCol; col <= endCol; col++)
                {
                    var cellText = sheet.Cells[row, col].Text;
                    if (!String.IsNullOrEmpty(cellText))
                    {
                        // проверяем не страна ли нам попалась в ячейке
                        var country = gisBlue.Countries.Where(c => c.Country.Names.Where(n => n.Name.ToLower().Equals(cellText.ToLower())).Any()).FirstOrDefault();
                        if (country == null)
                        {
                            country = gisTurkey.Countries.Where(c => c.Country.Names.Where(n => n.Name.ToLower().Equals(cellText.ToLower())).Any()).FirstOrDefault();
                        }
                        if (country != null)
                        {
                            //ищем точку входа
                            int valueCol = AvtFindValueEntry(sheet, row, col, startRow - 1);
                            //листаем строки со значениями по найденной стране
                            for (int i = startRow; i <= startRow + 31; i++)
                            {
                                try
                                {
                                    var dateCell = sheet.Cells[i, startCol];
                                    if (!dateCell.Style.Numberformat.Format.Equals("mm-dd-yy"))
                                    {
                                        break;
                                    }
                                    var date = dateCell.GetValue<DateTime>();
                                    var valueCell = sheet.Cells[i, valueCol];
                                    if (String.IsNullOrWhiteSpace(valueCell.Text) || date.Date > DateReport.Date)
                                    {
                                        break;
                                    }
                                    var value = valueCell.GetValue<Double>();
                                    value = value / 1000d;
                                    valueList.Add(new ReviewValueInputDTO()
                                    {
                                        GisId = gisBlue.Id,
                                        ValueId = country.Id,
                                        ReportDate = DateReport,
                                        inType = ReviewValueInputDTO.InputType.Country,
                                        valType = ReviewValueInputDTO.ValueType.Fact,
                                        Value = value
                                    });
                                }
                                catch (Exception ex)
                                {
                                    _toastService.ShowToast(ex.Message, Helpers.ToastLevel.Error);
                                }
                            }
                        }
                    }
                }
            }
            excelPackage.Dispose();
        }

        private int[] AvtFindDateEntry(ExcelWorksheet sheet)
        {
            int[] result = new int[2];
            result[0] = 0;
            for (int row = 1; row <= sheet.Dimension.End.Row; row++)
            {
                for (int col = 1; col <= sheet.Dimension.End.Column; col++)
                {
                    var cell = sheet.Cells[row, col];
                    var cellNextRow = sheet.Cells[row + 1, col];
                    if (cell.Style.Numberformat.Format.Equals("mm-dd-yy") && cellNextRow.Style.Numberformat.Format.Equals("mm-dd-yy"))
                    {
                        result[0] = row;
                        result[1] = col;
                        return result;
                    }
                }
            }
            return result;
        }

        private int AvtFindValueEntry(ExcelWorksheet sheet, int startRow, int startCol, int endRow)
        {
            int endCol = startCol + 2;
            if (endCol > sheet.Dimension.End.Column) endCol = sheet.Dimension.End.Column;
            for (int row = startRow; row <= endRow; row++)
            {
                for (int col = startCol; col <= endCol; col++)
                {
                    var cellText = sheet.Cells[row, col].Text;
                    if (StringParser.NameEqualsAnyList(_settings.FactValueEntry, cellText))
                    {
                        return col;
                    }
                }
            }
            return 0;
        }
    }
}
