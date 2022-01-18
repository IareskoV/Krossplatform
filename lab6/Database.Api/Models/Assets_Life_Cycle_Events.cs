using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Database.Api.Models
{
    public class Assets_Life_Cycle_Events
    {
        public int Asset_Life_Cycle_Event_ID { get; set; }

        public int Asset_ID { get; set; }
        [JsonIgnore]
        public virtual Asset Asset { get; set; }
        public string Life_Cycle_Code { get; set; }

        [JsonIgnore]
        public virtual Life_Cycle_Phases Life_Cycle_Phases { get; set; }

        public int Location_ID { get; set; }

        [JsonIgnore]
        public virtual Location Location { get; set; }

        public int Party_ID { get; set; }

        [JsonIgnore]
        public virtual Responsible_Party Responsible_Party { get; set; }

        public string Status_Code { get; set; }

        [JsonIgnore]
        public virtual Ref_Status Ref_Status { get; set; }

        public DateTimeOffset Date_From { get; set; }
        public DateTimeOffset Date_To { get; set; }


    }
}