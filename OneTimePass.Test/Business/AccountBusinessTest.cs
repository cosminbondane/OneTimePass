using NUnit.Framework;
using OneTimePass.Business;
using OneTimePass.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Test.Business
{
    [TestFixture]
    public class AccountBusinessTest
    {
        [Test]
        public void GetAccountNoUserTest()
        {
            AccountBusiness business = new AccountBusiness(new AccountMockRepository());

            var result = business.GetAccount(null);
            Assert.IsNull(result);

            result = business.GetAccount(string.Empty);
            Assert.IsNull(result);

            result = business.GetAccount("notAnUser");
            Assert.IsNull(result);
        }

        [Test]
        public void GetAccountUserTest()
        {
            AccountBusiness business = new AccountBusiness(new AccountMockRepository());

            var result = business.GetAccount("demo1");
            Assert.IsNotNull(result);
            Assert.AreEqual("demo1", result.Username);
        }

        [Test]
        public void UniqueNullPasswordTest()
        {
            AccountBusiness business = new AccountBusiness(new AccountMockRepository());

            var result = business.IsPasswordUnique(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void UniqueEmptyPasswordTest()
        {
            AccountBusiness business = new AccountBusiness(new AccountMockRepository());

            var result = business.IsPasswordUnique(string.Empty);
            Assert.IsFalse(result);
        }

        [Test]
        public void UniquePasswordTest()
        {
            AccountBusiness business = new AccountBusiness(new AccountMockRepository());

            var result = business.IsPasswordUnique("this is a random string");
            Assert.IsTrue(result);
        }

        [Test]
        public void NotUniquePasswordTest()
        {
            AccountBusiness business = new AccountBusiness(new AccountMockRepository());

            var result = business.IsPasswordUnique("abcdefg");
            Assert.IsFalse(result);
        }
    }
}
