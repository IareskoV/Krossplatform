using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Database.Api.Models
{
    public class Asset
    {
        public int Asset_ID { get; set; }
        public string Asset_Type_Code { get; set; }
        [JsonIgnore]
        public virtual Ref_Asset_Types Ref_Asset_Types { get; set; }
        public string Size_Code { get; set; }
        [JsonIgnore]
        public virtual Ref_Sizes Ref_Sizes { get; set; }
        public string Asset_Name { get; set; }
        public string Other_Details { get; set; }


    }
}