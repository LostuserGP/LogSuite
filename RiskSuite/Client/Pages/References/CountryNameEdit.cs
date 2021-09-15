using LogSuite.Client.Helpers;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class CountryNameEdit
    {
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [CascadingParameter] public CountryNameDTO CountryNameModel { get; set; }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
