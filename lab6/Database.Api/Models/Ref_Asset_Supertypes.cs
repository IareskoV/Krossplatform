using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Database.Api.Models
{
    public class Ref_Asset_Supertypes
    {
        public string Asset_Supertype_Code { get; set; }
        public string Asset_Category_Code { get; set; }
        [JsonIgnore]
        public virtual Ref_Asset_Categories Ref_Asset_Categories { get; set; }
        public string Asset_Supertype_Description { get; set; }
        public CutleryEnum Cutlery { get; set; }
        public enum CutleryEnum
        {
            Cutlery = 0
        }

    }
}