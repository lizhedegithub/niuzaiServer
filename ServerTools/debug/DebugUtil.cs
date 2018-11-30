using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTools
{
    public enum LogType
    {
        FATAL=0,//未捕获的异常(致命的错误)  LV.0
        ERROR=1,//捕获到的错误   LV.1
        WARRING=2,//运行警告   LV.2
        NOTICE=3,//高级别提醒  LV.3
        DEBUG=4//普通         LV.4
    }

    /// <summary>
    /// 日志输出系统
    /// </summary>
    public class DebugUtil
    {
        #region 单例
        private static DebugUtil instance;
        public static DebugUtil Instance
        {
           get
            {
                if(instance ==null)
                {
                    instance = new DebugUtil();
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 日志线程
        /// </summary>
        private Thread LogThread;

        /// <summary>
        /// 待打印的日志缓存池
        /// </summary>
        private List<LogClass> LogMessage = new List<LogClass>();

        //存储不同日志打印对应的颜色
        private Dictionary<LogType, ConsoleColor> WriteColor = new Dictionary<LogType, ConsoleColor>();
      
        //是否打印日志输出
        private bool IsWriteDebug = true;

        //将日志写入到本地的流对象
        private StreamWriter streamWrite;

        //是否正在写入中
        private bool IsWrite;

        DebugUtil() {
            WriteColor.Add (LogType.DEBUG,ConsoleColor.White);
            WriteColor.Add(LogType.ERROR, ConsoleColor.Red);
            WriteColor.Add(LogType.FATAL, ConsoleColor.Blue);
            WriteColor.Add(LogType.NOTICE, ConsoleColor.Green);
            WriteColor.Add(LogType.WARRING, ConsoleColor.Yellow);


            //初始化一个线程，将start函数以委托的形式赋予给线程
            LogThread = new Thread(new ThreadStart(Start));
            //日志线程启动
            LogThread.Start();
        }

        //线程执行到最后一句，就会被系统当做垃圾回收，要想让线程一直执行，使用循环
        //开启线程
        void Start()
        {
            do
            {
                if(LogMessage.Count > 0 && !IsWrite)
                {
                    //打印日志
                    //开始打印
                    IsWrite = true;
                    //将第0个日志打印
                    WriteMessage(LogMessage[0]);
                    //将下标为0的日志删除
                    LogMessage.RemoveAt(0);
                }
                //打印日志时阻塞线程10ms
                Thread.Sleep(10);
            } while (IsWriteDebug);
        }

        public void Close()
        {
            IsWriteDebug = false;
        }

        /// <summary>
        /// 添加一个日志输出
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        public void Log(object str,LogType type = LogType.DEBUG)
        {
            if (type == LogType.FATAL) return;
            LogMessage.Add(new ServerTools.LogClass(str, type));
        }

        /// <summary>
        /// 添加一个日志输出
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public void LogToTime(object msg,LogType type=LogType.DEBUG)
        {
            if (type == LogType.FATAL) return;
            LogMessage.Add(new ServerTools.LogClass (msg+"   "+DateTime.Now .ToString ("hh：mm：ss：ffff"),type));
        }


        //public void AddFatal(object str)
        //{
        //    LogMessage.Add(new ServerTools.LogClass(str + "   " + DateTime.Now.ToString("hh：mm：ss：ffff"), LogType.FATAL));
        //}

        /// <summary>
        /// 开始打印日志
        /// </summary>
        /// <param name="log"></param>
        void WriteMessage(LogClass log)
        {
            if (log == null)
            {
                Console.WriteLine("ERROR:Message is null");
                return;
            }
            //将控制台打印的日志信息颜色调整为对应日志级别的颜色
            Console.ForegroundColor = WriteColor[log.type];
            //将日志打印至控制台
            Console.WriteLine(log.msg);
            //重置下一次控制台打印的颜色
            Console.ResetColor();
            //开始存储打印信息至本地
            WriteSteamToFold(log);
        }

        /// <summary>
        /// 将日志信息存储至本地,将日志永久化存储
        /// </summary>
        /// <param name="log"></param>
        void WriteSteamToFold(LogClass log)
        {
            try
            {
                IsWrite = true;
                //获取当前程序的运行路径
                string path = Environment.CurrentDirectory;
                //根据日志的类型决定最终存储至本地的根目录
                path += "/DebugLog/" + log.type;

                //如果根目录不存在，创建
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //将根目录路径和文件名组合
                path += "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                //如果文件不存在，则创建一个文件，否则加载该文件，并将文件对象赋值给流对象
                if (streamWrite == null)
                    streamWrite = !File.Exists(path) ? File.CreateText(path) : File.AppendText(path);
                //开始写入文件,在文件最后一行写入日志信息
                streamWrite.WriteLine(log.msg);
            }finally
            {
                if(streamWrite !=null)
                {
                    //释放缓冲区资源
                    streamWrite.Flush();
                    //释放所占用的资源
                    streamWrite.Dispose();
                    //重置文件写入对象
                    streamWrite = null;
                }
                IsWrite = false;
            }
        }
    }
}
