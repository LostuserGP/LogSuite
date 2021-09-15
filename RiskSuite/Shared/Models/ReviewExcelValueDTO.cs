namespace LogSuite.Shared.Models
{
    public class ReviewExcelValueDTO
    {
        public string GisName { get; set; }
        public string CountryName { get; set; }
        public int Type { get; set; }
        public double Value { get; set; }

        public bool Like(ReviewExcelValueDTO reviewDTO)
        {
            if (GisName == reviewDTO.GisName
                && CountryName == reviewDTO.CountryName
                && Type == reviewDTO.Type)
            {
                return true;
            }
            return false;
        }

        public bool LikeCountryName(ReviewExcelValueDTO reviewDTO)
        {
            if (CountryName == reviewDTO.CountryName
                && Type == reviewDTO.Type)
            {
                return true;
            }
            return false;
        }
    }
}
