using System.Collections.Generic;
using System.Threading.Tasks;
using NickApp.Models;

namespace NickApp.Services
{
    public interface IUserAccountService
    {
   
        Task<IEnumerable<UserAccount>> GetUserAccountByUserName(string UserName);
        Task AddUserAccount(UserAccount UserAccount);
        Task SaveUserAccount(UserAccount UserAccount);
        Task DeleteUserAccount(UserAccount UserAccount);      
    }
}
