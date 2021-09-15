using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.CredRisk
{
    public partial class CounterpartyExternalRating
    {
        [Parameter] public int Id { get; set; }
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public IRatingExternalService service { get; set; }
        [Inject] public IRatingAgencyService agencyService { get; set; }
        [Inject] public IRatingService ratingService { get; set; }
        public IEnumerable<RatingExternalDTO> Ratings { get; set; } = new List<RatingExternalDTO>();
        public IEnumerable<RatingAgencyDTO> Agencies = new List<RatingAgencyDTO>();
        public IEnumerable<RatingDTO> RatingValues = new List<RatingDTO>();
        public string Order { get; set; }
        public bool OrderAsc { get; set; }
        public RatingExternalDTO RatingModel { get; set; } = new RatingExternalDTO() { DateStart = DateTime.Now};
        public bool NewRating { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Ratings = await service.Getall(Id);
            Agencies = await agencyService.Getall();
            RatingValues = await ratingService.Getall();
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
                    case "Rating":
                        Ratings = Ratings.OrderBy(x => x.Rating.Score);
                        break;
                    case "Agency":
                        Ratings = Ratings.OrderBy(x => x.RatingAgency.Name);
                        break;
                    case "Date":
                        Ratings = Ratings.OrderBy(x => x.DateStart);
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
                    case "Rating":
                        Ratings = Ratings.OrderByDescending(x => x.Rating.Score);
                        break;
                    case "Agency":
                        Ratings = Ratings.OrderByDescending(x => x.RatingAgency.Name);
                        break;
                    case "Date":
                        Ratings = Ratings.OrderByDescending(x => x.DateStart);
                        break;
                }
            }
        }

        private async Task HandleExternalRatingSubmit()
        {
            RatingExternalDTO result;
            if (RatingModel.Id > 0)
            {
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
                RatingModel = new RatingExternalDTO()
                {
                    DateStart = DateTime.Now
                };
            }
        }

        private async Task HandleExternalRatingDelete(int id)
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

        private void RatingToUpdate(RatingExternalDTO rating)
        {
            RatingModel = rating;
            NewRating = true;
        }

        private void Cancel()
        {
            NewRating = false;
            RatingModel = new RatingExternalDTO()
            {
                DateStart = DateTime.Now
            };
        }
    }
}
