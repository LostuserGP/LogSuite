namespace LogSuite.DataAccess.References
{
    public class FileTypeSetting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] MustHave { get; set; }
        public string[] NotHave { get; set; }
        public string[] CountryEntry { get; set; }
        public string[] GisEntry { get; set; }
        public string[] RequestedValueEntry { get; set; }
        public string[] AllocatedValueEntry { get; set; }
        public string[] EstimatedValueEntry { get; set; }
        public string[] FactValueEntry { get; set; }
        public string[] DataEntry { get; set; }
    }
}
