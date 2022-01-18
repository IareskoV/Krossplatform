namespace Lab6.Entities
{
    public class Ref_Sizes
    {
        public string Size_Code { get; set; }
        public string Size_Description { get; set; }
        public Sizes Size { get; set; }
        public enum Sizes
        {
            Small = 0,
            Medium = 1,
            Large = 2
        }
    }
}