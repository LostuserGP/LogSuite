using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Admin
{
    [Authorize(Roles = SD.Role_Admin)]
    public partial class Account
    {
        public IEnumerable<UserDetailDTO> Accounts { get; set; } = new List<UserDetailDTO>();
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Inject] public ToastService toastService { get; set; }
        [Inject] public IAccountService accountService { get; set; }
        private Params _parameters = new Params();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        [Parameter] public string AccId { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (AccId != null)
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
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
        }

        protected override void OnParametersSet()
        {
            if (AccId != null)
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

        protected async Task AccountSubmitEvent()
        {
            ShowDetail = false;
            //await jsRuntime.ToastrSuccess("Department succesfully updated");
            navigationManager.NavigateTo("/admin/account");
            await LoadAccounts();
        }

        private void ShowDetailCancel()
        {
            ShowDetail = false;
            navigationManager.NavigateTo("/admin/account");
        }
    }
}
