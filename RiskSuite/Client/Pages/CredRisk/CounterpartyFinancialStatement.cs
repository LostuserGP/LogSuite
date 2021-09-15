using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.CredRisk
{
    public partial class CounterpartyFinancialStatement
    {
        [Parameter] public int Id { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public IFinancialStatementService service { get; set; }
        [Inject] public IFinancialStatementStandardService standardService { get; set; }
        [Inject] public AuthenticationStateProvider authState { get; set; }
        public IEnumerable<FinancialStatementDTO> Fss { get; set; } = new List<FinancialStatementDTO>();
        public IEnumerable<FinancialStatementStandardDTO> FsStandards { get; set; } = new List<FinancialStatementStandardDTO>();
        public string Order { get; set; }
        public bool OrderAsc { get; set; }
        public FinancialStatementDTO FSModel { get; set; } = new FinancialStatementDTO() { DateStart = DateTime.Now };
        public bool NewFS { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Fss = await service.Getall(Id);
            FsStandards = await standardService.Getall();
        }

        private string SetSortIcon(string columnName)
        {
            if (Order != columnName)
            {
                return string.Empty;
            }
            if (OrderAsc)
            {
                return "oi-sort-ascending";
            }
            else
            {
                return "oi-sort-descending";
            }
        }

        private void OnSortChanged(string columnName)
        {
            if (columnName != Order)
            {
                Order = columnName;
                OrderAsc = true;
            }
            else
            {
                OrderAsc = !OrderAsc;
            }
            if (OrderAsc)
            {
                switch (columnName)
                {
                    case "Id":
                        Fss = Fss.OrderBy(x => x.Id);
                        break;
                    case "Date":
                        Fss = Fss.OrderBy(x => x.DateStart);
                        break;
                    case "Standard":
                        Fss = Fss.OrderBy(x => x.FinancialStatementStandard.Name);
                        break;
                }
            }
            else
            {
                switch (columnName)
                {
                    case "Id":
                        Fss = Fss.OrderByDescending(x => x.Id);
                        break;
                    case "Date":
                        Fss = Fss.OrderByDescending(x => x.DateStart);
                        break;
                    case "Standard":
                        Fss = Fss.OrderByDescending(x => x.FinancialStatementStandard.Name);
                        break;
                }
            }
        }

        private async Task HandleFSSubmit()
        {
            FinancialStatementDTO result;
            if (FSModel.Id > 0)
            {
                result = await service.Update(FSModel);
            }
            else
            {
                FSModel.CounterpartyId = Id;
                result = await service.Create(FSModel);
            }
            if (result != null)
            {
                Fss = await service.Getall(Id);
                NewFS = false;
                FSModel = new FinancialStatementDTO()
                {
                    DateStart = DateTime.Now
                };
            }
        }

        private async Task HandleFSDelete(int id)
        {
            var fsToDelete = Fss.Where(x => x.Id == id).FirstOrDefault();
            if (fsToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Rating {fsToDelete.FinancialStatementStandard.Name} at date {fsToDelete.DateStart.ToString("dd.MM.yyyy")} will be deleted",
                    Icon = SweetAlertIcon.Warning,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Delete",
                    CancelButtonText = "Cancel"
                });
                if (!string.IsNullOrEmpty(result.Value))
                {
                    var deleteResult = await service.Delete(id);
                    if (deleteResult)
                    {
                        await Swal.FireAsync(
                            "Deleted",
                            $"FS {fsToDelete.FinancialStatementStandard.Name} at date {fsToDelete.DateStart.ToString("dd.MM.yyyy")} was deleted",
                            SweetAlertIcon.Success);
                        Fss = await service.Getall(Id);
                    }
                }
            }
        }

        private void FSToUpdate(FinancialStatementDTO fs)
        {
            FSModel = fs;
            NewFS = true;
        }

        private async Task AddFS()
        {
            var state = await authState.GetAuthenticationStateAsync();
            FSModel = new FinancialStatementDTO()
            {
                DateStart = DateTime.Now,
            };
            NewFS = true;
        }

        private async Task CancelAsync()
        {
            NewFS = false;
            var state = await authState.GetAuthenticationStateAsync();
            FSModel = new FinancialStatementDTO()
            {
                DateStart = DateTime.Now,
            };
        }
    }
}
