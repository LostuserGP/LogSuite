using Microsoft.AspNetCore.Components;
using RiskSuite.Client.Helpers;
using RiskSuite.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RiskSuite.Client.Shared
{
    public partial class Pagination
    {
        private Timer _timer;
        [Parameter]
        public MetaData MetaData { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }
        public string Filter { get; set; }
        [Parameter]
        public EventCallback<string> OnFilterChanged { get; set; }

        private List<PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();

            _links.Add(new PagingLink(1, MetaData.HasPrevious, "<<"));

            _links.Add(new PagingLink(MetaData.CurrentPage - 1, MetaData.HasPrevious, "<"));

            for (int i = 1; i <= MetaData.TotalPages; i++)
            {
                if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
                }
            }

            _links.Add(new PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, ">"));
            _links.Add(new PagingLink(MetaData.TotalPages, MetaData.HasNext, ">>"));
        }

        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;

            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }

        private async Task OnSetPageSize(int pageSize)
        {
            MetaData.PageSize = pageSize;
            await SelectedPage.InvokeAsync(1);
        }

        private void FilterChanged()
        {
            if (_timer != null)
                _timer.Dispose();
            _timer = new Timer(OnTimerElapsed, null, 500, 0);
        }
        private void OnTimerElapsed(object sender)
        {
            OnFilterChanged.InvokeAsync(Filter);
            _timer.Dispose();
        }
    }
}
