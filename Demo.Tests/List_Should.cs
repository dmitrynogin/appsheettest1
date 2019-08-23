using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Tests
{
    [TestClass]
    public class List_Should
    {
        [TestMethod]
        public void Initialize_With_Token()
        {
            var (token, ids) = 
                ("abc", new[] { 1, 2});

            var list = new List(token, ids);
            Assert.AreEqual(token, list.Token);
            CollectionAssert.AreEqual(ids, list.Result.ToArray());
        }

        [TestMethod]
        public void Initialize_Without_Token()
        {
            var (token, ids) =
                ((string)null, new[] { 1, 2 });

            var list = new List(token, ids);
            Assert.AreEqual(token, list.Token);
            CollectionAssert.AreEqual(ids, list.Result.ToArray());
        }
    }
}
