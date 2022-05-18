using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Client.Services.References;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace LogSuite.Client.Pages.DailyReview.References;

[Authorize(Roles = SD.Role_User)]
public partial class PageCountry
{
    [Parameter] public int? Id { get; set; }
    [Parameter] public MetaData MetaData { get; set; } = new MetaData();
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public ICountryService Service { get; set; }
    [Inject] public NotificationService NotificationService { get; set; }
    private readonly IEnumerable<int> _pageSizeOptions = new int[] {10, 35, 70};
    private CountryDTO _selectedCountry;
    private IList<CountryDTO> _countries;
    private IList<NameObject> _names;
    private RadzenDataGrid<CountryDTO> _countriesGrid;
    private RadzenDataGrid<NameObject> _countryNamesGrid;
    private Params _parameters = new();
    private bool _isProcessing;
    private CountryDTO _countryToInsert;
    private NameObject _countryNameToInsert;

    protected override async Task OnParametersSetAsync()
    {
        if (Id != null)
        {
            _selectedCountry = await Service.Get(Id.Value);
            if (_selectedCountry != null) _names = NameObject.StringToObject(_selectedCountry.Names);
        };
    }

    private async Task LoadData(LoadDataArgs args)
    {
        _isProcessing = true;
        _parameters.Filter = args.Filter;
        _parameters.Top = args.Top ?? 10;
        _parameters.Skip = args.Skip ?? 0;
        _parameters.OrderBy = args.OrderBy;
        var pagingResponse = await Service.GetAll(_parameters);
        _countries = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
        //await countriesGrid.Reload();
        _isProcessing = false;
    }

    private void OnSelect(CountryDTO country)
    {
        if (_countryToInsert != null) return;
        NavigationManager.NavigateTo("/references/country/" + country.Id);
    }

    private async Task InsertRow()
    {
        _countryToInsert = new CountryDTO();
        await _countriesGrid.InsertRow(_countryToInsert);
    }

    private async Task InsertCountryName()
    {
        _countryNameToInsert = new NameObject("");
        await _countryNamesGrid.InsertRow(_countryNameToInsert);
    }

    private async Task OnCreateRow(CountryDTO country)
    {
        var result = await Service.Create(country);
        if (result != null)
        {
            await _countriesGrid.Reload();
            NavigationManager.NavigateTo("/references/country/" + result.Id);
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Страна создана",
                Detail = "Страна " + country.Name + " успешно создана",
                Duration = 3000
            });
        }
    }

    private async Task OnCreateRow(NameObject countryName)
    {
        _selectedCountry.Names.Add(countryName.Name);
        var result = await Service.Update(_selectedCountry);
        if (result != null)
        {
            //await countryNamesGrid.Reload();
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Наименование для страны добавлено",
                Detail = "Наименование " + countryName.Name + " успешно создано",
                Duration = 3000
            });
        }
    }

    private async Task DeleteRow(CountryDTO country)
    {
        if (_countries.Contains(country))
        {
            var result = await Service.Delete(country.Id);

            if (result)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Страна удалена",
                    Detail = "Страна " + country.Name + " успешно удалена",
                    Duration = 3000
                });
            }

            await _countriesGrid.Reload();
        }
        else
        {
            _countriesGrid.CancelEditRow(country);
        }
    }

    private async Task DeleteRow(NameObject countryName)
    {
        if (_selectedCountry.Names.Contains(countryName.Name))
        {
            _selectedCountry.Names.Remove(countryName.Name);
            var result = await Service.Update(_selectedCountry);
            await _countryNamesGrid.Reload();
        }
        else
        {
            _countryNamesGrid.CancelEditRow(countryName);
        }
    }

    private void EditRow(CountryDTO country)
    {
        _countriesGrid.EditRow(country);
    }

    private void EditRow(NameObject countryName)
    {
        _countryNamesGrid.EditRow(countryName);
    }

    private async Task OnUpdateRow(CountryDTO country)
    {
        var result = await Service.Update(_selectedCountry);
        if (result != null)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = "Страна обновлена",
                Detail = "Страна " + country.Name + " успешно обновлена",
                Duration = 3000
            });
        }
    }

    private async Task OnUpdateRow(NameObject countryName)
    {
        var result = await Service.Update(_selectedCountry);
    }

    private async Task SaveRow(CountryDTO country)
    {
        if (country == _countryToInsert)
        {
            _countryToInsert = null;
        }
        await _countriesGrid.UpdateRow(country);
    }

    private async Task SaveRow(NameObject cName)
    {
        if (cName == _countryNameToInsert)
        {
            _countryNameToInsert = null;
        }
        await _countryNamesGrid.UpdateRow(cName);
    }

    private void CancelEdit(CountryDTO country)
    {
        _countriesGrid.CancelEditRow(country);
    }

    private void CancelEdit(NameObject cName)
    {
        _countryNamesGrid.CancelEditRow(cName);
    }
}