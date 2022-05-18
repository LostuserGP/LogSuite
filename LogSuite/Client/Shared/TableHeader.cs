using LogSuite.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LogSuite.Client.Shared
{
    public partial class TableHeader
    {
        [Parameter] public string ColumnName { get; set; }
        [Parameter] public string DbName { get; set; }
        [Parameter] public Params Params { get; set; }
        [Parameter] public EventCallback OnSortChangedSubmit { get; set; }

        private async Task OnSortChanged()
        {
            //Params.PageNumber = 1;
            //if (DbName != Params.Order)
            //{
            //    Params.Order = DbName;
            //    Params.OrderAsc = true;
            //}
            //else
            //{
            //    Params.OrderAsc = !Params.OrderAsc;
            //}
            await OnSortChangedSubmit.InvokeAsync();
        }

        private string SetSortIcon()
        {
            //if (Params.Order != DbName)
            //{
            //    return string.Empty;
            //}
            //if (Params.OrderAsc)
            //{
            //    return "oi-sort-ascending";
            //}
            //else
            //{
            //    return "oi-sort-descending";
            //}
            return "";
        }
    }
}
