using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models.Operativka;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Operativka
{
    public partial class GisAddonEdit
    {
        [Inject] public IGisService gisService { get; set; }
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public IEnumerable<GisDTO> Gises { get; set; }
        [CascadingParameter] public GisAddonDTO GisAddonModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
