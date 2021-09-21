using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.CredRisk
{
    public partial class Counterparty
    {
        public IEnumerable<CounterpartyDTO> Counterparties { get; set; } = new List<CounterpartyDTO>();
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Inject] public ToastService toastService { get; set; }
        [Inject] public ICounterpartyService counterpartyService { get; set; }
        private Params _parameters = new Params();
        private bool IsProcessing { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _parameters.Order = "Name";
                _parameters.OrderAsc = true;
                _parameters.PageSize = 35;
                await LoadCounterparties();
            }
            catch (Exception e)
            {
                toastService.ToastError(e.Message);
            }
        }

        private async Task LoadCounterparties()
        {
            IsProcessing = true;
            var pagingResponse = await counterpartyService.Getall(_parameters);
            Counterparties = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private async Task SelectedPage(int page)
        {
            _parameters.PageNumber = page;
            _parameters.PageSize = MetaData.PageSize;
            await LoadCounterparties();
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
            await LoadCounterparties();
        }

        private async Task FilterChanged(string filter)
        {
            _parameters.PageNumber = 1;
            _parameters.Filter = filter;
            await LoadCounterparties();
        }
    }
}
