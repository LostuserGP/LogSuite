using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    [Authorize(Roles = SD.Role_User + ", " + SD.Role_Admin)]
    public partial class GisAddonList
    {
        [Inject] public IGisAddonService service { get; set; }
        [Inject] public IGisService gisService { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        public IEnumerable<GisDTO> Gises { get; set; } = new List<GisDTO>();
        public IEnumerable<GisAddonDTO> Addons { get; set; } = new List<GisAddonDTO>();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        private GisAddonDTO GisAddonModel { get; set; } = new GisAddonDTO();
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();
        

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Gises = await gisService.Getall();
                await Load();
            }
            catch (Exception e)
            {
                toastService.ToastError(e.Message);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                OnSelectAddon(Addons.FirstOrDefault(x => x.Id == Id));
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.GetPaged(_parameters);
            Addons = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            GisAddonModel = new GisAddonDTO();
            EditMode = "new";
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisAddonDTO result;
                if (GisAddonModel.Id > 0)
                {
                    result = await service.Update(GisAddonModel);
                    if (result != null)
                    {
                        toastService.ToastSuccess("Gis addon succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(GisAddonModel);
                    if (result != null)
                    {
                        toastService.ToastSuccess("Gis addon succesfully created");
                    }
                }
            }
            catch (Exception e)
            {
                GisAddonModel = new GisAddonDTO();
                toastService.ToastError(e.Message);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(GisAddonDTO addon)
        {
            GisAddonModel = addon;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var addonToDelete = Addons.Where(x => x.Id == id).FirstOrDefault();
            if (addonToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Gis addon {addonToDelete.Name} will be deleted",
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
                            $"Gis addon {addonToDelete.Name} was deleted",
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
    
        private void OnSelectAddon(GisAddonDTO addon)
        {
            navigationManager.NavigateTo($"/gisaddon/{addon.Id}");
            GisAddonModel = addon;
            ShowDetail = true;
        }
    }
}
