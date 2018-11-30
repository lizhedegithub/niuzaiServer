using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.business
{
    /// <summary>
    /// 业务逻辑代理
    /// </summary>
    public class BizFactory
    {
        /// <summary>
        /// 建立一个公开的只读的静态类
        /// readonly（可以修饰类）和const（不能修饰类,只能修饰变量，修饰之后为常量） 都是表示只读的
        /// </summary>
        //登录业务逻辑处理类
        public readonly static LoginBiz login;

        /// <summary>
        /// 用户业务逻辑处理类
        /// </summary>
        public readonly static UserBiz user;

        /// <summary>
        /// 用户匹配模块逻辑处理
        /// </summary>
        public readonly static MatchBiz match;

        static BizFactory() {
            login = new LoginBiz();
            user = new business.UserBiz();
            match = new business.MatchBiz();
        }
    }
}
