using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProtocol;
using GameProtocol.model.login;
using Server.cache;
using ServerTools;

namespace Server.business
{
    /// <summary>
    /// 登录业务逻辑处理
    /// </summary>
    public class LoginBiz
    {
        /*           
          0 表示登录成功
         -1 请求错误
         -2 账号密码不合法
         -3 表示没有此账号
         -4 表示密码错误
         -5 表示账号已经登录
        */
        public int Login(UserToken token,RequestLoginModel model)
        {
            //判定请求是否正确
            if(model ==null||string .IsNullOrEmpty (model .UserName )||string .IsNullOrEmpty (model .Password ))
            {
                DebugUtil.Instance.LogToTime("token=" + token.conn.RemoteEndPoint + "请求登录失败,请求错误",LogType.WARRING);
                return -1;
            }
            //判定账号密码是否合法
            if(model .UserName.Length <6||(model .Ditch ==0 && model .Password.Length < 6))
            {
                DebugUtil.Instance.LogToTime("token=" + token.conn.RemoteEndPoint + "请求登录失败,账号密码不合法",LogType.WARRING);
                return -2;
            }
            //判断是否含有此账号
            if (!CacheFactory.user.IsHasAccount (model.UserName)) {
                DebugUtil.Instance.LogToTime("token=" + model .UserName  + "请求登录失败,账号不存在", LogType.WARRING);
                return -3;
            }
            //是否密码正确
            if (model .Ditch ==0 && !CacheFactory.user.IsPassword(model.UserName, model.Password))
            {
                DebugUtil.Instance.LogToTime("token=" + model.UserName + "请求登录失败,账号密码不匹配", LogType.WARRING);
                return -4;
            }
            //账号是否正在登录
            if(CacheFactory .user .IsOnLine (token))
            {
                DebugUtil.Instance.LogToTime("token=" + model.UserName + "请求登录失败,账号已经在线", LogType.WARRING);
                return -5;
            }
            DebugUtil.Instance.LogToTime("username="+model .UserName +"请求登录验证成功");
            //全部条件满足，进行登录
            CacheFactory.user.OnLine(token, model.UserName);
            return 0;
        }

        /// <summary>
        /// 返回快速注册结果
        /// </summary>
        /// <returns></returns>
        public ResponseRegisterModel Reg(UserToken token)
        {
            DebugUtil.Instance.LogToTime("ip=" + token .conn .RemoteEndPoint + "请求快速注册");
            //建立新的账号
            ResponseRegisterModel m = new ResponseRegisterModel();
            m.password = CacheFactory.user.RegisterAccount(token);
            m.Status = 0;
            return m;
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        public void offLine(UserToken token)
        {
            CacheFactory.user.Save(token);
            CacheFactory.user.OffLine(token);
        }

    }
}
