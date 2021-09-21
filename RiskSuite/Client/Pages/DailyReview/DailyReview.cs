using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    //[Authorize(Roles = SD.Role_User)]
    public partial class DailyReview
    {
        //[Parameter] public int? countryId { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] public IGisService _gisService { get; set; }
        //public DailyReviewCountry valueComponent;
        private Params _parameters = new Params();
        private IEnumerable<GisDTO> gisList = new List<GisDTO>();
        private GisDTO selectedGis;
        private GisCountryDTO selectedCountry;
        private GisAddonDTO selectedAddon;
        private GisDTO selectedInput;
        private GisDTO selectedOutput;
        private List<GisInputValueDTO> inputList;
        private List<GisOutputValueDTO> outputList;
        private List<GisAddonValueDTO> addonList;
        private List<GisCountryValueDTO> countryValueList;
        private bool isLoading;
        private bool showDetail = false;
        private bool gisProcessing = true;

        public IEnumerable<GisDTO> GisList;

        protected override async Task OnInitializedAsync()
        {
            gisProcessing = true;
            gisList = await _gisService.Getall();
            selectedGis = gisList.FirstOrDefault();
            gisProcessing = false;
        }

        private void OnSelectGis(GisDTO gis)
        {
            //gis.Countries = gis.Countries.OrderBy(x => x.Country.Name).ToList();
            selectedGis = gis;
        }

        private void OnSelectInput()
        {
            selectedInput = selectedGis;
            selectedOutput = null;
            selectedCountry = null;
            selectedAddon = null;
        }

        private void OnSelectOutput()
        {
            selectedOutput = selectedGis;
            selectedInput = null;
            selectedCountry = null;
            selectedAddon = null;
        }

        private void OnSelectAddon(GisAddonDTO addon)
        {
            selectedAddon = addon;
            selectedOutput = null;
            selectedInput = null;
            selectedCountry = null;
        }

        private void OnSelect(GisCountryDTO country)
        {
            selectedCountry = country;
            selectedInput = null;
            selectedOutput = null;
            selectedAddon = null;
            //await valueComponent.Load();
        }

    }
}
