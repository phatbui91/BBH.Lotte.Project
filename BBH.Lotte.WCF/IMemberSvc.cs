using BBH.Lotte.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BBH.Lotte.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMemberSvc" in both code and config file together.
    [ServiceContract]
    public interface IMemberSvc
    {
        [OperationContract]
        string CheckAuthenticate(string email, string pass);

        [OperationContract]
        MemberBO GetMemberByToken(string token, string email);

        [OperationContract]
        MemberBO GetMemberByEmail(string email);
    }
}
