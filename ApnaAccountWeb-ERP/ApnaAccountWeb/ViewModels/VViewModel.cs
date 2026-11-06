using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class VViewModel
    {
        public int DVCId { get; set; }
        public string AccountId { get; set; }
        public string AccTitle { get; set; }
        public decimal Debit { get; set; }
        //public decimal Credit { get; set; }
        public string Narration { get; set; }
        public int DVId { get; set; }
        public System.DateTime Date { get; set; }
    }
}