using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NickWebApi.Models;
using NickWebApi.Repository;

namespace NickWebApi.Repository
{
    public interface IUserAccountRepository
    {

        Task<List<UserAccount>> GetUserAccountByUserName(string UserName);

        Task<string> AddUserAccount(UserAccount UserAccount);

        Task<int> DeleteUserAccount(string UserAccountCode);

        Task UpdateUserAccount(UserAccount UserAccount);
    }
}
