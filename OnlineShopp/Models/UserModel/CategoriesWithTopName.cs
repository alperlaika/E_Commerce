using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopp.Models.UserModel
{
    public class CategoriesWithTopName
    {
        public Guid CategoryID { get; set; }
        public Guid? TopCategoryID { get; set; }
        public string TopCategoryAdi { get; set; }
        public string CategoryAdi { get; set; }

    }
}