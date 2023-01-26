using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NickApp.Models;

namespace NickApp.SqliteServices
{
    public partial class SQLiteHelper
    {
        //Insert and Update new record
        public Task<int> SaveUserAccountAsync(UserAccount userAccount)
        {
            return db.UpdateAsync(userAccount);
        }

        public Task<int> AddUserAccountAsync(UserAccount userAccount)
        {
            return db.InsertAsync(userAccount);
        }
        //Delete
        public Task<int> DeleteItemAsync(UserAccount userAccount)
        {
            return db.DeleteAsync(userAccount);
        }

     
        public Task<List<UserAccount>> GetUserAccountByUserName(string userName)
        {
            return db.Table<UserAccount>().Where(s => s.UserName == userName).ToListAsync();
        }
    }
}
