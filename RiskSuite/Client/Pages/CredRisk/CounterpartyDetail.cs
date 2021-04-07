using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Pages.CredRisk
{
    public partial class CounterpartyDetail
    {
        [Inject] public IJSRuntime jsRuntime { get; set; }
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



        public async Task Delete()
        {
            if (Id != null)
            {
                ConfirmMessage = ConfirmMessage + Model.Name;
                await jsRuntime.InvokeVoidAsync("ShowConfirmationModal");
            }
        }

        public async Task ConfirmDelete(bool isConfirmed)
        {
            await jsRuntime.InvokeVoidAsync("HideConfirmationModal");
            if (isConfirmed)
            {
                await OnDeleteComfirmEvent.InvokeAsync();
            }
            //await jsRuntime.InvokeVoidAsync("HideConfirmationModal");

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
                await jsRuntime.ToastrError(e.Message);
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
                await jsRuntime.ToastrError(e.Message);
            }
        }
    }
}
