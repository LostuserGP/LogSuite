using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    [Authorize(Roles = SD.Role_Power_User + ", " + SD.Role_Admin)]
    public partial class References
    {
        public IEnumerable<ReferenceName> Values { get; set; } = new List<ReferenceName>();
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public IReferenceService service { get; set; }
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; }
        private string DetailTitle { get; set; } = "Create";
        private ReferenceName Model { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public string referenceName { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //try
            //{
            //    ShowDetail = false;
            //    Model = new ReferenceName();
            //    //await Load();
            //}
            //catch (Exception e)
            //{
            //    await jsRuntime.ToastrError(e.Message);
            //}

        }

        private string GetUrl(int id)
        {
            var absoluteUri = new Uri(navigationManager.Uri);
            if (absoluteUri.Segments.Length > 3)
            {
                return absoluteUri.Segments[1] + absoluteUri.Segments[2] + id;
            }
            else if (absoluteUri.Segments.Length < 3)
            {
                return absoluteUri.Segments[1];
            }
            else
            {
                return absoluteUri.Segments[1] + absoluteUri.Segments[2] + "/" + id;
            }
        }

        private string SetTitle()
        {
            //var absoluteUri = new Uri(navigationManager.Uri);
            if (referenceName == null)
            {
                return "References";
            }
            switch(referenceName) {
                case "committeelimit": return "Committee Limits";
                case "committeestatus": return "Committee Status";
                case "currency": return "Currency";
                case "financialstatementstandard": return "Financial Statement Standards";
                case "guaranteeapprovaldoctype": return "Approval Doc Types";
                case "guaranteetype": return "Guarantee Types";
                case "ratingagency": return "Rating Agencies";
                case "riskclass": return "Risk Classes";
            }
            return "References";
            
        }

        protected override async Task OnParametersSetAsync()
        {
            ShowDetail = false;
            Model = new ReferenceName();
            if (Id != null)
            {
                //updating
                DetailTitle = "Update";
                var result = await service.Get(Id.Value);
                if (result != null)
                {
                    Model = result;
                    ShowDetail = true;
                }
                else
                {
                    DetailTitle = "Create";
                    ShowDetail = false;
                    navigationManager.NavigateTo($"/references/{referenceName}");
                }
            }
            if (referenceName == null)
            {
                Values = new List<ReferenceName>();
            }
            await Load();
        }

        private async Task Load()
        {
            IsProcessing = true;
            if (referenceName != null)
            {
                IsProcessing = true;
                Values = await service.Getall();
            }
            IsProcessing = false;
        }

        protected async Task ValueSubmitEvent()
        {
            ShowDetail = false;
            navigationManager.NavigateTo($"/references/{referenceName}");
            await Load();
        }

        private void ShowDetailCancel()
        {
            ShowDetail = false;
            DetailTitle = "Create";
            navigationManager.NavigateTo($"/references/{referenceName}");
        }

        protected async Task DeleteComfirmEvent()
        {
            var result = await service.Delete(Id.Value);
            if (result)
            {
                navigationManager.NavigateTo($"/references/{referenceName}");
                await Load();
                //await jsRuntime.ToastrSuccess("Reference succesfully deleted");
            }
        }
    }
}
