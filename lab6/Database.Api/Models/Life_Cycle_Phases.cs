using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Api.Models
{
    public class Life_Cycle_Phases
    {

        public string Life_Cycle_Code { get; set; }
        public string Life_Cycle_Name { get; set; }
        public string Life_Cycle_Description { get; set; }
    }
}