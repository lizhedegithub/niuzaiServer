using ServerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using NetFrame.auto;
using GameProtocol;
using Server.logic.tool.poker;
using GameProtocol.model.fight;

namespace Server.logic.fight
{
    /// <summary>
    /// 赢三张游戏
    /// </summary>
    public class TPFightRoom:FightRoom 
    {
        int BetCoin = 1;

        /// <summary>
        /// 当前扑克列表
        /// </summary>
        List<PokerModel> PokerList = new List<PokerModel>(); 

        /// <summary>
        /// 赢三张工具类
        /// </summary>
        TPokerUtil TPokerUtil = new TPokerUtil();

        /// <summary>
        /// 进行游戏请求的逻辑处理
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        protected override void GameMessageReceive(UserToken token, SocketModel message)
        {
            switch (message .command)
            {
                //请求看牌
                case FightProtocol.TPCHECKCARD_CREQ:
                    { }
                    break;
                //请求比牌
                case FightProtocol.TPCOMCARD_CREQ:
                    { }
                    break;
                //请求下注
                case FightProtocol.TPBETCOIN_CREQ:
                    { }
                    break;
                //请求弃牌
                case FightProtocol.TPDISCARD_CREQ:
                    { }
                    break;
            }
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        protected override void StartGame()
        {
            DebugUtil.Instance.LogToTime(RoomId + "赢三张房间游戏开始");
            //获取扑克列表
            PokerList = TPokerUtil.GetPokerList();
            //根据玩家方位进行初始排序
            SortLoopSorderInDirection();
            //最多金币玩家id
            int MaxCoinId = -1;
            foreach (FightUserModel user in UserFight.Values)
            {
                //清楚之前的数据
                user.poker.Clear();
                if (MaxCoinId == -1)
                    MaxCoinId = user.id;
                //查找最多金币玩家id
                if (user.id != MaxCoinId && user.coin > UserFight[MaxCoinId].coin)
                {
                    MaxCoinId = user.id;
                }
            }
            //根据金币规则进行排序
            SortLoopInUser(MaxCoinId);
            //开始发牌
            for (int i = 0; i < 3; i++)//一人三张牌
            {
                for (int j = 0; j < LoopOrder .Count; j++)//玩家个数
                {
                    int userid = LoopOrder[j];
                    UserFight[userid].poker.Add(PokerList[0]);
                    PokerList.RemoveAt(0);
                }
            }
            //打印玩家手牌
            for (int j = 0; j < LoopOrder .Count; j++)
            {
                string card = LoopOrder[j] + ":";
                for (int i = 0; i <UserFight[LoopOrder [j]].poker .Count; i++)
                {
                    card += "V" + UserFight[LoopOrder[j]].poker[i].Value + "C" + UserFight[LoopOrder[j]].poker[i].Color;
                }
                DebugUtil.Instance.LogToTime("玩家手牌：" + card);
                //通知玩家自己摸到的牌
                //SendMessage(LoopOrder[j], FightProtocol.TPDRAWCARD_BRQ, UserFight[LoopOrder[j]].poker);
                //通知所有玩家该玩家摸了牌
                Broadcast(FightProtocol.TPDRAWCARDUSER_BRQ , LoopOrder[j]);
            }
            //广播下注
            foreach (FightUserModel model in UserFight.Values )
            {
                model.coin -= BetCoin;
            }
            Broadcast(FightProtocol.TPBETBASECOIN_BRQ, LoopOrder.Count*BetCoin);
            //广播玩家剩余筹码
            for (int i = 0; i < LoopOrder.Count; i++)
            {
                Broadcast(FightProtocol.PLAYERINFO_BRQ, UserFight[LoopOrder[i]]);
            }
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        protected override void GameOver(bool isSettlement = false, string exption = "")
        {
            DebugUtil.Instance.LogToTime(RoomId + "赢三张房间游戏结束");
        }
    }
}
