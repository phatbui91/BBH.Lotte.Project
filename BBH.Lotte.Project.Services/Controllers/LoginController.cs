using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBH.Lotte.WCF;
using BBH.Lotte.Data;
using BBH.Lotte.Domain;
using BBH.Lotte.Project.Services.MemberServicesSvc;

namespace BBH.Lotte.Project.Services.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        MemberServicesSvc.MemberSvcClient memberSv = new MemberServicesSvc.MemberSvcClient();

        [HttpPost]
        public string MemberLogin(string email, string pass)
        {
            string result = "";

            MemberBO member = new MemberBO();

            member = memberSv.GetMemberByEmail(email);
            if (member != null)
            {
                string passMd5 = Utility.MaHoaMD5(pass);
                string accessToken = memberSv.CheckAuthenticate(email, passMd5);
                if (accessToken == null || accessToken == string.Empty)
                {
                    result = "loginfaile";
                }
                else
                {
                    Session["Email"] = email;
                    result = "loginSuccess";
                    Session["Token"] = accessToken;
                }
            }
            else
            {
                result = "notExit";
            }
            return result;
        }

        [HttpPost]
        public string SubmitToken(string email)
        {
            string result = "";

            string token = (string)Session["Token"];
            string emailLogin= (string)Session["Email"];

            MemberBO member = null ;
            if (email != emailLogin)
            {
                result = "loginfaile";
            }
            else
            {
               
                    member = memberSv.GetMemberByToken(token, email);
                    if (member != null)
                    {
                        if (token == null)
                        {
                            result = "loginfaile";
                        }
                        else
                        {
                            result = "loginSuccess";
                        }
                    }
                
            }

            return result;
        }

        [HttpPost]
        public void LogoutMember()
        {
            Session["Email"] = null;
            Session["SessionToken_"] = null;
        }
	}
}