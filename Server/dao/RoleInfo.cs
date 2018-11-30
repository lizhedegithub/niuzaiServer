
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.dao
{
    public class RoleInfo
    {
        public int Id=-1;//用户id
        public string username;//账号
        public string password;//密码
        public string nickname;//昵称

        public string head="default";//头像
        public int coin=10000;//金币
        public int cash=10000;//钻石
        public int sex=0;//性别 0男 1女 2未知
        public string phone="";//手机号
        public int rank=0;//排行分数
    }
}
