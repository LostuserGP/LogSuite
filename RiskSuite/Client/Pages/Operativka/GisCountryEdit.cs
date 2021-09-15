using LogSuite.Shared.Models.Operativka;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Operativka
{
    public partial class GisCountryEdit
    {
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public IEnumerable<CountryDTO> Countries { get; set; }
        [CascadingParameter] public GisCountryDTO GisCountryModel { get; set; }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
