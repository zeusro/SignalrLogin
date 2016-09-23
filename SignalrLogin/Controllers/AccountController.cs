using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SignalrLogin.Models;

namespace SignalrLogin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            var user = new User()
            {
                Name = "小明",
                Uuid = ""
            };
            return Content(JsonConvert.SerializeObject(user));
        }

        public ActionResult Test()
        {
            Response.Write("正在登陆");
            Thread.Sleep(3000);
            return Content("gg");
        }

        public ActionResult Scan(string connectionId)
        {
            Response.Write("正在登陆");
            Thread.Sleep(3000);
            
            return Redirect("http://www.asp.net/signalr/overview/guide-to-the-api/hubs-api-guide-javascript-client");
        }

    }
}