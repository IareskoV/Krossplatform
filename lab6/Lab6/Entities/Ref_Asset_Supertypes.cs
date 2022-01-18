namespace Lab6.Entities
{
    public class Ref_Asset_Supertypes
    {
        public string Asset_Supertype_Code { get; set; }
        public string Asset_Category_Code { get; set; }
        public string Asset_Supertype_Description { get; set; }
        public CutleryEnum Cutlery { get; set; }
        public enum CutleryEnum
        {
            Cutlery = 0
        }

    }
}