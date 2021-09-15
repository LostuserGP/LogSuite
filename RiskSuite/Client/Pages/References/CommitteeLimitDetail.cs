using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class CommitteeLimitDetail
    {
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public ICommitteeLimitService service { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnDeleteComfirmEvent { get; set; }
        public bool IsProcessing { get; set; } = false;
        private string Title { get; set; } = "Create";
        private string ConfirmTitle { get; set; } = "Confirm delete";
        private string ConfirmMessage { get; set; } = "Do you really want delete ";
        private CommitteeLimitDTO Model { get; set; } = new CommitteeLimitDTO();



        public async Task Delete()
        {
            if (Model != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Вы уверены?",
                    Text = $"Ограниения комитета {Model.Name} будет удалено",
                    Icon = SweetAlertIcon.Warning,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Удалить",
                    CancelButtonText = "Отмена"
                });
                if (result.IsConfirmed)
                {
                    var delResult = await service.Delete(Id.Value);
                    if (delResult)
                    {
                        toastrService.ShowToast("Ограничение комитета удалено", ToastLevel.Info);
                        await OnValueSubmit.InvokeAsync();
                    }
                    await OnDeleteComfirmEvent.InvokeAsync();
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                //updating
                Title = "Update";
                Model = await service.Get(Id.Value);
            }
            else
            {
                //create
                Model = new CommitteeLimitDTO();
            }
        }

        private async Task HandleValueSubmit()
        {
            try
            {
                IsProcessing = true;
                CommitteeLimitDTO result = null;
                if (Id != null && Title == "Update")
                {
                    result = await service.Update(Model);
                    //await jsRuntime.ToastrSuccess("Committee limit succesfully updated");
                }
                else
                {
                    result = await service.Create(Model);
                    //await jsRuntime.ToastrSuccess("Committee limit succesfully created");
                }
                IsProcessing = false;
                await OnValueSubmit.InvokeAsync();
            }
            catch (Exception e)
            {
                IsProcessing = false;
                //await jsRuntime.ToastrError(e.Message);
            }
        }
    }
}
