using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.Operativka;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Parsers
{
    public class DailyReviewExcelParser
    {
        private IFileTypeSettingService _fileService;
        private ToastService _toastService;
        private IGisService _gisService;
        private IBrowserFile _file;
        private FileTypeSettingDTO fileSettings;
        private IEnumerable<GisDTO> _gisList;
        private List<ReviewValueInputDTO> valueList = new List<ReviewValueInputDTO>();
        private string message;

        public DailyReviewExcelParser(IBrowserFile file, IFileTypeSettingService fileService, IGisService gisService, ToastService toastService)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _file = file;
            _fileService = fileService;
            _gisService = gisService;
            _toastService = toastService;
        }

        public async Task ParseFile()
        {
            var filename = _file.Name;
            var fileTypes = await _fileService.Getall();
            fileSettings = fileTypes.Where(x => StringParser.NameContainAllList(x.MustHave, filename) && !StringParser.NameContainAnyList(x.NotHave, filename)).FirstOrDefault();
            if (fileSettings == null)
            {
                return;
            }
            else
            {
                _gisList = await _gisService.Getall();
                if (fileSettings.Name.Equals("Баланс ЦПДД"))
                {
                    var dateReport = StringParser.GetFirstDateFromString(filename);
                    if (dateReport == null)
                    {
                        message = "В файле " + filename + " даже даты нет!";
                        return;
                    }
                    var revisionTime = StringParser.GetDateWithTimeFromString(filename);
                    if (revisionTime > dateReport?.Date.AddDays(1).AddHours(5) || revisionTime < dateReport?.Date.AddDays(-1))
                    {
                        message = "Дата файла " + filename + " превышает оценочные параметры - ревизия от " + revisionTime?.ToString("dd.MM hh:mm");
                        return;
                    }
                    ExcelBalanceCpddParser parser = new ExcelBalanceCpddParser(_file, _gisList, fileSettings, _toastService);
                    valueList = await parser.GetResult();
                }
                else if (fileSettings.Name.Equals("Факт ЦПДД"))
                {
                    var dateReport = StringParser.GetFirstDateFromString(filename);
                    if (dateReport == null)
                    {
                        message = "В файле " + filename + " даже даты нет!";
                        return;
                    }
                    ExcelFactCpddParser parser = new ExcelFactCpddParser(_file, _gisList, fileSettings, _toastService);
                    valueList = await parser.GetResult();
                }
                else if (fileSettings.Name.Equals("1avt"))
                {
                    var dateReport = StringParser.GetFirstDateFromString(filename);
                    if (dateReport == null)
                    {
                        message = "В файле " + filename + " даже даты нет!";
                        return;
                    }
                    ExcelAvtParser parser = new ExcelAvtParser(_file, _gisList, fileSettings, _toastService);
                    valueList = await parser.GetResult();
                }
                else if (fileSettings.Name.Equals("Gazexport mail"))
                {
                    var dateReport = StringParser.GetFirstDateFromString(filename);
                    if (dateReport == null)
                    {
                        message = "В файле " + filename + " даже даты нет!";
                        return;
                    }
                    ExcelGeMailParser parser = new ExcelGeMailParser(_file, _gisList, fileSettings, _toastService);
                    valueList = await parser.GetResult();
                }
                else if (fileSettings.Name.Equals("Газовый день"))
                {
                    var dateReport = StringParser.GetFirstDateFromString(filename);
                    if (dateReport == null)
                    {
                        message = "В файле " + filename + " даже даты нет!";
                        return;
                    }
                    ExcelGasDayParser parser = new ExcelGasDayParser(_file, _gisList, fileSettings, _toastService);
                    valueList = await parser.GetResult();
                }
            }
        }

        public ReviewValueListDTO GetResult()
        {
            var result = new ReviewValueListDTO()
            {
                Values = valueList,
                Filename = _file.Name,
                Message = message
            };
            return result;
        }
    }
}
