using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ST.Framework.Utils
{
    public class UText
    {
        public static readonly string sKey = "w0x3hd89";

        #region 32 位 MD5 加密 public static string MD5(string s)
        /// <summary>
        /// 32 位 MD5 加密 
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>string</returns>
        public static string MD5(string s)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();

        }
        #endregion

        #region 编码成 sql 文本可以接受的格式 public static string SqlEncode(string s)
        /// <summary>
        /// 编码成 sql 文本可以接受的格式
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>string</returns>
        public static string SqlEncode(string s)
        {
            return s.Trim().Replace("'", "''").Replace(" ", "");
        }
        #endregion

        #region 加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="pToEncrypt">需要加密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中


                //原来使用的UTF8编码，我改成Unicode编码了，不行
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量


                //使得输入密码必须输入英文文本
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 解密方法
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">密匙</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string pToDecrypt)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 判断字符串是否合法的日期格式 protected virtual bool IsDate(string value)
        /// <summary>
        /// 判断字符串是否合法的日期格式
        /// </summary>
        /// <param name="value">value值</param>
        /// <returns>bool</returns>
        protected static bool IsDate(string value)
        {
            try
            {
                System.DateTime.Parse(value);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 判断字符串是否由数字组成
        /// <summary>
        /// 判断字符串是否由数字组成
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>bool</returns>
        public static bool IsNumeric(string s)
        {
            string pattern = @"^\-?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }
        #endregion

        #region 判断字符串是否由字母组成
        /// <summary>
        /// 判断字符串是否由字母组成
        /// </summary>
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>bool</returns>
        public static bool IsLetter(string s)
        {
            string pattern = @"^([a-z]|[0-9])+$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 判断字符串是否由汉字组成 public static bool IsChinese(string s)
        /// <summary>
        /// 判断字符串是否由汉字组成
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>bool</returns>
        public static bool IsChinese(string s)
        {
            string pattern = @"^[\u4e00-\u9fa5]{2,}$";
            return Regex.IsMatch(s, pattern);
        }
        #endregion

        #region 判断是否为正确的 email 地址格式 public static bool IsEmail(string s)
        /// <summary>
        /// 判断是否为正确的 email 地址格式
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>bool</returns>
        public static bool IsEmail(string s)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(s, pattern);
        }
        #endregion
    }
}
