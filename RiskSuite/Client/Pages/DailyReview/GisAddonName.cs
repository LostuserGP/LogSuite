using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    public partial class GisAddonName
    {
        [Inject] public IGisAddonNameService service { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Parameter] public GisAddonDTO Addon { get; set; }
        public IEnumerable<GisAddonNameDTO> AddonNames { get; set; } = new List<GisAddonNameDTO>();
        private bool IsProcessing { get; set; } = true;
        private GisAddonNameDTO GisAddonNameModel { get; set; } = new GisAddonNameDTO();
        private string EditMode { get; set; } = "none";
        private Params _parameters = new Params();


        protected override async Task OnInitializedAsync()
        {

        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await Load();
            }
            catch (Exception e)
            {
                toastService.ToastError(e.Message);
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.GetPagedByGisAddonId(Addon.Id, _parameters);
            AddonNames = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            GisAddonNameModel = new GisAddonNameDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisAddonNameDTO result;
                GisAddonNameModel.GisAddonId = Addon.Id;
                if (GisAddonNameModel.Id > 0)
                {
                    result = await service.Update(GisAddonNameModel);
                    if (result != null)
                    {
                        toastService.ToastSuccess("GisAddon name succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(GisAddonNameModel);
                    if (result != null)
                    {
                        toastService.ToastSuccess("GisAddon name succesfully created");
                    }
                }
            }
            catch (Exception e)
            {
                toastService.ToastError(e.Message);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(GisAddonNameDTO addonName)
        {
            GisAddonNameModel = addonName;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var gisAddonNameToDelete = AddonNames.Where(x => x.Id == id).FirstOrDefault();
            if (gisAddonNameToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"GisAddon name {gisAddonNameToDelete.Name} will be deleted",
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
                            $"GisAddon name {gisAddonNameToDelete.Name} was deleted",
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
    }
}
