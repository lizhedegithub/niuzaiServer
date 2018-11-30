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
    }
}
