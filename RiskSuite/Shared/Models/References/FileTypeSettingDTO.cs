using System.Collections.Generic;

namespace LogSuite.Shared.Models.References
{
    public class FileTypeSettingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> MustHave { get; set; }
        public List<string> NotHave { get; set; }
        public List<string> CountryEntry { get; set; }
        public List<string> GisEntry { get; set; }
        public List<string> RequestedValueEntry { get; set; }
        public List<string> AllocatedValueEntry { get; set; }
        public List<string> EstimatedValueEntry { get; set; }
        public List<string> FactValueEntry { get; set; }
        public List<string> DataEntry { get; set; }
    }
}
