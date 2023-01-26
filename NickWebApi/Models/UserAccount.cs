using System;
using System.Collections.Generic;

#nullable disable

namespace NickWebApi.Models
{
    public partial class UserAccount
    {
        public string UserAccountCode { get; set; }
        public string UserName { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }
}
