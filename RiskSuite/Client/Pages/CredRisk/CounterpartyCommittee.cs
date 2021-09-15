using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.CredRisk
{
    public partial class CounterpartyCommittee
    {
        [Parameter] public int Id { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public ICommitteeLimitService limitService { get; set; }
        [Inject] public ICommitteeStatusService statusService { get; set; }
        [Inject] public ICommitteeService service { get; set; }
        public IEnumerable<CommitteeDTO> Committees { get; set; } = new List<CommitteeDTO>();
        public IEnumerable<CommitteeLimitDTO> CommitteeLimits = new List<CommitteeLimitDTO>();
        public IEnumerable<CommitteeStatusDTO> CommitteeStatuses = new List<CommitteeStatusDTO>();
        public string Order { get; set; }
        public bool OrderAsc { get; set; }
        public CommitteeDTO CommitteeModel { get; set; } = new CommitteeDTO() {DateStart = DateTime.Now};
        public bool NewCommittee { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Committees = await service.Getall(Id);
            CommitteeLimits = await limitService.Getall();
            CommitteeStatuses = await statusService.Getall();
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
                        Committees = Committees.OrderBy(x => x.Id);
                        break;
                    case "Date":
                        Committees = Committees.OrderBy(x => x.DateStart);
                        break;
                }
            }
            else
            {
                switch (columnName)
                {
                    case "Id":
                        Committees = Committees.OrderByDescending(x => x.Id);
                        break;
                    case "Date":
                        Committees = Committees.OrderByDescending(x => x.DateStart);
                        break;
                }
            }
        }

        private async Task HandleCommitteeSubmit()
        {
            CommitteeDTO result;
            if (CommitteeModel.Id > 0)
            {
                result = await service.Update(CommitteeModel);
            }
            else
            {
                CommitteeModel.CounterpartyId = Id;
                result = await service.Create(CommitteeModel);
            }
            if (result != null)
            {
                Committees = await service.Getall(Id);
                NewCommittee = false;
                CommitteeModel = new CommitteeDTO();
            }
        }

        private async Task HandleCommitteeDelete(int id)
        {
            var committeeToDelete = Committees.Where(x => x.Id == id).FirstOrDefault();
            if (committeeToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Committee {committeeToDelete.CommitteeStatus.Name} at date {committeeToDelete.DateStart.ToString("dd.MM.yyyy")} will be deleted",
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
                            $"Rating {committeeToDelete.CommitteeStatus.Name} at date {committeeToDelete.DateStart.ToString("dd.MM.yyyy")} was deleted",
                            SweetAlertIcon.Success);
                        Committees = await service.Getall(Id);
                    }
                }
            }
        }

        private void CommitteeToUpdate(CommitteeDTO committee)
        {
            CommitteeModel = committee;
            NewCommittee = true;
        }

        private void Cancel()
        {
            NewCommittee = false;
            CommitteeModel = new CommitteeDTO();
        }
    }
}
