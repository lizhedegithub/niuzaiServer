using NetFrame;
using Server.dao;
using ServerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.cache
{
    /// <summary>
    /// 用户数据缓存
    /// </summary>
    public class UserCache
    {
        #region 根据玩家的id获取到用户连接，根据用户连接获取到用户账号，根据用户账号获取到用户数据
        /// <summary>
        /// 用户账号与用户数据的映射
        /// </summary>
        Dictionary<string, dao.RoleInfo> AccountMap = new Dictionary<string, dao.RoleInfo>();

        /// <summary>
        /// 在线连接和用户账号的映射
        /// </summary>
        Dictionary<UserToken, string> OnLineAccount = new Dictionary<UserToken, string >();

        /// <summary>
        /// 玩家id与用户连接的映射
        /// </summary>
        Dictionary<int, UserToken> IdToToken = new Dictionary<int, UserToken>();
        #endregion

        int index = 0;

        /// <summary>
        /// 注册账号
        /// </summary>
        public string RegisterAccount(UserToken token)
        {
            //创建一个新的角色账号
            dao.RoleInfo role = new dao.RoleInfo();
            index++;
            role.Id = index;
            //账号：lin10001
            role.username = "lin" + (index + 10000);
            //密码：password
            role.password = "password";
            //昵称：游客10001
            role.nickname = "游客" + (index + 10000);
            //头像：default
            //金币：10000
            //钻石：10000
            //性别：0 男
            //手机号：“”          
            AccountMap.Add(role.username, role);
            OnLine(token, role.username);
            DebugUtil.Instance.LogToTime("快速注册成功，账号=" + role.username + "昵称="+role .nickname);
            return role .password;
        }

        /// <summary>
        /// 读取账号
        /// </summary>
        public void LoadAccount()
        {

        }

        /// <summary>
        /// 是否含有此账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsHasAccount(string username)
        {
            //如果账号列表包含此账号
            if (AccountMap.ContainsKey(username))
                return true;
            return false;
        }

        /// <summary>
        /// 账号密码是否匹配
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsPassword(string username,string password)
        {
            if (!IsHasAccount(username)) return false;
            if (AccountMap[username].password.Equals(password))
                return true;
            return false;
        }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsOnLine(UserToken token)
        {
            if (OnLineAccount.ContainsKey(token))
                return true;
            return false;
        }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsOnLine(int id)
        {
            if (IdToToken.ContainsKey(id) && OnLineAccount .ContainsKey (IdToToken [id]))
                return true;
            return false;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username"></param>
        public void OnLine(UserToken token,string username)
        {
            if(IsOnLine(token))
            {
                DebugUtil.Instance.LogToTime(username +"已经在线", LogType.WARRING);
                return;
            }
            if(!IsHasAccount (username))
            {
                DebugUtil.Instance.LogToTime(username + "不存在", LogType.WARRING);
                return;
            }
            if (IdToToken.ContainsKey(AccountMap[username].Id))
            {
                DebugUtil.Instance.LogToTime(username + "移除账号连接", LogType.WARRING);
                IdToToken.Remove(AccountMap[username].Id);
            }
            DebugUtil.Instance.LogToTime(username + "上线成功", LogType.WARRING);
            IdToToken.Add(AccountMap[username].Id,token);
            OnLineAccount.Add(token, username);
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        public void OffLine(UserToken token)
        {
            //如果在线并且含有此账号
            if (IsOnLine(token) && IsHasAccount(OnLineAccount[token]))
            {
                int id = AccountMap[OnLineAccount[token]].Id;
                if (IdToToken.ContainsKey(id))
                    IdToToken.Remove(id);
                OnLineAccount.Remove(token);
                DebugUtil.Instance.LogToTime(id + "玩家下线了");
            }
        }

        /// <summary>
        /// 保存账户信息
        /// </summary>
        public void Save(UserToken token)
        {

        }

        public RoleInfo Get(UserToken token)
        {
            //判断是否在线
            if (!IsOnLine(token)) return null;
            //判断是否含有账号
            if (!IsHasAccount(OnLineAccount[token]))
            {
                return null;
            }
            return AccountMap[OnLineAccount[token]];
        }

        public RoleInfo Get(int id)
        {
            //判断是否在线
            if (!IsOnLine(id)) return null;
            //判断是否含有账号
            if (!IsHasAccount(OnLineAccount[IdToToken [id]]))
            {
                return null;
            }
            return AccountMap[OnLineAccount[IdToToken [id]]];
        }


        /// <summary>
        /// 通过连接获取用户id
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetIdToToken(UserToken token)
        {
            //如果当前没有在线，或没有此账号
            if (!IsOnLine(token) || !IsHasAccount (OnLineAccount [token])) return -1;
            return AccountMap[OnLineAccount[token]].Id;
        }

        /// <summary>
        /// 通过用户id获取用户连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserToken GetToken(int id)
        {
            //如果当前没有该id的映射
            if (!IdToToken.ContainsKey(id)) return null;
            return IdToToken[id];
        }
    }
}
