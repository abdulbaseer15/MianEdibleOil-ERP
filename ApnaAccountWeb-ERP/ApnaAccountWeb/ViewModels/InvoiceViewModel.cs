using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class InvoiceViewModel
    {
        public string vid { get; set; }
        public string comdate { get; set; }
        public string yourinvoice { get; set; }
        public string yourdate { get; set; }
        public string wbno { get; set; }
        public string grnno { get; set; }
        public string po { get; set; }
        //added by Mutlib for Production
        public string Batch { get; set; }
        public string Plant { get; set; }

        public string supid { get; set; }
        public string supname { get; set; }
        public string purid { get; set; }
        public string purname { get; set; }
        public string store { get; set; }
        public string veh { get; set; }
        public string narration { get; set; }
        public string Milw { get; set; }
        public string brokid { get; set; }
        public string brokname { get; set; }
        public string brokageid { get; set; }
        public string brokagename { get; set; }
        public string brorate { get; set; }
        public string mode { get; set; }
        //public string  ratecalc { get; set; }
        public string gst { get; set; }
        public string barid { get; set; }
        public string barname { get; set; }
        public string Bags { get; set; }
        public string labid { get; set; }
        public string labname { get; set; }
        public string freid { get; set; }
        public string frename { get; set; }
        public string freight { get; set; }
        public string sutid { get; set; }
        public string sutname { get; set; }
        public string discount { get; set; }
        public string nettotal { get; set; }
        public string totalquantity { get; set; }
        public string isupdate { get; set; }
        public string isprint { get; set; }
        public string salestax { get; set; }
        public List<VDetailViewModel> detail { get; set; }
        //** Ahsan New Variables  ***//
        public string BrockerID { get; set; }
        public string storeid { get; set; }
        public string ContractID { get; set; }
        public string BrockerageAccount { get; set; }
        public string BrockeyRate { get; set; }
        public string BrockeryMode { get; set; }
        public string VehcileType { get; set; }
        public string BagRate { get; set; }
        public string bagAmount { get; set; }
        public string TotalWeight { get; set; }
        public string RateCalc { get; set; }
        public string SO_NO { get; set; }
        public string FarmId { get; set; }
        public string NetAmount { get; set; }
        public string LabourAc { get; set; }
        public string BardanaAc { get; set; }
        public string FreightAc { get; set; }
        public string MarketComAccount { get; set; }
        public string LabourRate { get; set; }
        public string labourAmount { get; set; }
        public string SutliAmount { get; set; }
        public string DiscPercent { get; set; }
        public string LabCalOnWt { get; set; }
       public string DispatchNo { get; set; }
        public string GateNo { get; set; }
        //****************************//
    }
}