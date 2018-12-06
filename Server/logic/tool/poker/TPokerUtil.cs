using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProtocol.model.fight;

namespace Server.logic.tool.poker
{
    /// <summary>
    /// 赢三张扑克工具类
    /// </summary>
    public class TPokerUtil
    {
        /// <summary>
        /// 置随机数种子
        /// </summary>
        Random ran = new Random((int)DateTime .Now .Ticks);

        /// <summary>
        /// 获取一副新的扑克牌
        /// </summary>
        /// <returns></returns>
        public List <PokerModel> GetPokerList()
        {
            //声明一副新扑克
            List<PokerModel> newPoker = new List<PokerModel>();
            //依次将扑克装入列表中
            for (int i = 1; i < 14; i++)//i=牌值
            {
                for (int j = 1; j < 5; j++)//j=颜色值
                {
                    newPoker.Add(new PokerModel(i, j));
                }
            }

            //洗牌
            List<PokerModel> retPoker = new List<PokerModel>();
            while (newPoker .Count > 0)
            {
                //随机取一个下标
                int idx = ran.Next(0, newPoker.Count - 1);
                retPoker.Add(newPoker[idx]);
                newPoker.RemoveAt(idx);
            }
            return retPoker;
        }
    }


}
