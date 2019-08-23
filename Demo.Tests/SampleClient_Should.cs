using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Tests
{
    [TestClass]
    public class SampleClient_Should
    {
        [TestMethod]
        public async Task Fetch_List()
        {
            var expected = new[] { 1, 2 };
            var list = new List(null, expected);
            var api = new Mock<ISampleApi>();
            api
                .Setup(api => api.GetListAsync())
                .Returns(Task.FromResult(list));

            var client = new SampleClient(api.Object);
            var ids = await client.Ids().ToArrayAsync();

            CollectionAssert.AreEqual(expected, ids);
        }

        [TestMethod]
        public async Task Fetch_List_Sequence()
        {
            var token = "abc";
            var expected1 = new[] { 1, 2, 3 };
            var list1 = new List(token, expected1);

            var expected2 = new[] { 4, 5 };
            var list2 = new List(null, expected2);

            var expected = expected1.Concat(expected2).ToArray();

            var api = new Mock<ISampleApi>();
            api
                .Setup(api => api.GetListAsync())
                .Returns(Task.FromResult(list1));
            api
                .Setup(api => api.GetListAsync(token))
                .Returns(Task.FromResult(list2));

            var client = new SampleClient(api.Object);
            var ids = await client.Ids().ToArrayAsync();

            CollectionAssert.AreEqual(expected, ids);
        }

        [TestMethod]
        public async Task Fetch_Details()
        {
            var tom = new Detail(1, "Tom", 3, "(123) 456-7890", "http://example.com/cat.jpg", "...");
            var jerry = new Detail(2, "Jerry", 1, "(123) 456-7890", "http://example.com/mouse.jpg", "...");
            var list = new List(null, tom.Id, jerry.Id);
            var expected = new[] { tom, jerry };

            var api = new Mock<ISampleApi>();
            api
                .Setup(api => api.GetListAsync())
                .Returns(Task.FromResult(list));
            api
                .Setup(api => api.GetDetailAsync(tom.Id))
                .Returns(Task.FromResult(tom));
            api
                .Setup(api => api.GetDetailAsync(jerry.Id))
                .Returns(Task.FromResult(jerry));

            var client = new SampleClient(api.Object);
            var details = await client.Details().ToArrayAsync();

            CollectionAssert.AreEqual(expected, expected);
        }
    }
}
