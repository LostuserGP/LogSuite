using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared;
using RiskSuite.Shared.Authorization;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Pages.Admin
{
    public partial class Account
    {
        public IEnumerable<UserDTO> Accounts { get; set; } = new List<UserDTO>();
        [Parameter]
        public MetaData MetaData { get; set; } = new MetaData();
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        public IAccountService accountService { get; set; }
        private Params _parameters = new Params();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id != null)
                {
                    ShowDetail = true;
                }
                _parameters.Order = "Name";
                _parameters.OrderAsc = true;
                _parameters.PageSize = 35;
                await LoadAccounts();
            }
            catch (Exception e)
            {
                await jsRuntime.ToastrError(e.Message);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                ShowDetail = true;
            }
        }

        private async Task LoadAccounts()
        {
            IsProcessing = true;
            var pagingResponse = await accountService.Getall(_parameters);
            Accounts = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private async Task SelectedPage(int page)
        {
            _parameters.PageNumber = page;
            _parameters.PageSize = MetaData.PageSize;
            await LoadAccounts();
        }

        private string SetSortIcon(string columnName)
        {
            if (_parameters.Order != columnName)
            {
                return string.Empty;
            }
            if (_parameters.OrderAsc)
            {
                return "oi-sort-ascending";
            }
            else
            {
                return "oi-sort-descending";
            }
        }

        private async Task OnSortChanged(string columnName)
        {
            _parameters.PageNumber = 1;
            if (columnName != _parameters.Order)
            {
                _parameters.Order = columnName;
                _parameters.OrderAsc = true;
            }
            else
            {
                _parameters.OrderAsc = !_parameters.OrderAsc;
            }
            await LoadAccounts();
        }

        private async Task FilterChanged(string filter)
        {
            _parameters.PageNumber = 1;
            _parameters.Filter = filter;
            await LoadAccounts();
        }

        protected void DepartmentSubmitEvent()
        {
            ShowDetail = false;
            //await jsRuntime.ToastrSuccess("Department succesfully updated");
            navigationManager.NavigateTo("/admin/department");
            //await LoadDepartments();
        }

        private void ShowDetailCancel()
        {
            ShowDetail = false;
            navigationManager.NavigateTo("/admin/department");
        }
    }
}
