using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBH.Lotte.Domain;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using BBC.Core.Database;

namespace BBH.Lotte.Data
{
    public class MemberBusiness
    {
        public static string pathLog = ConfigurationManager.AppSettings["PathLog"];

        public static MemberBO LoginMemberAccount(string email, string pass)
        {
            string fileLog = Path.GetDirectoryName(Path.Combine(pathLog, "Logs"));
            Sqlhelper helper = new Sqlhelper("", "ConnectionString");
            try
            {
                MemberBO member = null;
                string sql = "SP_LoginMemberEmail";
                SqlParameter[] pa = new SqlParameter[2];
                pa[0] = new SqlParameter("@email", email);
                pa[1] = new SqlParameter("@pass", pass);

                SqlCommand command = helper.GetCommand(sql, pa, true);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    member = new MemberBO();
                    member.Email = reader["Email"].ToString();
                    member.Password = reader["Password"].ToString();
                }
                return member;
            }
            catch (Exception ex)
            {
                Utilitys.WriteLog(fileLog, "Exception login member : " + ex.Message);
                return null;
            }
            finally
            {
                helper.destroy();
            }
        }

        public static MemberBO GetMemberByEmail(string email)
        {
            string fileLog = Path.GetDirectoryName(Path.Combine(pathLog, "Logs"));
            Sqlhelper helper = new Sqlhelper("", "ConnectionString");
            try
            {
                MemberBO member = null;
                string sql = "select * from Member where Email=@email";
                SqlParameter[] pa = new SqlParameter[1];
                pa[0] = new SqlParameter("@email",email.ToString());
             
                SqlCommand command = helper.GetCommand(sql, pa, false);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    member = new MemberBO();

                    member.MemberID = int.Parse(reader["MemberID"].ToString());
                    member.E_Wallet = reader["E_Wallet"].ToString();
                    member.Email = reader["Email"].ToString();
                    member.CreateDate = DateTime.Parse(reader["CreateDate"].ToString());
                    member.Points = double.Parse(reader["Points"].ToString());
                    member.IsActive = int.Parse(reader["IsActive"].ToString());
                   

                }
                return member;
            }
            catch (Exception ex)
            {
                Utilitys.WriteLog(fileLog, ex.Message);
                return null;
            }
            finally
            {
                helper.destroy();
            }
        }
    }
}
