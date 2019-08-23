using Demo.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Tests
{
    [TestClass]
    public class BottomN_Should
    {
        [TestMethod]
        public async Task Keep_None()
        {
            var values = new[] { 10, 22, 3, 55, 66, 100, 200, 2 };
            var bottom = await values.ToAsyncEnumerable().Bottom(0, v => v).ToArrayAsync();
            CollectionAssert.AreEqual(new int[] { }, bottom);
        }

        [TestMethod]
        public async Task Keep_Three()
        {
            var values = new[] { 10, 22, 3, 55, 66, 100, 200, 2 };
            var bottom = await values.ToAsyncEnumerable().Bottom(3, v => v).ToArrayAsync();
            CollectionAssert.AreEqual(new int[] { 2, 3, 10 }, bottom);
        }

        [TestMethod]
        public async Task Keep_None_When_Empty()
        {
            var values = new int[] { };
            var bottom = await values.ToAsyncEnumerable().Bottom(3, v => v).ToArrayAsync();
            CollectionAssert.AreEqual(new int[] { }, bottom);
        }

        [TestMethod]
        public async Task Keep_No_More_Than_Exists()
        {
            var values = new[] { 100, 22 };
            var bottom = await values.ToAsyncEnumerable().Bottom(50, v => v).ToArrayAsync();
            CollectionAssert.AreEqual(new int[] { 22, 100 }, bottom);
        }
    }
}
