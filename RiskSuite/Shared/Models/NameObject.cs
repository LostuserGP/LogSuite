namespace LogSuite.Shared.Models;

public class INameObject
{
    public string Name { get; set; }

    public NameObject(string name)
    {
        this.Name = name;
    }
}