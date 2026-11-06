using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class VoucherViewModel
    {
        public string vid { get; set; }
        public string VoucherId { get; set; }
        public string date { get; set; }
        public string isupdate { get; set; }
        public string isprint { get; set; }
        public string CashType { get; set; }
        public List<VDetailViewModel> detail { get; set; }
    }
}