using Demo.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo
{

    public class SampleApi : RestApi, ISampleApi
    {
        public SampleApi(HttpMessageHandler handler = null)
            : base("https://appsheettest1.azurewebsites.net/sample/", handler)
        {
        }

        public Task<List> GetListAsync() => GetAsync<List>("list");
        public Task<List> GetListAsync(string token) => GetAsync<List>($"list?token={token}");
        public Task<Detail> GetDetailAsync(int id) => GetAsync<Detail>($"detail/{id}");
    }

    public interface ISampleApi
    {
        Task<List> GetListAsync();
        Task<List> GetListAsync(string token);
        Task<Detail> GetDetailAsync(int id);
    }

    public class List
    {
        public List(string token, params int[] result) =>
            (Result, Token) = (result, token);

        public IReadOnlyList<int> Result { get; }
        public string Token { get; }
    }

    public class Detail
    {
        public static readonly Detail Invalid = new Detail(-1, null, -1, null, null, null);

        public Detail(int id, string name, int age, string number, string photo, string bio) =>
            (Id, Name, Age, Number, Photo, Bio) = (id, name, age, number, photo, bio);

        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public Number Number { get; }
        public string Photo { get; }
        public string Bio { get; }
    }

    public class Number
    {
        public static implicit operator Number(string text) => new Number(text);

        public Number(string text) => Digits = (text ?? "")
            .Where(char.IsDigit)
            .Select(c => int.Parse($"{c}"))
            .ToArray();

        public IReadOnlyList<int> Digits { get; }
        public bool Valid => Digits.Count == 10;

        public override string ToString() => Valid 
            ? $"({Digits[0]}{Digits[1]}{Digits[2]}) {Digits[3]}{Digits[4]}{Digits[5]}-{Digits[6]}{Digits[7]}{Digits[8]}{Digits[9]}"
            : "N/A";
    }
}
