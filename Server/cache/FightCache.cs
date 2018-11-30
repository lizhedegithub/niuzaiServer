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
            FightRoom fight = new logic.fight.FightRoom();
            fight.Init(model);
            RoomDic.Add(model.RoomId, fight);
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

        }

        public void ClientClose(UserToken token)
        {

        }
    }
}
