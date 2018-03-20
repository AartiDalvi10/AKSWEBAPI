using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace akswebapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Quotes")]
    public class QuotesController : Controller
    {
        private static readonly string[] ListOfQuotes;
        static QuotesController()
        {
            ListOfQuotes = JsonConvert.DeserializeObject<QuoteList>(System.IO.File.ReadAllText("quotes.json")).Quotes;
        }
        [HttpGet]
        public IActionResult Get() => Ok(ListOfQuotes[new Random().Next(ListOfQuotes.Length)]);
        [HttpGet("{count}")]
        public IActionResult GetMany(int count)
        {
            if (count < 0 || count > ListOfQuotes.Length) return BadRequest($"number of quotes must be between 0 and {ListOfQuotes.Length}");
            var r = new Random();
            var z = 0.0001;
            for (int i = 0; i <= 30; i++)
            {
                Thread.Sleep(5000);
                z = z + Math.Sqrt(z);
            }
            string envmachine = Environment.MachineName;
            //return Ok(ListOfQuotes.OrderBy(_ => r.Next(ListOfQuotes.Length)).Take(count).Select(x => envmachine+"-{x}"));
            return Ok(ListOfQuotes.OrderBy(_ => r.Next(ListOfQuotes.Length)).Take(count).Select(x => $"{x}-{Environment.MachineName}"));
        }
        private class QuoteList
        {
            public string[] Quotes
            {
                get;
                set;
            }
        }
    }
}