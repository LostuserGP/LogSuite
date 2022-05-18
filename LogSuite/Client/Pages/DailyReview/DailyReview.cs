using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Client.Services.DailyReview.References;
using Microsoft.AspNetCore.Authorization;

namespace LogSuite.Client.Pages.DailyReview
{
    [Authorize(Roles = SD.Role_User)]
    public partial class DailyReview
    {
        [Inject] public IGisService GisService { get; set; }
        [Inject] public IGisCountryAddonService GisCountryAddonService { get; set; }
        public bool ShowDetail = false;
        public IEnumerable<int> PageSizeOptions = new int[] { 20, 35, 50 };
        private GisDTO _selectedGis;
        private GisAddonDTO _selectedAddon;
        private GisCountryDTO _selectedCountry;
        private GisCountryAddonDto _selectedCountryAddon;
        private IEnumerable<GisDTO> _gises;
        private IEnumerable<GisCountryAddonDto> _gisCountryAddons;
        private string _show = "none";
        private List<Phg> _phgList;

        protected override async Task OnInitializedAsync()
        {
            _phgList = new List<Phg>
            {
                new Phg() {Name = "Закачка в ПХГ"},
                new Phg() {Name = "Отбор из ПХГ"}
            };
            _gises = await GisService.GetAll();
            _selectedGis = _gises.FirstOrDefault();
        }

        private class Phg
        {
            public string Name { get; set; }
        }

        private async Task OnSelect(GisDTO gis)
        {
            _show = "none";
            _selectedGis = await GisService.Get(gis.Id);
            _gisCountryAddons = await GisCountryAddonService.GetAllByGisId(gis.Id);
        }

        private void OnSelect(Phg phg)
        {
            _show = phg.Name switch
            {
                "Закачка в ПХГ" => "input",
                "Отбор из ПХГ" => "output",
                _ => _show
            };
        }

        private void OnSelect(GisAddonDTO addon)
        {
            _selectedAddon = addon;
            _show = "addon";
        }

        private void OnSelect(GisCountryDTO gc)
        {
            _selectedCountry = gc;
            _show = "country";
        }

        private void OnSelect(GisCountryAddonDto gcAddon)
        {
            _selectedCountryAddon = gcAddon;
            _show = "countryAddon";
        }

    }
}
