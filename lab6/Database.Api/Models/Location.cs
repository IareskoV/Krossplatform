using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Api.Models
{
    public class Location
    {
        public int Location_ID { get; set; }
        public string Location_Details { get; set; }
    }
}