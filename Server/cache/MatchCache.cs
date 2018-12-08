using GameProtocol;
using GameProtocol.model.match;
using NetFrame;
using ServerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.cache
{
    public class MatchCache
    {
        /// <summary>
        /// 房间号和房间的对列
        /// </summary>
        public Dictionary<int, MatchInfoModel> MatchInfo = new Dictionary<int, MatchInfoModel>();

        /// <summary>
        /// 玩家和房间的映射
        /// </summary>
        public Dictionary<int, int> UserToMatch = new Dictionary<int, int>();

        /// <summary>
        /// 是否开始游戏
        /// 匹配对列id和游戏开始的映射
        /// </summary>
        public Dictionary<int, bool> IsStartGame = new Dictionary<int, bool>();

        /// <summary>
        /// 置随机数种子
        /// </summary>
        Random ran = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// 是否在匹配对列中
        /// </summary>
        /// <returns>-1 没有在对列中</returns>
        /// <returns>返回一个房间号</returns>
        public int IsOnMatchArray(int userid)
        {
            //如果玩家在房间中，并且该房间在房间对列中
            if (UserToMatch.ContainsKey(userid) && MatchInfo.ContainsKey(UserToMatch[userid]))
            {
                //返回房间号
                return UserToMatch[userid];
            }
            return -1;
        }

        /// <summary>
        /// 获取一个进入房间的最小货币数量
        /// </summary>
        /// <param name="type">房间类型（赢三张，血战到底）</param>
        /// <returns></returns>
        public int GetRoomCoinAtType(SConst.GameType type)
        {
            switch (type)
            {
                //至少有1000金币才能进入游戏
                case SConst.GameType.WINTHREEPOKER:
                    return 1000;
                case SConst.GameType.XZDD:
                    return 100;
                default:
                    return 1000000000;
            }
        }

        /// <summary>
        /// 添加匹配到匹配对列
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roomtype"></param>
        public void AddMatch(int userid,SConst .GameType roomtype,ref ResponseStartMatchInfo info)
        {
            #region 如果当前有匹配对列
            if (MatchInfo .Count > 0)
            {
                //创建一条匹配对列房间号的列表
                List<int> Roomid = new List<int>(MatchInfo.Keys);
                for (int i=0;i<Roomid.Count; i++)
                {
                    //如果匹配对列和将要进行的游戏类型一致
                    if(MatchInfo[Roomid [i]].GameType ==roomtype)
                    {
                        //如果当前对列中有队伍未满员
                        if (MatchInfo[Roomid[i]].Team.Count < MatchInfo[Roomid[i]].MaxPlayer)
                        {
                            //将玩家添加到匹配对列中
                            MatchInfo[Roomid[i]].Team.Add(userid);
                            //添加返回给客户端的信息
                            info.PlayerCount = MatchInfo[Roomid[i]].Team.Count;
                            info.MaxPlayer = MatchInfo[Roomid[i]].MaxPlayer;
                            info.Type = roomtype;
                            //添加玩家和房间号的映射
                            UserToMatch.Add(userid, Roomid[i]);

                            DebugUtil.Instance.LogToTime(userid + "请求加入匹配对列成功，对列号为：" + Roomid[i]);
                            IsFinish(Roomid[i]);
                            return;
                        }
                    }
                }
            }
            #endregion
            #region 创建一个新的匹配对列
            //创建一个新的匹配对列
            MatchInfoModel model = new MatchInfoModel();
            //设定当前具有玩家数量来开启游戏
            model.MaxPlayer = 2;
            model.RoomId = GetMatchId();
            model.GameType = roomtype;
            model.Team.Add(userid);
            //添加返回给客户端的信息
            info.PlayerCount = model .Team .Count;
            info.MaxPlayer = model.MaxPlayer;
            info.Type = roomtype;
            //添加房间号和房间的映射
            MatchInfo.Add(model.RoomId, model);
            //添加玩家和房间号的映射
            UserToMatch.Add(userid, model.RoomId);
            //添加游戏是否开始的映射
            if (!IsStartGame.ContainsKey(model.RoomId))
                IsStartGame.Add(model.RoomId, false);
            DebugUtil.Instance.LogToTime(userid + "请求创建匹配对列成功，对列号为：" + model .RoomId);
            IsFinish(model.RoomId);
            #endregion
        }

        /// <summary>
        /// 是否匹配完成
        /// </summary>
        /// <param name="roomid"></param>
        private void IsFinish(int roomid)
        {
            //将队伍成员信息广播给所有队伍成员
            for (int i=0;i<MatchInfo [roomid ].Team.Count; i++)
            {
                UserToken token = CacheFactory.user.GetToken(MatchInfo[roomid].Team[i]);
                token.write(TypeProtocol.MATCH, MatchProtocol.MATCHINFO_BRQ, MatchInfo[roomid]);
            }
            //匹配成功
            if(MatchInfo [roomid ].Team .Count ==MatchInfo [roomid].MaxPlayer)
            {
                DebugUtil.Instance.LogToTime(roomid +"对列当前匹配成功");
                //等待一秒钟后，创建游戏房间
                SheduleUtil.Instance.AddShedule(delegate () {
                    for (int i = 0; i < MatchInfo[roomid].Team.Count; i++)
                    {
                        UserToken token = cache.CacheFactory.user.GetToken(MatchInfo[roomid].Team[i]);
                        token.write(TypeProtocol.MATCH, MatchProtocol.MATCHFINISH_BRQ, MatchInfo[roomid]);
                    }
                    SetStartGame(roomid);
                    SheduleUtil.Instance.AddShedule(delegate ()
                    {
                        Console.WriteLine(1010100101);
                        CacheFactory.fight.Create(MatchInfo[roomid]);
                    }, 1000);
                }, 1000);
            }
        }

        /// <summary>
        /// 获取匹配对列id
        /// </summary>
        /// <returns></returns>
        private int GetMatchId()
        {
            //生成一个六位数的房间号
            int id = ran.Next(100000, 999999);
            while (MatchInfo.ContainsKey(id))
                id = ran.Next(100000, 999999);
            return id;
        }

        /// <summary>
        /// 获取是否游戏已经开始
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        private bool GetStartGame(UserToken token)
        {
            //通过用户连接获取用户id
            int userid = cache.CacheFactory.user.GetIdToToken(token);
            //通过用户id获取用户房间号
            int roomid = UserToMatch[userid];
            if (IsStartGame.ContainsKey(roomid))
                return false;
            return IsStartGame[roomid];
        }

        /// <summary>
        /// 获取是否游戏已经开始
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        private bool GetStartGame(int roomid)
        {
            if (IsStartGame.ContainsKey(roomid))
                return false;
            return IsStartGame[roomid];
        }

        /// <summary>
        /// 判断一个玩家是否在匹配对列
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool GetHaveMatch(UserToken token) {
            //获取玩家id
            int userid = cache.CacheFactory.user.GetIdToToken(token);
            //判断是否含有玩家匹配对列映射关系（玩家不在房间中，返回false）
            if (!UserToMatch.ContainsKey(userid)) return false;
            //判断匹配对列是否存在（该房间号不存在，返回false）
            if (!MatchInfo.ContainsKey(UserToMatch[userid])) return false;
            //判断匹配对列中是否有该玩家
            if (!MatchInfo[UserToMatch[userid]].Team.Contains(userid)) return false;
            return true;
        }

        /// <summary>
        /// 将游戏设置为已经开始
        /// </summary>
        /// <param name="roomid"></param>
        public void SetStartGame(int roomid)
        {
            if (IsStartGame.ContainsKey(roomid))
                IsStartGame[roomid] = true;
        }

        /// <summary>
        /// 离开匹配对列
        /// 0离开成功 -1游戏已经开始 -2不在房间内
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int LeaveMatch(UserToken token)
        {
            int userid = cache.CacheFactory.user.GetIdToToken(token);
            //判断是否含有匹配对列
            if (!GetHaveMatch(token)) {
                DebugUtil.Instance.LogToTime(userid + "请求离开匹配对列失败，当前没有匹配对列");
                return -2;
            }
            //判断游戏是否已经开始
            if (GetStartGame(token)) {
                DebugUtil.Instance.LogToTime(userid + "请求离开匹配对列失败，当前游戏已经开始");
                return -1;
            }
            int roomid = UserToMatch[userid];
            MatchInfoModel model = MatchInfo[roomid];
            model.Team.Remove(userid);
            UserToMatch.Remove(userid);
            //如果当前没有玩家了，则移除匹配对列
            if(model .Team .Count == 0)
            {
                MatchInfo.Remove(roomid);
                IsStartGame.Remove(roomid);
                DebugUtil.Instance.LogToTime(userid + "请求离开匹配对列成功，移除匹配对列"+roomid);
            }
            //否则，广播给此对列所有玩家
            else
            {
                for (int i=0;i<MatchInfo [roomid ].Team.Count; i++)
                {
                    UserToken tokens= CacheFactory.user.GetToken(MatchInfo[roomid].Team[i]);
                    tokens.write(TypeProtocol.MATCH, MatchProtocol.MATCHINFO_BRQ, MatchInfo[roomid]);
                }
                DebugUtil.Instance.LogToTime(userid + "请求离开匹配对列成功，移除匹配" );
            }
            return 0;
        }

        public void CloseMatch(int roomid)
        {
            if (!MatchInfo.ContainsKey(roomid)) return;
            for (int i = 0; i < MatchInfo [roomid ].Team .Count ; i++)
            {
                int userid = MatchInfo[roomid].Team[i];
                UserToken token = CacheFactory.user.GetToken(MatchInfo[roomid].Team[i]);
                token.write(TypeProtocol.MATCH, MatchProtocol.MATCHCLOSE_BRQ, null);
                //移除玩家映射
                if (UserToMatch.ContainsKey(userid))
                    UserToMatch.Remove(userid);
            }
            MatchInfo.Remove(roomid);
            DebugUtil.Instance.LogToTime(roomid + "匹配对列已经被移除", LogType.NOTICE);
        }
    }
}