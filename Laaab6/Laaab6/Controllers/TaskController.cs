using System;
using Laaab6.ClientApp;
using Laaab6.ClientApp.Model;
//using Laaab6.ClientApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace Laaab6.Controllers
{
    [Route("api/task")]
    public class TaskController : Controller
    {
        [HttpGet]
        public string OloloGet()
        {
            Console.WriteLine("!!");
            return "get";
        }

        [HttpPost]
        [Route("OloloPost")]
        public string OloloPost([FromBody]BackpackTask someName)
        {
            return "OK";
        }
    }
}
