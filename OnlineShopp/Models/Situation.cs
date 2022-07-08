using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopp.Models
{
    public class Situation
    {
        private Models.E_CommerceEntities db;

        public Situation()
        {
            db = new Models.E_CommerceEntities();
        }

        public situationmodel situationmmodel()
        {   
            situationmodel model = new situationmodel();
            model.ordernumber = db.Orders.Count();
            model.pendingorder = db.Orders.Where(x => x.OrderStatusID == 3).ToList().Count;
            model.customernumber = db.Customers.Count();
            model.productnumber = db.Products.Count();

            return model;


        }


        public class situationmodel
        {
            public int productnumber { get; set; }
            public int customernumber { get; set; }
            public int ordernumber { get; set; }
            public int pendingorder { get; set; }


    }

}
}