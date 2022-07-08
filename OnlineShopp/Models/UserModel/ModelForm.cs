using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopp.Models.UserModel
{
    public class ModelForm
    {

        public System.Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public string ProductDetails { get; set; }
        public string ProductStock_Code { get; set; }
        public Nullable<int> ProductNumber { get; set; }
        public Nullable<System.DateTime> ProductAddedDate { get; set; }
        public System.Guid CategoryID { get; set; }
        public Nullable<int> DeliveryID { get; set; }

    }
}