using LogSuite.Client.Parsers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    public partial class ReviewInput
    {
        [Inject] public ToastService toastService { get; set; }
        [Inject] public IGisService gisService { get; set; }
        [Inject] public IFileTypeSettingService fileService { get; set; }
        [Inject] public IReviewValueService valueService { get; set; }
        private bool isReady = false;
        private string currentFile;
        private List<FileMessage> messages = new List<FileMessage>();

        private async Task LoadExcelFiles(InputFileChangeEventArgs e)
        {
            isReady = false;
            messages = new List<FileMessage>();
            if (e.FileCount > 100)
            {
                toastService.ToastError("Слишком много файлов");
                isReady = true;
                return;
            }
            foreach (var file in e.GetMultipleFiles(100))
            {
                messages.Add(new FileMessage()
                {
                    Filename = file.Name
                });
            }
            foreach (var file in e.GetMultipleFiles(100))
            {
                currentFile = file.Name;
                var parser = new DailyReviewExcelParser(file, fileService, gisService, toastService);
                await parser.ParseFile();
                var reviewList = parser.GetResult();
                if (reviewList.Message != null)
                {
                    var message = messages.Where(x => x.Filename == reviewList.Filename).FirstOrDefault();
                    message.Message = reviewList.Message;
                    toastService.ToastError(reviewList.Message);
                }
                else
                {
                    await SaveResult(reviewList);
                }
                StateHasChanged();
            }
            currentFile = null;
            isReady = true;
        }

        private async Task SaveResult(ReviewValueListDTO reviewList)
        {
            var message = messages.Where(x => x.Filename == reviewList.Filename).FirstOrDefault();
            var result = await valueService.CreateOrUpdate(reviewList);
            message.SendedCount = reviewList.Values.Count;
            message.SavedCount = result[0];
            message.UpdatedCount = result[1];
        }

        private class FileMessage
        {
            public string Filename { get; set; }
            public string Message { get; set; }
            public int SavedCount { get; set; }
            public int UpdatedCount { get; set; }
            public int SendedCount { get; set; }
            public bool IsLoading { get; set; }
            public bool IsLoaded { get; set; }
        }
    }
}
