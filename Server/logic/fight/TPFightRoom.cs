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
        /// 现在的分数
        /// </summary>
        int NowScore = 1;

        /// <summary>
        /// 最大的可下注金额
        /// </summary>
        int MaxScore = 40;

        /// <summary>
        /// 看牌的玩家列表
        /// </summary>
        List<int> CheckList = new List<int>();

        /// <summary>
        /// 下注金额的集合
        /// </summary>
        Dictionary<int, List<int >> BetCoinList = new Dictionary<int, List<int >>();

        /// <summary>
        /// 请求下注
        /// </summary>
        /// <param name="uid">请求下注的玩家</param>
        /// <param name="coin">请求下注的金额</param>
        private void ReqBet(int uid,int coin)
        {
            //初始请求的下注金额
            int initCoin = coin;
            if(!UserFight .ContainsKey (uid ) || !LoopOrder .Contains (uid))
            {
                DebugUtil.Instance.LogToTime("请求错误，没有此玩家");
                SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -1);
                return;
            }
            if(LoopOrder [0]!=uid)
            {
                DebugUtil.Instance.LogToTime(uid + "请求错误，当前不是此玩家");
                SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -2);
                return;
            }
            if(!IsGameStart)
            {
                DebugUtil.Instance.LogToTime("请求错误，游戏尚未开始");
                SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -3);
                return;
            }
            //是否看过牌
            if(CheckList .Contains (uid))
            {
                //跟注
                if (coin == -1)
                {
                    coin = NowScore * 2;
                    if (coin == 4)
                        coin = 5;
                }
                //是否小于最小可下注金额
                if (coin <NowScore * 2)
                {
                    DebugUtil.Instance.LogToTime(uid+"请求下注失败，当前可下注金额最小为："+(NowScore *2));
                    SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -4);
                    return;
                }
                //是否大于最大下注金额
                if (coin >  MaxScore)
                {
                    DebugUtil.Instance.LogToTime(uid + "请求下注失败，当前可下注金额最大为：" + MaxScore);
                    SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -5);
                    return;
                }
                if (coin == 5)
                {
                    NowScore = 2;
                }
            }
            else
            {
                if (coin == -1)
                    coin = NowScore;
                //是否小于最小可下注金额
                if (coin < NowScore )
                {
                    DebugUtil.Instance.LogToTime(uid + "请求下注失败，当前可下注金额最小为：" + NowScore);
                    SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -4);
                    return;
                }
                //是否大于最大下注金额
                if (coin > MaxScore/2)
                {
                    DebugUtil.Instance.LogToTime(uid + "请求下注失败，当前可下注金额最大为：" + (MaxScore/2));
                    SendMessage(uid, FightProtocol.TPBETCOIN_SRES, -5);
                    return;
                }
                NowScore = coin;
            }
            //
            Bet(uid, coin, initCoin);
        }

        /// <summary>
        /// 处理下注事件
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="coin"></param>
        /// <param name="initCoin"></param>
        private void Bet(int uid,int coin,int initCoin)
        {
            //将当前下注玩家和下注金额添加到集合中，等待结算
            if (!BetCoinList.ContainsKey(uid))
                BetCoinList.Add(uid, new List<int>());
            BetCoinList[uid].Add(coin);
            //声明待广播的数据,将下注数据广播
            TPBetModel tpm = new TPBetModel();
            tpm.id = uid;
            tpm.coin = coin;
            tpm.isAdd = initCoin == -1 ? false : true;
            Broadcast(FightProtocol.TPBETCOIN_BRQ, tpm);
            //更新玩家筹码后广播给所有玩家
            UserFight[uid].coin -= coin;
            Broadcast(FightProtocol.PLAYERINFO_BRQ, UserFight[uid]);
            //玩家执行完下注，将玩家从当前下注列表中删除
            //将当前玩家移动到最后,让下一家发话
            LoopOrder.Add(LoopOrder[0]);
            LoopOrder.RemoveAt(0);
            DebugUtil.Instance.LogToTime(uid + "下注" + coin + "房间号" + RoomId + "是否看牌:" + CheckList.Contains(uid));
        }

        /// <summary>
        /// 进行游戏请求的逻辑处理
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        protected override void GameMessageReceive(UserToken token, SocketModel message)
        {
            int userid = cache.CacheFactory.user.GetIdToToken(token);
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
                    {
                        DebugUtil.Instance.LogToTime("请求下注"+message .GetMessage <int >());
                        ReqBet(userid, message.GetMessage<int>());
                    }
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
