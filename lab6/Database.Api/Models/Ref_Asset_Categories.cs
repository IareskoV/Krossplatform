using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Api.Models
{
    public class Ref_Asset_Categories
    {
        public string Asset_Category_Code { get; set; }
        public string Asset_Category_Description { get; set; }
        public DomesticEnum Domestic { get; set; }
        public enum DomesticEnum
        {
            Domestic = 0
        }
    }
}
