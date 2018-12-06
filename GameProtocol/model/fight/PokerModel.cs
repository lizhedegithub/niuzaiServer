﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProtocol.model.fight
{
    /// <summary>
    /// 扑克模型
    /// </summary>
    [Serializable]
    public class PokerModel
    {
        /// <summary>
        /// 扑克值
        /// A  2  3 4 5 6 7 8   9   10   J   Q    K    小王  大王  花牌     牌值:在客户端上显示给玩家
        /// 1  2  3 4 5 6 7 8   9   10   11  12   13   16    17   18       牌对应的定义值：在服务器上对应牌的模型
        /// 14 15 3 4 5 6 7 8   9   10   11  12   13   16    17   18       定义对应的序列：进行规则的定义，编写
        /// 12 13 1 2 3 4 5 6   7   8    9   10   11   14    15   16       序列对应的下标
        /// </summary>
        public int Value = -1;
        /// <summary>
        /// 颜色值
        /// 1 黑桃
        /// 2 红桃
        /// 3 方块
        /// 4 梅花
        /// </summary>
        public int Color = -1;

        public PokerModel() { }
        public PokerModel (int value ,int color)
        {
            this.Value = value;
            this.Color = color;
        }
    }
}
