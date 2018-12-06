using ServerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using NetFrame.auto;

namespace Server.logic.fight
{
    /// <summary>
    /// 赢三张游戏
    /// </summary>
    public class TPFightRoom:FightRoom 
    {
        /// <summary>
        /// 进行游戏请求的逻辑处理
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        protected override void GameMessageReceive(UserToken token, SocketModel message)
        {

        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        protected override void StartGame()
        {
            DebugUtil.Instance.LogToTime(RoomId + "赢三张房间游戏开始");
        }

        protected override void GameOver()
        {
            base.GameOver();
        }
    }
}
