using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame.auto;
using GameProtocol.model.fight;
using GameProtocol.model.match;
using GameProtocol;
using Server.dao;
using ServerTools;
using Server.cache;

namespace Server.logic.fight
{
    class FightRoom : IHandler
    {
        /// <summary>
        /// 房间号
        /// </summary>
        protected int RoomId = -1;

        /// <summary>
        /// 队伍成员id
        /// </summary>
        public List<int> TemeId = new List<int>();

        /// <summary>
        /// 玩家id和玩家信息的映射
        /// </summary>
        protected Dictionary<int, FightUserModel> UserFight = new Dictionary<int, FightUserModel>();

        /// <summary>
        /// 房间类型
        /// </summary>
        protected SConst.GameType GameType = SConst.GameType.WINTHREEPOKER;

        /// <summary>
        /// 确认准备好游戏开始
        /// </summary>
        private List<int> readrole = new List<int>();

        /// <summary>
        /// 游戏是否开始(当前局是否开始)
        /// </summary>
        protected bool IsGameStart = false;

        /// <summary>
        /// 房间是否开始，进行游戏
        /// </summary>
        protected bool IsRoomStart = false;

        /// <summary>
        /// 玩家最大人数
        /// </summary>
        protected int MaxPlayer = 0;

        public void ClientClose(UserToken token, string error)
        {
            
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            if (!CacheFactory.user.IsOnLine(token)) return;
            switch (message .command)
            {
                //玩家请求准备
                case FightProtocol.ENTERFIGHT_CREQ:
                    {
                        token.write(TypeProtocol.FIGHT, FightProtocol.ENTERFIGHT_SRES, Enter(token));
                    }
                    break;
                //离开房间
                case FightProtocol.LEAVEFIGHT_CREQ:
                    {
                        Leave(token);
                    }
                    break;
            }
        }

        /// <summary>
        /// 初始化房间
        /// </summary>
        /// <param name="model"></param>
        public void Init(MatchInfoModel model)
        {
            GameType = model.GameType;//初始化房间类型
            //初始化队伍成员
            TemeId.Clear();
            TemeId.AddRange(model.Team);
            RoomId = model.RoomId;//初始化房间id
            MaxPlayer = model.MaxPlayer;//初始化人数
            //初始化玩家信息
            for (int i=0;i<model .Team.Count; i++)
            {
                //初始化玩家信息
                FightUserModel m = new FightUserModel();
                //获取用户信息
                RoleInfo ri = cache.CacheFactory.user.Get(model.Team[i]);
                //如果获取到，则将玩家信息赋值给用户信息
                if(ri!=null)
                {
                    m.coin = ri.coin;
                    m.nickname = ri.nickname;
                    m.id = ri.Id;
                }
                //否则，设置为默认信息
                else
                {
                    m.coin = 0;
                    m.nickname = "nickname";
                    m.id = model .Team [i];
                }
                UserFight.Add(m.id, m);
            }
            //广播玩家信息
            for (int i = 0; i < TemeId .Count; i++)
            {
                Broadcast(FightProtocol.PLAYERINFO_BRQ, UserFight[TemeId[i]]);
            }

            DebugUtil.Instance.LogToTime(RoomId + "房间初始化成功");
        }

        /// <summary>
        /// 向本房间所有玩家发送消息
        /// </summary>
        /// <param name="Commond"></param>
        /// <param name="obj"></param>
        protected void Broadcast(int Commond,object obj)
        {
            for (int i = 0; i < TemeId .Count; i++)
            {
                UserToken token= cache.CacheFactory.user.GetToken(TemeId[i]);
                if(token !=null)
                {
                    token.write(TypeProtocol .FIGHT,Commond, obj);
                }
            }
        }

        /// <summary>
        /// 玩家确认准备开始游戏
        /// </summary>
        /// <param name="token"></param>
        /// <returns>-1 准备失败，已经准备了</returns>
        /// <returns>-2 准备失败，不在此房间</returns>
        int Enter(UserToken token)
        {
            int uid = CacheFactory.user.GetIdToToken(token);
            if(readrole .Contains (uid))
            {
                DebugUtil.Instance.LogToTime(uid + "玩家已经准备，无需再次准备");
                return -1;
            }
            if(!TemeId .Contains (uid))
            {
                DebugUtil.Instance.LogToTime(uid + "玩家不在此房间，无需准备");
                return -2;
            }
            //将玩家添加到准备列表
            readrole.Add(uid);
            //吧把准备列表广播给所有玩家
            Broadcast(FightProtocol.ENTERFIGHT_BRQ, readrole);
            DebugUtil.Instance.LogToTime(uid + "玩家准备成功");
            if(readrole .Count ==TemeId .Count)
            {
                DebugUtil.Instance.LogToTime(RoomId + "房间全部准备，游戏即将开始");
                IsGameStart = true;
                IsRoomStart = true;
                StartGame();
            }
            return 0;
        }

        void Add()
        {

        }

        void Leave(UserToken token)
        {

        }

        void Close()
        {

        }

        void StartGame()
        {
            DebugUtil.Instance.LogToTime(RoomId + "房间游戏开始");
        }

        void GameOver()
        {

        }
    }
}
