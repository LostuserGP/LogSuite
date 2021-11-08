using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    [Authorize(Roles = SD.Role_User + ", " + SD.Role_Admin)]
    public partial class FileTypeSettingList
    {
        [Inject] public IFileTypeSettingService service { get; set; }
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        public IEnumerable<FileTypeSettingDTO> ValueList { get; set; } = new List<FileTypeSettingDTO>();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        private FileTypeSettingDTO Model { get; set; } = new FileTypeSettingDTO();
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Load();
            }
            catch (Exception e)
            {
                toastrService.ToastError(e.Message);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                OnSelect(ValueList.FirstOrDefault(x => x.Id == Id));
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.Getall(_parameters);
            ValueList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            Model = new FileTypeSettingDTO();
            EditMode = "new";
        }

        private async Task HandleSubmit()
        {
            try
            {
                FileTypeSettingDTO result;
                if (Model.Id > 0)
                {
                    result = await service.Update(Model);
                    if (result != null)
                    {
                        toastrService.ToastSuccess("FileType succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(Model);
                    if (result != null)
                    {
                        toastrService.ToastSuccess("FileType succesfully created");
                    }
                }
            }
            catch (Exception e)
            {
                Model = new FileTypeSettingDTO();
                toastrService.ToastError(e.Message);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(FileTypeSettingDTO val)
        {
            Model = val;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var valueToDelete = ValueList.Where(x => x.Id == id).FirstOrDefault();
            if (valueToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"FileType {valueToDelete.Name} will be deleted",
                    Icon = SweetAlertIcon.Warning,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Delete",
                    CancelButtonText = "Cancel"
                });
                if (!string.IsNullOrEmpty(result.Value))
                {
                    var deleteResult = await service.Delete(id);
                    if (deleteResult)
                    {
                        await Swal.FireAsync(
                            "Deleted",
                            $"FileType {valueToDelete.Name} was deleted",
                            SweetAlertIcon.Success);
                        await Load();
                    }
                }
            }
        }

        private async Task OnSortChanged()
        {
            await Load();
        }

        private string SetSortIcon(string dbName)
        {
            if (_parameters.Order != dbName)
            {
                return string.Empty;
            }
            if (_parameters.OrderAsc)
            {
                return "oi-sort-ascending";
            }
            else
            {
                return "oi-sort-descending";
            }
        }

        private async Task SelectedPage(int page)
        {
            _parameters.PageNumber = page;
            _parameters.PageSize = MetaData.PageSize;
            await Load();
        }

        private async Task FilterChanged(string filter)
        {
            _parameters.PageNumber = 1;
            _parameters.Filter = filter;
            await Load();
        }
    
        private void OnSelect(FileTypeSettingDTO ft)
        {
            Model = ft;
            navigationManager.NavigateTo($"/references/filetypesetting/{ft.Id}");
            ShowDetail = true;
        }
    }
}
