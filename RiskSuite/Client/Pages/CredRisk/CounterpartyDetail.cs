using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.CredRisk
{
    public partial class CounterpartyDetail
    {
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public ICounterpartyService service { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnDeleteComfirmEvent { get; set; }
        public bool IsProcessing { get; set; } = false;
        [Parameter] public string Title { get; set; }
        private string ConfirmTitle { get; set; } = "Confirm delete";
        private string ConfirmMessage { get; set; } = "Do you really want delete ";
        public CounterpartyDTO Model { get; set; } = new CounterpartyDTO();
        private bool ShowRatingInternal { get; set; } = false;
        private bool ShowRatingExternal { get; set; } = false;
        private bool ShowFS { get; set; } = false;
        private bool ShowCommittee { get; set; } = false;

        public async Task Delete()
        {
            if (Model != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Вы уверены?",
                    Text = $"Контрагент {Model.Name} будет удалён",
                    Icon = SweetAlertIcon.Warning,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Удалить",
                    CancelButtonText = "Отмена"
                });
                if (result.IsConfirmed)
                {
                    await OnDeleteComfirmEvent.InvokeAsync();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsProcessing = true;
                Model = await service.Get(Id.Value);
                IsProcessing = false;
            }
            catch (Exception e)
            {
                toastrService.ShowToast(e.Message, ToastLevel.Error);
            }
        }

        private async Task HandleValueSubmit()
        {
            try
            {
                //IsProcessing = true;
                //ReferenceName result = null;
                //if (Id != null && Title == "Update")
                //{
                //    result = await service.Update(Model);
                //    await jsRuntime.ToastrSuccess("Reference succesfully updated");
                //}
                //else
                //{
                //    result = await service.Create(Model);
                //    await jsRuntime.ToastrSuccess("Reference succesfully created");
                //}
                //IsProcessing = false;
                //await OnValueSubmit.InvokeAsync();
            }
            catch (Exception e)
            {
                IsProcessing = false;
            }
        }
    }
}
