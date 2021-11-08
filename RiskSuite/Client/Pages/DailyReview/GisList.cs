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
    public partial class GisList
    {
        [Inject] public IGisService service { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Parameter] public int? Id { get; set; }
        [Parameter] public MetaData MetaData { get; set; } = new MetaData();
        public IEnumerable<GisDTO> Gises { get; set; } = new List<GisDTO>();
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        private GisDTO GisModel { get; set; } = new GisDTO();
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
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                OnSelectGis(Gises.FirstOrDefault(x => x.Id == Id));
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            var pagingResponse = await service.GetAll(_parameters);
            Gises = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsProcessing = false;
        }

        private void Create()
        {
            EditMode = "new";
            GisModel = new GisDTO();
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisDTO result;
                if (GisModel.Id > 0)
                {
                    result = await service.Update(GisModel);
                    if (result != null)
                    {
                        toastService.ShowToast("ГИС успешно обновлён", ToastLevel.Success);
                    }
                }
                else
                {
                    result = await service.Create(GisModel);
                    if (result != null)
                    {
                        toastService.ShowToast("ГИС успешно создан", ToastLevel.Success);
                    }
                }
            }
            catch (Exception e)
            {
                GisModel = new GisDTO();
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
            await Load();
            EditMode = "none";
        }

        private void HandleUpdate(GisDTO gis)
        {
            GisModel = gis;
            EditMode = "edit";
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleDelete(int id)
        {
            var gisToDelete = Gises.Where(x => x.Id == id).FirstOrDefault();
            if (gisToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Gis {gisToDelete.Name} will be deleted",
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
                            $"Gis {gisToDelete.Name} was deleted",
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
    
        private void OnSelectGis(GisDTO gis)
        {
            navigationManager.NavigateTo($"/gis/{gis.Id}");
            GisModel = gis;
            ShowDetail = true;
        }
    }
}
