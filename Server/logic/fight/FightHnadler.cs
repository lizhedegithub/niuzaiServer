using NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame.auto;

namespace Server.logic.fight
{
    public class FightHnadler : IHandler
    {
        public void ClientClose(UserToken token, string error)
        {
            cache.CacheFactory.fight.ClientClose(token);
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            if (cache.CacheFactory.user.IsOnLine(token))
                cache.CacheFactory.fight.MessageReceive(token, message);
        }
    }
}
