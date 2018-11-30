
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.login
{
    /// <summary>
    /// 用户信息模型
    /// </summary>
    [Serializable]
    public class UserModel
    {
        public string username = "";//账号
        public string nickname = "";//昵称
        public int id = 0;//用户id
        public string head = "";//头像
        public int coin = 0;//金币
        public int cash = 0;//钻石
        public int sex = 0;//性别
        public string phone = "";//手机号
    }
}
