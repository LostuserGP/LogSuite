using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class FileTypeSettingEdit
    {
        [Parameter] public EventCallback OnValueSubmit { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [CascadingParameter] public FileTypeSettingDTO Model { get; set; }
        private List<string> TypeList;

        protected override void OnInitialized()
        {
            TypeList = new List<string>();
            TypeList.Add(SD.File_Avt);
            TypeList.Add(SD.File_Balance_Cpdd);
            TypeList.Add(SD.File_Fact_Cpdd);
            TypeList.Add(SD.File_Fact_Supply);
            TypeList.Add(SD.File_Teterevka);
            TypeList.Add(SD.File_Ge_Mail);
            TypeList.Add(SD.File_Gas_Day);
        }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
