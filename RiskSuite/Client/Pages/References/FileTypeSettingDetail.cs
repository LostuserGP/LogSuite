using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    public partial class FileTypeSettingDetail
    {
        [Inject] public IFileTypeSettingService service { get; set; }
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Parameter] public FileTypeSettingDTO Model { get; set; }
        [Parameter] public EventCallback Reload { get; set; }
        private List<string> Names { get; set; }
        private string OldName { get; set; } = "";
        public string Name { get; set; } = "";
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();

        public async Task Save()
        {
            if (EditMode == "edit")
            {
                Names.Remove(OldName);
                Names.Add(Name);
            }
            else
            {
                Names.Add(Name);
            }
            try
            {
                FileTypeSettingDTO result = await service.Update(Model);
                if (result != null)
                {
                    toastrService.ToastrSuccess("FileType succesfully updated");
                }
            }
            catch (Exception e)
            {
                Model = new FileTypeSettingDTO();
                toastrService.ToastrError(e.Message);
            }
            await Reload.InvokeAsync();
            Name = OldName = "";
            EditMode = "none";
        }

        private void HandleUpdate(string name)
        {
            OldName = Name = name;
            EditMode = "edit";
        }

        private void Create()
        {
            OldName = Name = "";
            EditMode = "new";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(string name)
        {
            Names.Remove(name);
            try
            {
                FileTypeSettingDTO result = await service.Update(Model);
                if (result != null)
                {
                    toastrService.ToastrSuccess("FileType succesfully updated");
                }
            }
            catch (Exception e)
            {
                Model = new FileTypeSettingDTO();
                toastrService.ToastrError(e.Message);
            }
            await Reload.InvokeAsync();
            EditMode = "none";
        }

        private async Task OnSortChanged()
        {
        }
    }
}
