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
    public class ExcelGasDayParser
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

        public ExcelGasDayParser(IBrowserFile file, IEnumerable<GisDTO> gisList, FileTypeSettingDTO settings, ToastService toastService)
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
            filename = _file.Name;
            GisDTO gisVelke = _gisList.Where(x => x.Name.ToLower().Equals("велке капушаны")).FirstOrDefault();
            if (gisVelke == null)
            {
                _toastService.ToastError("В БД не найден ГИС Велке Капушаны");
                return;
            }
            //находим закачку
            var geoplinInput = gisVelke.Addons.Where(x => x.Name.ToLower().Equals("закачка геоплин")).FirstOrDefault();
            if (geoplinInput == null)
            {
                _toastService.ToastError("В БД не найдена Закачка Геоплин");
                return;
            }
            var geoplinOutput = gisVelke.Addons.Where(x => x.Name.ToLower().Equals("отбор геоплин")).FirstOrDefault();
            if (geoplinOutput == null)
            {
                _toastService.ToastError("В БД не найден Отбор Геоплин");
                return;
            }
            var dateReport = StringParser.GetFirstDateFromString(filename);
            if (dateReport == null)
            {
                _toastService.ShowToast("В результате парсинга файла " + filename + " не удалось установить дату", Helpers.ToastLevel.Error);
                return;
            }
            var excelPackage = new ExcelPackage();
            var stream = _file.OpenReadStream(10000000);
            await excelPackage.LoadAsync(stream);
            sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
            filename = _file.Name;
            int endRow = sheet.Dimension.End.Row;
            int endCol = sheet.Dimension.End.Column;
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
            bool wasInput = false;
            bool wasOutput = false;
            for (int row = endRow; row >= 1; row--)
            {
                var cellText = sheet.Cells[row, 1].Text;
                if (!String.IsNullOrEmpty(cellText))
                {
                    // проверяем не наш ли это Геоплин
                    var isInput = geoplinInput.Names.Where(x => StringParser.StrictLike(x.Name, cellText)).Any();
                    if (isInput)
                    {
                        wasInput = true;
                        if (GetAddonValue(geoplinInput, row)) continue;
                    }
                    var isOutput = geoplinOutput.Names.Where(x => StringParser.StrictLike(x.Name, cellText)).Any();
                    if (isOutput)
                    {
                        wasOutput = true;
                        if (GetAddonValue(geoplinOutput, row)) continue;
                    }
                }
                if (wasInput == true && wasOutput == true) break;
            }
            excelPackage.Dispose();
        }

        private bool GetAddonValue(GisAddonDTO addon, int row)
        {
            if (addon != null)
            {
                try
                {
                    if (requestedCol > 0)
                    {
                        double val = sheet.Cells[row, requestedCol].GetValue<double>();
                        val = val / 10.45 / 1000000;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = addon.GisId,
                            ValueId = addon.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Addon,
                            valType = ReviewValueInputDTO.ValueType.Requsted,
                            Value = val
                        });
                    }
                    if (allocatedCol > 0)
                    {
                        double val = sheet.Cells[row, allocatedCol].GetValue<double>();
                        val = val / 10.45 / 1000000;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = addon.GisId,
                            ValueId = addon.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Addon,
                            valType = ReviewValueInputDTO.ValueType.Allocated,
                            Value = val
                        });
                    }
                    if (estimatedCol > 0)
                    {
                        double val = sheet.Cells[row, estimatedCol].GetValue<double>();
                        val = val / 10.45 / 1000000;
                        valueList.Add(new ReviewValueInputDTO()
                        {
                            GisId = addon.GisId,
                            ValueId = addon.Id,
                            ReportDate = DateReport,
                            inType = ReviewValueInputDTO.InputType.Addon,
                            valType = ReviewValueInputDTO.ValueType.Estimated,
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
            for (int col = 1; col <= sheet.Dimension.End.Column; col++)
            {
                for (int row = 1; row <= sheet.Dimension.End.Row; row++)
                {
                    var cellText = sheet.Cells[row, col].Text;
                    if (!String.IsNullOrEmpty(cellText))
                    {
                        if (StringParser.NameEqualsAnyList(names, cellText))
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
