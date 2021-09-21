using LogSuite.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Pages.DailyReview
{
    public partial class Operativka
    {
        [Inject] public IJSRuntime jsRuntime { get; set; }
        [Inject] public ILogger<Operativka> Logger { get; set; }
        private List<IBrowserFile> loadedFiles = new();
        private IBrowserFile reviewFile;
        private long maxFileSize = 1024 * 15;
        private int maxAllowedFiles = 3;
        private bool isLoading;
        private string result = "pusto";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                
            }
            catch (Exception e)
            {
                await jsRuntime.ToastrError(e.Message);
            }
        }

        private void LoadReview(InputFileChangeEventArgs e)
        {
        }

        private void LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                try
                {
                    loadedFiles.Add(file);
                }
                catch (Exception ex)
                {
                    Logger.LogError("File: {Filename} Error: {Error}",
                        file.Name, ex.Message);
                }
            }

            isLoading = false;
        }
    }
}
