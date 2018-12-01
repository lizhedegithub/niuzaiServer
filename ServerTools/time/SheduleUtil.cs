using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ServerTools
{
    /// <summary>
    /// 建立一个任务委托
    /// </summary>
    public delegate void TimeTask();

    /// <summary>
    /// 计时器模块
    /// </summary>
    public class SheduleUtil
    {
        #region 单例模块
        private static SheduleUtil instance;
        public static SheduleUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SheduleUtil();
                }
                return instance;
            }
        }
        #endregion

        //计时器线程
        Thread TimeThread;
        //待执行的任务
        Dictionary<int, TimeTaskModel> TaskDic = new Dictionary<int, TimeTaskModel>();
        //待移除的任务
        List<int> removeList = new List<int>();

        int Index = 0;
        SheduleUtil()
        {
            //开始计时器线程
            TimeThread = new Thread(timeStart);
            TimeThread.Start();
        }

        void timeStart()
        {
            while (true)
            {
                ////获取现行刻度
                ////（本次刻度数-上次刻度数）*10000=毫秒数
                //long time = DateTime.Now.Ticks;
                //int i = 0;
                ////每10ms执行一次任务
                //long endtime = DateTime.Now.Ticks;
                //while (i < 1000)
                //{
                //    endtime = DateTime.Now.Ticks;
                //    i = (int)(endtime - time) / 10000;
                //}
                //Console.WriteLine(i + ",");
                Thread.Sleep(20);
                CallBack();
            }
        }

        /// <summary>
        /// 执行任务回调
        /// </summary>
        void CallBack()
        {
            //线程锁，以防止数据竞争
            lock (removeList)
            {
                lock (TaskDic)
                {
                    //执行前将待移除的任务移除
                    for (int i = 0; i < removeList.Count; i++)
                        TaskDic.Remove(removeList[i]);
                    //清除待移除列表
                    removeList.Clear();
                    long endTime = DateTime.Now.Ticks;
                    List<int> IdKey = new List<int>(TaskDic.Keys.ToList());
                    for (int i = 0; i < IdKey.Count; i++)
                    {
                        //如果待执行的时间大于等于当前时间
                        if (TaskDic [IdKey [i]].Time <=endTime )
                        {
                            // Console.WriteLine("Time:"+m.Time +"/"+endTime +"");
                            //将本任务添加至移除列表
                            removeList.Add(TaskDic [IdKey [i]].ID);
                            //执行任务
                            try
                            {
                                TaskDic[IdKey[i]].Run();
                            }
                            //抛出异常
                            catch (Exception e)
                            {
                                DebugUtil.Instance.LogToTime(e, LogType.ERROR);
                            }
                        }

                    }                    
                }
            }
        }

        /// <summary>
        /// 添加一个计时器任务
        /// </summary>
        /// <param name="task">待执行的任务</param>
        /// <param name="time">任务执行时间——精度毫秒</param>
        public int AddShedule(TimeTask task,long time)
        {
            lock (TaskDic)
            {
                //任务id
                Index++;
                //根据现行时间添加任务时间
                long nowtime = DateTime.Now.Ticks;
                nowtime += time * 10000;
                //创建一个新的任务
                TimeTaskModel model = new TimeTaskModel(Index, task, time);
                //将任务添加至任务字典
                TaskDic.Add(Index, model);
                return Index;
            }
        }

        /// <summary>
        /// 根据任务id移除任务
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns></returns>
        public bool RemoveShedule(int taskId)
        {
            //如果该任务已经存在于移除列表，则直接返回成功
            if(removeList .Contains (taskId ))
                return true ;
            //如果该任务包含在待执行列表，将该任务添加至移除列表，返回成功
            if (TaskDic.ContainsKey(taskId))
            {
                removeList.Add(taskId);
                return true;
            }
            //否则返回失败
            return false;
        }
    }
}
