using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace ApnaAccountWeb.ViewModels
{
    public class DbConection
    {
        public  string Connect(string db)
        {
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                ////********Local DATABASE Yaqoob-PC************
                //DataSource = "YAQOOB-PC\\SQLEXPRESS01", // Server name
                //InitialCatalog = db,  //Database
                //UserID = "sa",         //Username
                //Password = "123",  //Password  

                ////New RPD Server    173.208.208.143,61926
                DataSource = "localhost", // Server name
                InitialCatalog = db,  //Database
                UserID = "admin",         //Username
                Password = "6w%lJ!#8Cd@",  //Password  


                ////********Demo For Chakki Khata ATTA************
                //DataSource = "atta", // Server name
                //InitialCatalog = db,  //Database
                //UserID = "sa",         //Username
                //Password = "123",  //Password  


            };
            EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                Metadata = "res://*/Models.ApnaAccount.csdl|res://*/Models.ApnaAccount.ssdl|res://*/Models.ApnaAccount.msl",
                ProviderConnectionString = sqlString.ToString()
            };
            return entityString.ConnectionString;
        }
    }
}