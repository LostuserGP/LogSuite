using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class FileTypeSettingEdit
    {
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [CascadingParameter] public FileTypeSettingDTO Model { get; set; }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
