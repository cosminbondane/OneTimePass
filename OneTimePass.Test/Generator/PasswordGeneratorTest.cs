using NUnit.Framework;
using OneTimePass.Util.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.Test.Generator
{
    [TestFixture]
    public class PasswordGeneratorTest
    {
        [Test]
        public void GeneratePasswordTest()
        {
            PasswordGenerator generator = new PasswordGenerator();
            string password = generator.Generate();

            Assert.IsNotNull(password);
            Assert.IsNotEmpty(password);

            password = generator.Generate();

            Assert.IsNotNull(password);
            Assert.IsNotEmpty(password);

            password = generator.Generate();

            Assert.IsNotNull(password);
            Assert.IsNotEmpty(password);
        }

        [Test]
        public void GenerateDifferentPasswordsSameInstanceTest()
        {
            PasswordGenerator generator = new PasswordGenerator();

            string pass1 = generator.Generate();

            Assert.IsNotNull(pass1);
            Assert.IsNotEmpty(pass1);

            string pass2 = generator.Generate();

            Assert.IsNotNull(pass2);
            Assert.IsNotEmpty(pass2);

            Assert.AreNotEqual(pass1, pass2);
        }

        [Test]
        public void GenerateDifferentPasswordsMultipleInstancesTest()
        {
            PasswordGenerator generator1 = new PasswordGenerator();
            PasswordGenerator generator2 = new PasswordGenerator();

            string pass1 = generator1.Generate();

            Assert.IsNotNull(pass1);
            Assert.AreNotEqual(string.Empty, pass1);

            string pass2 = generator2.Generate();

            Assert.IsNotNull(pass2);
            Assert.IsNotEmpty(pass2);

            Assert.AreNotEqual(pass1, pass2);
        }
    }
}
