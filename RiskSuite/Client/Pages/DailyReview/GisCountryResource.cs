using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    [Authorize(Roles = SD.Role_User)]
    public partial class GisCountryResource
    {
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] public IGisService _gisService { get; set; }
        private IEnumerable<GisDTO> gisList = new List<GisDTO>();
        private GisDTO selectedGis;
        private GisCountryDTO selectedCountry;
        private bool gisProcessing = true;

        protected override async Task OnInitializedAsync()
        {
            gisProcessing = true;
            gisList = await _gisService.Getall();
            selectedGis = gisList.FirstOrDefault();
            gisProcessing = false;
        }

        private void OnSelectGis(GisDTO gis)
        {
            selectedGis = gis;
            selectedCountry = gis.Countries.FirstOrDefault();
        }

        private void OnSelect(GisCountryDTO country)
        {
            selectedCountry = country;
        }

    }
}
