using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LogSuite.Desktop.Views
{
    public class TodoListView : Window
    {
        public TodoListView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
