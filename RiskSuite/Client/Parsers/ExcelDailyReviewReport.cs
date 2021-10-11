using LogSuite.Client.Services;
using OfficeOpenXml;
using System;

namespace LogSuite.Client.Parsers
{
    public class ExcelDailyReviewReport
    {
        private ToastService _toastService { get; set; }
        private ExcelWorksheet sheet;
        private string filename;
        private DateTime ReportDate;
        private DateTime StartDate;
        private DateTime FinishDate;
        private IEnumerable<GisDTO> _gisList;
        private List<ReviewValueInputDTO> valueList = new List<ReviewValueInputDTO>();
        private FileTypeSettingDTO _settings;
        private int requestedCol = 0;
        private int allocatedCol = 0;
        private int estimatedCol = 0;
        private int countryCol = 0;
        private int gisCol = 0;

        public ExcelDailyReviewReport(IBrowserFile file, IEnumerable<GisDTO> gisList, FileTypeSettingDTO settings, ToastService toastService)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _file = file;
            _gisList = gisList;
            _settings = settings;
            _toastService = toastService;
        }
    }
}
