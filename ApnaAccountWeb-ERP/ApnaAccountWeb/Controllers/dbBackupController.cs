using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ApnaAccountWeb.Controllers
{
    public class dbBackupController : Controller
    {
        //
        // GET: /dbBackup/
        private  string _connectionString;
        private  string _backupFolderFullPath;
        private  string[] _systemDatabaseNames = { "master", "tempdb", "model", "msdb" };
        public ActionResult DbBackup()
        {
            BackupService();
            BackupAllUserDatabases();
            var file = Directory.EnumerateFiles(Server.MapPath("~/../tmp"));
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                string filename = string.Format("{0}-{1}.bak", connection.Database, DateTime.Now.ToString("yyyy-MM-dd"));
                string filepath = Server.MapPath("~/../tmp") + filename;
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                string contentType = MimeMapping.GetMimeMapping(filepath);
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = filename,
                    Inline = true,
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                return File(filedata, contentType);
        }
        public void BackupService()
        {
            _connectionString =  Convert.ToString(Session["consql"]); ;
            //_backupFolderFullPath = @"D:/Inetpub/vhosts/iagsoftware.com/tmp/";
            _backupFolderFullPath = "~/../tmp";
        }
        public void BackupAllUserDatabases()
        {
            //foreach (string databaseName in GetAllUserDatabases())
            //{
            //    BackupDatabase(databaseName);
            //}
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            BackupDatabase(connection.Database);
        }
        public void BackupDatabase(string databaseName)
        {
            string filePath = BuildBackupPathWithFilename(databaseName);
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", databaseName, filePath);
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 950;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private IEnumerable<string> GetAllUserDatabases()
        {
            var databases = new List<String>();
            DataTable databasesTable;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                databasesTable = connection.GetSchema("Databases");
                connection.Close();
            }
            foreach (DataRow row in databasesTable.Rows)
            {
                string databaseName = row["database_name"].ToString();
                if (_systemDatabaseNames.Contains(databaseName))
                    continue;
                databases.Add(databaseName);
            }
            return databases;
        }
        private string BuildBackupPathWithFilename(string databaseName)
        {
            string filename = string.Format("{0}-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MM-dd"));
            return Server.MapPath(_backupFolderFullPath+filename);
        }
    }
}
