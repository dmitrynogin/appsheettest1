using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Tests
{
    [TestClass]
    public class Number_Should
    {
        [TestMethod]
        public void Validate()
        {
            Number valid1 = "(123) 456-7890";
            Number valid2 = "(123)456-7890";
            Number valid3 = "123 456 7890";
            Number valid4 = "1234567890";

            Assert.IsTrue(valid1.Valid);
            Assert.IsTrue(valid2.Valid);
            Assert.IsTrue(valid3.Valid);
            Assert.IsTrue(valid4.Valid);

            Number invalid1 = (string)null;
            Number invalid2 = "";
            Number invalid3 = "1234";
            Number invalid4 = "1234-5678";

            Assert.IsFalse(invalid1.Valid);
            Assert.IsFalse(invalid2.Valid);
            Assert.IsFalse(invalid3.Valid);
            Assert.IsFalse(invalid4.Valid);
        }

        [TestMethod]
        public void Format()
        {
            Number valid1 = "(123) 456-7890";
            Number valid2 = "(123)456-7890";
            Number valid3 = "123 456 7890";
            Number valid4 = "1234567890";

            Assert.AreEqual("(123) 456-7890", valid1.ToString());
            Assert.AreEqual("(123) 456-7890", valid2.ToString());
            Assert.AreEqual("(123) 456-7890", valid3.ToString());
            Assert.AreEqual("(123) 456-7890", valid4.ToString());

            Number invalid1 = (string)null;
            Number invalid2 = "";
            Number invalid3 = "1234";
            Number invalid4 = "1234-5678";

            Assert.AreEqual("N/A", invalid1.ToString());
            Assert.AreEqual("N/A", invalid2.ToString());
            Assert.AreEqual("N/A", invalid3.ToString());
            Assert.AreEqual("N/A", invalid4.ToString());
        }
    }
}
