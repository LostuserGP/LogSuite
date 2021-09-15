using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.References
{
    [Authorize(Roles = SD.Role_Power_User + ", " + SD.Role_Admin)]
    public partial class CommitteeLimit
    {
        public IEnumerable<CommitteeLimitDTO> Values { get; set; } = new List<CommitteeLimitDTO>();
        [Inject] public ICommitteeLimitService service { get; set; }
        private bool IsProcessing { get; set; } = true;
        private bool ShowDetail { get; set; } = false;
        [Parameter] public int? Id { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //if (Id != null)
                //{
                //    ShowDetail = true;
                //}
                await Load();
            }
            catch (Exception e)
            {
                //await jsRuntime.ToastrError(e.Message);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
            {
                ShowDetail = true;
            }
        }

        private async Task Load()
        {
            IsProcessing = true;
            Values = await service.Getall();
            IsProcessing = false;
        }

        protected async Task ValueSubmitEvent()
        {
            ShowDetail = false;
            navigationManager.NavigateTo("/references/committeelimit");
            await Load();
        }

        private void ShowDetailCancel()
        {
            ShowDetail = false;
            navigationManager.NavigateTo("/references/committeelimit");
        }
    }
}
