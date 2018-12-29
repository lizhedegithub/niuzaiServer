using GameProtocol;
using GameProtocol.model.match;
using NetFrame;
using Server.dao;
using ServerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.business
{
    /// <summary>
    /// 用来处理匹配逻辑的模块
    /// </summary>
    public class MatchBiz
    {
        /// <summary>
        /// 请求开始匹配
        /// </summary>
        /// <param name="token">用户连接对象</param>
        /// <param name="type">房间类型</param>
        /// <returns>0 请求开始匹配成功</returns>
        /// <returns>-1 当前金币余额不足</returns>
        /// <returns>-2 当前玩家已在匹配列表</returns>
        /// <returns>-3 当前玩家无效</returns>
        public ResponseStartMatchInfo StartMatch(UserToken token, SConst.GameType type)
        {
            //通过连接获取账号
            roleinfo user = cache.CacheFactory.user.Get(token);
            ResponseStartMatchInfo info = new ResponseStartMatchInfo();
            if (user == null) {
                info.Status = -3;
                DebugUtil.Instance.LogToTime(token .conn .RemoteEndPoint + "请求开始匹配失败,连接无效");
                return info;
            }
            int uid = user.id;
            int ucoin = user.coin;

            //获取进入房间需要的最少的金币
            int coin = cache.CacheFactory.match.GetRoomCoinAtType(type);
            if (ucoin < coin)
            {
                info.Status = -1;
                DebugUtil.Instance.LogToTime(uid + "请求开始匹配失败,当前金币余额不足，余额为："+ucoin);
                return info;
            }
            //获取是否在匹配对列中
            int matchid = cache.CacheFactory.match.IsOnMatchArray(uid);
            if (matchid > 0)
            {
                info.Status = -2;
                DebugUtil.Instance.LogToTime(uid + "请求开始匹配");
                return info;
            }
            //创建一个匹配对列，并返回匹配成功
            cache.CacheFactory.match.AddMatch(uid, type,ref info);
            info.Status = 0;
            DebugUtil.Instance.LogToTime(uid + "请求开始匹配成功，创建一个匹配对列");
            return info;
        }

        /// <summary>
        /// 请求离开匹配队列
        /// </summary>
        /// <param name="token">用户连接对象</param>
        /// <returns>0 请求成功</returns>
        /// <returns>-1 游戏已经开始</returns>
        /// <returns>-2 当前玩家不在对列中</returns>
        public int LeaveMatch(UserToken token)
        {
            return cache.CacheFactory.match.LeaveMatch(token);
        }
    }
}
