using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class POModel
    {
        public string PO_No { get; set; }
        public int ProductID { get; set; }
        public string ItemDetail { get; set; }
        public decimal QtyDr { get; set; }
        public decimal WtDr { get; set; }
        public decimal Rate { get; set; }
        public string RateCalc { get; set; }
    }
}