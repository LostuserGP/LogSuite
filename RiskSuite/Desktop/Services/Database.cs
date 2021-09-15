using LogSuite.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Desktop.Services
{
    public class Database
    {
        public IEnumerable<TodoItem> GetItems() => new[]
        {
            new TodoItem { Description = "Walk the dog" },
            new TodoItem { Description = "Buy some milk" },
            new TodoItem { Description = "Learn Avalonia", IsChecked = true },
        };
    }
}
