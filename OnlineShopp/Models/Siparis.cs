using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopp.Models
{
    public class SiparisUst
    {
        public int OrderID { get; set; }
        public System.DateTime DateOfSale { get; set; }
        public System.Guid CustomerID { get; set; }
        public Nullable<int> OrderStatusID { get; set; }
        public string ParcelTracking { get; set; }
        public Nullable<int> PaymentMethodID { get; set; }
        public Nullable<int> OrderCargoID { get; set; }
        public Nullable<double> OrderTotalPrice { get; set; }
        public string OrderDeliveryAddress { get; set; }
        public string OrderStatusName { get; set; }
        public string PaymentMethodName { get; set; }
        public string OrderCargoName { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
    }
    public class SiparisSatirlarim
    {
        public int OrderDetailsID { get; set; }
        public Nullable<System.Guid> ProductID { get; set; }
        public Nullable<double> OrderDetailsPrice { get; set; }
        public Nullable<int> OrderDetailsAmount { get; set; }
        public int? OrderDetailsOrderID { get; set; }
        public string UrunResim { get; set; }
        public decimal? UrunBirimFiyat { get; set; }
        public string UrunIsmi { get; set; }


    }

}