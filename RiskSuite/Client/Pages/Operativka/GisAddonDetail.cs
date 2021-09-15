using LogSuite.Shared.Models.Operativka;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Operativka
{
    public partial class GisAddonDetail
    {
        [Parameter] public GisAddonDTO Addon { get; set; }
        private string ShowDetail { get; set; } = "names";


        protected override async Task OnInitializedAsync()
        {
        }

        protected override async Task OnParametersSetAsync()
        {
        }
    }
}
