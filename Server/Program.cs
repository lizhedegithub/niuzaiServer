using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using ServerTools;
using System.Data;

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

            #region 为数据库添加日志输出委托
            MySQLTools.SqlManager.Instance.DEBUG = delegate (object obj)
            {
                DebugUtil.Instance.LogToTime(obj,LogType.DEBUG);
            };
            MySQLTools.SqlManager.Instance.ERROR = delegate (object obj)
            {
                DebugUtil.Instance.LogToTime(obj,LogType.ERROR);
            };
            #endregion
            //连接数据库
            MySQLTools.SqlManager.Instance.Start("127.0.0.1", "niuzai", "root", "newpasswd");
            //获取玩家信息最后一位的玩家id
            dao.roleinfo info = new dao.roleinfo();
            int MaxId= info.GetEndLineValueById();
            cache.CacheFactory.user.Index = MaxId;
            //读取数据库的缓存
            cache.CacheFactory.user.LoadAccount();
            DebugUtil.Instance.LogToTime("服务器当前最大id" + MaxId);

            DebugUtil.Instance.LogToTime("服务器执行成功", LogType.NOTICE);

            #region 测试计时器
            //SheduleUtil.Instance.AddShedule(() => { 
            //    DebugUtil .Instance .LogToTime ("计时器执行成功",LogType .NOTICE );
            //},5000);
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

            #region 测试数据库插入语句
            //SheduleUtil.Instance.AddShedule(delegate () {
            //    //string[] sql = { "2", "aaa", "8" };
            //    //string[] sql1 = { "8", "bbb", "18" };
            //    //string[] sql2 = { "4", "ccc", "28" };
            //    //string[] sql3 = { "5", "ddd", "38" };
            //    //util.InsertData("test", sql);
            //    //util.InsertData("test", sql1);
            //    //util.InsertData("test", sql2);
            //    //util.InsertData("test", sql3);
            //}, 5000);
            #endregion

            #region 测试数据库根据字段插入语句
            //SheduleUtil.Instance.AddShedule(delegate () {
            //    //string[] sql1 = { "2", "8", "aaa" };
            //    //string[] Asql1 = { id, year, name };
            //    //string[] sql2 = { "4", "28" };
            //    //string[] Asql2 = { id, year};
            //    //util.InsertData("test", sql1,Asql1);
            //    //util.InsertData("test", sql2,Asql2);
            //}, 5000);
            #endregion

            #region 测试数据库修改语句
            //SheduleUtil.Instance.AddShedule(delegate () {
            //    //string[] sql1 = { "year", "name" };
            //    //string[] sql2 = { "4", "ffff" };
            //    //util.UpdateData("test", sql1, sql2 ,"id","1");
            //}, 5000);
            #endregion

            #region 测试数据库删除语句
            //SheduleUtil.Instance.AddShedule(delegate ()
            //{
            //    util.DeleteData("test", "id", "1");
            //}, 5000);
            #endregion

            #region 测试数据库查询语句
            //SheduleUtil.Instance.AddShedule(delegate ()
            //{
            //    //要查询额字段
            //    string[] sqlc = { "year", "name" };
            //    DataSet ds = util.SelectData("test", sqlc, "id", "2");
            //    if(ds!=null)
            //    {
            //        DataTable table = ds.Tables[0];
            //        foreach (DataRow row in table .Rows)
            //        {
            //            foreach (DataColumn column in table .Columns )
            //            {
            //                DebugUtil.Instance.LogToTime(column.Caption + ":" + row[column]);
            //            }
            //        }
            //        //DebugUtil.Instance.LogToTime(ds.Tables[0].Rows[0]["a\u0090\u008da--123"]);
            //    }
            //}, 5000);
            #endregion

            #region 测试修改后的插入语句
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test");
            //param.Add("id", 5);
            //param.Add("name", "ddd");
            //param.Add("year", 55);
            //MySQLTools.SqlManager.Instance.AddTable(param);
            #endregion

            #region 测试修改后的根据字段插入语句
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test");
            //param.Add("id", 8);
            //param.Add("name", "lizhe");
            //MySQLTools.SqlManager.Instance.AddTableAtItems(param);
            #endregion

            #region 测试修改后的修改语句
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test");
            //param.Add("name", "lizhuo");
            //param.Add("id", 6);
            //param.Add("year", "15");
            //MySQLTools.SqlManager.Instance.UpdateInItems(param);
            #endregion

            #region 测试修改后的删除语句
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test");
            //param.Add("name", "lizhuo");
            //param.Add("id", 6);
            //param.Add("year", "15");
            //MySQLTools.SqlManager.Instance.Delete(param);
            #endregion

            #region 测试修改后的查询语句
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test");
            //param.Add("id", 5);
            //param.Add("year", "3");
            //MySQLTools.SqlManager.Instance.GetTable(ref param);
            #endregion

            #region 测试修改后的查询某列最后的值
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test",false);
            //param.Add("id", null);
            //DebugUtil.Instance.Log(MySQLTools.SqlManager.Instance.GetEndLineValue(ref param));
            #endregion

            #region 测试修改后的查询某列最后的值
            //MySQLTools.DictionaryPara param = new MySQLTools.DictionaryPara("test", false);
            //param.Add("id", null);
            //param.Add("year", null);
            //List<MySQLTools.DictionaryPara> dp = MySQLTools.SqlManager.Instance.GetColume(param);
            #endregion


            while (true) { }
        }
    }
}
