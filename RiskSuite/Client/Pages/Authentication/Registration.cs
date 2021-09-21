using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Authorization;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Authentication
{
    public partial class Registration
    {
        private UserRequestDTO UserForRegistration = new UserRequestDTO();
        public IEnumerable<DepartmentDTO> Departments = new List<DepartmentDTO>();
        [Inject] public ToastService toastService { get; set; }
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string ResponseUrl { get; set; } = "";
        [Inject] public IAuthenticationService authenticationService { get; set; }
        [Inject] public IDepartmentService departmentService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadDepartments();
            }
            catch (Exception e)
            {
                toastService.ToastError(e.Message);
            }
        }

        private async Task LoadDepartments()
        {
            Departments = await departmentService.Getall();
        }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await authenticationService.RegisterUser(UserForRegistration);
            if (result.IsRegistrationSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
        }

        public async Task RegisterWithInvite()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await authenticationService.RegisterWithInvite(UserForRegistration);
            if (result.IsRegistrationSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }

        }
    }
}
