using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using BBH.Lotte.Domain;
using System.Configuration;
using System.Web;
using System.ServiceModel.Activation;

//using BBH.Lotte.Data;

namespace BBH.Lotte.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MemberSvc" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MemberSvc.svc or MemberSvc.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class MemberSvc : IMemberSvc
    {
        string keySHA = ConfigurationManager.AppSettings["Key_SHA"];

        public string CheckAuthenticate(string email, string pass)
        {
            string strToken = string.Empty;
            MemberBO member= BBH.Lotte.Data.MemberBusiness.LoginMemberAccount(email, pass);
            if(member!=null)
            {
                Utility utility = new Utility();
                string strCode = utility.RandomString();
                if(strCode!="")
                {
                    strToken = utility.EncryptText(strCode, keySHA);
                    HttpContext.Current.Session["SessionToken"] = strToken;
                    if(strToken!= "")
                    {
                        HttpContext.Current.Session["SessionToken"] = strToken;
                    }
                }
            }
            return strToken;
        }

        public  MemberBO GetMemberByToken(string token, string email)
        {
            MemberBO member = null;
            string tokenstr = (string)HttpContext.Current.Session["SessionToken"];
            if(tokenstr==null || tokenstr==string.Empty)
            {
                tokenstr = token;
                HttpContext.Current.Session["SessionToken"] = tokenstr;
            }
          
            if (token != null && token != "") 
            {
                if (token != tokenstr)
                {
                    return null;
                }
                else
                {
                    member = BBH.Lotte.Data.MemberBusiness.GetMemberByEmail(email);
                    if (member != null)
                    {
                        member.Email = email;
                    }
                    
                }
            }
           
            return member;
        }


        public MemberBO GetMemberByEmail(string email)
        {
            return BBH.Lotte.Data.MemberBusiness.GetMemberByEmail(email);
        }
    }
}
