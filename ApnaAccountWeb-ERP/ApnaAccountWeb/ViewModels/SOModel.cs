using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class SOModel
    {
        public string SO_No { get; set; }
        public int ProductID { get; set; }
        public string ItemDetail { get; set; }
        public decimal QtyCr { get; set; }
        public decimal WtCr { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int FarmId { get; set; }
    }
}