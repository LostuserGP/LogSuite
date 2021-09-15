using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.Operativka
{
    public partial class GisName
    {
        [Inject] public IGisNameService service { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        [Parameter] public GisDTO Gis { get; set; }
        public IEnumerable<GisNameDTO> GisNames { get; set; } = new List<GisNameDTO>();
        private bool IsProcessing { get; set; } = true;
        private GisNameDTO GisNameModel { get; set; } = new GisNameDTO();
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
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.GetAllByGisId(Gis.Id, _parameters);
            GisNames = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            GisNameModel = new GisNameDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisNameDTO result;
                GisNameModel.GisId = Gis.Id;
                if (GisNameModel.Id > 0)
                {
                    result = await service.Update(GisNameModel);
                    if (result != null)
                    {
                        toastService.ShowToast("Наименование для ГИС успешно обновлено", ToastLevel.Success);
                    }
                }
                else
                {
                    result = await service.Create(GisNameModel);
                    if (result != null)
                    {
                        toastService.ShowToast("Наименование для ГИС успешно создано", ToastLevel.Success);
                    }
                }
            }
            catch (Exception e)
            {
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(GisNameDTO gisName)
        {
            GisNameModel = gisName;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var gisNameToDelete = GisNames.Where(x => x.Id == id).FirstOrDefault();
            if (gisNameToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Gis name {gisNameToDelete.Name} will be deleted",
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
                            $"Gis name {gisNameToDelete.Name} was deleted",
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
