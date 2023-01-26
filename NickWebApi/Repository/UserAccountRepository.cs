using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NickWebApi.Models;
using NickWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace NickWebApi.Repository
{
    public class UserAccountRepository: IUserAccountRepository
    {

        NickDBContext db;
        public UserAccountRepository(NickDBContext _db)
        {
            db = _db;
        }


        
            public async Task<List<UserAccount>> GetUserAccountByUserName(string UserName)
        {
            if (db != null)
            {


                return await db.UserAccounts.Where(x => x.UserName == UserName).ToListAsync();


            }

            return null;
        }

       
        public async Task<string> AddUserAccount(UserAccount UserAccount)
        {
            if (db != null)
            {
                await db.UserAccounts.AddAsync(UserAccount);
                await db.SaveChangesAsync();

                return UserAccount.UserAccountCode;
            }

            return string.Empty;
        }

        public async Task<int> DeleteUserAccount(string UserAccountCode)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var UserAccount = await db.UserAccounts.FirstOrDefaultAsync(x => x.UserAccountCode == UserAccountCode);

                if (UserAccount != null)
                {
                    //Delete that post
                    db.UserAccounts.Remove(UserAccount);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateUserAccount(UserAccount UserAccount)
        {
            if (db != null)
            {
                //Update that UserAccount
                db.Update(UserAccount);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }


}
