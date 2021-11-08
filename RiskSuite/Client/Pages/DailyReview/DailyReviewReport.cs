using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    public partial class DailyReviewReport
    {
        [Inject] public ToastService toastService { get; set; }
        [Inject] public IGisService _gisService { get; set; }
        [Inject] public IGisCountryValueService _countryService { get; set; }
        [Inject] public IGisCountryResourceService _resourceService { get; set; }
        [Inject] public IJSRuntime js { get; set; }
        public DateRange Model = new DateRange();
        public IEnumerable<GisDTO> _gisList;
        private int round = 1;

        public class DateRange
        {
            public DateTime StartDate { get; set; } = DateTime.Now;
            public DateTime FinishDate { get; set; } = DateTime.Now;
        }

        public async Task Run()
        {
            _gisList = await _gisService.GetOnDateRange(Model.StartDate, Model.FinishDate);
            var excelBytes = GenerateExcelReport();
            await js.InvokeVoidAsync("saveAsFile", $"test_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx", Convert.ToBase64String(excelBytes));
        }

        public byte[] GenerateExcel()
        {
            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("Review");
            int dec = 6;
            int row = 1;
            int col = 3;
            int valCol = 3;
            for (DateTime d = Model.StartDate; d <= Model.FinishDate;)
            {
                row = 1;
                ws.Cells[row, ++col].Value = d;
                ws.Cells[row, col, row, col + 4].Merge = true;
                ws.Cells[row, col, row, col + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                ws.Cells[row, col, row, col + 4].Style.Numberformat.Format = "dd.MM.yyyy";
                ws.Cells[++row, col].Value = d;
                ws.Cells[row, col, row, col + 4].Merge = true;
                ws.Cells[row, col, row, col + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                ws.Cells[row, col, row, col + 4].Style.Numberformat.Format = "dddd";
                ws.Cells[++row, col].Value = "Заявки утренние";
                ws.Cells[row, col + 1].Value = "Выделено";
                ws.Cells[row, col + 2].Value = "Факт оценка";
                ws.Cells[row, col + 3].Value = "Факт";
                ws.Cells[row, col + 4].Value = "Факт к графику";
                ws.Column(col).Width = 12;
                ws.Column(col + 1).Width = 12;
                ws.Column(col + 2).Width = 12;
                ws.Column(col + 3).Width = 12;
                ws.Column(col + 4).Width = 12;
                ws.Column(col + 4).PageBreak = true;
                col = col + 4;
                d = d.AddDays(1);
            }
            ws.Row(3).Style.WrapText = true;

            ws.Cells[1, 2].Value = Model.FinishDate.AddMonths(-1).ToString("MMMM");
            ws.Cells[1, 2, 2, 2].Merge = true;
            ws.Cells[3, 2].Value = "График ГПЭ";
            ws.Cells[1, 3].Value = Model.FinishDate.ToString("MMMM");
            ws.Cells[1, 3, 2, 3].Merge = true;
            ws.Cells[3, 3].Value = "График ГПЭ";
            ws.Cells[1, 2, 3, 3].Style.Font.Color.SetColor(Color.Red);

            int mainRow = ++row;
            ws.Cells[row, 1].Value = "Подача ресурса - всего";
            ws.Cells[row, 1].Style.Font.Bold = true;
            ws.Cells[row, 1].Style.Font.Color.SetColor(Color.Red);
            ws.Column(1).Width = 50;

            int inputRow = ++row;
            ws.Cells[row, 1].Value = "в т.ч. закачка - всего";
            ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            int ukraineRow = ++row;
            ws.Cells[row, 1].Value = "транспорт через Украину";
            ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            int moldovaRow = ++row;
            ws.Cells[row, 1].Value = "в т.ч. Молдавия";
            ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            int gasRow = ++row;
            ws.Cells[row, 1].Value = "Товарный газ - всего";

            int gpsRow = ++row;
            ws.Cells[row, 1].Value = "Товарный газ ГПЭ без учёта ГПШ";

            row++;
            ws.Cells[row, 1].Value = "в т.ч. спот продажи через трейдинг/ЭТП";
            ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            int outputRow = ++row;
            ws.Cells[row, 1].Value = "в т.ч. отбор - всего";
            ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            foreach (var gis in _gisList)
            {
                col = valCol;
                //int gisRow = ++row;
                if (gis.Name.Equals("Суджа"))
                {
                    var sudja = gis.Countries.FirstOrDefault();
                    if (sudja == null)
                    {
                        continue;
                    }
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        GisCountryValueDTO val = sudja.Values.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val == null)
                        {
                            col = col + 5;
                            continue;
                        }
                        decimal reqVal = Math.Round(val.RequstedValue, dec);
                        decimal allocVal = Math.Round(val.AllocatedValue, dec);
                        decimal estimVal = Math.Round(val.EstimatedValue, dec);
                        decimal factVal = Math.Round(val.FactValue, dec);
                        ws.Cells[moldovaRow, ++col].Value = reqVal;
                        ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[moldovaRow, ++col].Value = allocVal;
                        ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[moldovaRow, ++col].Value = estimVal;
                        ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[moldovaRow, ++col].Value = factVal;
                        ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + factVal;
                        col++;
                    }
                    continue;
                }
                if (gis.Name.Equals("Китай"))
                {
                    continue;
                }
                int gisRow = ++row;
                var name = gis.DailyReviewName;
                if (String.IsNullOrWhiteSpace(name)) name = gis.Name;
                ws.Cells[row, 1].Value = name.ToUpper();
                ws.Cells[row, 1].Style.Font.Color.SetColor(Color.Red);
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                if (!gis.NoPhg)
                {
                    ws.Cells[++row, 1].Value = "Закачка ПХГ";
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    col = valCol;
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        var val = gis.GisInputValues.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val == null)
                        {
                            col = col + 5;
                            continue;
                        }
                        ws.Cells[row, ++col].Value = Math.Round(val.RequstedValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.RequstedValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.RequstedValue, dec);
                        ws.Cells[inputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.RequstedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.AllocatedValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.AllocatedValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.AllocatedValue, dec);
                        ws.Cells[inputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.AllocatedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.EstimatedValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.EstimatedValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.EstimatedValue, dec);
                        ws.Cells[inputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.EstimatedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.FactValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.FactValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.FactValue, dec);
                        ws.Cells[inputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.FactValue, dec);
                    }
                    ws.Cells[++row, 1].Value = "Отбор ПХГ";
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    col = valCol;
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        var val = gis.GisOutputValues.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val == null)
                        {
                            col = col + 5;
                            continue;
                        }
                        ws.Cells[row, ++col].Value = Math.Round(val.RequstedValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.RequstedValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.RequstedValue, dec);
                        ws.Cells[outputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.RequstedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.AllocatedValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.AllocatedValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.AllocatedValue, dec);
                        ws.Cells[outputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.AllocatedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.EstimatedValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.EstimatedValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.EstimatedValue, dec);
                        ws.Cells[outputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.EstimatedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.FactValue, dec);
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + Math.Round(val.FactValue, dec);
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + Math.Round(val.FactValue, dec);
                        ws.Cells[outputRow, col].Value = ws.Cells[inputRow, col].GetValue<decimal>() + Math.Round(val.FactValue, dec);
                    }
                }
                foreach (var addon in gis.Addons)
                {
                    ws.Cells[++row, 1].Value = addon.DailyReviewName;
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    col = valCol;
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        var val = addon.Values.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val == null)
                        {
                            col = col + 5;
                            continue;
                        }
                        ws.Cells[row, ++col].Value = Math.Round(val.RequstedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.AllocatedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.EstimatedValue, dec);
                        ws.Cells[row, ++col].Value = Math.Round(val.FactValue, dec);
                        ws.Cells[row, ++col].Value = "";
                    }
                }
                if (gis.IsOneRow)
                {
                    var country = gis.Countries.FirstOrDefault();
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    var resourceDate = new DateTime(Model.FinishDate.Year, Model.FinishDate.Month, 1);
                    var firstRes = country.Resources.Where(x => x.Month == resourceDate.AddMonths(-1)).FirstOrDefault();
                    var secondRes = country.Resources.Where(x => x.Month == resourceDate).FirstOrDefault();
                    decimal firstVolume = 0;
                    decimal secondVolume = 0;
                    if (firstRes != null)
                    {
                        firstVolume = firstRes.Value;
                    }
                    if (secondRes != null)
                    {
                        secondVolume = secondRes.Value;
                    }
                    ws.Cells[row, 2].Value = firstVolume;
                    ws.Cells[row, 2].Style.Font.Color.SetColor(Color.Red);
                    ws.Cells[row, 3].Value = secondVolume;
                    ws.Cells[row, 3].Style.Font.Color.SetColor(Color.Red);
                    ws.Cells[gpsRow, 2].Value = ws.Cells[gpsRow, 2].GetValue<decimal>() + firstVolume;
                    ws.Cells[gpsRow, 3].Value = ws.Cells[gpsRow, 3].GetValue<decimal>() + secondVolume;
                    col = valCol;
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        var val = country.Values.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val == null)
                        {
                            col = col + 5;
                            continue;
                        }
                        decimal reqVal = Math.Round(val.RequstedValue, dec);
                        decimal allocVal = Math.Round(val.AllocatedValue, dec);
                        decimal estimVal = Math.Round(val.EstimatedValue, dec);
                        decimal factVal = Math.Round(val.FactValue, dec);
                        ws.Cells[row, ++col].Value = reqVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + reqVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + reqVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[row, ++col].Value = allocVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + allocVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + allocVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[row, ++col].Value = estimVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + estimVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + estimVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[row, ++col].Value = factVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + factVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + factVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + factVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + factVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + factVal;
                        col++;
                    }
                    continue;
                }
                int gisGasRow = ++row;
                ws.Cells[row, 1].Value = "Товарный газ";
                ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, 4, row, ws.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                foreach (var country in gis.Countries)
                {
                    ws.Cells[++row, 1].Value = country.Country.Name;
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    var resourceDate = new DateTime(Model.FinishDate.Year, Model.FinishDate.Month, 1);
                    var firstRes = country.Resources.Where(x => x.Month == resourceDate.AddMonths(-1)).FirstOrDefault();
                    var secondRes = country.Resources.Where(x => x.Month == resourceDate).FirstOrDefault();
                    decimal firstVolume = 0;
                    decimal secondVolume = 0;
                    if (firstRes != null)
                    {
                        firstVolume = firstRes.Value;
                    }
                    if (secondRes != null)
                    {
                        secondVolume = secondRes.Value;
                    }
                    ws.Cells[row, 2].Value = firstVolume;
                    ws.Cells[row, 2].Style.Font.Color.SetColor(Color.Red);
                    ws.Cells[row, 3].Value = secondVolume;
                    ws.Cells[row, 3].Style.Font.Color.SetColor(Color.Red);
                    ws.Cells[gpsRow, 2].Value = ws.Cells[gpsRow, 2].GetValue<decimal>() + firstVolume;
                    ws.Cells[gpsRow, 3].Value = ws.Cells[gpsRow, 3].GetValue<decimal>() + secondVolume;
                    ws.Cells[gisGasRow, 2].Value = ws.Cells[gisGasRow, 2].GetValue<decimal>() + firstVolume;
                    ws.Cells[gisGasRow, 3].Value = ws.Cells[gisGasRow, 3].GetValue<decimal>() + secondVolume;
                    col = valCol;
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        var val = country.Values.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val == null)
                        {
                            col = col + 5;
                            continue;
                        }
                        decimal reqVal = Math.Round(val.RequstedValue, dec);
                        decimal allocVal = Math.Round(val.AllocatedValue, dec);
                        decimal estimVal = Math.Round(val.EstimatedValue, dec);
                        decimal factVal = Math.Round(val.FactValue, dec);
                        ws.Cells[row, ++col].Value = reqVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + reqVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + reqVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[gisGasRow, col].Value = ws.Cells[gisGasRow, col].GetValue<decimal>() + reqVal;
                        ws.Cells[row, ++col].Value = allocVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + allocVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + allocVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[gisGasRow, col].Value = ws.Cells[gisGasRow, col].GetValue<decimal>() + allocVal;
                        ws.Cells[row, ++col].Value = estimVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + estimVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + estimVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[gisGasRow, col].Value = ws.Cells[gisGasRow, col].GetValue<decimal>() + estimVal;
                        ws.Cells[row, ++col].Value = factVal;
                        //if (!country.IsCalculated) { ws.Cells[gpsRow, col].Value = ws.Cells[gpsRow, col].GetValue<decimal>() + factVal; }
                        if (gis.IsUkraineTransport) { ws.Cells[ukraineRow, col].Value = ws.Cells[ukraineRow, col].GetValue<decimal>() + factVal; }
                        ws.Cells[gisRow, col].Value = ws.Cells[gisRow, col].GetValue<decimal>() + factVal;
                        ws.Cells[mainRow, col].Value = ws.Cells[mainRow, col].GetValue<decimal>() + factVal;
                        ws.Cells[gasRow, col].Value = ws.Cells[gasRow, col].GetValue<decimal>() + factVal;
                        ws.Cells[gisGasRow, col].Value = ws.Cells[gisGasRow, col].GetValue<decimal>() + factVal;
                        col++;
                    }
                }
                var startCol = valCol + 4;
                for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                {
                    var checkMonth = d.ToString("MMMM");
                    if (ws.Cells[1, 2].GetValue<string>() == checkMonth)
                    {
                        if (ws.Cells[gisGasRow, startCol].GetValue<decimal>() > 0)
                        {
                            ws.Cells[gisGasRow, startCol + 1].Value = ws.Cells[gisGasRow, startCol].GetValue<decimal>() - ws.Cells[gisGasRow, 2].GetValue<decimal>();
                        }
                        else if (ws.Cells[gisGasRow, startCol - 1].GetValue<decimal>() > 0)
                        {
                            ws.Cells[gisGasRow, startCol + 1].Value = ws.Cells[gisGasRow, startCol - 1].GetValue<decimal>() - ws.Cells[gisGasRow, 2].GetValue<decimal>();
                        }
                        else if (ws.Cells[gisGasRow, startCol - 2].GetValue<decimal>() > 0)
                        {
                            ws.Cells[gisGasRow, startCol + 1].Value = ws.Cells[gisGasRow, startCol - 2].GetValue<decimal>() - ws.Cells[gisGasRow, 2].GetValue<decimal>();
                        }
                    }
                    else if (ws.Cells[1, 3].GetValue<string>() == checkMonth)
                    {
                        if (ws.Cells[gisGasRow, startCol].GetValue<decimal>() > 0)
                        {
                            ws.Cells[gisGasRow, startCol + 1].Value = ws.Cells[gisGasRow, startCol].GetValue<decimal>() - ws.Cells[gisGasRow, 3].GetValue<decimal>();
                        }
                        else if (ws.Cells[gisGasRow, startCol - 1].GetValue<decimal>() > 0)
                        {
                            ws.Cells[gisGasRow, startCol + 1].Value = ws.Cells[gisGasRow, startCol - 1].GetValue<decimal>() - ws.Cells[gisGasRow, 3].GetValue<decimal>();
                        }
                        else if (ws.Cells[gisGasRow, startCol - 2].GetValue<decimal>() > 0)
                        {
                            ws.Cells[gisGasRow, startCol + 1].Value = ws.Cells[gisGasRow, startCol - 2].GetValue<decimal>() - ws.Cells[gisGasRow, 3].GetValue<decimal>();
                        }
                    }
                    if (ws.Cells[gisGasRow, startCol + 1].GetValue<decimal>() < 0)
                    {
                        ws.Cells[gisGasRow, startCol + 1].Style.Font.Italic = true;
                    }
                    startCol = startCol + 5;
                }
            }
            //Китай
            var china = _gisList.Where(x => x.Name.Equals("Китай")).FirstOrDefault().Countries.FirstOrDefault();
            row++;
            col = valCol;
            var cValues = china.Values;
            ws.Cells[row, 1].Value = "Китай";
            ws.Cells[row + 1, 1].Value = "Товарный газ всего с учетом Китая и ГПШ";
            for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
            {
                var val = cValues.Where(x => x.DateReport == d).FirstOrDefault();
                if (val == null)
                {
                    col = col + 5;
                    continue;
                }
                decimal reqVal = Math.Round(val.RequstedValue, dec);
                decimal allocVal = Math.Round(val.AllocatedValue, dec);
                decimal estimVal = Math.Round(val.EstimatedValue, dec);
                decimal factVal = Math.Round(val.FactValue, dec);
                ws.Cells[row, ++col].Value = reqVal;
                ws.Cells[row + 1, col].Value = reqVal + ws.Cells[gasRow, col].GetValue<decimal>();
                ws.Cells[row, ++col].Value = allocVal;
                ws.Cells[row + 1, col].Value = allocVal + ws.Cells[gasRow, col].GetValue<decimal>();
                ws.Cells[row, ++col].Value = estimVal;
                ws.Cells[row + 1, col].Value = estimVal + ws.Cells[gasRow, col].GetValue<decimal>();
                ws.Cells[row, ++col].Value = factVal;
                ws.Cells[row + 1, col].Value = factVal + ws.Cells[gasRow, col].GetValue<decimal>();
                col++;
            }

            //hide null rows
            for (int r = 4; r <= ws.Dimension.End.Row; r++)
            {
                decimal sum = 0;
                for (int c = 4; c < ws.Dimension.End.Column; c++)
                {
                    try
                    {
                        sum = sum + ws.Cells[r, c].GetValue<decimal>();
                    }
                    catch (Exception ex)
                    {
                        toastService.ToastError("Не удалось обработать ячейку " + r + ":" + c);
                    }
                }
                if (sum == 0)
                {
                    ws.Row(r).Hidden = true;
                }
            }
            //Styling(ws);
            //ws.Cells[4, 2, ws.Dimension.End.Row, ws.Dimension.End.Column].Style.Numberformat.Format = "0.0";
            //ws.Cells[4, 2, ws.Dimension.End.Row, ws.Dimension.End.Column].Style.Numberformat.Format = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";
            ws.Cells[4, 2, ws.Dimension.End.Row, ws.Dimension.End.Column].Style.Numberformat.Format = "_-* #,##0.0_-;-* #,##0.0_-;_-* \"-\"??_-;_-@_-";
            //ws.Cells[4, 2, ws.Dimension.End.Row, ws.Dimension.End.Column].Style.Numberformat.Format = "# ##0,0\\ _₽;-# ##0,0\\ _₽";
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Font.Bold = true;
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[4, 1, ws.Dimension.End.Row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.WrapText = true;
            ws.Cells[1, 1, 3, ws.Dimension.End.Column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[1, 1, 3, ws.Dimension.End.Column].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 4, 3, ws.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[gasRow, 4, gasRow, ws.Dimension.End.Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[gasRow, 4, gasRow, ws.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            ws.Cells[gasRow, 4, gasRow, ws.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[gasRow, 4, gasRow, ws.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.Cells[gasRow, 4, gasRow, ws.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[gasRow, 4, gasRow, ws.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[gpsRow, 4, gpsRow, ws.Dimension.End.Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[gpsRow, 4, gpsRow, ws.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            ws.Cells[gpsRow, 4, gpsRow, ws.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[gpsRow, 4, gpsRow, ws.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.Cells[gpsRow, 4, gpsRow, ws.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[gpsRow, 4, gpsRow, ws.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            ws.PrinterSettings.PrintArea = ws.Cells[1, 4, ws.Dimension.End.Row, ws.Dimension.End.Column];
            ws.PrinterSettings.RepeatColumns = ws.Cells["$A:$C"];
            //ws.PrinterSettings.TopMargin = (decimal) 0.7;
            //ws.PrinterSettings.BottomMargin = (decimal) 0.7;
            //ws.PrinterSettings.LeftMargin = (decimal) 0.2;
            //ws.PrinterSettings.RightMargin = (decimal) 0.2;
            //ws.PrinterSettings.HorizontalCentered = true;
            //ws.PrinterSettings.FitToWidth = 1;

            return package.GetAsByteArray();
        }
        //}

        public byte[] GenerateExcelReport()
        {
            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("Review");
            int startRow = 1;
            int startCol = 1;
            int row = startRow;
            int col = startCol + 2;
            //оформляем заголовки
            for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
            {
                row = startRow;
                ws.Cells[row, ++col].Value = d;
                ws.Cells[row, col, row, col + 4].Merge = true;
                ws.Cells[row, col, row, col + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                ws.Cells[row, col, row, col + 4].Style.Numberformat.Format = "dd.MM.yyyy";
                ws.Cells[++row, col].Value = d;
                ws.Cells[row, col, row, col + 4].Merge = true;
                ws.Cells[row, col, row, col + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                ws.Cells[row, col, row, col + 4].Style.Numberformat.Format = "dddd";
                ws.Cells[++row, col].Value = "Заявки утренние";
                ws.Cells[row, col + 1].Value = "Выделено";
                ws.Cells[row, col + 2].Value = "Факт оценка";
                ws.Cells[row, col + 3].Value = "Факт";
                ws.Cells[row, col + 4].Value = "Факт к графику";
                ws.Column(col).Width = 12;
                ws.Column(++col).Width = 12;
                ws.Column(++col).Width = 12;
                ws.Column(++col).Width = 12;
                ws.Column(++col).Width = 12;
                ws.Column(col).PageBreak = true;
            }
            ws.Row(3).Style.WrapText = true;

            //оформляем график поставок
            col = startCol + 1;
            row = startRow;
            ws.Cells[row, col].Value = Model.FinishDate.AddMonths(-1).ToString("MMMM");
            ws.Cells[row, col + 1].Value = Model.FinishDate.ToString("MMMM");
            ws.Cells[row, col, row + 1, col].Merge = true;
            ws.Cells[row, col + 1, row + 1, col + 1].Merge = true;
            ws.Cells[row + 2, col].Value = "График ГПЭ";
            ws.Cells[row + 2, col + 1].Value = "График ГПЭ";
            ws.Cells[row, col, row + 2, col + 1].Style.Font.Color.SetColor(Color.Red);

            //подача ресура всего
            row = startRow + 3;
            col = startCol;
            ws.Cells[row, col].Value = "Подача ресурса - всего";
            ws.Cells[row, col].Style.Font.Bold = true;
            ws.Cells[row, col].Style.Font.Color.SetColor(Color.Red);
            ws.Column(col).Width = 50;
            SetMainRow(ws, row, col + 3);

            row++;
            ws.Cells[row, col].Value = "в т.ч. закачка - всего";
            SetInputRow(ws, row, col + 3);

            row++;
            ws.Cells[row, 1].Value = "транспорт через Украину";
            SetUkraineRow(ws, row, col + 3);

            //строки для расположения наверху (например Молдавия)
            row++;
            row = SetTopRows(ws, row, col);

            int gasRow = ++row;
            ws.Cells[row, 1].Value = "Товарный газ - всего";

            int gpsRow = ++row;
            ws.Cells[row, 1].Value = "Товарный газ ГПЭ без учёта ГПШ";

            row++;
            ws.Cells[row, 1].Value = "в т.ч. спот продажи через трейдинг/ЭТП";

            int outputRow = ++row;
            ws.Cells[row, 1].Value = "в т.ч. отбор - всего";

            return package.GetAsByteArray();
        }

        private void SetMainRow(ExcelWorksheet ws, int row, int col)
        {
            for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
            {
                decimal requestedValue = 0;
                decimal allocatedValue = 0;
                decimal estimatedValue = 0;
                decimal factValue = 0;
                foreach (var item in _gisList)
                {
                    var input = item.GisInputValues.Where(x => x.DateReport == d).FirstOrDefault();
                    if (input != null)
                    {
                        requestedValue += input.RequstedValue;
                        allocatedValue += input.AllocatedValue;
                        estimatedValue += input.EstimatedValue;
                        factValue += input.FactValue;
                    }
                    var output = item.GisOutputValues.Where(x => x.DateReport == d).FirstOrDefault();
                    if (output != null)
                    {
                        requestedValue -= output.RequstedValue;
                        allocatedValue -= output.AllocatedValue;
                        estimatedValue -= output.EstimatedValue;
                        factValue -= output.FactValue;
                    }
                    foreach (var country in item.Countries)
                    {
                        var val = country.Values.Where(x => x.DateReport == d).FirstOrDefault();
                        if (val != null)
                        {
                            requestedValue += val.RequstedValue;
                            allocatedValue += val.AllocatedValue;
                            estimatedValue += val.EstimatedValue;
                            factValue += val.FactValue;
                        }
                    }
                }
                ws.Cells[row, col].Value = Math.Round(requestedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                SetBorder(ws.Cells[row, col]);
                ws.Cells[row, ++col].Value = Math.Round(allocatedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                SetBorder(ws.Cells[row, col]);
                ws.Cells[row, ++col].Value = Math.Round(estimatedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                SetBorder(ws.Cells[row, col]);
                ws.Cells[row, ++col].Value = Math.Round(factValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                SetBorder(ws.Cells[row, col]);
                ws.Cells[row, ++col].Value = 0;
                col++;
            }
        }

        private void SetInputRow(ExcelWorksheet ws, int row, int col)
        {
            for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
            {
                decimal requestedValue = 0;
                decimal allocatedValue = 0;
                decimal estimatedValue = 0;
                decimal factValue = 0;
                foreach (var item in _gisList)
                {
                    var input = item.GisInputValues.Where(x => x.DateReport == d).FirstOrDefault();
                    if (input != null)
                    {
                        requestedValue += input.RequstedValue;
                        allocatedValue += input.AllocatedValue;
                        estimatedValue += input.EstimatedValue;
                        factValue += input.FactValue;
                    }
                }
                ws.Cells[row, col].Value = Math.Round(requestedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, ++col].Value = Math.Round(allocatedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, ++col].Value = Math.Round(estimatedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                ws.Cells[row, ++col].Value = Math.Round(factValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                ws.Cells[row, ++col].Value = 0;
                col++;
            }
        }

        private void SetUkraineRow(ExcelWorksheet ws, int row, int col)
        {
            for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
            {
                decimal requestedValue = 0;
                decimal allocatedValue = 0;
                decimal estimatedValue = 0;
                decimal factValue = 0;
                foreach (var item in _gisList)
                {
                    if (item.IsUkraineTransport)
                    {
                        var input = item.GisInputValues.Where(x => x.DateReport == d).FirstOrDefault();
                        if (input != null)
                        {
                            requestedValue += input.RequstedValue;
                            allocatedValue += input.AllocatedValue;
                            estimatedValue += input.EstimatedValue;
                            factValue += input.FactValue;
                        }
                        var output = item.GisOutputValues.Where(x => x.DateReport == d).FirstOrDefault();
                        if (output != null)
                        {
                            requestedValue -= output.RequstedValue;
                            allocatedValue -= output.AllocatedValue;
                            estimatedValue -= output.EstimatedValue;
                            factValue -= output.FactValue;
                        }
                        foreach (var country in item.Countries)
                        {
                            var val = country.Values.Where(x => x.DateReport == d).FirstOrDefault();
                            if (val != null)
                            {
                                requestedValue += val.RequstedValue;
                                allocatedValue += val.AllocatedValue;
                                estimatedValue += val.EstimatedValue;
                                factValue += val.FactValue;
                            }
                        }
                    }
                }
                ws.Cells[row, col].Value = Math.Round(requestedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, ++col].Value = Math.Round(allocatedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, ++col].Value = Math.Round(estimatedValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                ws.Cells[row, ++col].Value = Math.Round(factValue, round);
                ws.Cells[row, col].Style.Font.Bold = true;
                ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                ws.Cells[row, ++col].Value = 0;
                col++;
            }
        }

        private int SetTopRows(ExcelWorksheet ws, int row, int col)
        {
            foreach (var item in _gisList)
            {
                if (item.ShowOnBottom && item.IsOneRow)
                {
                    int inCol = col;
                    ws.Cells[row, inCol].Value = item.DailyReviewName;
                    for (DateTime d = Model.StartDate; d <= Model.FinishDate; d = d.AddDays(1))
                    {
                        decimal requestedValue = 0;
                        decimal allocatedValue = 0;
                        decimal estimatedValue = 0;
                        decimal factValue = 0;
                        foreach (var country in item.Countries)
                        {
                            var val = country.Values.Where(x => x.DateReport == d).FirstOrDefault();
                            if (val != null)
                            {
                                requestedValue += val.RequstedValue;
                                allocatedValue += val.AllocatedValue;
                                estimatedValue += val.EstimatedValue;
                                factValue += val.FactValue;
                            }
                        }
                        inCol += 2;
                        ws.Cells[row, inCol].Value = Math.Round(requestedValue, round);
                        ws.Cells[row, inCol].Style.Font.Bold = true;
                        ws.Cells[row, ++inCol].Value = Math.Round(allocatedValue, round);
                        ws.Cells[row, inCol].Style.Font.Bold = true;
                        ws.Cells[row, ++inCol].Value = Math.Round(estimatedValue, round);
                        ws.Cells[row, inCol].Style.Font.Bold = true;
                        ws.Cells[row, inCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[row, inCol].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                        ws.Cells[row, ++inCol].Value = Math.Round(factValue, round);
                        ws.Cells[row, inCol].Style.Font.Bold = true;
                        ws.Cells[row, inCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[row, inCol].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                        ws.Cells[row, ++inCol].Value = 0;
                        inCol++;
                    }
                    row++;
                }
            }
            return row;
        }

        private void SetBorder(ExcelRange cell)
        {
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }
    }
}
