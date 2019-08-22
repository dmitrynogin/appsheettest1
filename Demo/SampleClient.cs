using System;
using System.Collections.Generic;

namespace Demo
{
    public class SampleClient
    {
        public SampleClient() : this(new SampleApi()) { }
        public SampleClient(ISampleApi api) => 
            Api = api ?? throw new ArgumentNullException(nameof(api));

        ISampleApi Api { get; }

        public async IAsyncEnumerable<Detail> Details()
        {
            await foreach (var id in Ids())
            {
                Detail detail; 
                try { detail = await Api.GetDetailAsync(id); } catch { detail = Detail.Invalid; }
                yield return detail;
            }
        }

        public async IAsyncEnumerable<int> Ids()
        {
            for (var list = await Api.GetListAsync(); true; list = await Api.GetListAsync(list.Token))
            {
                foreach (var id in list.Result)
                    yield return id;

                if (list.Token == null)
                    break;
            }
        }
    }
}
