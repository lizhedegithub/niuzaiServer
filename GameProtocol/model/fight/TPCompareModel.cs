using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.fight
{
    /// <summary>
    /// 赢三张比牌模型
    /// </summary>
    [Serializable]
    public class TPCompareModel
    {
        /// <summary>
        /// 比牌的玩家
        /// </summary>
        public int userId = -1;
        /// <summary>
        /// 被比牌的玩家
        /// </summary>
        public int compId = -1;
        /// <summary>
        /// 比牌的结果
        /// </summary>
        public bool Result = false;
        /// <summary>
        /// 比牌的玩家的牌
        /// </summary>
        public List<PokerModel> PokerList1 = new List<PokerModel>();
        /// <summary>
        /// 被比牌的玩家的牌
        /// </summary>
        public List<PokerModel> PokerList2 = new List<PokerModel>();
    }
}
