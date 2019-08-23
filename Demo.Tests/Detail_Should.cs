using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Tests
{
    [TestClass]
    public class Detail_Should
    {
        [TestMethod]
        public void Initialize()
        {
            var (id, name, age, number, photo, bio) = 
                (1, "Donald", 85, "(123) 456-7890", "http://example.com/duck.jpg", "...");

            var detail = new Detail(id, name, age, number, photo, bio);
            Assert.AreEqual(id, detail.Id);
            Assert.AreEqual(name, detail.Name);
            Assert.AreEqual(age, detail.Age);
            Assert.AreEqual(number, $"{detail.Number}");
            Assert.AreEqual(photo, detail.Photo);
            Assert.AreEqual(bio, detail.Bio);
        }
    }
}
