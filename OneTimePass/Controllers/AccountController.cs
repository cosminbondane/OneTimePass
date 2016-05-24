using OneTimePass.Audit;
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
        private IAuditLogger auditLogger;

        public AccountController(
            IPasswordGenerator passwordGenerator,
            IRepository<Account> accountRepo,
            IAuditLogger auditLogger)
        {
            this.passwordGenerator = passwordGenerator;
            this.accountBusiness = new AccountBusiness(accountRepo);
            this.auditLogger = auditLogger;
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

        [HttpGet]
        public bool Login(string username, string password)
        {
            try
            {
                var account = accountBusiness.GetAccount(username);
                if (account == null )
                {
                    auditLogger.Log(
                        string.Format("Unsucessfully login. User {0} cannot be found ({1})", (username ?? "not-defined"), DateTime.UtcNow));
                    return false;
                }

                if (account.Password != password)
                {
                    auditLogger.Log(
                        string.Format("Unsucessfully login. User {0} wrong password ({1})", username, DateTime.UtcNow));

                    return false;
                }

                if (account.PasswordExpiration < DateTime.UtcNow)
                {
                    auditLogger.Log(
                        string.Format("Unsucessfully login. User {0} expired password ({1})", username, DateTime.UtcNow));

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
