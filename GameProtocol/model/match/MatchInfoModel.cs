using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.match
{
    /// <summary>
    /// 匹配信息模型
    /// </summary>
    [Serializable]
    public class MatchInfoModel
    {
        public int RoomId = 0;//房间号
        public int MaxPlayer = 0;//最大人数
        public List<int> Team = new List<int>();//当前玩家id列表
        public SConst.GameType GameType = SConst.GameType.WINTHREEPOKER;//游戏类型
    }
}
