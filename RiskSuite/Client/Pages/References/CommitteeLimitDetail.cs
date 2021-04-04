using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Client.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Pages.References
{
    public partial class CommitteeLimitDetail
    {
        [Inject] public IJSRuntime jsRuntime { get; set; }
        [Inject] public ICommitteeLimitService service { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        public bool IsProcessing { get; set; } = false;
        private string Title { get; set; } = "Create";
        private string ConfirmTitle { get; set; } = "Confirm delete";
        private string ConfirmMessage { get; set; } = "Do you really want delete ";
        private CommitteeLimitDTO Model { get; set; } = new CommitteeLimitDTO();



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
            if (isConfirmed)
            {
                var result = await service.Delete(Id.Value);
                if (result)
                {
                    await jsRuntime.ToastrSuccess("Committee limit succesfully deleted");
                    await OnValueSubmit.InvokeAsync();
                }
            }
            await jsRuntime.InvokeVoidAsync("HideConfirmationModal");

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
                    await jsRuntime.ToastrSuccess("Committee limit succesfully updated");
                }
                else
                {
                    result = await service.Create(Model);
                    await jsRuntime.ToastrSuccess("Committee limit succesfully created");
                }
                IsProcessing = false;
                await OnValueSubmit.InvokeAsync();
            }
            catch (Exception e)
            {
                IsProcessing = false;
                await jsRuntime.ToastrError(e.Message);
            }
        }
    }
}
