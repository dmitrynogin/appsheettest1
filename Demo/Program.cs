using Demo.Utils;
using Nito.AsyncEx;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace Demo
{
    static class Program
    {
        static void Main() => AsyncContext.Run(MainAsync);
        static async Task MainAsync()
        {
            var client = new SampleClient();
            await foreach (var detail in client.Details()
                .Where(d => d.Number.Valid)
                .Bottom(5, d => d.Age)
                .OrderBy(d => d.Name))
            {
                WriteLine($"{detail.Number} {detail.Name}: {detail.Age} years old");
            }
        }
    }
}
