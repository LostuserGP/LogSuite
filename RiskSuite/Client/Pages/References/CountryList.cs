using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    [Authorize(Roles = SD.Role_Power_User + ", " + SD.Role_Admin)]
    public partial class CountryList
    {
        [Inject] public ICountryService service { get; set; }
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        public IEnumerable<CountryDTO> Countries { get; set; } = new List<CountryDTO>();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        private CountryDTO CountryModel { get; set; } = new CountryDTO();
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();
        

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Load();
            }
            catch (Exception e)
            {
                toastrService.ShowToast(e.Message, ToastLevel.Error);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                OnSelectCountry(Countries.FirstOrDefault(x => x.Id == Id));
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.Getall(_parameters);
            Countries = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            CountryModel = new CountryDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                CountryDTO result;
                if (CountryModel.Id > 0)
                {
                    result = await service.Update(CountryModel);
                    if (result != null)
                    {
                        toastrService.ToastrSuccess("Country succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(CountryModel);
                    if (result != null)
                    {
                        toastrService.ToastrSuccess("Country succesfully created");
                    }
                }
            }
            catch (Exception e)
            {
                CountryModel = null;
                toastrService.ToastrError(e.Message);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(CountryDTO country)
        {
            CountryModel = country;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var countryToDelete = Countries.Where(x => x.Id == id).FirstOrDefault();
            if (countryToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Country {countryToDelete.Name} will be deleted",
                    Icon = SweetAlertIcon.Warning,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Delete",
                    CancelButtonText = "Cancel"
                });
                if (!string.IsNullOrEmpty(result.Value))
                {
                    var deleteResult = await service.Delete(id);
                    if (deleteResult)
                    {
                        await Swal.FireAsync(
                            "Deleted",
                            $"Country {countryToDelete.Name} was deleted",
                            SweetAlertIcon.Success);
                        await Load();
                    }
                }
            }
        }

        private async Task OnSortChanged()
        {
            await Load();
        }

        private async Task SelectedPage(int page)
        {
            _parameters.PageNumber = page;
            _parameters.PageSize = MetaData.PageSize;
            await Load();
        }

        private async Task FilterChanged(string filter)
        {
            _parameters.PageNumber = 1;
            _parameters.Filter = filter;
            await Load();
        }
    
        private void OnSelectCountry(CountryDTO country)
        {
            navigationManager.NavigateTo($"/references/country/{country.Id}");
            CountryModel = country;
            ShowDetail = true;
        }
    }
}
