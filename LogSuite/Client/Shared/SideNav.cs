using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LogSuite.Client.Shared
{
    public partial class SideNav
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ThemeState ThemeState { get; set; }

        private readonly dynamic themes = new[] {
            new { Text = "Default", Value = "default"},
            new { Text = "Dark", Value="dark" },
            new { Text = "Software", Value = "software"},
            new { Text = "Humanistic", Value = "humanistic" }
        };

        private string Theme => $"{ThemeState.CurrentTheme}.css";

        private void ChangeTheme(object value)
        {
            ThemeState.CurrentTheme = value.ToString();
            NavigationManager.NavigateTo(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).ToString());
        }
    }
}
