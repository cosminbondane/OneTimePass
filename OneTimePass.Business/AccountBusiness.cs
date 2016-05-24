using OneTimePass.Data;
using OneTimePass.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Business
{
    public class AccountBusiness
    {
        private IRepository<Account> accountRepo;

        public AccountBusiness(IRepository<Account> accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        public Account GetAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            return accountRepo.Get(x => x.Username == username);
        }

        public bool IsPasswordUnique(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            return accountRepo.Get(x => x.Password == password) == null;
        }

        public bool SetPassword(string username, string password, int expireInSeconds)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            Account account = accountRepo.Get(x => x.Username == username);
            if (account != null)
            {
                account.Password = password;
                account.PasswordExpiration = DateTime.UtcNow.AddSeconds(expireInSeconds);

                return accountRepo.Update(account);
            }

            return false;
        }
    }
}
