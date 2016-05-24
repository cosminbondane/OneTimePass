using NUnit.Framework;
using OneTimePass.Audit;
using OneTimePass.Business;
using OneTimePass.Controllers;
using OneTimePass.DataAccess;
using OneTimePass.Util.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                new AccountMockRepository(),
                new AuditLogger());

            var response = controller.GeneratePassword(null);

            Assert.IsNull(response);
        }

        [Test]
        public void UsernameIsEmptyTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository(),
                new AuditLogger());

            var response = controller.GeneratePassword(string.Empty);

            Assert.IsNull(response);
        }

        [Test]
        public void UsernameIsNotExistTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository(),
                new AuditLogger());

            var response = controller.GeneratePassword("notAnUser");

            Assert.IsNull(response);
        }

        [Test]
        public void GeneratePasswordForUserTest()
        {
            AccountController controller = new AccountController(
                new PasswordGenerator(),
                new AccountMockRepository(),
                new AuditLogger());

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
               mockRepository,
               new AuditLogger());

            var response = controller.GeneratePassword("demo1");

            Assert.AreNotEqual(null, response);

            Assert.AreEqual("demo1", response.Username);

            Assert.IsNotNull(response.Password);
            Assert.IsNotEmpty(response.Password);

            AccountBusiness business = new AccountBusiness(mockRepository);

            var account = mockRepository.Get(x => x.Password == response.Password);
            Assert.AreEqual(account.Username, response.Username);
        }

        [Test]
        public void UnsuccessfullyLoginWithNoUserTest()
        {
            var auditLogger = new AuditLogger();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               new AccountMockRepository(),
               auditLogger);

            var result = controller.Login(null, null);
            Assert.IsFalse(result);

            var log = auditLogger.GetLastLine();
            Assert.IsTrue(log.Contains("cannot be found"));
        }

        [Test]
        public void UnsuccessfullyLoginWithNotExistingUserTest()
        {
            var auditLogger = new AuditLogger();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               new AccountMockRepository(),
               auditLogger);

            var result = controller.Login("notAnUser", null);
            Assert.IsFalse(result);

            var log = auditLogger.GetLastLine();
            Assert.IsTrue(log.Contains("cannot be found"));
        }

        [Test]
        public void UnsuccessfullyLoginWithWrongPasswordTest()
        {
            var auditLogger = new AuditLogger();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               new AccountMockRepository(),
               auditLogger);

            var response = controller.GeneratePassword("demo1");

            var result = controller.Login("demo1", response.Password + "x");
            Assert.IsFalse(result);

            var log = auditLogger.GetLastLine();
            Assert.IsTrue(log.Contains("wrong password"));
        }

        [Test]
        public void UnsuccessfullyLoginExpiredPasswordTest()
        {
            var auditLogger = new AuditLogger();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               new AccountMockRepository(),
               auditLogger);

            var response = controller.GeneratePassword("demo1");

            Thread.Sleep(1000 * 31);

            var result = controller.Login("demo1", response.Password);
            Assert.IsFalse(result);

            var log = auditLogger.GetLastLine();
            Assert.IsTrue(log.Contains("expired password"));
        }

        [Test]
        public void SuccessfullyLoginTest()
        {
            var auditLogger = new AuditLogger();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               new AccountMockRepository(),
               auditLogger);

            var result = controller.Login("demo1", "abcdefg");
            Assert.IsTrue(result);
        }

        [Test]
        public void SuccessfullyLogiChangePasswordTest()
        {
            var auditLogger = new AuditLogger();

            AccountController controller = new AccountController(
               new PasswordGenerator(),
               new AccountMockRepository(),
               auditLogger);

            var response = controller.GeneratePassword("demo1");

            Thread.Sleep(1000 * 5);

            var result = controller.Login("demo1", response.Password);
            Assert.IsTrue(result);
        }

    }
}
