using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AlarmMonitor.Common
{
    public class Untils
    {
        public static string ConvertSecond(string secondsStr)
        {
            if(string.IsNullOrEmpty(secondsStr))
            {
                return "";
            }
            else
            {
                int seconds = Convert.ToInt32(secondsStr);
                TimeSpan ts = new TimeSpan(0, 0, seconds);
                if(ts.Hours>0)
                {
                    return ts.Hours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒";
                }
                else
                {
                    if(ts.Minutes>0)
                    {
                        return ts.Minutes + "分钟" + ts.Seconds + "秒";
                    }
                    else
                    {
                        return ts.Seconds + "秒";
                    }
                }
            }
        }
        /// <summary>
        /// 用MD5加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }
    }
}