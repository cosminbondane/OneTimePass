using NUnit.Framework;
using OneTimePass.Business;
using OneTimePass.Controllers;
using OneTimePass.DataAccess;
using OneTimePass.Util.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Test.Controller
{
    [TestFixture]
    public class AccountControllerTest
    {
        [Test]
        public void UsernameIsNullTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository());

            var response = controller.GeneratePassword(null);

            Assert.IsNull(response);
        }

        [Test]
        public void UsernameIsEmptyTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository());

            var response = controller.GeneratePassword(string.Empty);

            Assert.IsNull(response);
        }

        [Test]
        public void UsernameIsNotExistTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository());

            var response = controller.GeneratePassword("notAnUser");

            Assert.IsNull(response);
        }

        [Test]
        public void GeneratePasswordForUserTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository());

            var response = controller.GeneratePassword("demo1");

            Assert.IsNotNull(response);

            Assert.AreEqual("demo1", response.Username);

            Assert.IsNotNull(response.Password);
            Assert.IsNotEmpty(response.Password);
        }

        [Test]
        public void VerifyPasswordUniquessTest()
        {
            AccountMockRepository mockRepository = new AccountMockRepository();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               mockRepository);

            var response = controller.GeneratePassword("demo1");

            Assert.AreNotEqual(null, response);

            Assert.AreEqual("demo1", response.Username);

            Assert.IsNotNull(response.Password);
            Assert.IsNotEmpty(response.Password);

            AccountBusiness business = new AccountBusiness(mockRepository);

            var account = mockRepository.Get(x => x.Password == response.Password);
            Assert.AreEqual(account.Username, response.Username);
        }
    }
}
