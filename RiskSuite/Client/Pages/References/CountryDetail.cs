using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class CountryDetail
    {
        [Inject] public ICountryNameService service { get; set; }
        [Inject] public ICountryService countryService { get; set; }
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Parameter] public CountryDTO Country { get; set; }
        public IEnumerable<CountryNameDTO> CountryNames { get; set; } = new List<CountryNameDTO>();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        private CountryNameDTO CountryNameModel { get; set; } = new CountryNameDTO();
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();


        protected override async Task OnInitializedAsync()
        {

        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await Load();
            }
            catch (Exception e)
            {
                //await jsRuntime.ToastrError(e.Message);
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.GetAllByCountryId(Country.Id, _parameters);
            CountryNames = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            CountryNameModel = new CountryNameDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                CountryNameDTO result;
                CountryNameModel.CountryId = Country.Id;
                if (CountryNameModel.Id > 0)
                {
                    result = await service.Update(CountryNameModel);
                    if (result != null)
                    {
                        //await jsRuntime.ToastrSuccess("Country name succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(CountryNameModel);
                    if (result != null)
                    {
                        //await jsRuntime.ToastrSuccess("Country name succesfully created");
                    }
                }
            }
            catch (Exception e)
            {
                //await jsRuntime.ToastrError(e.Message);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(CountryNameDTO countryName)
        {
            CountryNameModel = countryName;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var countryNameToDelete = CountryNames.Where(x => x.Id == id).FirstOrDefault();
            if (countryNameToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Country name {countryNameToDelete.Name} will be deleted",
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
                            $"Country name {countryNameToDelete.Name} was deleted",
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
    }
}
