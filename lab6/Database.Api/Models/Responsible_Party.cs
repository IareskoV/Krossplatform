using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Api.Models
{
    public class Responsible_Party
    {
        public int Party_ID { get; set; }
        public string Party_Details { get; set; }

    }
}