using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.CredRisk
{
    public partial class CounterpartyInternalRating
    {
        [Parameter] public int Id { get; set; }
        [Inject] public ToastService toastrService { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public IRatingInternalService service { get; set; }
        [Inject] public IRatingService ratingService { get; set; }
        [Inject] public IFinancialStatementService fsService { get; set; }
        [Inject] public IRiskClassService rcService { get; set; }
        [Inject] public AuthenticationStateProvider authState { get; set; }
        public IEnumerable<RatingInternalDTO> Ratings { get; set; } = new List<RatingInternalDTO>();
        public IEnumerable<RatingDTO> RatingValues { get; set; } = new List<RatingDTO>();
        public IEnumerable<RiskClassDTO> RiskClasses { get; set; } = new List<RiskClassDTO>();
        public IEnumerable<FinancialStatementDTO> fss { get; set; } = new List<FinancialStatementDTO>();
        public string Order { get; set; }
        public bool OrderAsc { get; set; }
        public RatingInternalDTO RatingModel { get; set; } = new RatingInternalDTO(){DateStart = DateTime.Now};
        public bool NewRating { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Ratings = await service.Getall(Id);
            RatingValues = await ratingService.Getall();
            fss = await fsService.Getall(Id);
            RiskClasses = await rcService.Getall();
        }

        private string SetSortIcon(string columnName)
        {
            if (Order != columnName)
            {
                return string.Empty;
            }
            if (OrderAsc)
            {
                return "oi-sort-ascending";
            }
            else
            {
                return "oi-sort-descending";
            }
        }

        private void OnSortChanged(string columnName)
        {
            if (columnName != Order)
            {
                Order = columnName;
                OrderAsc = true;
            }
            else
            {
                OrderAsc = !OrderAsc;
            }
            if (OrderAsc)
            {
                switch (columnName)
                {
                    case "Id":
                        Ratings = Ratings.OrderBy(x => x.Id);
                        break;
                    case "rName":
                        Ratings = Ratings.OrderBy(x => x.Rating.Score);
                        break;
                    case "wcName":
                        Ratings = Ratings.OrderBy(x => x.RatingWc.Score);
                        break;
                    case "RiskClass":
                        Ratings = Ratings.OrderBy(x => x.RiskClass.Name);
                        break;
                    case "FinStatement":
                        Ratings = Ratings.OrderBy(x => x.FinancialStatement.DateStart);
                        break;
                    case "Date":
                        Ratings = Ratings.OrderBy(x => x.DateStart);
                        break;
                    case "Analyst":
                        Ratings = Ratings.OrderBy(x => x.Analyst);
                        break;
                    case "Conservative":
                        Ratings = Ratings.OrderBy(x => x.IsConservative);
                        break;
                }
            }
            else
            {
                switch (columnName)
                {
                    case "Id":
                        Ratings = Ratings.OrderByDescending(x => x.Id);
                        break;
                    case "rName":
                        Ratings = Ratings.OrderByDescending(x => x.Rating.Score);
                        break;
                    case "wcName":
                        Ratings = Ratings.OrderByDescending(x => x.RatingWc.Score);
                        break;
                    case "RiskClass":
                        Ratings = Ratings.OrderByDescending(x => x.RiskClass.Name);
                        break;
                    case "FinStatement":
                        Ratings = Ratings.OrderByDescending(x => x.FinancialStatement.DateStart);
                        break;
                    case "Date":
                        Ratings = Ratings.OrderByDescending(x => x.DateStart);
                        break;
                    case "Analyst":
                        Ratings = Ratings.OrderByDescending(x => x.Analyst);
                        break;
                    case "Conservative":
                        Ratings = Ratings.OrderByDescending(x => x.IsConservative);
                        break;
                }
            }
        }

        private async Task HandleInternalRatingSubmit()
        {
            RatingInternalDTO result;
            var state = await authState.GetAuthenticationStateAsync();
            if (RatingModel.Id > 0)
            {
                RatingModel.Analyst = state.User.Identity.Name;
                result = await service.Update(RatingModel);
            }
            else
            {
                RatingModel.CounterpartyId = Id;
                result = await service.Create(RatingModel);
            }
            if (result != null)
            {
                Ratings = await service.Getall(Id);
                NewRating = false;
                RatingModel = new RatingInternalDTO()
                {
                    DateStart = DateTime.Now
                };
            }
        }

        private async Task HandleInternalRatingDelete(int id)
        {
            var ratingToDelete = Ratings.Where(x => x.Id == id).FirstOrDefault();
            if (ratingToDelete != null)
            {
                SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Are you sure?",
                    Text = $"Rating {ratingToDelete.Rating.Name} at date {ratingToDelete.DateStart.ToString("dd.MM.yyyy")} will be deleted",
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
                            $"Rating {ratingToDelete.Rating.Name} at date {ratingToDelete.DateStart.ToString("dd.MM.yyyy")} was deleted", 
                            SweetAlertIcon.Success);
                        Ratings = await service.Getall(Id);
                    }
                }
            }
        }

        private void RatingToUpdate(RatingInternalDTO rating)
        {
            RatingModel = rating;
            NewRating = true;
        }

        private async Task AddRating()
        {
            var state = await authState.GetAuthenticationStateAsync();
            RatingModel = new RatingInternalDTO()
            {
                DateStart = DateTime.Now,
                Analyst = state.User.Identity.Name
            };
            NewRating = true;
        }

        private async Task CancelAsync()
        {
            NewRating = false;
            var state = await authState.GetAuthenticationStateAsync();
            RatingModel = new RatingInternalDTO()
            {
                DateStart = DateTime.Now,
                Analyst = state.User.Identity.Name
            };
        }
    }
}
