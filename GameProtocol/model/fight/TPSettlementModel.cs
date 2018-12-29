using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.fight
{
    /// <summary>
    /// 赢三张游戏结算模型
    /// </summary>
    [Serializable]
    public class TPSettlementModel
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string head = "";
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickName = "";
        /// <summary>
        /// 用户id
        /// </summary>
        public int id = 0;
        /// <summary>
        /// 用户分数
        /// </summary>
        public int score = 0;
        /// <summary>
        /// 用户牌列表
        /// </summary>
        public List<PokerModel> poker = new List<PokerModel>();
    }
}
