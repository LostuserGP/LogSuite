using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Operativka
{
    public partial class GisCountry
    {
        [Inject] public IGisCountryService service { get; set; }
        [Inject] public ICountryService countryService { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Parameter] public GisDTO Gis { get; set; }
        public IEnumerable<GisCountryDTO> GisCountries { get; set; } = new List<GisCountryDTO>();
        public IEnumerable<CountryDTO> Countries { get; set; } = new List<CountryDTO>();
        private bool IsProcessing { get; set; } = true;
        private GisCountryDTO GisCountryModel { get; set; } = new GisCountryDTO();
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();

        protected override async Task OnInitializedAsync()
        {
            Countries = await countryService.Getall();
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await Load();
            }
            catch (Exception e)
            {
                toastService.ToastrError(e.Message);
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.GetAllPagedByGisId(Gis.Id, _parameters);
            GisCountries = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            GisCountryModel = new GisCountryDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisCountryDTO result;
                GisCountryModel.GisId = Gis.Id;
                GisCountryModel.Country = null;
                if (GisCountryModel.Id > 0)
                {
                    result = await service.Update(GisCountryModel);
                    if (result != null)
                    {
                        toastService.ToastrSuccess("GisCountry succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(GisCountryModel);
                    if (result != null)
                    {
                        toastService.ToastrSuccess("GisCountry succesfully created");
                    }
                }
            }
            catch (Exception e)
            {
                toastService.ToastrError(e.Message);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(GisCountryDTO gisName)
        {
            GisCountryModel = gisName;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var gisCountryToDelete = GisCountries.Where(x => x.Id == id).FirstOrDefault();
            if (gisCountryToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"GisCountry {gisCountryToDelete.Country.Name} will be deleted",
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
                            $"GisCountry {gisCountryToDelete.Country.Name} was deleted",
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
