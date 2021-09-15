using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Admin
{
    public partial class DepartmentDetail
    {
        [Parameter] public int? Id { get; set; }
        public bool IsProcessing { get; set; } = false;
        private string Title { get; set; } = "Create";
        private DepartmentDTO DepartmentModel { get; set; } = new DepartmentDTO();
        [Inject] public ToastService toastService { get; set; }
        [Inject] public IDepartmentService departmentService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public EventCallback OnDepartmentSubmit { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //}

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
                    toastService.ToastrSuccess("Department succesfully updated");
                }
                else
                {
                    result = await departmentService.Create(DepartmentModel);
                    toastService.ToastrSuccess("Department succesfully created");
                }
                IsProcessing = false;
                await OnDepartmentSubmit.InvokeAsync();
                //navigationManager.NavigateTo("/admin/department");
            }
            catch (Exception e)
            {
                IsProcessing = false;
                toastService.ToastrError(e.Message);
            }
        }
    }
}
