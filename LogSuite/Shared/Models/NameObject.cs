using System.Collections.Generic;
using System.Linq;

namespace LogSuite.Shared.Models;

public class NameObject
{
    public string Name { get; set; }

    public NameObject(string name)
    {
        this.Name = name;
    }

    public static List<NameObject> StringToObject(IEnumerable<string> list)
    {
        return list.Select(name => new NameObject(name)).ToList();
    }
}