using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class OpStock
    {
        public int Productid { get; set; }
        public string ProductName { get; set; }
        public decimal UnitQty { get; set; }
        public decimal UnitWt { get; set; }
        public decimal UnitRate { get; set; }
        public decimal Amounts { get; set; }
    }
}