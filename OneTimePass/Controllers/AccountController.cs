using OneTimePass.Business;
using OneTimePass.Data;
using OneTimePass.DataAccess;
using OneTimePass.Models;
using OneTimePass.Util;
using OneTimePass.Util.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OneTimePass.Controllers
{
    public class AccountController : ApiController
    {
        private IPasswordGenerator passwordGenerator;
        private AccountBusiness accountBusiness;

        public AccountController(
            IPasswordGenerator passwordGenerator,
            IRepository<Account> accountRepo)
        {
            this.passwordGenerator = passwordGenerator;
            this.accountBusiness = new AccountBusiness(accountRepo);
        }

        [HttpGet]
        public NewPasswordModel GeneratePassword(string username)
        {
            try
            {
                string password = null;
                int numberOfTries = 10; // avoid infinte loops

                if (accountBusiness.GetAccount(username) == null)
                {
                    // no account for this user
                    return null;
                }

                while (numberOfTries > 0)
                {
                    // generate a random password and verify uniqueness
                    password = passwordGenerator.Generate();
                    if (accountBusiness.IsPasswordUnique(password))
                    {
                        break;
                    }
                }

                if (password == null)
                {
                    // log exception
                    return null;
                }

                if (!accountBusiness.SetPassword(username, password, Config.PasswordExpiration))
                {
                    return null;
                }

                return new NewPasswordModel { Username = username, Password = password };
            }
            catch (Exception ex)
            {
                // log exception
                return null;
            }
        }
    }
}
