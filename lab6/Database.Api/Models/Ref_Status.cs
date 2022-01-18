using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Api.Models
{
    public class Ref_Status
    {
        public string Status_Code { get; set; }
        public string Status_Description { get; set; }
    }
}