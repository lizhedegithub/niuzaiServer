using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using ServerTools;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //开启的服务器端口
            int port = 6650;
            //最大客户端连接人数
            int maxClient = 1000;
            DebugUtil.Instance.LogToTime("服务器初始化",LogType.NOTICE);
            DebugUtil.Instance.LogToTime("PORT:"+port, LogType.NOTICE);

            //初始化一个服务器通讯程序
            ServerStart server = new ServerStart(maxClient);//等待的最大人数

            #region 为服务器添加日志输出委托
            DebugMessage.debug = delegate (object obj) {
                DebugUtil.Instance.LogToTime(obj,LogType.DEBUG);
            };
            DebugMessage.notice = delegate (object obj) {
                DebugUtil.Instance.LogToTime(obj,LogType.NOTICE);
            };
            DebugMessage.error = delegate (object obj) {
                DebugUtil.Instance.LogToTime(obj,LogType.ERROR);
            };
            #endregion

            //初始化一个消息分发中心
            server.center = new logic.HandleCenter();
            //该服务器支持IPv4也支持IPv6
            //开启服务器
            server.Start(port);
            DebugUtil.Instance.LogToTime("初始化控制台", LogType.NOTICE);

            //控制台工具
            ConsoleUtil cons = new ConsoleUtil();
            //注册正常关闭监听函数
            cons.RegisterCtrlHandler();
            //注册异常关闭的监听函数
            //为当前应用作用域添加一个异常函数
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(cons.UnHandlerExceptionEventHandler);

            #region 测试计时器
            SheduleUtil.Instance.AddShedule(() => { 
                DebugUtil .Instance .LogToTime ("计时器执行成功",LogType .NOTICE );
            },5000);
            #endregion

            #region 测试异常关闭(需要将SheduleUtil中的try catch注释掉)
            //List<int> test = new List<int>();
            //test.Add(0);
            //SheduleUtil.Instance.AddShedule(delegate ()
            // {
            //     test.RemoveAt(0);
            //     DebugUtil.Instance.LogToTime(test[0]);
            // },10000);
            #endregion
            while (true) { }
        }
    }
}
