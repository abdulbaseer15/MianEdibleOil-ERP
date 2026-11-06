using ApnaAccountWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class GroupAccounts:Account
    {
        public int IsCtrlAccount { get; set; }
    }
}