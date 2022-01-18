using System;

namespace Lab6.Entities
{
    public class Assets_Life_Cycle_Events
    {
        public int Asset_Life_Cycle_Event_ID { get; set; }
        public int Asset_ID { get; set; }
        public string Life_Cycle_Code { get; set; }

        public int Location_ID { get; set; }

        public int Party_ID { get; set; }

        public string Status_Code { get; set; }

        public DateTimeOffset Date_From { get; set; }
        public DateTimeOffset Date_To { get; set; }


    }
}