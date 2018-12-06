using GameProtocol.model.match;
using NetFrame;
using NetFrame.auto;
using Server.logic.fight;
using ServerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.cache
{
    public class FightCache
    {
        /// <summary>
        /// 房间号和房间的映射
        /// </summary>
        Dictionary<int, FightRoom> RoomDic = new Dictionary<int, FightRoom>();

        /// <summary>
        /// 玩家和房间号的映射
        /// </summary>
        Dictionary<int, int> UserToRoom = new Dictionary<int, int>();

        /// <summary>
        /// 创建房间
        /// </summary>
        public void Create(MatchInfoModel model)
        {
            if(RoomDic .ContainsKey (model .RoomId))
            {
                DebugUtil.Instance.LogToTime(model.RoomId + "房间已经存在，不可重新创建");
                return;
            }
            FightRoom fight;
            //如果当前游戏是赢三张，则使用FightRoom的子类TPFightRoom
            if (model.GameType == GameProtocol.SConst.GameType.WINTHREEPOKER)
                fight = new TPFightRoom();
            else
                fight = new FightRoom();
            //初始化房间
            fight.Init(model);
            //将房间号和房间绑定
            RoomDic.Add(model.RoomId, fight);
            //将玩家和房间号绑定
            string str = "";
            for (int i=0;i<model .Team.Count; i++)
            {
                if (UserToRoom.ContainsKey(model.Team[i]))
                    UserToRoom[model.Team[i]] = model.RoomId;
                else
                    UserToRoom.Add(model.Team[i], model.RoomId);
                str += model.Team[i] + "_";
            }
            DebugUtil.Instance.LogToTime(model.RoomId + "房间_"+str+"创建成功");
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            return false;
        }

        /// <summary>
        /// 关闭房间
        /// </summary>
        public void Leave()
        {

        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            //是否在线
            if (!CacheFactory.user.IsOnLine(token)) return;
            //获取用户id
            int uid = CacheFactory.user.GetIdToToken(token);
            //判断是否包含在玩家和房间号的映射中
            if (!UserToRoom.ContainsKey(uid)) return;
            //是否存在该房间
            if (!RoomDic.ContainsKey(UserToRoom[uid])) return;
            //转发消息到房间中
            RoomDic[UserToRoom[uid]].MessageReceive(token, message);
        }

        public void ClientClose(UserToken token)
        {
            //是否在线
            if (!CacheFactory.user.IsOnLine(token)) return;
            //获取用户id
            int uid = CacheFactory.user.GetIdToToken(token);
            //判断是否包含在玩家和房间号的映射中
            if (!UserToRoom.ContainsKey(uid)) return;
            //是否存在该房间
            if (!RoomDic.ContainsKey(UserToRoom[uid])) return;
            //转发消息到房间中
            RoomDic[UserToRoom[uid]].ClientClose(token,"");
        }
    }
}
