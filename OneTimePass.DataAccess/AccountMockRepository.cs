using OneTimePass.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.DataAccess
{
    public class AccountMockRepository : IRepository<Account>
    {
        private List<Account> data;

        public AccountMockRepository()
        {
            data = new List<Account> {
                new Account { AccountId = 1, Username = "demo1", Password = "abcdefg", PasswordExpiration = DateTime.UtcNow.AddMinutes(10) },
                new Account { AccountId = 1, Username = "demo2", Password = "abcdefh", PasswordExpiration = DateTime.UtcNow.AddMinutes(10) },
                new Account { AccountId = 1, Username = "demo3", Password = "abcdefi", PasswordExpiration = DateTime.UtcNow.AddMinutes(10) }
            };
        }

        public void Delete(string id)
        {
        }

        public Account Get(Func<Account, bool> where)
        {
            return data.FirstOrDefault(where);
        }

        public Account Insert(Account item)
        {
            return item;
        }

        public bool Update(Account item)
        {
            return true;
        }
    }
}
