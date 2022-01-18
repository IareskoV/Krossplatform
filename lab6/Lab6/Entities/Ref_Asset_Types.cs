namespace Lab6.Entities
{
    public class Ref_Asset_Types
    {
        public string Asset_Type_Code { get; set; }

        public string Asset_Supertype_Code { get; set; }

        public string Asset_Type_Description { get; set; }
        public SpoonEnum Spoon { get; set; }
        public enum SpoonEnum
        {
            Spoon = 0
        }
    }
}