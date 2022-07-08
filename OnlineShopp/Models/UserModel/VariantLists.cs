using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopp.Models.UserModel
{
    public class VariantLists
    {
            public int ID { get; set; }
            public string Description { get; set; }
            public string CategoryName { get; set; }
            public Guid CategoryID { get; set; }

    }
}