using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{

    public partial class GisCountryResourceDetail
    {
        [Parameter] public GisCountryDTO GisCountry { get; set; }
        [Inject] public IGisCountryResourceService service { get; set; }
        [Inject] public ToastService toastService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        private MetaData MetaData = new MetaData();
        private Params _parameters = new Params();
        private IEnumerable<GisCountryResourceDTO> valueList;
        private GisCountryResourceDTO Model = new GisCountryResourceDTO();
        private bool isProcessing;
        private string inputValue;
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

        private void OnSelectValue(GisCountryResourceDTO value)
        {
            Model = value;
            inputValue = value.Value.ToString();
        }

        private void Create()
        {
            EditMode = "new";
            Model = new GisCountryResourceDTO()
            {
                GisCountryId = GisCountry.Id,
                Month = DateTime.Now.Date
            };
            inputValue = "";
        }

        private void Edit(GisCountryResourceDTO value)
        {
            Model = value;
            inputValue = value.Value.ToString();
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
                    Text = $"Значение на дату {valueToDelete.Month.Month}.{valueToDelete.Month.Year} будет удалено",
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
                            $"Значение на дату {valueToDelete.Month.Month}.{valueToDelete.Month.Year} было удалено",
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
                //Model.Value = StringParser.TryGetDecimal(inputValue);
                GisCountryResourceDTO result;
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
                Model = new GisCountryResourceDTO();
                toastService.ShowToast(e.Message, ToastLevel.Error);
            }
            await Load();
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
