using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Labs
{
    interface ILab
    {
        public string Execute(string input);
        public string Description { get; set; }
    }
}
