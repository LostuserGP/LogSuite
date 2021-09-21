using LogSuite.Client.Services;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Parsers
{
    public class ExcelGeMailParser
    {
        private ToastService _toastService { get; set; }
        private IBrowserFile _file;
        private ExcelWorksheet sheet;
        private string filename;
        private DateTime DateReport;
        private IEnumerable<GisDTO> _gisList;
        private List<ReviewValueInputDTO> valueList = new List<ReviewValueInputDTO>();
        private FileTypeSettingDTO _settings;
        private int requestedCol = 0;
        private int allocatedCol = 0;
        private int estimatedCol = 0;
        private int countryCol = 0;
        private int gisCol = 0;

        public ExcelGeMailParser(IBrowserFile file, IEnumerable<GisDTO> gisList, FileTypeSettingDTO settings, ToastService toastService)
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
            var revisionTime = StringParser.GetDateWithTimeFromString(filename);
            if (DateReport.AddDays(-1).Date < revisionTime && revisionTime < DateReport.Date.AddHours(5))
            {
                requestedCol = FindColumnEntry(_settings.RequestedValueEntry);
                allocatedCol = FindColumnEntry(_settings.AllocatedValueEntry);
            }
            else
            {
                estimatedCol = FindColumnEntry(_settings.EstimatedValueEntry);
            }
            countryCol = FindColumnEntry(_settings.CountryEntry);
            gisCol = FindColumnEntry(_settings.GisEntry);
            if (countryCol == 0) countryCol = gisCol + 1;
            GisDTO gis = null;
            for (int row = 1; row <= endRow; row++)
            {
                var cellGisText = sheet.Cells[row, gisCol].Text;
                var cellCountryText = sheet.Cells[row, countryCol].Text;
                if (!String.IsNullOrEmpty(cellGisText))
                {
                    // проверяем не гис ли нам попался в ячейке
                    var isGis = _gisList.Where(x => x.Names.Where(n => StringParser.StrictLike(n.Name, cellGisText)).Any()).FirstOrDefault();
                    if (isGis != null)
                    {
                        //попался гис, делаем его текущим, выходим из цикла, чтобы начать обрабатывать следующую строку
                        gis = isGis;
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(cellCountryText))
                {                    
                    if (gis != null)
                    {
                        //проверяем не страна ли это и подщиваем по ней значения
                        if (GetCountryValue(gis, row)) continue;
                        //проверяем не аддон ли это и подшиваем по нему значения
                        if (GetAddonValue(gis, row)) continue;
                        //проверяем не закачка ли это и подшиваем по ней значения
                        if (GetInputValue(gis, row)) continue;
                        //проверяем не отбор ли это и подшиваем по нему значения
                        if (GetOutputValue(gis, row)) continue;
                    }
                }
                //обнуляем ГИС на пустой ячейке
                if (String.IsNullOrEmpty(cellGisText) && String.IsNullOrEmpty(cellCountryText))
                {
                    gis = null;
                }
            }
            excelPackage.Dispose();
        }

        private bool GetCountryValue(GisDTO gis, int row)
        {
            var cellText = sheet.Cells[row, countryCol].Text;
            var gc = gis.Countries.Where(x => x.Country.Names.Where(n => StringParser.StrictLike(n.Name, cellText)).Any()).FirstOrDefault();
            if (gc != null)
            {
                try
                {
                    if (requestedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Requsted,
                            Value = sheet.Cells[row, requestedCol].GetValue<double>()
                        });
                    }
                    if (allocatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Allocated,
                            Value = sheet.Cells[row, allocatedCol].GetValue<double>()
                        });
                    }
                    if (estimatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = gc.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Country,
                            valType = ReviewValueInputDTO.ValueType.Estimated,
                            Value = sheet.Cells[row, estimatedCol].GetValue<double>()
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

        private bool GetAddonValue(GisDTO gis, int row)
        {
            var cellText = sheet.Cells[row, countryCol].Text;
            var addon = gis.Addons.Where(x => x.Names.Where(n => StringParser.StrictLike(n.Name, cellText)).Any()).FirstOrDefault();
            if (addon != null)
            {
                try
                {
                    if (requestedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = addon.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Addon,
                            valType = ReviewValueInputDTO.ValueType.Requsted,
                            Value = sheet.Cells[row, requestedCol].GetValue<double>()
                        });
                    }
                    if (allocatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = addon.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Addon,
                            valType = ReviewValueInputDTO.ValueType.Allocated,
                            Value = sheet.Cells[row, allocatedCol].GetValue<double>()
                        });
                    }
                    if (estimatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ValueId = addon.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Addon,
                            valType = ReviewValueInputDTO.ValueType.Estimated,
                            Value = sheet.Cells[row, estimatedCol].GetValue<double>()
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

        private bool GetInputValue(GisDTO gis, int row)
        {
            var cellText = sheet.Cells[row, countryCol].Text;
            if (gis.GisInputNames.Where(x => StringParser.StrictLike(x.Name, cellText)).Any())
            {
                try
                {
                    if (requestedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Input,
                            valType = ReviewValueInputDTO.ValueType.Requsted,
                            Value = sheet.Cells[row, requestedCol].GetValue<double>()
                        });
                    }
                    if (allocatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Input,
                            valType = ReviewValueInputDTO.ValueType.Allocated,
                            Value = sheet.Cells[row, allocatedCol].GetValue<double>()
                        });
                    }
                    if (estimatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Input,
                            valType = ReviewValueInputDTO.ValueType.Estimated,
                            Value = sheet.Cells[row, estimatedCol].GetValue<double>()
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

        private bool GetOutputValue(GisDTO gis, int row)
        {
            var cellText = sheet.Cells[row, countryCol].Text;
            if (gis.GisOutputNames.Where(x => StringParser.StrictLike(x.Name, cellText)).Any())
            {
                try
                {
                    if (requestedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Output,
                            valType = ReviewValueInputDTO.ValueType.Requsted,
                            Value = sheet.Cells[row, requestedCol].GetValue<double>()
                        });
                    }
                    if (allocatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Output,
                            valType = ReviewValueInputDTO.ValueType.Allocated,
                            Value = sheet.Cells[row, allocatedCol].GetValue<double>()
                        });
                    }
                    if (estimatedCol > 0)
                    {
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = gis.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Output,
                            valType = ReviewValueInputDTO.ValueType.Estimated,
                            Value = sheet.Cells[row, estimatedCol].GetValue<double>()
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
            for (int col = 1; col <= sheet.Dimension.End.Column; col++)
            {
                for (int row = 1; row <= sheet.Dimension.End.Row; row++)
                {
                    var cellText = sheet.Cells[row, col].Text;
                    if (!String.IsNullOrEmpty(cellText))
                    {
                        if (StringParser.NameContainAnyList(names, cellText))
                        {
                            return col;
                        }
                    }
                }
            }
            return 0;
        }
    }
}
