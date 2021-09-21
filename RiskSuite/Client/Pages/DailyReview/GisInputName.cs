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
    public partial class GisInputName
    {
        [Inject] public IGisInputNameService service { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Parameter] public GisDTO Gis { get; set; }
        public IEnumerable<GisInputNameDTO> GisInputNames { get; set; } = new List<GisInputNameDTO>();
        private bool IsProcessing { get; set; } = true;
        private GisInputNameDTO GisInputNameModel { get; set; } = new GisInputNameDTO();
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
            var pagingResponse = await service.GetAllByGisId(Gis.Id, _parameters);
            GisInputNames = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            GisInputNameModel = new GisInputNameDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisInputNameDTO result;
                GisInputNameModel.GisId = Gis.Id;
                if (GisInputNameModel.Id > 0)
                {
                    result = await service.Update(GisInputNameModel);
                    if (result != null)
                    {
                        toastService.ToastSuccess("Gis input name succesfully updated");
                    }
                }
                else
                {
                    result = await service.Create(GisInputNameModel);
                    if (result != null)
                    {
                        toastService.ToastSuccess("Gis input name succesfully created");
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

        private void HandleUpdate(GisInputNameDTO gisInputName)
        {
            GisInputNameModel = gisInputName;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var gisInputNameToDelete = GisInputNames.Where(x => x.Id == id).FirstOrDefault();
            if (gisInputNameToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Gis input name {gisInputNameToDelete.Name} will be deleted",
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
                            $"Gis input name {gisInputNameToDelete.Name} was deleted",
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
