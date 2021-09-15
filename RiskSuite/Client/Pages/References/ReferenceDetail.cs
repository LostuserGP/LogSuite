using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class ReferenceDetail
    {
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public IReferenceService service { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnDeleteComfirmEvent { get; set; }
        public bool IsProcessing { get; set; } = false;
        [Parameter] public string Title { get; set; }
        private string ConfirmTitle { get; set; } = "Confirm delete";
        private string ConfirmMessage { get; set; } = "Do you really want delete ";
        [Parameter] public ReferenceName Model { get; set; }



        public async Task Delete()
        {
            if (Id != null)
            {
                ConfirmMessage = ConfirmMessage + Model.Name;
                //await jsRuntime.InvokeVoidAsync("ShowConfirmationModal");
            }
        }

        public async Task ConfirmDelete(bool isConfirmed)
        {
            //await jsRuntime.InvokeVoidAsync("HideConfirmationModal");
            if (isConfirmed)
            {
                await OnDeleteComfirmEvent.InvokeAsync();
            }
            //await jsRuntime.InvokeVoidAsync("HideConfirmationModal");

        }

        protected override async Task OnParametersSetAsync()
        {

        }

        private async Task HandleValueSubmit()
        {
            try
            {
                IsProcessing = true;
                ReferenceName result = null;
                if (Id != null && Title == "Update")
                {
                    result = await service.Update(Model);
                    //await jsRuntime.ToastrSuccess("Reference succesfully updated");
                }
                else
                {
                    result = await service.Create(Model);
                    //await jsRuntime.ToastrSuccess("Reference succesfully created");
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
