using ECommerceWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWebSite.Models
{
    // Sadece sepete ekleyen görünmesi için aşağıdaki kod(kim ekledi, Başka kullanıcı görünmemesi için)
    public class SepetUrunOku
    {
        public string UrunResim { get; set; }
        public decimal? UrunBirimFiyat { get; set; }
        public Guid? ShoppingCartProductID { get; set; }
        public Guid? ShoppingCartCustormerID { get; set; }
        public double? ShoppingCartTotal { get; set; }
        public int? ShoppingCartAmount { get; set; }
        public string UrunIsmi { get; set; }
        public int ShoppingCartID { get; set; }
        public int ShoppingCartStatus { get; set; }
    }
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