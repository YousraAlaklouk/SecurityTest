using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityTest.Model
{
    public class Results
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "TestID")]
        public string TestID { get; set; }

        [Display(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [Display(Name = "Result")]
        public string Result { get; set; }

        [Display(Name = "State")]
        public bool State { get; set; }

        public List<Results> Enrollsinfo { get; set; }
    }
}