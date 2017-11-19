using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing.Drawing2D;
using System.Drawing.Configuration;
using System.Drawing;
using System.Drawing.Imaging;

namespace BBH.Lotte.WCF
{
    public class GenCode
    {
         public string RandomString()
        {
            Random objRandom = new Random();
            string combination = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder token = new StringBuilder();

            for (int i = 0; i < 12; i++)
            {
                token.Append(combination[objRandom.Next(combination.Length)]);
            }
            return token.ToString();

        }

         //protected void main()
         //{
         //    Response.Clear();
         //    Bitmap objBmp = new Bitmap(220, 28);
         //    RectangleF rectf = new RectangleF(15, 5, 220, 28);
         //    Graphics objGraphics = Graphics.FromImage(objBmp);
         //    objGraphics.Clear(Color.White);
         //    objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
         //    objGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
         //    objGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
         //    string strValue = RandomString();

         //    Session["CaptchaCode"] = strValue.ToLower();
         //    objGraphics.DrawString(strValue, new Font("Times News Roman", 16, FontStyle.Regular), Brushes.Black, rectf);
         //    objGraphics.DrawRectangle(new Pen(Color.White), 0, 0, 220, 28);
         //    objGraphics.Flush();
         //    //Response.ContentType = "image/jpeg";
         //    objBmp.Save(Response.OutputStream, ImageFormat.Jpeg);
         //    objGraphics.Dispose();
         //    objBmp.Dispose();
         //    Response.End();
         //}
    }
}