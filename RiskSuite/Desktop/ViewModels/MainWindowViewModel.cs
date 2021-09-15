using LogSuite.Desktop.Services;
using LogSuite.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel(Database db)
        {
            List = new TodoListViewModel(db.GetItems());
        }

        public TodoListViewModel List { get; }
    }
}
