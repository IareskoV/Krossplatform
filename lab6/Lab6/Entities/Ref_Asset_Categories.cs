namespace Lab6.Entities
{
    public class Ref_Asset_Categories
    {
        public string Asset_Category_Code { get; set; }
        public string Asset_Category_Description { get; set; }
        public DomesticEnum Domestic { get; set; }
        public enum DomesticEnum
        {
            Domestic = 0
        }
    }
}
