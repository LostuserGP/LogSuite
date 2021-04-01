using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Pages.Admin
{
    public partial class DepartmentDetail
    {
        [Parameter]
        public int? Id { get; set; }
        public bool IsProcessing { get; set; } = false;
        private string Title { get; set; } = "Create";
        private DepartmentDTO DepartmentModel { get; set; } = new DepartmentDTO();
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        public IDepartmentService departmentService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public EventCallback OnDepartmentSubmit { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //if (Id != null)
            //{
            //    //updating
            //    Title = "Update";
            //    DepartmentModel = await departmentService.Get(Id.Value);
            //}
            //else
            //{
            //    //create
            //    DepartmentModel = new DepartmentDTO();
            //}
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                //updating
                Title = "Update";
                DepartmentModel = await departmentService.Get(Id.Value);
            }
            else
            {
                //create
                DepartmentModel = new DepartmentDTO();
            }
        }

        private async Task HandleDepartmentSubmit()
        {
            try
            {
                IsProcessing = true;
                DepartmentDTO result = null;
                if (Id != null && Title == "Update")
                {
                    result = await departmentService.Update(DepartmentModel);
                    await jsRuntime.ToastrSuccess("Department succesfully updated");
                }
                else
                {
                    result = await departmentService.Create(DepartmentModel);
                    await jsRuntime.ToastrSuccess("Department succesfully created");
                }
                IsProcessing = false;
                await OnDepartmentSubmit.InvokeAsync();
                //navigationManager.NavigateTo("/admin/department");
            }
            catch (Exception e)
            {
                IsProcessing = false;
                await jsRuntime.ToastrError(e.Message);
            }
        }
    }
}
