using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using NickApp.Models;
namespace NickApp.SqliteServices
{
    public partial class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath, false);
            db.CreateTableAsync<Incident>().Wait();
            db.CreateTableAsync<UserAccount>().Wait();

        }


    }
}
