using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class VouchersVerification
    {
        public string AccountId { get; set;}
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string Narration { get; set; }
        public int VId { get; set; }
        public DateTime VDate { get; set; }
        public string Vtype { get; set; }
        public string Title { get; set; }
        public string RefNo { get; set; }
        public int VId2 { get; set; }
        public string ischecked { get; set; }
    }
}