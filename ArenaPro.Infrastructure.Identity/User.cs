using Microsoft.AspNet.Identity;
using System;

namespace ArenaPro.Infrastructure.Identity
{
    public class User : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

       
        string IUser<string>.Id
        {
            get { return UserId.ToString(); }
        }
    }
}
