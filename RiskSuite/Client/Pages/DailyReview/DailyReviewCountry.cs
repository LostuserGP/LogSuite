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
    //[Authorize(Roles = SD.Role_User)]
    public partial class DailyReviewCountry
    {
        [Parameter] public GisCountryDTO GisCountry { get; set; }
        [Inject] public IGisCountryValueService service { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        private MetaData MetaData = new MetaData();
        private Params _parameters = new Params();
        private IEnumerable<GisCountryValueDTO> valueList;
        private GisCountryValueDTO Model = new GisCountryValueDTO();
        private bool isProcessing;
        private string EditMode = "none";

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

        public async Task Load()
        {
            isProcessing = true;
            var pagingResponse = await service.GetPagedByGisCountryId(GisCountry.Id, _parameters);
            valueList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            isProcessing = false;
        }

        private void OnSelectValue(GisCountryValueDTO value)
        {
            Model = value;
        }

        private void Create()
        {
            EditMode = "new";
            Model = new GisCountryValueDTO();
        }

        private void Edit(GisCountryValueDTO value)
        {
            Model = value;
            EditMode = "edit";
        }

        private async Task Delete(int id)
        {
            var valueToDelete = valueList.Where(x => x.Id == id).FirstOrDefault();
            if (valueToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Вы уверенеы?",
                    Text = $"Значение на дату {valueToDelete.DateReport.ToString("dd.MM.yy")} будет удалено",
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
                        await Load();
                        await Swal.FireAsync(
                            "Deleted",
                            $"Значение на дату {valueToDelete.DateReport.ToString("dd.MM.yy")} было удалено",
                            SweetAlertIcon.Success);
                    }
                }
            }
        }

        private void Cancel()
        {
            EditMode = "none";
        }

        private async Task HandleSubmit()
        {
            try
            {
                GisCountryValueDTO result;
                if (Model.Id > 0)
                {
                    result = await service.Update(Model);
                    if (result != null)
                    {
                        toastService.ShowToast("Данные успешно обновлены", ToastLevel.Success);
                    }
                }
                else
                {
                    result = await service.Create(Model);
                    if (result != null)
                    {
                        toastService.ShowToast("Данные успешно созданы", ToastLevel.Success);
                    }
                }
            }
            catch (Exception e)
            {
                Model = new GisCountryValueDTO();
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
            EditMode = "none";
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
