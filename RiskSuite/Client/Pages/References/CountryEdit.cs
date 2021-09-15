using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class CountryEdit
    {
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [CascadingParameter] public CountryDTO CountryModel { get; set; }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
