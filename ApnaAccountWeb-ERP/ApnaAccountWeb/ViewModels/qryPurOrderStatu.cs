using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class qryPurOrderStatu
    {
        public string InvNo { get; set; }
        public Nullable<System.DateTime> Idate { get; set; }
        public string AccountId { get; set; }
        public string Title { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ItemDetail { get; set; }
        public Nullable<decimal> QtyDr { get; set; }
        public Nullable<decimal> QtyCr { get; set; }
        public Nullable<decimal> WtDr { get; set; }
        public Nullable<decimal> WtCr { get; set; }
        public string PO_No { get; set; }
        public Nullable<int> Sno { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string RateCalc { get; set; }
        public Nullable<decimal> AmountDR { get; set; }
        public int AmountCR { get; set; }
        public string VehNo { get; set; }
    }
}