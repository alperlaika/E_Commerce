using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopp.Models
{
    public class baseClass
    {

        public int Id { get; set; }
        public DateTime Record_Date { get; set; }
        public DateTime Update_Date { get; set; }
        public bool IsDelete { get; set; }

    }
}