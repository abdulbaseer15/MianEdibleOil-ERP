using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class POModel1
    {
        public string PO_No { get; set; }
        public decimal ProductID { get; set; }
        public string ItemDetail { get; set; }
        public decimal QtyDr { get; set; }
        public decimal WtDr { get; set; }
        public decimal Rate { get; set; }
    }
}