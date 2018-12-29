
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerTools.WeChat
{
    public static class WeChatUtil
    {
        /// <summary>
        /// 应用在微信上注册的id和秘钥
        /// </summary>
        static string APP_ID = "wx38clea42b1137e2b";
        static string APP_SECRET = "d26ad9d4d07fa4930469776bd9ed460d";//忘记只能重置

        /// <summary>
        /// 设置程序信息
        /// </summary>
        public static void SetAppInfo(string appid,string appsecret)
        {
            APP_ID = appid;
            APP_SECRET = appsecret;
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns></returns>
        public static int GetWeChatLogin(string code) {
            //微信获取access_token接口
            //https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
            //定义微信请求的地址，？之前的数据表示请求地址，之后的数据表示参数，&表示多参数连接
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" +
                "appid=" + APP_ID + "&" +
                "secret=" + APP_SECRET + "&" +
                "code=" + code + "&" +
                "grant_type=authorization_code";
            //WebRequest/WebResponse进行网络HTTP/WEB交互
            //创建一个基于url地址的网络请求
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //请求类型默认为get
            req.Method = "GET";
            //获取返回(获取到的是流数据)
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            return 0;
            return -6;
        }

        /// <summary>
        /// 将流数据转化为字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string GetInput(Stream s)
        {
            try
            {
                //声明为UTF8格式的流字节
                StreamReader sr = new StreamReader(s, Encoding.UTF8);
                //将流数据的开头到结尾转为字符串
                string ret = sr.ReadToEnd();
                return ret;
            }
            catch(Exception e)
            {
                DebugUtil.Instance.LogToTime(e.ToString(),LogType.ERROR);
                return "";
            }
        }
    }
}
