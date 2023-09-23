using OtakuOrganize.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Entities
{
    public class User
    {
        public User(Guid id, string username, string password, string email ,RoleType role)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            LastLogin = DateTime.Now;
        }

        public Guid Id { get;  private set; }
        public string Username { get;  private set; }
        public string Password { get;  private set; }
        public string Email { get;  private set; }
        public RoleType Role { get; private set; }
        public Customer? Customer { get;  private set; }
        public DateTime LastLogin { get;  private set; }
    }
}
