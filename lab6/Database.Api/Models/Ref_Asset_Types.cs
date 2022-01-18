using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Database.Api.Models
{
    public class Ref_Asset_Types
    {
        public string Asset_Type_Code { get; set; }

        public string Asset_Supertype_Code { get; set; }
        [JsonIgnore]
        public virtual Ref_Asset_Supertypes Ref_Asset_Supertypes { get; set; }
        public string Asset_Type_Description { get; set; }
        public SpoonEnum Spoon { get; set; }
        public enum SpoonEnum
        {
            Spoon = 0
        }
    }
}