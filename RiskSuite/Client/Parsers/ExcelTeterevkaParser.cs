using LogSuite.Client.Services;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Parsers
{
    public class ExcelTeterevkaParser
    {
        private ToastService _toastService { get; set; }
        private IBrowserFile _file;
        private ISheet sheet;
        private string filename;
        private DateTime DateReport;
        private IEnumerable<GisDTO> _gisList;
        private List<ReviewValueInputDTO> valueList = new List<ReviewValueInputDTO>();
        private FileTypeSettingDTO _settings;
        private int requestedCol = 0;
        private int allocatedCol = 0;
        private int estimatedCol = 0;
        private int factCol = 0;
        private int countryCol = 0;

        public ExcelTeterevkaParser(IBrowserFile file, IEnumerable<GisDTO> gisList, FileTypeSettingDTO settings, ToastService toastService)
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
            valueList = SplitExcelValues(valueList);
            return valueList;
        }

        private async Task Parse()
        {
            filename = _file.Name;
            GisDTO gis = _gisList.Where(x => x.Name.ToLower().Equals("тетеревка")).FirstOrDefault();
            if (gis == null)
            {
                _toastService.ToastError("В БД не найден ГИС Тетеревка");
                return;
            }
            var dateReport = StringParser.GetFirstDateFromString(filename);
            if (dateReport == null)
            {
                _toastService.ShowToast("В результате парсинга файла " + filename + " не удалось установить дату", Helpers.ToastLevel.Error);
                return;
            }
            MemoryStream ms = new MemoryStream();
            var stream = _file.OpenReadStream(_file.Size);
            await stream.CopyToAsync(ms);
            stream.Close();
            ms.Position = 0;
            HSSFWorkbook xssWorkbook = new HSSFWorkbook(ms, true);
            sheet = xssWorkbook.GetSheetAt(0);
            DateReport = dateReport.Value;
            var revisionTime = DateReport.AddHours(12);
            if (filename.Contains("perv"))
            {
                requestedCol = FindColumnEntry(_settings.RequestedValueEntry);
                allocatedCol = FindColumnEntry(_settings.AllocatedValueEntry);
            }
            else if (filename.Contains("utoch"))
            {
                estimatedCol = FindColumnEntry(_settings.EstimatedValueEntry);
            }
            else if (filename.Contains("fakt"))
            {
                factCol = FindColumnEntry(_settings.FactValueEntry);
            }
            countryCol = FindColumnEntry(_settings.CountryEntry);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) break;
                var cell = row.GetCell(countryCol);
                if (cell == null) break;
                if (!String.IsNullOrEmpty(cell.ToString()))
                {
                    GetCountryValue(gis, i);
                }
            }
            xssWorkbook.Close();
            ms.Dispose();
        }

        private bool GetCountryValue(GisDTO gis, int row)
        {
            var Row = sheet.GetRow(row);
            string cellText = Row.GetCell(countryCol).ToString();
            if (string.IsNullOrWhiteSpace(cellText)) return false;
            var gc = gis.Countries.Where(x => x.Country.Names.Where(n => StringParser.StrictLike(n.Name, cellText)).Any()).FirstOrDefault();
            if (gc != null)
            {
                try
                {
                    if (requestedCol > 0)
                    {
                        var cell = Row.GetCell(requestedCol);
                        var val = cell.NumericCellValue;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Requsted,
                            Value = val
                        });
                    }
                    if (allocatedCol > 0)
                    {
                        var cell = Row.GetCell(allocatedCol);
                        var val = cell.NumericCellValue;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Allocated,
                            Value = val
                        });
                    }
                    if (estimatedCol > 0)
                    {
                        var cell = Row.GetCell(estimatedCol);
                        var val = cell.NumericCellValue;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Estimated,
                            Value = val
                        });
                    }
                    if (factCol > 0)
                    {
                        var cell = Row.GetCell(factCol);
                        var val = cell.NumericCellValue;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Fact,
                            Value = val
                        });
                    }
                }
                catch (Exception ex)
                {
                    _toastService.ShowToast(ex.Message, Helpers.ToastLevel.Error);
                }
                return true;
            }
            return false;
        }

        private int FindColumnEntry(List<string> names)
        {
            if (names == null) return 0;
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                for (int col = 0; col <= row.LastCellNum; col++)
                {
                    var cell = row.GetCell(col);
                    if (cell == null) continue;
                    if (!String.IsNullOrEmpty(cell.ToString()))
                    {
                        if (StringParser.NameEqualsAnyList(names, cell.ToString()))
                        {
                            return col;
                        }
                    }
                }
            }
            return 0;
        }

        public List<ReviewValueInputDTO> SplitExcelValues(List<ReviewValueInputDTO> spisok)
        {
            var result = new List<ReviewValueInputDTO>();
            foreach (var item in spisok)
            {
                ReviewValueInputDTO value = result.Where(x => x.LikeValue(item)).FirstOrDefault();
                if (value != null)
                {
                    value.Value = value.Value + item.Value;
                }
                else
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
