using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Authorization;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Admin
{
    public partial class AccountDetail
    {
        [Parameter] public string Id { get; set; }
        public bool IsProcessing { get; set; } = false;
        private string Title { get; set; } = "Create";
        private UserDetailDTO AccountModel { get; set; } = new UserDetailDTO();
        public IEnumerable<DepartmentDTO> Departments = new List<DepartmentDTO>();
        public List<RoleModel> RoleModels = new List<RoleModel>();
        [Inject] public IDepartmentService departmentService { get; set; }
        [Inject] public IAccountService accountService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public EventCallback OnAccountSubmit { get; set; }
        [Inject] public ToastService toastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Departments = await departmentService.Getall();
            var roles = await accountService.GetRoles();
            foreach (var role in roles)
            {
                RoleModels.Add(new RoleModel()
                {
                    Name = role,
                    IsSelected = false
                });
            }
            toastService.ShowToast("Тостер работает", ToastLevel.Warning);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                //updating
                Title = "Update";
                AccountModel = await accountService.Get(Id);
                FillRolesFromApi();
            }
            else
            {
                //create
                AccountModel = new UserDetailDTO();
            }
        }

        private void FillRolesFromApi()
        {
            foreach (var roleModel in RoleModels)
            {
                roleModel.IsSelected = false;
                foreach (var role in AccountModel.Roles)
                {
                    if (roleModel.Name == role)
                    {
                        roleModel.IsSelected = true;
                    }
                }
            }
        }

        private void FillRolesFromModel()
        {
            var roles = new List<string>();
            foreach (var roleModel in RoleModels)
            {
                if (roleModel.IsSelected)
                {
                    roles.Add(roleModel.Name);
                }
            }
            AccountModel.Roles = roles;
        }

        private async Task HandleAccountSubmit()
        {
            try
            {
                IsProcessing = true;
                //UserDetailDTO result = null;
                FillRolesFromModel();
                if (Id != null && Title == "Update")
                {
                    await accountService.Update(AccountModel);
                    toastService.ToastrSuccess("Account succesfully updated");
                }
                else
                {
                    await accountService.Create(AccountModel);
                    toastService.ToastrSuccess("Account succesfully created");
                }
                IsProcessing = false;
                await OnAccountSubmit.InvokeAsync();
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
