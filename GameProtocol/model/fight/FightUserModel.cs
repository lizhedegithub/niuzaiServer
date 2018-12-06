using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.fight
{
    /// <summary>
    /// 战斗用户信息
    /// </summary>
    [Serializable]
    public class FightUserModel
    {
        public int id=-1;
        public string nickname="";
        public int coin = 0;
        public string head="";
        public int direction = -1;//玩家的方位信息
        /// <summary>
        /// 玩家自己的手牌
        /// </summary>
        public List<PokerModel> poker = new List<PokerModel>();
    }
}
